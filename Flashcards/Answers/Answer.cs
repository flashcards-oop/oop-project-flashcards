using System;

namespace Flashcards
{
    public abstract class Answer
    {
        public string Id { get; }

        public Answer()
        {
            Id = Guid.NewGuid().ToString();
        }

        public abstract bool IsTheSameAs(Answer other);
    }
}