using System.Collections.Generic;

namespace FlashcardsClient
{
    public class TestRequest
    {
        public string CollectionId { get; set; }
        public List<QuestionRequest> Blocks { get; set; }
    }
}
