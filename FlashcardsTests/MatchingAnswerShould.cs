﻿using System.Collections.Generic;
using Flashcards;
using Flashcards.Answers;
using NUnit.Framework;

namespace FlashcardsTests
{
    [TestFixture]
    public class MatchingAnswerShould
    {
        [Test]
        public void BeEqual_WhenEqualMatches()
        {
            var first = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Solomon is a", "human"},
                {"Programmer is", "god"}
            });

            var second = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Programmer is", "god"},
                {"Solomon is a", "human"},
            });

            Assert.IsTrue(first.IsTheSameAs(second));
        }

        [Test]
        public void NotBeEqual_WhenTermsAreDifferent()
        {
            var first = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Programmer is", "human"},
            });
            var second = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Programmer is", "god"},
            });

            Assert.IsFalse(first.IsTheSameAs(second));
        }


        [Test]
        public void NotBeEqual_WhenFirstHasExtraDefinitions()
        {
            var first = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Solomon is a", "human"},
                {"Programmer is", "god"},
            });
            var second = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Solomon is a", "human"},
            });

            Assert.IsFalse(first.IsTheSameAs(second));
        }


    }
}
