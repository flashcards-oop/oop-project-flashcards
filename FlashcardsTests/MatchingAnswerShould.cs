using System.Collections.Generic;
using Flashcards;
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
            }, "1");

            var second = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Programmer is", "god"},
                {"Solomon is a", "human"},
            }, "1");

            Assert.IsTrue(first.IsTheSameAs(second));
        }

        [Test]
        public void NotBeEqual_WhenTermsAreDifferent()
        {
            var first = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Programmer is", "human"},
            }, "1");
            var second = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Programmer is", "god"},
            }, "1");

            Assert.IsFalse(first.IsTheSameAs(second));
        }


        [Test]
        public void NotBeEqual_WhenFirstHasExtraDefinitions()
        {
            var first = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Solomon is a", "human"},
                {"Programmer is", "god"},
            }, "1");
            var second = new MatchingAnswer(new Dictionary<string, string>
            {
                {"Solomon is a", "human"},
            }, "1");

            Assert.IsFalse(first.IsTheSameAs(second));
        }


    }
}
