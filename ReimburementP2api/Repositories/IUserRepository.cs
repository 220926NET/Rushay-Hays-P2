using ReimburementP2api.Models;

namespace ReimburementP2api.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUserNameAndPass(string username, string password);
        void AddUser(User user);


    }
}
