using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private PigeonContext context;

        public UserRepository(PigeonContext _context)
        {
            this.context = _context;
        }

        public string GetUserPasswordHash(string eMail)
        {
            var userId = GetUserIDFromEmail(eMail);
            if (userId != 0)
            {
                return Encoding.UTF8.GetString(context.Users.Find(userId).PasswordHash);
            }
            else
            {
                return "";
            }
        }

        public int GetUserIDFromEmail(string eMail)
        {
            foreach (var user in context.Users)
            {
                if (user.Email.Equals(eMail))
                {
                    return user.Iduser;
                }
            }
            return 0;
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return context.Users.Find(userId);
        }

        public void Insert(User user)
        {
            context.Users.Add(user);
        }

        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int userId)
        {
            User user = context.Users.Find(userId);
            context.Users.Remove(user);
        }
    }
}
