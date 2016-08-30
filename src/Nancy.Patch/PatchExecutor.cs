using System;
using System.Collections.Generic;
using System.Reflection;
using Nancy.Patch.Exceptions;

namespace Nancy.Patch
{
    internal class PatchExecutor
    {
        public PatchResult Patch<T>(T from, T to, IEnumerable<string> propertiesToMerge)
        {
            try
            {
                MergeProperties(from, to, propertiesToMerge);
            }
            catch (Exception ex)
            {
                return new PatchResult
                {
                    Succeeded = false,
                    Message = ex.Message
                };
            }
            return new PatchResult {Succeeded = true};
        }

        private static void MergeProperties<T>(T from, T to, IEnumerable<string> propertiesToMerge)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            var type = typeof(T);

            foreach (var propertyToMerge in propertiesToMerge)
            {
                var propertyInfo = type.GetProperty(propertyToMerge, bindingFlags);

	            if (propertyInfo == null || !propertyInfo.CanWrite)
		            throw new PropertyNotFoundException(propertyToMerge);

                var newValue = propertyInfo.GetValue(from, null);

                propertyInfo.SetValue(to, newValue, null);
            }
        }
    }
}
