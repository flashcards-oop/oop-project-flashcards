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
    public class TestsController_CheckingTests
    {
        ITestStorage fakeTestStorage;
        IStorage fakeStorage;
        TestsController controller;

        OpenAnswer answer = new OpenAnswer("London");
        OpenAnswerQuestion question = new OpenAnswerQuestion("Capital of GB");
        Exercise[] exercises;
        Test test;
        TestAnswersDto answersDto;
        
        public TestsController_CheckingTests()
        {
            answer = new OpenAnswer("London");
            question = new OpenAnswerQuestion("Capital of GB");
            exercises = new Exercise[]
            {
                new Exercise("1", answer, question, new List<string>{"c1", "c2" })
            };
            test = new Test(exercises, "admin", "id");
            answersDto = new TestAnswersDto()
            {
                Answers = 
            };

            fakeStorage = A.Fake<IStorage>();
            fakeTestStorage = A.Fake<ITestStorage>();
            A.CallTo(() => fakeTestStorage.FindTest("id", A<CancellationToken>._))
                .Returns(test);

            controller = new TestsController(fakeStorage, fakeTestStorage, null, null);
            ControllerTestsHelper.AttachUserToControllerContext(controller, "admin");
        }

        [Test]
        public async Task CheckAnswers_Should()
        {
            controller.CheckAnswers()
        }
    }
}
