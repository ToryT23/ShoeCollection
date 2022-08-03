using ShoeCollection.Models;
using System.Collections.Generic;

namespace ShoeCollection.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void DeleteUser(User user);
        List<User> GetAllUsers();
        User GetByFirebaseUserId(string firebaseUserId);
    }
}