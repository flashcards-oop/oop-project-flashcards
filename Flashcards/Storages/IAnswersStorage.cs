using System.Collections.Generic;

namespace Flashcards
{
    public interface IAnswersStorage
    {
        string AddAnswers(IEnumerable<Exercise> exercises);
        IEnumerable<Answer> FindAnswers(string testId);
        void DeleteTest(string testId);
    }
}