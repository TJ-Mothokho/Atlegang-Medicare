using DataAccesslayer.Models.Domains.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public interface ICityRepository
    {
        Task<bool> AddCityAsync(City city);
        Task<bool> DeleteCityAsync(int CityID);
        Task<bool> UpdateCityAsync(City city);
        Task<City> GetCityByIdAsync(int CityID);
        Task<IEnumerable<City>> GetAllCitiesAsync();
    }
}
