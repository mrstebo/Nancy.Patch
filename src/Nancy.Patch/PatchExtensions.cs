using System.Collections.Generic;
using System.IO;
using Nancy.ModelBinding;
using Nancy.Patch.Factories;

namespace Nancy.Patch
{
    public static class PatchExtensions
    {
        public static PatchResult Patch<T>(this INancyModule module, T target)
        {
            var boundModel = module.Bind<T>();
            var propertiesToMerge = ExtractPropertiesToMerge(module.Request);

            return new PatchExecutor().Patch(boundModel, target, propertiesToMerge);
        }

        private static IEnumerable<string> ExtractPropertiesToMerge(Request request)
        {
            var propertyExtractor = PropertyExtractorFactory.CreateFor(request);

            request.Body.Seek(0, SeekOrigin.Begin);

            return propertyExtractor.Extract(request);
        }
    }
}
