using NUnit.Framework;
using FlashcardsApi.Controllers;
using FlashcardsApi.Models;
using FakeItEasy;
using Flashcards;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;

namespace FlashcardsApiTests
{
    [TestFixture]
    public class CardsController_Tests
    {
        Card[] cards = new[]
            {
                new Card("t1", "d1", "admin", "coll1", "card0"),
                new Card("t2", "d2", "admin", "coll1", "card1")
            };

        CardDto cardDto = new CardDto()
        {
            Term = "term",
            Definition = "definition",
            CollectionId = "coll2"
        };

        Collection ownedCollection = new Collection("name", "admin", "coll2");
        Collection notOwnedCollection = new Collection("name", "user", "coll2");
        Card notOwnedCard = new Card("t2", "d2", "user", "coll1", "card1");

        IStorage fakeStorage;
        CardsController controller;

        [SetUp]
        public void SetUp()
        {
            fakeStorage = A.Fake<IStorage>();
            controller = new CardsController(fakeStorage);
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");
        }

        [Test]
        public async Task GetAll_ShouldReturnAllUsersCards()
        {
            A.CallTo(() => fakeStorage.GetAllCards(default(CancellationToken))).Returns(cards);

            var result = await controller.GetAll(default(CancellationToken));

            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult).Value.Should().BeEquivalentTo(cards);
        }

        [Test]
        public async Task CreateCard_ShouldAddNewCard()
        {
            A.CallTo(() => fakeStorage.FindCollection(A<string>._, default(CancellationToken)))
                .Returns(ownedCollection);
           
            var result = await controller.CreateCard(cardDto, default(CancellationToken));

            result.Should().BeOfType<CreatedAtRouteResult>();
            A.CallTo(() => fakeStorage.AddCard(A<Card>.That.Matches(
                card => card.Term == cardDto.Term &&
                        card.Definition == cardDto.Definition &&
                        card.OwnerLogin == "admin"), default(CancellationToken)))
                .MustHaveHappened();
        }

        [Test]
        public async Task CreateCard_ShouldReturnUnprocessable_WhenCannotFindCollection()
        {
            A.CallTo(() => fakeStorage.FindCollection(A<string>._, default(CancellationToken)))
                .Returns<Collection>(null);

            var result = await controller.CreateCard(cardDto, default(CancellationToken));

            result.Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Test]
        public async Task CreateCard_ShouldForbid_WhenCollectionIsNotOwned()
        {
            A.CallTo(() => fakeStorage.FindCollection(A<string>._, default(CancellationToken)))
                .Returns(notOwnedCollection);

            var result = await controller.CreateCard(cardDto, default(CancellationToken));

            result.Should().BeOfType<ForbidResult>();
        }

        [Test]
        public async Task GetById_ShouldReturnNotFound_WhenCannotFindCard()
        {
            A.CallTo(() => fakeStorage.FindCard(A<string>._, default(CancellationToken)))
                .Returns<Card>(null);

            var result = await controller.GetById("id", default(CancellationToken));

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetById_ShouldForbid_WhenCardIsNotOwned()
        {
            A.CallTo(() => fakeStorage.FindCard(A<string>._, default(CancellationToken)))
                .Returns(notOwnedCard);

            var result = await controller.GetById(notOwnedCard.Id, default(CancellationToken));

            result.Result.Should().BeOfType<ForbidResult>();
        }

        [Test]
        public async Task GetById_ShouldReturnRequestedCard()
        {
            A.CallTo(() => fakeStorage.FindCard(A<string>._, default(CancellationToken)))
                .Returns(cards[0]);

            var result = await controller.GetById(cards[0].Id, default(CancellationToken));

            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult).Value.Should().BeEquivalentTo(cards[0]);
        }

        [Test]
        public async Task DeleteCard_ShouldDeleteCard()
        {
            A.CallTo(() => fakeStorage.FindCard(A<string>._, default(CancellationToken)))
                .Returns(cards[0]);

            var result = await controller.DeleteCard(cards[0].Id, default(CancellationToken));

            result.Should().BeOfType<OkObjectResult>();
            A.CallTo(() => fakeStorage.DeleteCard(cards[0].Id, default(CancellationToken)))
                .MustHaveHappened();
        }
    }
}
