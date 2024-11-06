using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesslayer.SqlDataAccess;


namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public class VitalRepository : IVitalRepository
    {
        private readonly ISqlDataAccess _data;

        public VitalRepository(ISqlDataAccess data)
        {
            _data = data;
        }
    
        public async Task<bool> AddVital(Vital vital)
        {
            try
            {
                await _data.SaveData("spCreateVital", new { vital.Description });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> DeleteVital(int vitalId)
        {
            try
            {
                await _data.SaveData("spDeleteVital", new {Id=vitalId });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Vital>> GetAllVitals()
        {
            string query = "spGetAllVitals";
            return await _data.GetData<Vital,dynamic>(query,new{});
        }

        public async Task<Vital> GetVitalById(int vitalId)
        {
            IEnumerable<Vital> result = await _data.GetData<Vital,dynamic>("spGetVital", new { Id = vitalId });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateVital(Vital vital)
        {
            try
            {
                await _data.SaveData("spUpdateVital", new { vital.Description });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
