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

    public class TestQueryDto
    {
        public string CollectionId { get; }
        public readonly List<TestBlockDto> Blocks;
        
        public TestQueryDto(string collectionId, List<TestBlockDto> blocks)
        {
            CollectionId = collectionId;
            Blocks = blocks;
        }
    }
}