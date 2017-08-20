using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace LogXtreme.Extensions {

    public static partial class ExpressionExtensions {

        const string ErrMsgExpressionIsNotProperty = @"The specified expression does not reference a property.";
        const string ErrMsgNoPropertyInfoForMemberExpression = @"The specified member expression does not have property information.";

        public static PropertyInfo ToPropertyInfo<TSource, TProperty>(
            this Expression<Func<TSource, TProperty>> propertyExpression) {

            Contract.Requires(propertyExpression != null);

            Type type = typeof(TSource);

            var expressionBody = propertyExpression.Body as MemberExpression;

            if(expressionBody == null) {
                throw new ArgumentException(ErrMsgExpressionIsNotProperty);
            }

            var propertyInfo = expressionBody.Member as PropertyInfo;

            if(propertyInfo == null) {
                throw new ArgumentException(ErrMsgNoPropertyInfoForMemberExpression);
            }

            if(!propertyInfo.ReflectedType.IsAssignableFrom(type)) {
                throw new ArgumentException($"The lamba expresion '{propertyExpression.ToString()}' refers to a property that is not from type {type}");
            }

            return propertyInfo;
        }

        public static string ToPropertyName<TSource, TProperty>(
            this Expression<Func<TSource, TProperty>> propertyExpression) {

            var propertyInfo = propertyExpression.ToPropertyInfo();
            return propertyInfo.Name;
        }
    }
}