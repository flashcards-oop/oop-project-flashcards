using System;
using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace FlashcardsClient
{
    public class TestRequest
    {
        public Guid CollectionId { get; set; }
        public List<TestBlock> Blocks { get; set; }
    }
}