using NUnit.Framework;
using FlashcardsApi.Controllers;
using FlashcardsApi.Models;
using FakeItEasy;
using Flashcards;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace FlashcardsApiTests
{
    [TestFixture]
    public class CardsControllerTests
    {
        [Test]
        public void GetAll_ShouldReturnAllUsersCards()
        {
            var cards = new[]
                {
                    new Card("t1", "d1", "admin"),
                    new Card("t2", "d2", "admin")
                };

            var fakeStorage = A.Fake<IStorage>();
            A.CallTo(() => fakeStorage.GetAllCards()).Returns(cards);

            var controller = new CardsController(fakeStorage, ControllerTestsHelper.GetStandardAuthorizationService());
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");

            var result = controller.GetAll();

            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult).Value.Should().BeEquivalentTo(cards);
        }

        [Test]
        public void CreateCard_ShouldAddNewCard()
        {
            var cardDto = new CardDto()
            {
                Term = "term",
                Definition = "definition"
            };
            var fakeStorage = A.Fake<IStorage>();
            var controller = new CardsController(fakeStorage, ControllerTestsHelper.GetStandardAuthorizationService());
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");

            var result = controller.CreateCard(cardDto);
            result.Should().BeOfType<CreatedAtRouteResult>();

            A.CallTo(() => fakeStorage.AddCard(A<Card>.That.Matches(
                card => card.Term == cardDto.Term &&
                        card.Definition == cardDto.Definition &&
                        card.OwnerLogin == "admin")))
                .MustHaveHappened();
        }
    }
}
