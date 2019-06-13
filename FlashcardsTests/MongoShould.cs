using System;
using System.Linq;
using System.Threading.Tasks;
using Flashcards;
using FlashcardsApi.Config;
using FlashcardsApi.Mongo;
using NUnit.Framework;

namespace FlashcardsTests
{
    [TestFixture]
    public class MongoShould
    {
        private readonly MongoContext context = new MongoContext(new MongoDbConfig
        {
            Host = "localhost", 
            Port = 27017
        });

        private MongoCardStorage mongo;
        
        [SetUp]
        public void Init()
        {
            mongo = new MongoCardStorage(context);
        }

        [TearDown]
        public async Task Clear()
        {
            await mongo.Clear();
        }
        
        [Test]
        public async Task InsertCards()
        {
            var card = new Card("Solomon is a", "human", "0", Guid.NewGuid());
            await mongo.AddCard(card);

            var insertedCard = await mongo.FindCard(card.Id);
            Assert.That(card.Id, Is.EqualTo(insertedCard.Id));
            Assert.That(card.Term, Is.EqualTo(insertedCard.Term));
            Assert.That(card.Definition, Is.EqualTo(insertedCard.Definition));
            Assert.That(card.CollectionId, Is.EqualTo(insertedCard.CollectionId));
        }
        
        [Test]
        public async Task DeleteCard()
        {
            var card = new Card("Solomon is a", "human", "0", Guid.NewGuid());
            await mongo.AddCard(card);
            await mongo.DeleteCard(card.Id);
            Assert.IsNull(await mongo.FindCard(card.Id));
        }

        [Test]
        public async Task ReturnListOfCards()
        {
            var card1 = new Card("Solomon is a", "human", "0", Guid.NewGuid());
            var card2 = new Card("Programmer is", "human", "0", Guid.NewGuid());
            var card3 = new Card("Programmer is", "god", "0", Guid.NewGuid());
            
            await mongo.AddCard(card1);
            await mongo.AddCard(card2);
            await mongo.AddCard(card3);

            var allCards = await mongo.GetAllCards();
            Assert.That(allCards.Count(), Is.EqualTo(3));

            foreach (var card in allCards)
            {
                Assert.True(card.Id.Equals(card1.Id) || card.Id.Equals(card2.Id) || card.Id.Equals(card3.Id));
            }
        }

        [Test]
        public async Task InsertCollection()
        {
            var collection = new Collection("Important terms", "0");
            await mongo.AddCollection(collection);
        }

        [Test]
        public async Task DeleteCollection()
        {
            var collection = new Collection("Important terms", "0");
            await mongo.AddCollection(collection);
            await mongo.DeleteCollection(collection.Id);
            Assert.IsNull(await mongo.FindCollection(collection.Id));
        }

        [Test]
        public async Task AddCardToCollection()
        {
            var collection = new Collection("Important terms", "0");
            await mongo.AddCollection(collection);


            await mongo.AddCard(new Card("Solomon is a", "human", "0", collection.Id));
            await mongo.AddCard(new Card("Programmer is", "human", "0", collection.Id));
            await mongo.AddCard(new Card("Programmer is", "god", "0", collection.Id));

            Assert.NotNull(await mongo.FindCollection(collection.Id));
            var collectionCards = await mongo.GetCollectionCards(collection.Id);
            Assert.That(collectionCards.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task RemoveCardFromCollection()
        {
            var collection = new Collection("Important terms", "0");
            await mongo.AddCollection(collection);

            var card = new Card("Solomon is a", "human", "0", collection.Id);
            await mongo.AddCard(card);
            await mongo.AddCard(new Card("Programmer is", "human", "0", collection.Id));
            await mongo.AddCard(new Card("Programmer is", "god", "0", collection.Id));
            
            await mongo.DeleteCard(card.Id);
            Assert.That((await mongo.GetCollectionCards(collection.Id)).Count, Is.EqualTo(2));
        }
    }

    public static class MongoExtensions
    {
        public static async Task Clear(this MongoCardStorage mongo)
        {
            foreach (var card in await mongo.GetAllCards())
            {
                await mongo.DeleteCard(card.Id);
            }
            
            foreach (var collection in await mongo.GetAllCollections())
            {
                await mongo.DeleteCollection(collection.Id);
            }
        }
    }
}