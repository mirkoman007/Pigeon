using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private PigeonContext _context;

        public UserRepository(PigeonContext context)
        {
            _context = context;
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new ApplicationException("Email \"" + user.Email + "\" is already taken");

            byte[] passwordHash = System.Text.Encoding.UTF8.GetBytes(SecurePasswordHasher.Hash(password));

            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null, string passwordOld = null)
        {
            var user = _context.Users.Find(userParam.Iduser);

            if (user == null)
                throw new ApplicationException("User not found");

            // update mail if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
            {
                // throw error if the new mail is already taken
                if (_context.Users.Any(x => x.Email == userParam.Email))
                    throw new ApplicationException("Email " + userParam.Email + " is already taken");

                user.Email = userParam.Email;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            if (userParam.GetType().GetProperty("CityId") != null)
                user.CityId = userParam.CityId;

            if (userParam.GenderId == 1 || userParam.GenderId == 2)
                user.GenderId = userParam.GenderId;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrEmpty(passwordOld))
            {
                if (SecurePasswordHasher.Verify(password, System.Text.Encoding.UTF8.GetString(user.PasswordHash)))
                {
                    byte[] passwordHash = System.Text.Encoding.UTF8.GetBytes(SecurePasswordHasher.Hash(password));
                    user.PasswordHash = passwordHash;
                }
                else
                {
                    throw new ApplicationException("Incorrect old password");
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods


        private static bool VerifyPasswordHash(string password, byte[] storedHash)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            var hash = System.Text.Encoding.Default.GetString(storedHash);
            return SecurePasswordHasher.Verify(password, hash);
        }
    }
}
