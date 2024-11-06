using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public class WardRepository : IWardRepository
    {
        private readonly ISqlDataAccess _db;

        public WardRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddWardAsync(Ward ward)
        {
            try
            {
                await _db.SaveData("spInsertWard", new { ward.Name });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  async Task<bool> DeleteWardAsync(int WardID)
        {
            try
            {
                await _db.SaveData("spDeleteWard", new { WardID });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Ward>> GetAllWardsAsync()
        {
            return await _db.GetData<Ward, dynamic>("spGetAllWards", new { });
        }

        public async Task<Ward> GetWardByIdAsync(int WardID)
        {
            var result = await _db.GetData<Ward, dynamic>("spGetWardById", new { WardID });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateWardAsync(Ward ward)
        {
            try
            {
                await _db.SaveData("spUpdateWard", new { ward.WardID, ward.Name });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
