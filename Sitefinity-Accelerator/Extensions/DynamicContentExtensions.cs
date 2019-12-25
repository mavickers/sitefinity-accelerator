using System;
using System.Collections.Generic;
using System.Linq;
using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;

namespace SitefinityAccelerator.Extensions
{
    public static class DynamicContentExtensions
    {
        public static T? GetEnumValueOrNull<T>(this DynamicContent source, string fieldName) where T : struct, IConvertible
        {
            var sourceValue = source?.GetValue(fieldName);

            if (sourceValue == null)
            {
                return default;
            }

            // if it's an int value try sending corresponding enum back, otherwise try matching to the string value
            return int.TryParse((sourceValue as ChoiceOption)?.PersistedValue ?? "NaN", out var sourceValueAsInt)
                ? EnumExtensions.GetEnumValue<T>(sourceValueAsInt)
                : EnumExtensions.GetEnumValue<T>(sourceValue.ToString());
        }

        public static decimal GetDecimalValueOrDefault(this DynamicContent source, string fieldName, decimal defaultValue = default)
        {
            var sourceValue = source.GetValue(fieldName);
            var returnValue = defaultValue;

            if (sourceValue == null)
            {
                return returnValue;
            }

            if (decimal.TryParse(sourceValue.ToString(), out returnValue))
            {
                return returnValue;
            }

            return defaultValue;
        }

        public static int? GetIntValueOrDefault(this DynamicContent source, string fieldName, int? defaultValue = null)
        {
            var sourceValue = source.GetValue(fieldName);

            if (sourceValue == null)
            {
                return defaultValue;
            }

            if (int.TryParse(sourceValue.ToString(), out var parsedInt))
            {
                return parsedInt;
            }

            return null;
        }

        public static string GetStringValueOrEmpty(this DynamicContent source, string fieldName)
        {
            return source.GetValue<Lstring>(fieldName)?.ToString() ?? string.Empty;
        }

        public static IEnumerable<T> MapPublishedItems<T>
        (
            this IQueryable<DynamicContent> sourceItems, 
            IDynamicContentMapper<T> mapper, 
            bool withNullFiltering = true
        ) 
        {
            var mappedItems = 
                sourceItems?
                    .Where(i => i.Visible && i.Status == ContentLifecycleStatus.Live)?
                    .Select(i => mapper.Map(i));

            if (mappedItems == null)
            {
                return null;
            }

            return withNullFiltering
                ? mappedItems.Where(i => i != null)
                : mappedItems;
        }

        public static IEnumerable<T> GetAndMapRelatedItems<T>(this DynamicContent source, string fieldName, IDynamicContentMapper<T> mapper, bool withNullFiltering = true)
        {
            var items = source.GetRelatedItems<DynamicContent>(fieldName);

            return items?.MapPublishedItems(mapper, withNullFiltering);
        }
    }
}