using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GoldMantis.Common
{
    public class ExpressionConditionHelper
    {
        /// <summary>
        /// 表达式逻辑关系
        /// </summary>
        public enum ExpressionRelation
        {
            And,
            Or
        }


        /// <summary>
        /// 表达式条件
        /// </summary>
        public enum ExpressionValueRelation
        {
            Equal,
            NotEqual,
            GreaterThanOrEqual,
            GreaterThan,
            LessThan,
            LessThanOrEqual,
            Like,
            CustomLike,
            Contains,
            EqualNull,
            NotEqualNull
        }


        /// <summary>
        /// 多条件查询类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ExpressionCondition<T>
        {
            private ParameterExpression _parameter = Expression.Parameter(typeof (T));

            private ExpressionCondition()
            {

            }

            /// <summary>
            /// 前置条件
            /// </summary>
            private Expression<Func<T, bool>> Criteria { get; set; }

            /// <summary>
            /// 前置条件与Condition 之间的关系
            /// </summary>
            public ExpressionRelation PrefixConditionRelation { get; set; }

            /// <summary>
            /// 
            /// </summary>
            private System.Linq.Expressions.Expression conditionExpression { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static ExpressionCondition<T> GetInstance()
            {
                return new ExpressionCondition<T>();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="member"></param>
            /// <param name="value"></param>
            /// <param name="valueRelation"></param>
            /// <param name="expressionRelation"></param>
            /// <returns></returns>
            private ExpressionCondition<T> ComposeCondition(Expression<Func<T, object>> member, object value,
                ExpressionValueRelation valueRelation, ExpressionRelation expressionRelation)
            {
                if (value == null &&
                    valueRelation != ExpressionValueRelation.NotEqualNull &&
                    valueRelation != ExpressionValueRelation.EqualNull)
                {
                    return this;
                }

                //判断contains,如果传过来的数组或列表为空,则返回
                if (valueRelation == ExpressionValueRelation.Contains)
                {
                    if (value.GetType().BaseType == typeof (Array))
                    {
                        var temp = value as Array;

                        if (temp.Length == 0)
                        {
                            return this;
                        }
                    }
                    else
                    {
                        Type type = value.GetType();
                        PropertyInfo property = type.GetProperty("Count");
                        var count = Convert.ToInt32(property.GetValue(value, null));

                        if (count == 0)
                        {
                            return this;
                        }
                    }
                }

                MemberExpression memberExpr = null;

                if (member.Body.NodeType == ExpressionType.Convert)
                {
                    memberExpr = ((UnaryExpression) member.Body).Operand as MemberExpression;
                }
                else if (member.Body.NodeType == ExpressionType.MemberAccess)
                {
                    memberExpr = member.Body as MemberExpression;
                }


                MemberExpression left = Expression.Property(_parameter, memberExpr.Member.Name);
                ConstantExpression right = null;

                try
                {
                    if (valueRelation != ExpressionValueRelation.Contains &&
                        valueRelation != ExpressionValueRelation.NotEqualNull &&
                        valueRelation != ExpressionValueRelation.EqualNull)
                    {
                        if (memberExpr.Type == typeof (String))
                        {
                            value = value.ToString().Trim();
                        }

                        /* Nullable类型处理----------------------------------------------------------*/
                        Type underlyingType = Nullable.GetUnderlyingType(memberExpr.Type);

                        if (underlyingType != null)
                        {
                            value = Convert.ChangeType(value, underlyingType);
                        }

                        right = Expression.Constant(value, memberExpr.Type);
                    }
                    else if (valueRelation == ExpressionValueRelation.Contains)
                    {
                        right = Expression.Constant(value);
                    }
                    else
                    {
                        right = Expression.Constant(null);
                    }
                }
                catch
                {
                    return this;
                }

                Expression tempBinary = null;

                switch (valueRelation)
                {
                    case ExpressionValueRelation.Equal:
                        tempBinary = Expression.Equal(left, right);
                        break;
                    case ExpressionValueRelation.NotEqual:
                        tempBinary = Expression.NotEqual(left, right);
                        break;
                    case ExpressionValueRelation.GreaterThan:
                        tempBinary = Expression.GreaterThan(left, right);
                        break;
                    case ExpressionValueRelation.GreaterThanOrEqual:
                        tempBinary = Expression.GreaterThanOrEqual(left, right);
                        break;
                    case ExpressionValueRelation.LessThan:
                        tempBinary = Expression.LessThan(left, right);
                        break;
                    case ExpressionValueRelation.LessThanOrEqual:
                        tempBinary = Expression.LessThanOrEqual(left, right);
                        break;
                    case ExpressionValueRelation.Like:
                        var isLikeMethod =
                            typeof (NHibernateLinqExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                .First(mi => mi.Name == "IsLike");
                        right = value == null
                            ? Expression.Constant("%%")
                            : Expression.Constant(string.Format("%{0}%", value.ToString()));
                        var isLikeMethodExpression = Expression.Call(isLikeMethod, left, right);
                        tempBinary = isLikeMethodExpression;
                        break;
                    case ExpressionValueRelation.CustomLike:
                        var isCustomLikeMethod =
                            typeof (NHibernateLinqExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                .First(mi => mi.Name == "IsLike");
                        right = value == null
                            ? Expression.Constant("%%")
                            : Expression.Constant(string.Format("{0}", value.ToString()));
                        var isCustomLikeMethodExpression = Expression.Call(isCustomLikeMethod, left, right);
                        tempBinary = isCustomLikeMethodExpression;
                        break;
                    //case ExpressionValueRelation.Len:
                    //    var isCustomLikeMethod = typeof(NHibernateLinqExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public).First(mi => mi.Name == "IsLike");
                    //    right = value == null ? Expression.Constant("%%") : Expression.Constant(string.Format("Len({0})", value.ToString()));
                    //    var isCustomLikeMethodExpression = Expression.Call(isCustomLikeMethod, left, right);
                    //    tempBinary = isCustomLikeMethodExpression;
                    //    break;
                    case ExpressionValueRelation.Contains:
                        MethodInfo containMethod = null;
                        Expression containMethodExpression = null;
                        if (value.GetType().BaseType == typeof (Array))
                        {
                            containMethod =
                                typeof (Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                    .First(mi => mi.Name == "Contains")
                                    .MakeGenericMethod(memberExpr.Type);
                            containMethodExpression = Expression.Call(containMethod, right, left);
                        }
                        else
                        {
                            containMethod = value.GetType().GetMethod("Contains");
                            containMethodExpression = Expression.Call(right, containMethod, left);
                        }
                        tempBinary = containMethodExpression;
                        break;
                    case ExpressionValueRelation.EqualNull:
                        tempBinary = Expression.Equal(left, right);
                        break;
                    case ExpressionValueRelation.NotEqualNull:
                        tempBinary = Expression.NotEqual(left, right);
                        break;
                }

                if (conditionExpression != null)
                {
                    switch (expressionRelation)
                    {
                        case ExpressionRelation.And:
                            conditionExpression = Expression.MakeBinary(ExpressionType.AndAlso, conditionExpression,
                                tempBinary);
                            break;
                        case ExpressionRelation.Or:
                            conditionExpression = Expression.MakeBinary(ExpressionType.OrElse, conditionExpression,
                                tempBinary);
                            break;
                    }
                }
                else
                {
                    conditionExpression = tempBinary;
                }

                return this;
            }

            /// <summary>
            /// And 条件
            /// </summary>
            /// <param name="member"></param>
            /// <param name="value"></param>
            /// <param name="valueRelation"></param>
            /// <returns></returns>
            public ExpressionCondition<T> And(Expression<Func<T, object>> member, object value, ExpressionValueRelation valueRelation)
            {
                return ComposeCondition(member, value, valueRelation, ExpressionRelation.And);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="criteria"></param>
            /// <returns></returns>
            public ExpressionCondition<T> And(Expression<Func<T, bool>> criteria)
            {
                return ComposeCriteria(criteria, ExpressionRelation.And);
            }

            /// <summary>
            /// 合并查询条件
            /// </summary>
            /// <param name="criteria"></param>
            /// <param name="expressionRelation"></param>
            /// <returns></returns>
            private ExpressionCondition<T> ComposeCriteria(Expression<Func<T, bool>> criteria, ExpressionRelation expressionRelation)
            {
                if (criteria == null)
                {
                    return this;
                }
                if (Criteria == null)
                {
                    Criteria = criteria;
                    return this;
                }

                InvocationExpression invocationExpressionOne = null;
                invocationExpressionOne = Expression.Invoke(Criteria, _parameter);

                InvocationExpression invocationExpressionTwo = null;
                invocationExpressionTwo = Expression.Invoke(criteria, _parameter);
                BinaryExpression composedCondtion = null;

                switch (expressionRelation)
                {
                    case ExpressionRelation.And:
                        composedCondtion = Expression.MakeBinary(ExpressionType.AndAlso, invocationExpressionOne, invocationExpressionTwo);
                        break;
                    case ExpressionRelation.Or:
                        composedCondtion = Expression.MakeBinary(ExpressionType.OrElse, invocationExpressionOne, invocationExpressionTwo);
                        break;
                }


                Criteria = Expression.Lambda<Func<T, bool>>(composedCondtion, _parameter);

                return this;
            }

            /// <summary>
            /// And 不等于 NULL
            /// </summary>
            /// <param name="member"></param>
            /// <returns></returns>
            public ExpressionCondition<T> AndNotEqualNull(Expression<Func<T, object>> member)
            {
                return ComposeCondition(member, null, ExpressionValueRelation.NotEqualNull, ExpressionRelation.And);
            }

            /// <summary>
            /// And 等于 NULL
            /// </summary>
            /// <param name="member"></param>
            /// <returns></returns>
            public ExpressionCondition<T> AndEqualNull(Expression<Func<T, object>> member)
            {
                return ComposeCondition(member, null, ExpressionValueRelation.EqualNull, ExpressionRelation.And);
            }

            /// <summary>
            /// Or 条件
            /// </summary>
            /// <param name="member"></param>
            /// <param name="value"></param>
            /// <param name="valueRelation"></param>
            /// <returns></returns>
            public ExpressionCondition<T> Or(Expression<Func<T, object>> member, object value, ExpressionValueRelation valueRelation)
            {
                return ComposeCondition(member, value, valueRelation, ExpressionRelation.Or);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="criteria"></param>
            /// <returns></returns>
            public ExpressionCondition<T> Or(Expression<Func<T, bool>> criteria)
            {
                return ComposeCriteria(criteria, ExpressionRelation.Or);
            }

            /// <summary>
            /// Or 不等于 NULL
            /// </summary>
            /// <param name="member"></param>
            /// <returns></returns>
            public ExpressionCondition<T> OrNotEqualNull(Expression<Func<T, object>> member)
            {
                return ComposeCondition(member, null, ExpressionValueRelation.NotEqualNull, ExpressionRelation.Or);
            }

            /// <summary>
            /// Or 等于 NULL
            /// </summary>
            /// <param name="member"></param>
            /// <returns></returns>
            public ExpressionCondition<T> OrEqualNull(Expression<Func<T, object>> member)
            {
                return ComposeCondition(member, null, ExpressionValueRelation.EqualNull, ExpressionRelation.Or);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="U"></typeparam>
            /// <param name="member"></param>
            /// <param name="value"></param>
            /// <param name="valueRelation"></param>
            /// <param name="expressionRelation"></param>
            /// <param name="partForShow"></param>
            /// <param name="partForSave"></param>
            /// <returns></returns>
            private ExpressionCondition<T> ComposeEnumCondition<U>(Expression<Func<T, object>> member, object value,
                ExpressionValueRelation valueRelation, ExpressionRelation expressionRelation, EnumPart partForShow,
                EnumPart partForSave)
            {
                if (valueRelation == ExpressionValueRelation.Contains)
                {
                    return ComposeCondition(member, value, valueRelation, expressionRelation);
                }

                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return this;
                }

                MemberExpression memberExpr = null;

                if (member.Body.NodeType == ExpressionType.Convert)
                {
                    memberExpr = ((UnaryExpression) member.Body).Operand as MemberExpression;
                }
                else if (member.Body.NodeType == ExpressionType.MemberAccess)
                {
                    memberExpr = member.Body as MemberExpression;
                }

                //定义object 类型数组
                object list = null;
                MethodInfo helperMethod = typeof (ReflectionHelper).GetMethod("BuildListHelper");
                helperMethod = helperMethod.MakeGenericMethod(new Type[] {memberExpr.Type});
                list = helperMethod.Invoke(null, null);

                //获取list add方法
                MethodInfo addMethod = list.GetType().GetMethod("Add");

                U[] values = (U[]) System.Enum.GetValues(typeof (U));

                for (int i = 0; i < values.Length; i++)
                {
                    switch (valueRelation)
                    {
                        case ExpressionValueRelation.Equal:
                        case ExpressionValueRelation.NotEqual:
                        case ExpressionValueRelation.GreaterThanOrEqual:
                        case ExpressionValueRelation.GreaterThan:
                        case ExpressionValueRelation.LessThan:
                        case ExpressionValueRelation.LessThanOrEqual:
                            if (values[i].ToString().Equals(value.ToString().Trim()))
                            {
                                return ComposeCondition(member, (int) (Object) values[i], valueRelation, expressionRelation);}
                            break;
                        case ExpressionValueRelation.Like:
                            string forShowValue = string.Empty;

                            switch (partForShow)
                            {
                                case EnumPart.AttachStringAndText:
                                    forShowValue = AttachDataAttribute.GetAttachedData<U>(values[i].ToString(), AttachDataKey.String);
                                    break;
                                case EnumPart.AttachInt:
                                    forShowValue = AttachDataAttribute.GetAttachedData<U>(values[i].ToString(), AttachDataKey.Int);
                                    break;
                                case EnumPart.AttachDecimal:
                                    forShowValue = AttachDataAttribute.GetAttachedData<U>(values[i].ToString(), AttachDataKey.Decimal);
                                    break;
                                case EnumPart.Text:
                                    forShowValue = values[i].ToString();
                                    break;
                                case EnumPart.Value:
                                    forShowValue = ((int) (object) values[i]).ToString();
                                    break;
                            }

                            if (forShowValue.Contains(value.ToString().Trim()))
                            {
                                object forSaveValue = null;

                                switch (partForSave)
                                {
                                    case EnumPart.AttachStringAndText:
                                        forSaveValue = AttachDataAttribute.GetAttachedData<U>(values[i].ToString(), AttachDataKey.String);
                                        addMethod.Invoke(list, new object[] {forSaveValue});
                                        break;
                                    case EnumPart.Text:
                                        addMethod.Invoke(list, new object[] {values[i].ToString()});
                                        break;

                                    case EnumPart.AttachDecimal:
                                        forSaveValue = AttachDataAttribute.GetAttachedData<U>(values[i].ToString(), AttachDataKey.Decimal);
                                        addMethod.Invoke(list, new object[] {(decimal) (object) forSaveValue});
                                        break;

                                    case EnumPart.AttachInt:
                                        forSaveValue = AttachDataAttribute.GetAttachedData<U>(values[i].ToString(), AttachDataKey.Int);
                                        addMethod.Invoke(list, new object[] {(int) (object) forSaveValue});
                                        break;
                                    case EnumPart.Value:
                                        /* bool 型处理 ----------------------------------------------------------*/
                                        Type genericType = list.GetType().GetGenericArguments()[0];

                                        if (Nullable.GetUnderlyingType(genericType) != null)
                                        {
                                            genericType = Nullable.GetUnderlyingType(genericType);
                                        }

                                        if (genericType == typeof (Boolean))
                                        {
                                            addMethod.Invoke(list, new object[] {(int) (object) values[i] == 1});
                                        }
                                        else
                                        {
                                            addMethod.Invoke(list, new object[] {(int) (object) values[i]});
                                        }
                                        break;
                                }
                            }
                            break;
                        case ExpressionValueRelation.EqualNull:
                            break;
                        case ExpressionValueRelation.NotEqualNull:
                            break;
                    }
                }

                valueRelation = valueRelation == ExpressionValueRelation.Like
                    ? ExpressionValueRelation.Contains
                    : valueRelation;

                return ComposeCondition(member, list, valueRelation, expressionRelation);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="U"></typeparam>
            /// <param name="member"></param>
            /// <param name="value"></param>
            /// <param name="valueRelation"></param>
            /// <param name="partForShow"></param>
            /// <param name="partForSave"></param>
            /// <returns></returns>
            public ExpressionCondition<T> And<U>(Expression<Func<T, object>> member, object value,
                ExpressionValueRelation valueRelation, EnumPart partForShow, EnumPart partForSave)
            {
                return ComposeEnumCondition<U>(member, value, valueRelation, ExpressionRelation.And, partForShow, partForSave);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="U"></typeparam>
            /// <param name="member"></param>
            /// <param name="value"></param>
            /// <param name="valueRelation"></param>
            /// <param name="partForShow"></param>
            /// <param name="partForSave"></param>
            /// <returns></returns>
            public ExpressionCondition<T> Or<U>(Expression<Func<T, object>> member, object value,
                ExpressionValueRelation valueRelation, EnumPart partForShow, EnumPart partForSave)
            {
                return ComposeEnumCondition<U>(member, value, valueRelation, ExpressionRelation.Or, partForShow, partForSave);
            }

            /// <summary>
            /// 普通类型枚举And
            /// </summary>
            /// <typeparam name="U"></typeparam>
            /// <param name="member"></param>
            /// <param name="value"></param>
            /// <param name="valueRelation"></param>
            /// <returns></returns>
            public ExpressionCondition<T> And<U>(Expression<Func<T, object>> member, object value, ExpressionValueRelation valueRelation)
            {
                return ComposeEnumCondition<U>(member, value, valueRelation, ExpressionRelation.And, EnumPart.Text, EnumPart.Value);
            }

            /// <summary>
            /// 普通类型枚举Or
            /// </summary>
            /// <typeparam name="U"></typeparam>
            /// <param name="member"></param>
            /// <param name="value"></param>
            /// <param name="valueRelation"></param>
            /// <returns></returns>
            public ExpressionCondition<T> Or<U>(Expression<Func<T, object>> member, object value, ExpressionValueRelation valueRelation)
            {
                return ComposeEnumCondition<U>(member, value, valueRelation, ExpressionRelation.Or, EnumPart.Text, EnumPart.Value);
            }

            /// <summary>
            /// 查询表达式
            /// </summary>
            public Expression<Func<T, bool>> ConditionExpression
            {
                get
                {
                    InvocationExpression invocationExpression = null;
                    if (Criteria != null)
                    {
                        invocationExpression = Expression.Invoke(Criteria, _parameter);
                    }


                    if (conditionExpression == null && Criteria == null)
                    {
                        return null;
                    }
                    else if (conditionExpression != null && Criteria == null)
                    {
                        return Expression.Lambda<Func<T, bool>>(conditionExpression, _parameter);
                    }
                    else if (conditionExpression == null && Criteria != null)
                    {
                        return Expression.Lambda<Func<T, bool>>(invocationExpression, _parameter);
                    }
                    else if (conditionExpression != null && Criteria != null)
                    {
                        switch (PrefixConditionRelation)
                        {
                            case ExpressionRelation.And:
                                conditionExpression = Expression.MakeBinary(ExpressionType.AndAlso, conditionExpression, invocationExpression);
                                break;
                            case ExpressionRelation.Or:
                                conditionExpression = Expression.MakeBinary(ExpressionType.OrElse, conditionExpression, invocationExpression);
                                break;
                        }
                    }

                    return Expression.Lambda<Func<T, bool>>(conditionExpression, _parameter);
                }
            }
        }
    }
}