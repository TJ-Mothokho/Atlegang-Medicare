using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public class BedRepository : IBedRepository
    {
        private readonly ISqlDataAccess _db;

        public BedRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddBedAsync(int WardID)
        {
            try
            {
                await _db.SaveData("spAddBed", new { WardID });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBedAsync(int BedID)
        {
            try
            {
                await _db.SaveData("spDeleteBed", new { BedID });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Bed>> GetAllBedsAsync()
        {
            return await _db.GetData<Bed, dynamic>("spGetAllBed", new { });
        }

        public async Task<Bed> GetBedByIdAsync(int BedID)
        {
            var result = await _db.GetData<Bed, dynamic>("spGetBedById", new { BedID });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateBedAsync(Bed bed)
        {
            try
            {
                await _db.SaveData("spUpdateBed", new { bed.BedID, bed.RoomID, bed.Status });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
