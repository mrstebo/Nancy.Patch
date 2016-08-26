using System;

namespace Nancy.Patch.Tests.Mocks
{
    public class TestModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ReadOnlyName
        {
            get { return "Test"; }
        }
    }
}
