using System.Linq;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class CityRepository : ICityRepository
    {
        private PigeonContext _context;

        public CityRepository(PigeonContext context)
        {
            _context = context;
        }

        public bool AddCity(City city)
        {
            if (!_context.Cities.Any(a => a.Name == city.Name))
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteCity(int IDCity)
        {
            var city = _context.Cities.Find(IDCity);
            if (city != null)
            {
                _context.Cities.Remove(city);
                _context.SaveChanges();
            }
        }

        public City GetCity(int IDCity)
        {
            var city = _context.Cities.Find(IDCity);
            if (city != null)
            {
                return city;
            }
            return null;
        }

        public City GetCityFromName(string Name)
        {
            var city = _context.Cities.Where(c => c.Name == Name).FirstOrDefault();
            if (city != null)
            {
                return city;
            }
            return null;
        }
    }
}
