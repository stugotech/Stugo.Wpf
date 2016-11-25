using System;
using System.Collections;
using System.Windows;

namespace Stugo.Wpf
{
    public static class FormattingExtensions
    {
        public static bool IsTruthy(this object value, bool zeroIsTrue = true)
        {
            return value != null
                   && !value.Equals(string.Empty)
                   && !value.Equals(false)
                   && (zeroIsTrue || !IsZero(value))
                   && !(value is IEnumerable && !((IEnumerable)value).GetEnumerator().MoveNext());
        }


        public static bool IsNumeric(this object x)
        {
            return x != null && IsNumeric(x.GetType());
        }


        public static bool IsNumeric(this Type type)
        {
            var typeCode = Type.GetTypeCode(type);
            return typeCode == TypeCode.Decimal
                || (type.IsPrimitive 
                    && typeCode != TypeCode.Object 
                    && typeCode != TypeCode.Boolean 
                    && typeCode != TypeCode.Char);
        }


        public static bool IsZero(this object value)
        {
            return (bool)((dynamic)value == 0);
        }


        public static Visibility ToVisibility(this bool value)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }


        public static Visibility ToVisibility(this object value)
        {
            return value.IsTruthy().ToVisibility();
        }
    }
}
