using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public class SpecializeRepository : ISpecializeRepository
    {
        private readonly ISqlDataAccess _db;

        public SpecializeRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddSpecializeAsync(Specialize specialize)
        {
            try
            {
                await _db.SaveData("spInsertSpecialize", new { specialize.Description });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<IEnumerable<dynamic>> GetAllSpecializesAsync()
        {
            return await _db.GetData<dynamic, dynamic>("spGetAllSpecializes", new { });
        }

        public async Task<Specialize> GetSpecializeByIdAsync(int SpecializeID)
        {
            var result = await _db.GetData<Specialize, dynamic>("spGetSpecializeById", new { SpecializeID });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateSpecializeAsync(Specialize specialize)
        {
            try
            {
                await _db.SaveData("spUpdateSpecialize", new { specialize.SpecialtyID, specialize.Description });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
