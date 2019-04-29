using System;

namespace Flashcards
{
    public abstract class Question
    {
        public string Id { get; }

        public Question()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}