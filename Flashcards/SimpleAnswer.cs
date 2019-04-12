using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    public class SimpleAnswer : Answer
    {
        public string Answer { get; set; }

        public override bool IsTheSameAs(Answer otherAnswer)
        {
            if (!(otherAnswer is SimpleAnswer))
                return false;
            return Answer == ((SimpleAnswer) otherAnswer).Answer;
        }
    }
}
