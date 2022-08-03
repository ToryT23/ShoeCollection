using Microsoft.Extensions.Configuration;
using ShoeCollection.Models;
using System.Collections.Generic;
using ShoeCollection.Utils;
using Microsoft.Data.SqlClient;

namespace ShoeCollection.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    select [User].Id as Id, [User].FirstName as FirstName, [User].LastName as LastName,  [User].FirebaseUserId as FireBaseUserId, [User].Email as Email
                        From [User] 
                        Order by [User].LastName  ";
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            users.Add(NewPostFromReader(reader));
                        }
                }
            }
            return users;
        }

        public User GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
  select [User].Id as Id, [User].FirstName as FirstName, [User].LastName as LastName,  [User].FirebaseUserId as FirebaseUserId, [User].Email as Email
                        From [User] 
                        Where FirebaseUserId = @FirebaseUserId
                        Order by [User].LastName  ";

                    DbUtils.AddParameter(cmd, "@FireBaseUserId", firebaseUserId);

                    User user = null;
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Email = DbUtils.GetString(reader, "Email"),
                        };
                    }
                    reader.Close();
                    return user;
                }
            }
        }

        public void AddUser(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
  Insert into [User] (FirstName, LastName, FirebaseUserId, Email)
  OUTPUT INSERTED.ID
  Values (@FirstName, @LastName, @FirebaseUserId, @Email)";
                    DbUtils.AddParameter(cmd, "@FirstName", user.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", user.LastName);
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", user.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@Email", user.Email);

                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeleteUser(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                Delete
                                 From [User]
                                 Where Email = @email";
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        



        private User NewPostFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = DbUtils.GetInt(reader, "Id"),
                FirstName = DbUtils.GetString(reader, "FirstName"),
                LastName = DbUtils.GetString(reader, "LastName"),
                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                Email = DbUtils.GetString(reader, "Email"),
            };
        }

    }

}
