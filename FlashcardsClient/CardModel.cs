using System;

namespace FlashcardsClient
{
    public class CardModel
    {
        public Guid CollectionId { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}