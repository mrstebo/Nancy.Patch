using System;
using System.Linq;
using Nancy.Patch.PropertyExtractors;

namespace Nancy.Patch.Factories
{
    internal static class PropertyExtractorFactory
    {
        public static IPropertyExtractor CreateFor(Request request)
        {
            var contentType = request.Headers["Content-Type"].FirstOrDefault();

            switch (contentType)
            {
                case "application/json":
                    return new JsonPropertyExtractor();
                    
            }
            throw new NotSupportedException("Content type not supported");
        }
    }
}
