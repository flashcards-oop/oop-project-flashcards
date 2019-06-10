using System.Collections.Generic;

namespace FlashcardsApi.Models
{
    public class FilterDto
    {
        public string Name { get; set; }
        public Dictionary<string, object> Options { get; set; }
    }
}
