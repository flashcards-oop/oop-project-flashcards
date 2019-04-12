using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Flashcards
{
    public class Mongo : IStorage
    {
        private readonly IMongoCollection<BsonDocument> cards;
        private readonly IMongoCollection<BsonDocument> collections;

        private static readonly Func<string, FilterDefinition<BsonDocument>> IdFilter = 
            id => Builders<BsonDocument>.Filter.Eq("_id", id);
        
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
            var card = cards.Find(IdFilter(id)).First();
            return BsonSerializer.Deserialize<Card>(card);
        }
        
        public List<Card> GetAllCards()
        {
            var allCards = new List<Card>();
            foreach (var card in cards.AsQueryable())
            {
                allCards.Add(BsonSerializer.Deserialize<Card>(card));
            }

            return allCards;
        }

        public void DeleteCard(string id)
        {
            cards.FindOneAndDelete(IdFilter(id));
        }

        public void AddCollection(Collection collection)
        {
            foreach (var card in collection.Cards)
            {
                AddCard(card);
            }
            var mongoCollection = MongoCollection.FromCollection(collection);
            collections.InsertOne(mongoCollection.ToBsonDocument());
        }
        
        public Collection GetCollection(string id)
        {
            var mongoCollection = collections.Find(IdFilter(id)).First();
            var deserializedCollection = BsonSerializer.Deserialize<MongoCollection>(mongoCollection);
            var collection = new Collection(deserializedCollection.Name, deserializedCollection.Id);
            foreach (var cardId in deserializedCollection.CardsId)
            {
                collection.Cards.Add(GetCard(cardId));
            }

            return collection;
        }

        public List<Collection> GetAllCollections()
        {
            var allCollections = new List<Collection>();
            foreach (var mongoCollection in collections.AsQueryable())
            {
                var deserializedCollection = BsonSerializer.Deserialize<MongoCollection>(mongoCollection);
                var collection = new Collection(deserializedCollection.Name, deserializedCollection.Id);
                foreach (var cardId in deserializedCollection.CardsId)
                {
                    collection.Cards.Add(GetCard(cardId));
                }
            }

            return allCollections;
        }
        
        public void DeleteCollection(string id)
        {
            collections.FindOneAndDelete(IdFilter(id));
        }
    }
}