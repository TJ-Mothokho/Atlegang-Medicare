using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public class CityRepository : ICityRepository
    {
        private readonly ISqlDataAccess _db;

        public CityRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddCityAsync(City city)
        {
            try
            {
                await _db.SaveData("spInsertCity", new { city.CityName });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCityAsync(int CityID)
        {
            try
            {
                await _db.SaveData("spDeleteCity", new { CityID });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _db.GetData<City, dynamic>("spGetAllCities", new { });
        }

        public async Task<City> GetCityByIdAsync(int CityID)
        {
            var result = await _db.GetData<City, dynamic>("spGetCityById", new { CityID });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateCityAsync(City city)
        {
            try
            {
                await _db.SaveData("spUpdateCity", new { city.CityID, city.CityName });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
