using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Nancy.ModelBinding;
using Nancy.Patch.PropertyExtractors;

namespace Nancy.Patch
{
    public static class PatchExtensions
    {
        public static T Patch<T>(this INancyModule module, T target)
        {
            var boundModel = module.Bind<T>();
            var propertiesToMerge = ExtractPropertiesToMerge(module);

            MergeProperties(boundModel, target, propertiesToMerge);

            return target;
        }

        private static IEnumerable<string> ExtractPropertiesToMerge(INancyModule module)
        {
            module.Request.Body.Seek(0, SeekOrigin.Begin);

            var contentType = module.Request.Headers["Content-Type"].FirstOrDefault();
            IPropertyExtractor propertyExtractor;

            switch (contentType)
            {
                case "application/json":
                    propertyExtractor = new JsonPropertyExtractor();
                    break;

                default:
                    throw new NotSupportedException("Content type not supported");
            }

            return propertyExtractor.Extract(module.Request);
        }

        private static void MergeProperties<T>(T from, T to, IEnumerable<string> propertiesToMerge)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            var type = typeof(T);

            foreach (var propertyToMerge in propertiesToMerge)
            {
                var propertyInfo = type.GetProperty(propertyToMerge, bindingFlags);
                var newValue = propertyInfo.GetValue(from, null);

                propertyInfo.SetValue(to, newValue, null);
            }
        }
    }
}
