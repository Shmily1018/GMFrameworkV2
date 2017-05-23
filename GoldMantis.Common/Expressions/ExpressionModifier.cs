using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GoldMantis.Common.Expressions
{
	public class ExpressionModifier : ExpressionVisitor
	{
		public System.Linq.Expressions.Expression Modify(System.Linq.Expressions.Expression expression)
		{
			return Visit(expression);
		}


		protected override System.Linq.Expressions.Expression VisitMethodCall(MethodCallExpression node)
		{
			if (!node.Method.Name.Equals("Contains") && !node.Method.Name.Equals("NotContains"))
				return base.VisitMethodCall(node);

			if (!node.Method.Name.Equals("NotContains")
				&&
				!(
					node.Method.Name.Equals("Contains")
					&&
					(node.Method.DeclaringType == typeof(Enumerable)
						|| node.Method.DeclaringType.Name.Equals(typeof(ICollection<>).Name)
						|| node.Method.DeclaringType.Name.Equals(typeof(IList<>).Name)
						|| node.Method.DeclaringType.Name.Equals(typeof(List<>).Name)
					)
				)
			)
			{
				return base.VisitMethodCall(node);
			}

			/*
			if (
				(
				!node.Method.Name.Equals("Contains")
				|| !node.Method.Name.Equals("NotContains")
				)
				&& (node.Method.DeclaringType != typeof(Enumerable) 
					|| !node.Method.DeclaringType.Name.Equals(typeof(ICollection<>).Name)
					|| !node.Method.DeclaringType.Name.Equals(typeof(IList<>).Name)
					|| !node.Method.DeclaringType.Name.Equals(typeof(List<>).Name)
					)
			)
				return base.VisitMethodCall(node);
			 */
			System.Linq.Expressions.Expression trueConstantExpression = System.Linq.Expressions.Expression.Constant(true, typeof(bool));
			System.Linq.Expressions.Expression falseConstantExpression = System.Linq.Expressions.Expression.Constant(false, typeof(bool));
			IEnumerable array = null;
			System.Linq.Expressions.Expression binaryExpression = null;
			if (node.Method.DeclaringType == typeof(Enumerable))
			{
				LambdaExpression lambda = System.Linq.Expressions.Expression.Lambda(node.Arguments[0]);
				Delegate fn = lambda.Compile();
				array = System.Linq.Expressions.Expression.Constant(fn.DynamicInvoke(null), node.Arguments[0].Type).Value as IEnumerable;
				if (array == null || !array.GetEnumerator().MoveNext())
					binaryExpression = System.Linq.Expressions.Expression.AndAlso(System.Linq.Expressions.Expression.NotEqual(node.Arguments[1], node.Arguments[1]), falseConstantExpression);
			}
			else if (node.Method.DeclaringType.Name.Equals(typeof(ICollection<>).Name)
			|| node.Method.DeclaringType.Name.Equals(typeof(IList<>).Name)
			|| node.Method.DeclaringType.Name.Equals(typeof(List<>).Name)
			)
			{
				LambdaExpression lambda = System.Linq.Expressions.Expression.Lambda(node.Object);
				Delegate fn = lambda.Compile();
				array = System.Linq.Expressions.Expression.Constant(fn.DynamicInvoke(null), node.Object.Type).Value as IEnumerable;
				if (array == null || !array.GetEnumerator().MoveNext())
					binaryExpression = System.Linq.Expressions.Expression.AndAlso(System.Linq.Expressions.Expression.NotEqual(node.Arguments[0], node.Arguments[0]), falseConstantExpression);
			}
			else if (node.Method.Name.Equals("NotContains"))
			{
				LambdaExpression lambda = System.Linq.Expressions.Expression.Lambda(node.Arguments[0]);
				Delegate fn = lambda.Compile();
				array = System.Linq.Expressions.Expression.Constant(fn.DynamicInvoke(null), node.Arguments[0].Type).Value as IEnumerable;
				if (array == null || !array.GetEnumerator().MoveNext())
					binaryExpression = System.Linq.Expressions.Expression.AndAlso(System.Linq.Expressions.Expression.NotEqual(node.Arguments[0], node.Arguments[0]), trueConstantExpression);
			}

			if (array != null)
			{
				foreach (var item in array)
				{
					MemberExpression memberExpression = (MemberExpression)node.Arguments[node.Arguments.Count - 1];
					Type underlyingType = Nullable.GetUnderlyingType(memberExpression.Type);
					System.Linq.Expressions.Expression tempExpression = null;
					if (node.Method.Name.Equals("Contains"))
					{
						tempExpression = underlyingType == null ? System.Linq.Expressions.Expression.Equal(node.Arguments[node.Arguments.Count - 1], System.Linq.Expressions.Expression.Constant(item))
							: System.Linq.Expressions.Expression.Equal(node.Arguments[node.Arguments.Count - 1], System.Linq.Expressions.Expression.Constant(item, memberExpression.Type));
						if (binaryExpression == null)
							binaryExpression = System.Linq.Expressions.Expression.OrElse(tempExpression, falseConstantExpression);
						else
							binaryExpression = System.Linq.Expressions.Expression.OrElse(tempExpression, binaryExpression);
					}
					else if (node.Method.Name.Equals("NotContains"))
					{
						tempExpression = underlyingType == null ? System.Linq.Expressions.Expression.NotEqual(node.Arguments[node.Arguments.Count - 1], System.Linq.Expressions.Expression.Constant(item))
							: System.Linq.Expressions.Expression.NotEqual(node.Arguments[node.Arguments.Count - 1], System.Linq.Expressions.Expression.Constant(item, memberExpression.Type));
						if (binaryExpression == null)
							binaryExpression = System.Linq.Expressions.Expression.AndAlso(tempExpression, trueConstantExpression);
						else
							binaryExpression = System.Linq.Expressions.Expression.AndAlso(tempExpression, binaryExpression);
					}
				}
			}

			if (binaryExpression == null)
				return base.VisitMethodCall(node);

			return binaryExpression;

		}

	}
}
