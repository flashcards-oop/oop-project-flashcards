namespace Flashcards
{
    public class Exercise
    {
        public readonly Answer Answer;
        public readonly Question Question;

        public Exercise(Answer answer, Question question)
        {
            Answer = answer;
            Question = question;
        }

        protected bool Equals(Exercise other)
        {
            return Equals(Answer, other.Answer) && Equals(Question, other.Question);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Exercise) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Answer != null ? Answer.GetHashCode() : 0) * 397) ^ (Question != null ? Question.GetHashCode() : 0);
            }
        }
    }
}
