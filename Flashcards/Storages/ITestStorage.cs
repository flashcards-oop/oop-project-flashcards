using System.Threading;
using System.Threading.Tasks;

namespace Flashcards
{
    public interface ITestStorage
    {
        Task AddTest(Test test, CancellationToken token = default(CancellationToken));
        Task<Test> FindTest(string testId, CancellationToken token = default(CancellationToken));
        Task DeleteTest(string testId, CancellationToken token = default(CancellationToken));
    }
}