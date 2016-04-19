using System.Collections.Generic;

namespace Nancy.Patch.PropertyExtractors
{
    public interface IPropertyExtractor
    {
        IEnumerable<string> Extract(Request request);
    }
}
