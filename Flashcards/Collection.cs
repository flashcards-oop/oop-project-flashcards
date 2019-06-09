using MongoDB.Bson.Serialization.Attributes;

namespace Flashcards
{
    public class Collection : IOwnedResource
    {
        public string Id { get; }
        [BsonElement]
        public string Name { get; }
        [BsonElement]
        public string OwnerLogin { get; }
        
        [BsonConstructor]
        public Collection(string name, string ownerLogin, string id = null)
        {
            Id = id ?? GuidGenerator.GenerateGuid();
            Name = name;
            OwnerLogin = ownerLogin;
        }

        protected Collection()
        {
            
        }
    }
}