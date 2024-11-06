using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public class PatientAllergyRepository : IPatientAllergyRepository
    {
        private readonly ISqlDataAccess _db;

        public PatientAllergyRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddPatientAllergyAsync(PatientAllergy patientAllergy)
        {
            try
            {
                await _db.SaveData("spInsertPatientAllegy", new { patientAllergy.PatientID, patientAllergy.AllergyID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePatientAllergyAsync(PatientAllergy patientAllergy)
        {
            try
            {
                await _db.SaveData("spDeletePatientAllergy", new {patientAllergy.PatientAllergyID});
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<PatientAllergy>> GetAllAllergiesByPatientIDAsync(int patientID)
        {
            string query = "spGetAllAllergiesByPatientID";
            return await _db.GetData<PatientAllergy, dynamic>(query, new { patientID });
        }

        public async Task<IEnumerable<PatientAllergy>> GetAllPatientAllergyAsync()
        {
            string query = "spGetAllPatientAllergy";
            return await _db.GetData<PatientAllergy, dynamic>(query, new { });
        }

        
        public async Task<PatientAllergy> GetPatientAllergyByIDAsync(int patientAllergyID)
        {
            IEnumerable<PatientAllergy>result = await _db.GetData<PatientAllergy,dynamic>
                ("spGetAllPatientAllergy", new {patientAllergyID = patientAllergyID});
            return result.FirstOrDefault();
         
        }

        public Task<bool> UpdatePatientAllergyAsync(PatientAllergy patientAllergy)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePatientAllergyByIDAsync(PatientAllergy patientAllergy)
        {
            throw new NotImplementedException();
        }
    }
}
