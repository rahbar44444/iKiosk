using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.Framework.Wpf.Helpers
{
	public static class ExpressionHelpers
	{
		public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
		{
			return lambda.Compile().Invoke();
		}

		public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
		{
			if (lambda.Body is not MemberExpression member)
				throw new ArgumentException("Expression must be a property expression.");

			if (member.Member is not PropertyInfo property)
				throw new ArgumentException("Expression must reference a property.");

			// Get target instance
			var objectMember = Expression.Convert(member.Expression, typeof(object));
			var getterLambda = Expression.Lambda<Func<object>>(objectMember);
			var target = getterLambda.Compile().Invoke();

			// Now safely set the property value
			property.SetValue(target, value, null);
		}
	}
}
