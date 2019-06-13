using System;
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
using Flashcards.TestProcessing;
using Flashcards.Storages;

namespace FlashcardsApiTests
{
    [TestFixture]
    public class TestsController_Tests
    {
        private ITestStorage testStorage;
        private IStorage storage;
        private TestsController controller;
        private ITestBuilderFactory factory;
        
        private readonly Guid id = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            storage = A.Fake<IStorage>();
            testStorage = A.Fake<ITestStorage>();
            factory = A.Fake<ITestBuilderFactory>();
            controller = new TestsController(storage, testStorage, factory, new IExerciseGenerator[0], null);
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");
        }

        [Test]
        public async Task GenerateTest_ForNonExistentCollection_ShouldReturnNotFound()
        {
            A.CallTo(() => storage.FindCollection(A<Guid>._, default(CancellationToken)))
                .Returns<Collection>(null);

            var result = await controller.GenerateTest(
                new TestQueryDto(id, new TestBlockDto[0].ToList(), null), default(CancellationToken));
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
