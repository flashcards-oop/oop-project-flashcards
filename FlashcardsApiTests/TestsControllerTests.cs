using NUnit.Framework;
using FlashcardsApi.Controllers;
using FlashcardsApi.Models;
using FakeItEasy;
using Flashcards;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading.Tasks;

namespace FlashcardsApiTests
{
    [TestFixture]
    public class TestsControllerTests
    {
        IAnswersStorage answersStorage;
        IStorage storage;

        [SetUp]
        public void SetUp()
        {
            storage = A.Fake<IStorage>();
            answersStorage = A.Fake<IAnswersStorage>();
        }

        [Test]
        public async Task GenerateTest_ForNonExistentCollection_ShouldReturnNotFound()
        {
            A.CallTo(() => storage.FindCollection(A<string>._)).Returns(null);
            var controller = new TestsController(storage, answersStorage, ControllerTestsHelper.GetStandardAuthorizationService());
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");

            var result = await controller.GenerateTest(new TestDto("0", 0, 0, 0));
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
