using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flashcards
{
    public interface ITestStorage
    {
        Task AddTest(Test test);
        Task<Test> FindTest(string testId);
        Task DeleteTest(string testId);
    }
}