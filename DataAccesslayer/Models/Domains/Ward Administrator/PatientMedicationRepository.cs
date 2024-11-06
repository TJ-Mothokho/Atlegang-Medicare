using DataAccesslayer.SqlDataAccess;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Repository.Ward_Aministrator;

namespace DataAccesslayer.Repository.Ward_Administrator
{
    public class PatientMedicationRepository : IPatientMedicationRepository
    {
        private readonly ISqlDataAccess _data;

        public PatientMedicationRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddPatientMedicationAsync(PatientMedication patientMedication)
        {
            try
            {
                await _data.SaveData("spInsertPatientMedication", new { patientMedication.PatientID, patientMedication.MedicationID });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<PatientMedication>> GetAllPatientMedicationAsync()
        {
            return await _data.GetData<PatientMedication, dynamic>("spGetAllPatientMedication", new { });
        }

        public async Task<PatientMedication> GetPatientMedicationByIdAsync(int PatientMedsID)
        {
            var result = await _data.GetData<PatientMedication, dynamic>("spGetPatientMedicationByID", new { PatientMedsID });
            return result.FirstOrDefault();
        }
    }

   
}
