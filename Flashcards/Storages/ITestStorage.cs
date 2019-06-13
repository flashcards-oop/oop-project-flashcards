using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flashcards.Storages
{
    public interface ITestStorage
    {
        Task AddTest(Test test, CancellationToken token = default(CancellationToken));
        Task<Test> FindTest(Guid testId, CancellationToken token = default(CancellationToken));
        Task DeleteTest(Guid testId, CancellationToken token = default(CancellationToken));
    }
}