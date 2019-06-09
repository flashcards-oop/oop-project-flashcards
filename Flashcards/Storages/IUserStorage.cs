using System.Threading;
using System.Threading.Tasks;

namespace Flashcards
{
    public interface IUserStorage
    {
        Task AddUser(User user, CancellationToken token = default(CancellationToken));
        Task<User> FindUserById(string id, CancellationToken token = default(CancellationToken));
        Task<User> FindUserByLogin(string login, CancellationToken token = default(CancellationToken));
    }
}
