using System.Collections.Generic;
using System.Reflection;

namespace Nancy.Patch
{
    internal class PatchExecutor
    {
        public T Patch<T>(T from, T to, IEnumerable<string> propertiesToMerge)
        {
            MergeProperties(from, to, propertiesToMerge);

            return to;
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
