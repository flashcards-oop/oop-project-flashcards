using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace FlashcardsClient
{
    public class TestRequest
    {
        public string CollectionId { get; set; }
        public List<TestBlock> Blocks { get; set; }
    }
}