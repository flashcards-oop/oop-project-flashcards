using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flashcards
{
    public interface IAnswersStorage
    {
        Task<string> AddAnswers(IEnumerable<Exercise> exercises);
        Task<IEnumerable<Answer>> FindAnswers(string testId);
        Task DeleteTest(string testId);
    }
}