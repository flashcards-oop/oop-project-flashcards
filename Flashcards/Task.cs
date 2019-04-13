using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    public class Task
    {
        public readonly Answer Answer;
        public readonly Question Question;

        public Task(Answer answer, Question question)
        {
            Answer = answer;
            Question = question;
        }

        public bool Equals(Task self, Task other)
        {
            if (ReferenceEquals(self, other)) return true;
            if (ReferenceEquals(self, null)) return false;
            if (ReferenceEquals(other, null)) return false;
            if (self.GetType() != other.GetType()) return false;
            return Equals(self.Answer, other.Answer) && Equals(self.Question, other.Question);
        }

        public int GetHashCode(Task obj)
        {
            unchecked
            {
                return ((obj.Answer != null ? obj.Answer.GetHashCode() : 0) * 397) ^ (obj.Question != null ? obj.Question.GetHashCode() : 0);
            }
        }
    }
}
