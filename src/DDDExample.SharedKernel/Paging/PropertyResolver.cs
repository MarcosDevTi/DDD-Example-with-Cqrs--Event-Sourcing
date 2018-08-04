using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DDDExample.SharedKernel.Paging
{
    public static class PropertyResolver
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression unaryExpression)

                memberExpression = unaryExpression.Operand as MemberExpression;
            else
                memberExpression = lambda.Body as MemberExpression;

            var propertyInfo = (PropertyInfo)memberExpression?.Member;

            return propertyInfo?.Name;

        }
    }
}
