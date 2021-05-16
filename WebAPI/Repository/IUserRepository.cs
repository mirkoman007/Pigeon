using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User GetUserById(int userId);

        string GetUserPasswordHash(string eMail);

        void Insert(User user);

        void Update(User user);

        void Delete(int userId);

    }
}
