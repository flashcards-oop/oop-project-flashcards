using System.Collections.Generic;
using System.Linq;
using Flashcards;
using NUnit.Framework;

namespace FlashcardsTests
{
    [TestFixture]
    public class MongoShould
    {
        private Mongo mongo;
        
        [SetUp]
        public void Init()
        {
            mongo = new Mongo();
        }

        [TearDown]
        public void Clear()
        {
            mongo.Clear();
        }
        
        [Test]
        public void InsertCards()
        {
            var card = new Card("Solomon is a", "human", "0");
            mongo.AddCard(card);

            var insertedCard = mongo.FindCard(card.Id);
            Assert.That(card.Id, Is.EqualTo(insertedCard.Id));
            Assert.That(card.Term, Is.EqualTo(insertedCard.Term));
            Assert.That(card.Definition, Is.EqualTo(insertedCard.Definition));
        }
        
        [Test]
        public void DeleteCard()
        {
            var card = new Card("Solomon is a", "human", "0");
            mongo.AddCard(card);
            mongo.DeleteCard(card.Id);
            Assert.IsNull(mongo.FindCard(card.Id));
        }

        [Test]
        public void ReturnListOfCards()
        {
            var card1 = new Card("Solomon is a", "human", "0");
            var card2 = new Card("Programmer is", "human", "0");
            var card3 = new Card("Programmer is", "god", "0");
            
            mongo.AddCard(card1);
            mongo.AddCard(card2);
            mongo.AddCard(card3);

            var allCards = mongo.GetAllCards();
            Assert.That(allCards.AsEnumerable().Count(), Is.EqualTo(3));

            foreach (var card in mongo.GetAllCards())
            {
                Assert.True(card.Id.Equals(card1.Id) || card.Id.Equals(card2.Id) || card.Id.Equals(card3.Id));
            }
        }

        [Test]
        public void InsertCollection()
        {
            var collection = new Collection("Important terms", "0",
                cards: new List<Card>
                {
                    new Card("Solomon is a", "human", "0"),
                    new Card("Programmer is", "human", "0"),
                    new Card("Programmer is", "god", "0")
                });
            mongo.AddCollection(collection);
        }

        [Test]
        public void DeleteCollection()
        {
            var collection = new Collection("Important terms", "0",
                cards: new List<Card>
                {
                    new Card("Solomon is a", "human", "0"),
                    new Card("Programmer is", "human", "0"),
                    new Card("Programmer is", "god", "0")
                });
            mongo.AddCollection(collection);
            mongo.DeleteCollection(collection.Id);
            Assert.IsNull(mongo.FindCollection(collection.Id));
        }

        [Test]
        public void AddCardToCollection()
        {
            var collection = new Collection("Important terms", "0",
                cards: new List<Card>
                {
                    new Card("Solomon is a", "human", "0"),
                    new Card("Programmer is", "human", "0"),
                    new Card("Programmer is", "god", "0")
                });
            mongo.AddCollection(collection);
            
            var card = new Card("Solomon is a", "god", "0");
            mongo.AddCard(card);
            
            mongo.AddCardToCollection(collection.Id, card.Id);
            var updatedCollection = mongo.FindCollection(collection.Id);
            Assert.That(updatedCollection.Cards.Count, Is.EqualTo(4));
            Assert.That(updatedCollection.Cards.Select(c => c.Id), Has.Member(card.Id));
        }

        [Test]
        public void RemoveCardFromCollection()
        {
            var collection = new Collection("Important terms", "0",
                cards: new List<Card>
                {
                    new Card("Solomon is a", "human", "0"),
                    new Card("Programmer is", "human", "0"),
                    new Card("Programmer is", "god", "0")
                });
            mongo.AddCollection(collection);
            
            mongo.RemoveCardFromCollection(collection.Id, collection.Cards[0].Id);
            var updatedCollection = mongo.FindCollection(collection.Id);
            Assert.That(updatedCollection.Cards.Count, Is.EqualTo(2));
            Assert.That(updatedCollection.Cards.Select(c => c.Id), Has.No.Member(collection.Cards[0].Id));
        }
    }

    public static class MongoExtensions
    {
        public static void Clear(this Mongo mongo)
        {
            foreach (var card in mongo.GetAllCards())
            {
                mongo.DeleteCard(card.Id);
            }
            
            foreach (var collection in mongo.GetAllCollections())
            {
                mongo.DeleteCollection(collection.Id);
            }
        }
    }
}