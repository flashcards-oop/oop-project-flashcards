using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Flashcards
{
    public class Mongo : IStorage
    {
        private readonly IMongoCollection<Card> cards;
        private readonly IMongoCollection<MongoCollection> collections;
        private readonly IClientSession session;


        public Mongo()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("flashcards");
            cards = database.GetCollection<Card>("cards");
            collections = database.GetCollection<MongoCollection>("collections");
            session = client.StartSession();
        }
        
        public void AddCard(Card card)
        {
            try
            {
                cards.InsertOne(card);
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Card already exists");
            }
        }
        
        public Card FindCard(string id)
        {
            return cards.Find(c => c.Id == id).FirstOrDefault();
        }
        
        public IEnumerable<Card> GetAllCards()
        {
            return cards.AsQueryable();
        }

        public void DeleteCard(string id)
        {
            cards.FindOneAndDelete(c => c.Id == id);
        }

        public void AddCollection(Collection collection)
        {
            session.StartTransaction();
            try
            {
                foreach (var card in collection.Cards)
                {
                    try
                    {
                        AddCard(card);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                var mongoCollection = MongoCollection.FromCollection(collection);
                collections.InsertOne(mongoCollection);
                session.CommitTransaction();
            }
            catch (InvalidOperationException)
            {
                session.AbortTransaction();
                throw new Exception("Collection already exists");
            }
        }
        
        public Collection FindCollection(string id)
        {
            var mongoCollection = collections.Find(c => c.Id == id).FirstOrDefault();
            if (mongoCollection is null)
            {
                return null;
            }
            var collection = new Collection(mongoCollection.Name, mongoCollection.Id);
            foreach (var cardId in mongoCollection.CardsId)
            {
                collection.Cards.Add(FindCard(cardId));
            }

            return collection;
        }

        public IEnumerable<Collection> GetAllCollections()
        {
            foreach (var id in collections.AsQueryable().Select(c => c.Id))
            {
               yield return FindCollection(id);
            }
        }
        
        public void DeleteCollection(string id)
        {
            collections.FindOneAndDelete(c => c.Id == id);
        }

        public void AddCardToCollection(string collectionId, string cardId)
        {
            session.StartTransaction();
            try
            {
                var update = Builders<MongoCollection>.Update.Push(c => c.CardsId, cardId);
                collections.UpdateOne(c => c.Id == collectionId, update);
                session.CommitTransaction();
            }
            catch (InvalidOperationException)
            {
                session.AbortTransaction();
                throw new Exception("No collection with such id");
            }
        }

        public void RemoveCardFromCollection(string collectionId, string cardId)
        {
            session.StartTransaction();
            try
            {
                var update = Builders<MongoCollection>.Update.Pull(c => c.CardsId, cardId);
                collections.FindOneAndUpdate(c => c.Id == collectionId, update);
                session.CommitTransaction();
            }
            catch (InvalidOperationException)
            {
                session.AbortTransaction();
                throw new Exception("No collection or card with such id");
            }
        }
    }
}