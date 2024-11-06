
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
    public class PatientVitalRepository:IPatientVitalRepository
    {
        private readonly ISqlDataAccess _Data;

        public PatientVitalRepository(ISqlDataAccess data)
        {
            _Data = data;
        }

        public async Task<bool> AddPatientVital(PatientVital patientVital)
        {
            try
            {
                await _Data.SaveData("spAddPatientVital", new { patientVital.FileID, patientVital.NurseID, patientVital.Date });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public async Task<bool> UpdatePatientVital(PatientVitalsDetailsViewModel patientVital)
        //{
        //    try
        //    {
        //        await _Data.SaveData("spUpdatePatientVitals", new { patientVital.FileID, patientVital.VitalID });
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public async Task<bool> DeletePatientVital(int patientVitalID)
        //{
        //     try
        //    {
        //        await _Data.SaveData("spDeletePatientVital", new { PatientVitaltID =patientVitalID });
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        return false;
        //    }
        //}
        //public async Task<VMPatientVitals> GetPatientVitalsById(int patientVitalID)
        //{

        //    var result = await _Data.GetData<VMPatientVitals, dynamic>("spGetPatientVitalsByID", new { PatientVitalID = patientVitalID });
        //    return result.FirstOrDefault();
        //}

        //Good
        //public async Task<IEnumerable<VMPatientVitals>> GetPatientVitalsByFileId(int fileId)
        //{

        //    return await _Data.GetData<VMPatientVitals, dynamic>("spGetPatientVitalsByFileID", new { FileID = fileId });
        //}
        

        //Good

        public async Task<IEnumerable<VMPatientVitals>> GetAllPatientVitals()
        {

            return await _Data.GetData<VMPatientVitals, dynamic>("spGetAllPatientVitals", new { });


        }

        public async Task<PatientVital> GetPatientVitalAdded(int nurseID)
        {
            var result = await _Data.GetData<PatientVital, dynamic>("spGetPatientVitalAdded", new { nurseID });
            return result.FirstOrDefault();
        }
    }
}
