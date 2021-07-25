using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.Group;

namespace WebApplication.Services
{
    public interface IGroupService
    {
        Task<IList<SearchGroup>> Search(string searchString);

    }
    public class GroupService: IGroupService
    {
        private IHttpService _httpService;

        public GroupService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IList<SearchGroup>> Search(string searchString)
        {
            return await _httpService.Post<IList<SearchGroup>>("/api/Group/search", new { searchString });
        }
    }
}
