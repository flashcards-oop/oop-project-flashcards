using System.Collections.Generic;

namespace FlashcardsApi.Models
{
    public class CollectionDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> CardIds { get; set; }
    }
}
