using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.Models.View_Models.WardAdministror;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public class PatientTreatmentRepository : IPatientTreatmentRepository
    {
        private readonly ISqlDataAccess _Data;

        public PatientTreatmentRepository (ISqlDataAccess data)
        {
            _Data = data;
        }


        public async Task<bool> AddPatientTreatment(PatientTreatment patientTreatment)
        {
            try
            {
                await _Data.SaveData("spAddPatientTreatment", new { patientTreatment.FileID, patientTreatment.TreatmentID});
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdatePatientTreatment(VMPatientTreatment patientTreatment)
        {
            try
            {
                await _Data.SaveData("spUpdatePatientTreatment", new { patientTreatment.PatientTreatmentID, patientTreatment.TreatmentID});
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public async Task<bool> DeletePatientTreatment(int patientTreatmentID)
        //{
        //    try
        //    {
        //        await _Data.SaveData("spDeletePatientTreatment", new { PatientTreatnentID = patientTreatmentID });
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        return false;
        //    }
        //}
        public async Task<VMPatientTreatment> GetPatientTreatmentById(int patientTreatmentID)
        {

            var result = await _Data.GetData<VMPatientTreatment, dynamic>("spGetPatientTreatmentsByID", new { patientTreatmentID });
            return result.FirstOrDefault();
        }

      
        public async Task<IEnumerable<VMPatientTreatment>> GetAllPatientTreatments()
        {
            return await _Data.GetData< VMPatientTreatment, dynamic>("spGetAllPatientTreatments", new { });
        }

        public async Task<IEnumerable<VMPatientTreatment>> GetPatientTreatmentByFileId(int FileID)
        {
            return await _Data.GetData<VMPatientTreatment, dynamic>("spGetPatientTreatmentsByFileID", new { FileID });
        }

        //Doctor Get Patient Treatments by File ID
        public async Task<IEnumerable<VMPatientTreatment>> GetPatientTreatmentByIdAsync(int FileID)
        {
            var result = await _Data.GetData<VMPatientTreatment, dynamic>("spGetPatientTreatmentByFileID", new { FileID });
            return result;
        }

        public async Task<IEnumerable<PatientVitalsDetailsViewModel>> GetPatientVitalsByIdAsync(int FileID)
        {
            var result = await _Data.GetData<PatientVitalsDetailsViewModel, dynamic>("spGetPatientVitalByFileID", new { FileID });
            return result;
        }
    }
}

