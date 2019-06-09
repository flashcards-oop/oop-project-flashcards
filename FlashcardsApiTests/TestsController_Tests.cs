using NUnit.Framework;
using FlashcardsApi.Controllers;
using FlashcardsApi.Models;
using FakeItEasy;
using Flashcards;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace FlashcardsApiTests
{
    [TestFixture]
    public class TestsController_Tests
    {
        ITestStorage testStorage;
        IStorage storage;

        [SetUp]
        public void SetUp()
        {
            storage = A.Fake<IStorage>();
            testStorage = A.Fake<ITestStorage>();
        }

        [Test]
        public async Task GenerateTest_ForNonExistentCollection_ShouldReturnNotFound()
        {
            A.CallTo(() => storage.FindCollection(A<string>._, default(CancellationToken)))
                .Returns(Task.FromResult<Collection>(null));

            var controller = new TestsController(storage, testStorage, new IExerciseGenerator[0]);
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");

            var result = await controller.GenerateTest(
                new TestQueryDto("hello", new TestBlockDto[0].ToList()), default(CancellationToken));
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
