using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Flashcards
{
    public class Mongo : IStorage
    {
        private readonly IMongoCollection<BsonDocument> cards;
        private readonly IMongoCollection<BsonDocument> collections;
        
        public Mongo()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("flashcards");
            cards = database.GetCollection<BsonDocument>("cards");
            collections = database.GetCollection<BsonDocument>("collections");
        }
        
        public void AddCard(Card card)
        {
            cards.InsertOne(card.ToBsonDocument());
        }
        
        public Card GetCard(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var card = cards.Find(filter).First();
            return BsonSerializer.Deserialize<Card>(card);
        }
        
        public List<Card> GetAllCards()
        {
            var allCards = cards.AsQueryable();
            var lol = new List<Card>();
            foreach (var card in allCards)
            {
                lol.Add(BsonSerializer.Deserialize<Card>(card));
            }

            return lol;
        }

        public void DeleteCard(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            cards.FindOneAndDelete(filter);
        }

        public void AddCollection(Collection collection)
        {
            collections.InsertOne(collection.ToBsonDocument());
        }
        
        public Collection GetCollection(string id)
        {
            throw new System.NotImplementedException();
        }

        public List<Collection> GetAllCollections()
        {
            throw new System.NotImplementedException();
        }
        
        public void DeleteCollection(string id)
        {
            throw new NotImplementedException();
        }
    }
}