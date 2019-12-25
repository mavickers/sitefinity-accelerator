using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using SitefinityAccelerator.Attributes;

namespace SitefinityAccelerator.Extensions
{
    public static class EnumExtensions
    {
        public static T? GetEnumValue<T>(string stringValue) where T : struct
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return null;
            }

            // first try a match on the enum names
            if (Enum.TryParse(stringValue, out T parseEnumValue))
            {
                return parseEnumValue;
            }
            
            // then try matching on the enum values
            foreach (var enumValue in Enum.GetValues(typeof(T)))
            {
                if (enumValue.ToString() == stringValue || enumValue.ToClientValue() == stringValue)
                {
                    return (T) enumValue;
                }
            }

            return null;
        }

        public static T? GetEnumValue<T>(int intValue) where T : struct
        {
            if (Enum.IsDefined(typeof(T), intValue))
            {
                return (T)(object)intValue;
            }

            return null;
        }

        public static List<string> ToArray(this Type source)
        {
            if (!source.IsEnum) return null;

            return Enum.GetNames(source).ToList();
        }

        public static List<string> ToClientValuesArray<T>()
        {
            var type = typeof(T);

            if (!type.IsEnum) return null;

            return Enum.GetValues(type).Cast<T>().Select(v => v.ToClientValue()).ToList();
        }

        public static string ToClientValue<T>(this T source)
        {
            var type = source.GetType();

            if (!type.IsEnum)
            {
                return null;
            }

            var memberInfo = type.GetMember(source.ToString());

            if (memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(ClientValueAttribute), false);

                if (attributes.Length > 0)
                {
                    return ((ClientValueAttribute) attributes[0]).ClientValue;
                }
            }

            return source.ToString();
        }

        public static IDictionary<string, int> ToDictionary<T>()
        {
            if (typeof(T).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }

            return Enum.GetValues(typeof(T)).Cast<int>().ToDictionary(i => Enum.GetName(typeof(T), i));
        }

        public static string ToSerializedJson<T>()
        {
            var type = typeof(T);

            if (type == null) throw new ArgumentNullException();
            if (!type.IsEnum) throw new ArgumentException();

            var texts = ToClientValuesArray<T>();
            var values = Enum.GetValues(type).Cast<T>().ToArray();
            var serializer = new JavaScriptSerializer();

            return serializer.Serialize(texts.Select(t => new { text = t, value = values[texts.IndexOf(t)] }));
        }
    }
}