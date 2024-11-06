using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public class PatientVitalDetailsRepository : IPatientVitalDetailsRepository
    {
        private readonly ISqlDataAccess _data;
        public PatientVitalDetailsRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddPatientVitalDetailsAsync(PatientVitalDetails patientVitalDetails)
        {
            try
            {
                await _data.SaveData("spAddPatientVitalDetails", new { patientVitalDetails.VitalID, patientVitalDetails.VitalValue, patientVitalDetails.PatientVitalsID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<PatientVitalsDetailsViewModel>> GetAllPatientVitalDetails()
        {
            return await _data.GetData<PatientVitalsDetailsViewModel, dynamic>("spGetAllPatientVitalsDetails", new { });
        }

        public async Task<IEnumerable<PatientVitalsDetailsViewModel>> GetPatientVitalDetailsByFileId(int fileID)
        {
            return await _data.GetData<PatientVitalsDetailsViewModel, dynamic>("spGetPatientVitalsDetailsByFileID", new { fileID });
            
        }

        public async Task<PatientVitalsDetailsViewModel> GetPatientVitalDetailsById(int patientsVitalsDetailsID)
        {
           var result = await _data.GetData<PatientVitalsDetailsViewModel, dynamic>("spGetPatientVitalsDetailsByID", new {PatientVitalsDetailsID = patientsVitalsDetailsID });

            return result.FirstOrDefault();
        }
    }
}
