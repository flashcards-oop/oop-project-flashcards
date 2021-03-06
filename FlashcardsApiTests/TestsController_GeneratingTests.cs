﻿using System;
using NUnit.Framework;
using FlashcardsApi.Controllers;
using FlashcardsApi.Models;
using FakeItEasy;
using Flashcards;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Threading.Tasks;
using System.Threading;
using Flashcards.TestProcessing;
using System.Collections.Generic;
using Flashcards.Storages;
using Flashcards.TestProcessing.QuestionGenerators;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace FlashcardsApiTests
{
    [TestFixture]
    public class TestsController_GeneratingTests
    {
        private readonly ITestStorage fakeTestStorage;
        private readonly IStorage fakeStorage;
        private readonly TestsController controller;
        private readonly ITestBuilderFactory factory;
        private readonly IExerciseGenerator[] generators;
        private readonly ITestBuilder fakeBuilder;

        private readonly List<Card> cards;
        private readonly Collection collection;
        private readonly TestQueryDto testQuery;
        private readonly IEnumerable<Exercise> exercises;

        private readonly Guid id = Guid.NewGuid();

        public TestsController_GeneratingTests()
        {
            generators = new IExerciseGenerator[] {
                new OpenQuestionExerciseGenerator(),
                new MatchingQuestionExerciseGenerator(),
                new ChoiceQuestionExerciseGenerator()
                };

            exercises = new Exercise[0];
            cards = new List<Card>
            {
                new Card("t1", "d1", "admin", id),
                new Card("t2", "d2", "admin", id)
            };
            collection = new Collection("coll", "admin", id);
            testQuery = new TestQueryDto(id,
                new List<TestBlockDto>
                {
                    new TestBlockDto(generators[0].GetTypeCaption(), 1),
                    new TestBlockDto(generators[1].GetTypeCaption(), 2),
                    new TestBlockDto(generators[2].GetTypeCaption(), 3)
                }, null);

            fakeStorage = A.Fake<IStorage>();
            fakeTestStorage = A.Fake<ITestStorage>();
            factory = A.Fake<ITestBuilderFactory>();
            fakeBuilder = A.Fake<ITestBuilder>();

            A.CallTo(() => fakeStorage.FindCollection(A<Guid>._, A<CancellationToken>._)).Returns(collection);
            A.CallTo(() => fakeStorage.GetCollectionCards(id, A<CancellationToken>._)).Returns(cards);
            A.CallTo(() => fakeBuilder.Build()).Returns(exercises);
            A.CallTo(() => fakeBuilder.WithGenerator(A<IExerciseGenerator>._, A<int>._)).Returns(fakeBuilder);
            A.CallTo(() => factory.GetBuilder(cards, A<ICardsSelector>._)).Returns(fakeBuilder);

            controller = new TestsController(fakeStorage, fakeTestStorage, factory, generators, null);
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");
        }

        [Test]
        public async Task GenerateTest_ShouldReturnTest()
        {
            var result = await controller.GenerateTest(testQuery, default(CancellationToken));

            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult).Value.Should().BeOfType<TestDto>();
        }

        [Test]
        public async Task GenerateTest_ShouldCallBuilderProperly()
        {
            await controller.GenerateTest(testQuery, default(CancellationToken));

            A.CallTo(() => fakeBuilder.WithGenerator(generators[0], 1)).MustHaveHappened();
            A.CallTo(() => fakeBuilder.WithGenerator(generators[1], 2)).MustHaveHappened();
            A.CallTo(() => fakeBuilder.WithGenerator(generators[2], 3)).MustHaveHappened();
        }

        [Test]
        public async Task GenerateTest_ShouldAddExercisesToStorage()
        {
            await controller.GenerateTest(testQuery, default(CancellationToken));

            A.CallTo(() => fakeTestStorage.AddTest(
                A<Test>.That.Matches(t => t.OwnerLogin == "admin"), A<CancellationToken>._))
                .MustHaveHappened();
        }
    }
}
