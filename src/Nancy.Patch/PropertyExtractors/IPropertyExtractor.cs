using System.Collections.Generic;
using System.IO;

namespace Nancy.Patch.PropertyExtractors
{
    public interface IPropertyExtractor
    {
        IEnumerable<string> Extract(Stream requestStream);
    }
}
