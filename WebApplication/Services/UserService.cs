using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        private IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _httpService.Get<IEnumerable<User>>("/api/users");
        }
    }
}
