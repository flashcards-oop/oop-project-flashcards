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
using Flashcards.QuestionGenerators;
using System.Collections.Generic;

namespace FlashcardsApiTests
{
    [TestFixture]
    public class TestsController_Tests
    {
        ITestStorage testStorage;
        IStorage storage;
        TestsController controller;
        ITestBuilderFactory factory;
        IExerciseGenerator[] generators;

        [SetUp]
        public void SetUp()
        {
            storage = A.Fake<IStorage>();
            testStorage = A.Fake<ITestStorage>();
            factory = A.Fake<ITestBuilderFactory>();
            controller = new TestsController(storage, testStorage, factory, new IExerciseGenerator[0]);
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");
        }

        [Test]
        public async Task GenerateTest_ForNonExistentCollection_ShouldReturnNotFound()
        {
            A.CallTo(() => storage.FindCollection(A<string>._, default(CancellationToken)))
                .Returns<Collection>(null);

            var result = await controller.GenerateTest(
                new TestQueryDto("hello", new TestBlockDto[0].ToList()), default(CancellationToken));
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        //[Test]
        //public async Task GenerateTest_ShouldReturnTest()
        //{
        //    A.CallTo(() => storage.FindCollection(A<string>._, default(CancellationToken)))
        //        .Returns(new Collection("coll", "admin", "id"));
        //    var cards = new List<Card>
        //    {
        //        new Card("t1", "d1", "admin", "id"),
        //        new Card("t2", "d2", "admin", "id"),
        //    };
        //    A.CallTo(() => storage.GetCollectionCards("id", default(CancellationToken))).Returns(cards);
        //    var testQuery = new TestQueryDto("id", 
        //        new List<TestBlockDto>
        //        {
        //            new TestBlockDto(generators[0].GetTypeCaption(), 1),
        //            new TestBlockDto(generators[1].GetTypeCaption(), 2),
        //            new TestBlockDto(generators[2].GetTypeCaption(), 3)
        //        });
        //    var exercises = new Exercise[0];
        //    var fakeBuilder = A.Fake<ITestBuilder>();
        //    A.CallTo(() => fakeBuilder.Build()).Returns(exercises);
        //    A.CallTo(() => fakeBuilder.WithGenerator(A<IExerciseGenerator>._, A<int>._)).Returns(fakeBuilder);
        //    A.CallTo(() => factory.GetBuilder(cards, A<ICardsSelector>._)).Returns(fakeBuilder);

        //    var result = await controller.GenerateTest(testQuery, default(CancellationToken));

        //    result.Result.Should().BeOfType<OkObjectResult>();
        //    A.CallTo(() => fakeBuilder.WithGenerator(generators[0], 1)).MustHaveHappened();
        //    A.CallTo(() => fakeBuilder.WithGenerator(generators[1], 2)).MustHaveHappened();
        //    A.CallTo(() => fakeBuilder.WithGenerator(generators[2], 3)).MustHaveHappened();
        //    A.CallTo(() => testStorage.AddTest(
        //        A<Test>.That.Matches(t => t.Exercises == exercises && t.OwnerLogin == "admin"), A<CancellationToken>._))
        //        .MustHaveHappened();
        //}
    }
}
