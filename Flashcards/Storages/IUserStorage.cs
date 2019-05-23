using System.Threading.Tasks;

namespace Flashcards
{
    public interface IUserStorage
    {
        Task AddUser(User user);
        Task<User> FindUserById(string id);
        Task<User> FindUserByLogin(string login);
    }
}
