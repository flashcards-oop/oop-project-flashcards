using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsClient
{
    public class QuestionRequest
    {
        public string Type { get; set; }
        public int Amount { get; set; }
    }

    public class TestRequest
    {
        public string CollectionId { get; set; }
        public List<QuestionRequest> Blocks { get; set; }
    }
}