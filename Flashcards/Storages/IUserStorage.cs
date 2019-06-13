using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flashcards.Storages
{
    public interface IUserStorage
    {
        Task AddUser(User user, CancellationToken token = default(CancellationToken));
        Task<User> FindUserById(Guid id, CancellationToken token = default(CancellationToken));
        Task<User> FindUserByLogin(string login, CancellationToken token = default(CancellationToken));
    }
}
