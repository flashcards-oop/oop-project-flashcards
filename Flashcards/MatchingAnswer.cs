﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Flashcards
{
    public class MatchingAnswer : Answer
    {
        public Dictionary<string, string> Matches;

        public override bool IsTheSameAs(Answer otherAnswer)
        {
            if (!(otherAnswer is MatchingAnswer))
                return false;
            var otherMatchingAnswer = (MatchingAnswer) otherAnswer;

            if (!ContnainsSameDefinitionsAs(otherMatchingAnswer))
                return false;

            throw new NotImplementedException();
        }

        private bool ContnainsSameDefinitionsAs(MatchingAnswer otherMatchingAnswer)
        {
            return Matches.Keys.All(key => otherMatchingAnswer.Matches.ContainsKey(key)) &&
                otherMatchingAnswer.Matches.Keys.All(key => Matches.ContainsKey(key));
        }
    }
}