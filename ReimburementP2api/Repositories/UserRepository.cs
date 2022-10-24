using Microsoft.Data.SqlClient;
using ReimburementP2api.Models;

namespace ReimburementP2api.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }


        public User GetUserByUserNameAndPass(string username, string password)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @$"
                        SELECT Id, [Name], UserName, [Password], Email, IsAdmin
                        FROM Users 
                        WHERE UserName = @username AND [Password] = @password 
                    ";

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User acquiredUser = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Username = reader.GetString(reader.GetOrdinal("UserName")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
                            };

                            return acquiredUser;
                        }
                        else
                        {
                            User notAUser = new User
                            {
                                Id = 0
                            
                            };
                            return notAUser;
                        }
                    }
                }

            }

        }

        public bool AddUser(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                User testUser = GetUserByUserNameAndPass(user.Username, user.Password);
                //if the username isn't taken then testUser.Id will eqaul 0
                if(testUser.Id == 0)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            INSERT INTO Users
                            (Name, UserName, Password, Email, IsAdmin)
                            VALUES(@name, @userName, @pass, @email, 0);
                        ";
                        cmd.Parameters.AddWithValue("@name", user.Name);
                        cmd.Parameters.AddWithValue("@userName", user.Username);
                        cmd.Parameters.AddWithValue("@pass", user.Password);
                        cmd.Parameters.AddWithValue("@email", user.Email);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                else
                {
                    return false;

                }
            }
        }
    }
}
