using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Card : IOwnedResource
    {
        [BsonId]
        public string Id { get; }
        [BsonElement]
        public string CollectionId { get; }
        [BsonElement]
        public string Term { get; }
        [BsonElement]
        public string Definition { get; }
        [BsonElement]
        public int Awareness { get; }
        [BsonElement]
        public string OwnerLogin { get; }

        [BsonConstructor]
        public Card(string term, string definition, string ownerLogin, string collectionId, 
            string id = null, int awareness = 0)
        {
            Id = id ?? GuidGenerator.GenerateGuid();
            CollectionId = collectionId;
            Term = term;
            Definition = definition;
            Awareness = awareness;
            OwnerLogin = ownerLogin;
        }
    }
}
