using System;
using System.Collections.Generic;
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
            var card = new Card("Solomon is a", "human");
            mongo.AddCard(card);

            var insertedCard = mongo.GetCard(card.Id);
            Assert.That(card.Id, Is.EqualTo(insertedCard.Id));
            Assert.That(card.Term, Is.EqualTo(insertedCard.Term));
            Assert.That(card.Definition, Is.EqualTo(insertedCard.Definition));
        }
        
        [Test]
        public void DeleteCard()
        {
            var card = new Card("Solomon is a", "human");
            mongo.AddCard(card);
            Assert.DoesNotThrow(() => mongo.GetCard(card.Id));
            mongo.DeleteCard(card.Id);
            Assert.Throws<InvalidOperationException>(() => mongo.GetCard(card.Id));
        }

        [Test]
        public void ReturnListOfCards()
        {
            var card1 = new Card("Solomon is a", "human");
            var card2 = new Card("Programmer is", "human");
            var card3 = new Card("Programmer is", "god");
            
            mongo.AddCard(card1);
            mongo.AddCard(card2);
            mongo.AddCard(card3);

            var allCards = mongo.GetAllCards();
            Assert.That(allCards.Count, Is.EqualTo(3));

            foreach (var card in mongo.GetAllCards())
            {
                Assert.True(card.Id.Equals(card1.Id) || card.Id.Equals(card2.Id) || card.Id.Equals(card3.Id));
            }
        }

        [Test]
        public void InsertCollections()
        {
            var collection = new Collection("Important terms", 
                cards: new List<Card>
                {
                    new Card("Solomon is a", "human"),
                    new Card("Programmer is", "human"),
                    new Card("Programmer is", "god")
                });
            mongo.AddCollection(collection);
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