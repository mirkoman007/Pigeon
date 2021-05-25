using WebAPI.Models;

namespace WebAPI.Repository
{
    interface ICityRepository
    {
        City GetCity(int IDCity);
        City GetCityFromName(string Name);
        void DeleteCity(int IDCity);
        bool AddCity(City city);
    }
}
