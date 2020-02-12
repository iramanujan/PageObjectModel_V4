﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Selenium.Framework.Development.Kit.Helper.Utils
{
    public static class ReflectionUtils
    {
        public static IEnumerable<PropertyInfo> GetPropertiesOfSpecifiedTypeWithAttribute(this object value, Type propertyType, Type attributeType)
        {
            var predicate = new Func<PropertyInfo, bool>(x => Attribute.IsDefined(x, attributeType) && x.PropertyType == propertyType);
            return value.GetType().GetFilteredProperties(predicate);
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute(this object value, Type attributeType)
        {
            var predicate = new Func<PropertyInfo, bool>(x => Attribute.IsDefined(x, attributeType));
            return value.GetType().GetFilteredProperties(predicate);
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute(this Type objectType, Type attributeType)
        {
            var predicate = new Func<PropertyInfo, bool>(x => Attribute.IsDefined(x, attributeType));
            return objectType.GetFilteredProperties(predicate);
        }

        public static IEnumerable<PropertyInfo> GetFilteredProperties(this Type objectType, Func<PropertyInfo, bool> predicate)
        {
            return objectType
                   .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                   .Where(predicate);
        }

        public static IEnumerable<TItem> GetPropertyValuesWithFilteredAttribute<TItem, TAttribute>(this object value,
            Predicate<TAttribute> filter)
            where TItem : class
            where TAttribute : Attribute
        {
            var propertiesWithAttribute = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(
                    x =>
                        x.PropertyType == typeof(TItem) && Attribute.IsDefined(x, typeof(TAttribute)) &&
                        filter(x.GetCustomAttribute<TAttribute>())).ToArray();
            return propertiesWithAttribute.Length < 1
                ? Enumerable.Empty<TItem>()
                : propertiesWithAttribute.Select(x => x.GetValue(value) as TItem);
        }

        public static IEnumerable<TItem> GetPublicPropertyValuesOfType<TItem>(this object value) where TItem : class
        {
            var propertiesWithAttribute = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(
                    x =>
                        x.PropertyType == typeof(TItem)).ToList();
            return propertiesWithAttribute.Count < 1
                ? Enumerable.Empty<TItem>()
                : propertiesWithAttribute.Select(x => x.GetValue(value) as TItem);
        }

        public static void PrintPublicPropertiesToConsole(this object value)
        {
            PropertyInfo[] props = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in props)
            {
                var propertyName = propertyInfo.Name;
                var propertyValue = propertyInfo.GetValue(value, null);
                if (propertyValue == null ||
                    (propertyValue.GetType().IsGenericType && propertyValue.GetType().GetGenericArguments().Length == 1))
                {
                    continue;
                }
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}={1}", propertyName, propertyValue));
            }
        }

        public static IEnumerable<TItem> GetFieldValuesWithFilteredAttribute<TItem, TAttribute>(this object value,
            Predicate<TAttribute> filter)
            where TItem : class
            where TAttribute : Attribute
        {
            var propertiesWithAttribute = value.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(
                    x =>
                        x.FieldType == typeof(TItem) && Attribute.IsDefined(x, typeof(TAttribute)) &&
                        filter(x.GetCustomAttribute<TAttribute>())).ToArray();
            return propertiesWithAttribute.Length < 1
                ? Enumerable.Empty<TItem>()
                : propertiesWithAttribute.Select(x => x.GetValue(value) as TItem);
        }

        public static string PublicPropertiesToString(this object value)
        {
            PropertyInfo[] props = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(value.GetType().Name).Append(": ");
            if (props.Any())
            {
                foreach (var propertyInfo in props)
                {
                    var propertyName = propertyInfo.Name;
                    var propertyValue = propertyInfo.GetValue(value, null);
                    if (propertyValue == null ||
                        (propertyValue.GetType().IsGenericType &&
                         propertyValue.GetType().GetGenericArguments().Length == 1))
                    {
                        continue;
                    }
                    stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", propertyName, propertyValue).AppendLine();
                }
                return stringBuilder.ToString();
            }
            return stringBuilder.Append("No public properties found.").ToString();
        }

        public static object GetValue(this MemberInfo memberInfo, object forObject)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo).GetValue(forObject);
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).GetValue(forObject);
                default:
                    throw new NotImplementedException();
            }
        }

        public static IEnumerable<T> GetPropertyValuesOfType<T>(this object obj,
             BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
        {
            return obj.GetType().GetProperties(bindingFlags)
                .Where(_ => _.PropertyType == typeof(T) || typeof(T).IsAssignableFrom(_.PropertyType)).Select(_ => _.GetValue(obj, null)).Cast<T>();
        }
    }
}
