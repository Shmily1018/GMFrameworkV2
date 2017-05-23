using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using LinqToSqlExtensions;
using Serialize.Linq.Extensions;
using Serialize.Linq.Nodes;

namespace GoldMantis.Common.Expressions
{
	[DataContract]
	public class ExpressionWrapper<T>
	{
		public Expression<Func<T,bool>> ExpressionBool
		{
			get;
			set;
		}

		public Expression<Func<T,object>> ExpressionObject
		{
			get;
			set;
		}

		private ExpressionNode result;

		[DataMember]
		public ExpressionNode Result
		{
			get
			{
				if (result != null)
					return result;
				if (ExpressionBool != null)
				{
					//PartialEvaluator evaluator = new PartialEvaluator();
					ExpressionBool = Evaluator.PartialEval(ExpressionBool) as Expression<Func<T, bool>>;
					result = ExpressionBool.ToExpressionNode();
				}
				else if (ExpressionObject != null)
				{
					result = ExpressionObject.ToExpressionNode();
				}
				return result;
			}
			set
			{
				result = value;
			}
	
		}

		public static ExpressionWrapper<T> Where(Expression<Func<T,bool>> expression)
		{
			var temp = new ExpressionWrapper<T>();
			ExpressionModifier modifier = new ExpressionModifier();
			temp.ExpressionBool = modifier.Modify(expression) as Expression<Func<T,bool>>;
			return temp;
		}

		public static ExpressionWrapper<T> Select(Expression<Func<T,object>> expression)
		{
			var temp = new ExpressionWrapper<T>();
			temp.ExpressionObject = expression;
			return temp;
		}
	}
}
