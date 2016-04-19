using System.Collections.Generic;
using System.Linq;
using Nancy.Extensions;
using Newtonsoft.Json;

namespace Nancy.Patch.PropertyExtractors
{
    public class JsonPropertyExtractor : IPropertyExtractor
    {
        public IEnumerable<string> Extract(Request request)
        {
            var content = request.Body.AsString();
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            return data == null 
            ? Enumerable.Empty<string>()
            : data.Keys.Select(x => x.Replace("_", ""));
        }
    }
}
