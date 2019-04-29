namespace Flashcards.Storage
{
    public interface IUserStorage
    {
        void AddUser(User user);
        User FindUserById(string id);
        User FindUserByLogin(string login);
    }
}
