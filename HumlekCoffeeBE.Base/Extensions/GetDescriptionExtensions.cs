using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Extensions
{
    public static class GetDescriptionExtensions
    {
        public static string EnumDescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return source.ToString();
        }

        public static string EnumDescriptionAttr<T>(this T source, string value)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return string.Format(attributes[0].Description, value);
            else
                return string.Format(source.ToString(), value);
        }

        public static string EnumDescriptionAttr<T>(this T source, params string[] values)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return string.Format(attributes[0].Description, values);
            else
                return string.Format(source.ToString(), values);
        }

        public static string DescriptionAttr<T>(this T source, string fieldName)
        {
            string result;
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            var fieldProperty = properties.Find(fieldName, false);
            if (fieldProperty != null)
                result = fieldProperty.Description;
            else
                result = fieldName;

            return result;
        }
    }
}
