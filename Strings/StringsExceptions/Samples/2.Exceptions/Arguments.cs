using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Samples.Exceptions;
// ReSharper disable PossibleMultipleEnumeration

static internal class Arguments
{
    public static void NotNullOrEmpty<T>(Expression<Func<T>> expression)
    {
        MemberExpression memberExpression = expression.Body as MemberExpression ?? throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
        ConstantExpression constantExpression = memberExpression.Expression as ConstantExpression ?? throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");

        Object value;
        switch (memberExpression.Member)
        {
            case FieldInfo field:
                value = field.GetValue(constantExpression.Value);
                break;
            case PropertyInfo property:
                value = property.GetValue(constantExpression.Value);
                break;
            default:
                throw new NotSupportedException();
        }

        String memberName = memberExpression.Member.Name;
        if (value is null)
            throw new ArgumentNullException(memberName, $"Argument [{memberName}] cannot be null.");

        if (value is IEnumerable enumerable)
        {
            var enumerator = enumerable.GetEnumerator();
            using (enumerator as IDisposable)
            {
                if (!enumerator.MoveNext())
                    throw new ArgumentEmptyException(memberName, $"Argument [{memberName}] cannot be empty.", enumerable);
            }
        }
    }
}