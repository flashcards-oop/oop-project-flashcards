using System;
using System.Collections.Generic;
using System.Linq;
using Flashcards.Infrastructure;

namespace Flashcards
{
    public class OpenQuestionExerciseGenerator : IExerciseGenerator
    {
        public int RequiredAmountOfCards => 1;

        public string GetTypeCaption()
        {
            return "open";
        }

        public Exercise GenerateExerciseFrom(IList<Card> cards)
        {
            if (cards.Count != RequiredAmountOfCards)
                throw new ArgumentException("Invalid amount of cards");
            
            var exerciseId = GuidGenerator.GenerateGuid();

            var card = cards.First();
            return new Exercise(
                new OpenAnswer(card.Term, exerciseId), 
                new OpenAnswerQuestion(card.Definition, exerciseId));
        }
    }
}
