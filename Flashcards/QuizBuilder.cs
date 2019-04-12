using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public class QuizBuilder
    {
        private readonly List<Collection> collections;
        private readonly List<Card> quiz;

        //public QuizBuilder()
        //{
        //    quiz = new List<Card>();
        //}

        public QuizBuilder(List<Collection> collections)
        {
            this.collections = collections;
            quiz = new List<Card>();
        }

        public void BuildQuiz(List<Card> cards)
        {
            quiz.AddRange(cards);
        }

        public List<Card> ChoiseCollection(string name)
        {
            foreach (var collection in collections)
            {
                if (collection.Name == name)
                {
                    BuildQuiz(collection.Cards);
                    break;
                }
            }
            return quiz;
        }

        public void GenerateQuestions(int cardsNumber, Type questionType)
        {
            var cardsCounter = 0;
            var allCards = collections.SelectMany(collection => collection.Cards).ToList();
            foreach (var card in allCards)
            {
                if (cardsNumber == cardsCounter)
                    break;
                if (card.Question.GetType() == questionType)
                {
                    quiz.Add(card);
                    cardsCounter++;
                }
            }

        }

        public List<Card> GetQuiz()
        {
            return quiz;
        }

    }
}