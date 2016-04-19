using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Nancy.Patch.PropertyExtractors
{
    class JsonPropertyExtractor : IPropertyExtractor
    {
        public IEnumerable<string> Extract(Stream requestStream)
        {
            var content = ExtractStringFromStream(requestStream);
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            return data == null
                ? Enumerable.Empty<string>()
                : data.Keys.Select(x => x.Replace("_", ""));
        }

        private static string ExtractStringFromStream(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
