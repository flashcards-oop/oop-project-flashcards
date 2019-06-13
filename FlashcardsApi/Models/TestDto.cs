using System;
using System.Collections.Generic;
using System.Linq;
using Flashcards;

namespace FlashcardsApi.Models
{
    public class TestDto
    {
        public Guid TestId { get; }
        public List<ExerciseDto> Exercises { get; }

        public TestDto(Test test)
        {
            TestId = test.Id;
            Exercises = test.Exercises.Select(e => new ExerciseDto(e.Id, e.Question)).ToList();
        }
    }
}