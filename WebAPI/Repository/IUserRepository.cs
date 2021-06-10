using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null, string passwordOld = null);
        void Delete(int id);

    }
}
