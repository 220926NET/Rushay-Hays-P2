using ReimburementP2api.Models;

namespace ReimburementP2api.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }
        public User GetUserByUserNameAndPass(string username, string password)
        {
            User user = new User
            {

            };
            return user;
        }
    }
}
