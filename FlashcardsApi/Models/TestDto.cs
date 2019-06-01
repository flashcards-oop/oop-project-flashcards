using System.Collections.Generic;

namespace FlashcardsApi.Models
{
    public class TestBlockDto
    {
        public string Type { get; }
        public int Amount { get; }

        public TestBlockDto(string type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }

    public class TestDto
    {
        public string CollectionId { get; }
        public readonly List<TestBlockDto> Blocks;
        
        public TestDto(string collectionId, List<TestBlockDto> blocks)
        {
            CollectionId = collectionId;
            Blocks = blocks;
        }
    }
}