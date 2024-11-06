using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public class SuburbRepository : ISuburbRepository
    {
        private readonly ISqlDataAccess _db;

        public SuburbRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddSuburbAsync(Suburb suburb)
        {
            try
            {
                await _db.SaveData("spInsertSuburb", new { suburb.SuburbName, suburb.CityID, suburb.PostalCode});
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Suburb>> GetAllSuburbsAsync()
        {
            return await _db.GetData<Suburb, dynamic>("spGetAllSuburbs", new { });
        }

        public async Task<Suburb> GetSuburbByIdAsync(int SuburbID)
        {
            var result = await _db.GetData<Suburb, dynamic>("spGetSuburbById", new { SuburbID });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateSuburbAsync(Suburb suburb)
        {
            try
            {
                await _db.SaveData("spUpdateSuburb", new { suburb.SuburbID, suburb.SuburbName, suburb.CityID, suburb.PostalCode });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
