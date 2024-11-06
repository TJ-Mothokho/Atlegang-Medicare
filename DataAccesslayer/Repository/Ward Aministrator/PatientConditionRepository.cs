using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public class PatientConditionRepository : IPatientConditionRepository
    {
        private readonly ISqlDataAccess _db;

        public PatientConditionRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddPatientConditionAsync(PatientCondition patientCondition)
        {

            try
            {
                await _db.SaveData("spInsertPatientCondition", new { patientCondition.PatientID, patientCondition.ConditionID });
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Task<bool> DeletePatientConditionByIdAsync(PatientCondition patientCondition)
        {
            throw new NotImplementedException();
        }

        public  async Task<IEnumerable<PatientCondition>> GetAllPatientConditionsAsync()
        {
            string query = "spGetAllChronicConditions";
            return await _db.GetData<PatientCondition, dynamic>(query, new { });

        }

        public async Task<PatientCondition> GetPatientConditionByIdAsync(int PatientConditionID)
        {
            IEnumerable<PatientCondition> result = await _db.GetData<PatientCondition, dynamic>
              ("spGetAllChronicConditions", new { patientConditionID = PatientConditionID });
            return result.FirstOrDefault();

        }

        public Task<bool> UpdatePatientConditionAsync(PatientCondition patientCondition)
        {
            throw new NotImplementedException();
        }
    }
}
