using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public class TreatmentRepository : ITreatmentRepository
    {

        private readonly ISqlDataAccess _data;

        public TreatmentRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddAsync(Treatment treatment)
        {
            try
            {
                await _data.SaveData("spCreateTreatment", new { treatment.Description });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> DeleteAsync(int treatmentId)
        {
            try
            {
                await _data.SaveData("spDeleteTreatment", new { Id = treatmentId });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<Treatment>> GetAllAsync()
        {
            string query = "spGetAllTreatments";
            return await _data.GetData<Treatment, dynamic>(query, new { });


        }

        public async Task<Treatment> GetByIdAsync(int treatmentId)
        {
            IEnumerable<Treatment> result = await _data.GetData<Treatment, dynamic>("spGetTreatment", new { Id = treatmentId });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Treatment treatment)
        {
            try
            {
                await _data.SaveData("spUpdateTreatment", new { treatment.Description });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
