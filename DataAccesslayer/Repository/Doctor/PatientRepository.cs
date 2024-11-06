using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Models.View_Models.WardAdministror;
using DataAccesslayer.Repository.Ward_Aministrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Doctor
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ISqlDataAccess _data;

        public PatientRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        //Add Referral
        public async Task<bool> AddReferralAsync(Patient patient)
        {
            try
            {
                await _data.SaveData("spAddReferral", new { patient.LastName, patient.FirstName, patient.IDNumber, patient.Gender, patient.Email, patient.PhoneNumbers, patient.RoleID, patient.DateOfBirth, patient.ResidentialAddress, patient.SuburbID, patient.Status });
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<dynamic>> GetAllReferralsAsync()
        {
            return await _data.GetData<dynamic, dynamic>("spGetAllReferrals", new { });
        }
        public async Task<VMPatient> GetReferralByIdAsync(int UserID)
        {
            var result = await _data.GetData<VMPatient, dynamic>("spGetReferralById", new { UserID });
            return result.FirstOrDefault();
        }
        public async Task<bool> UpdateReferralAsync(VMPatient patient)
        {
            try
            {
                await _data.SaveData("spUpdateReferral", new { patient.UserID, patient.LastName, patient.FirstName, patient.Gender, patient.PhoneNumbers, patient.ResidentialAddress, patient.SuburbID });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteReferralAsync(VMPatient patient)
        {
            try
            {
                await _data.SaveData("spDeleteReferral", new { patient.UserID });
                return true;
            }
            catch
            {
                return false;
            }
        }
        //End of Referral


        //Retrieve Referral
        public async Task<bool> RetrieveReferralAsync(VMPatient patient)
        {
            try
            {
                await _data.SaveData("spRetrieveReferral", new { patient.UserID });
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<VMPatient> GetRemovedReferralByIdAsync(int UserID)
        {
            var result = await _data.GetData<VMPatient, dynamic>("spGetRemovedReferralById", new { UserID });
            return result.FirstOrDefault();
        }
        public async Task<IEnumerable<dynamic>> GetAllRemovedReferralsAsync()
        {
            return await _data.GetData<dynamic, dynamic>("spGetAllRemovedReferrals", new { });
        }
        //End of Retrieve Referral



        //Discharged Patients 
        public async Task<IEnumerable<dynamic>> GetAllDischargedPatientsAsync()
        {
            return await _data.GetData<dynamic, dynamic>("spGetAllDischargedPatients", new { });
        }
        public async Task<VMPatient> GetDischargedPatientByIdAsync(int UserID)
        {
            var result = await _data.GetData<VMPatient, dynamic>("spGetDischargedPatientById", new { UserID });
            return result.FirstOrDefault();
        }
        //End of Discharged Patients


        //Refer a past patient again to appear aas part of referred patients
        public async Task<bool> ReferPatientAsync(VMPatient patient)
        {
            try
            {
                await _data.SaveData("spReferPatientAgain", new { patient.UserID });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<VMPatient> GetPatientByIdAsync(int userID)
        {
            var patientFile = await _data.GetData<VMPatient, dynamic>("spGetAllPatientByID", new { userID });
            return patientFile.FirstOrDefault();
        }

        public async Task<IEnumerable<VMPatient>> GetPatient()
        {
            return await _data.GetData<VMPatient, dynamic>("spGetAllPatient", new {PatientID =0, UserID =0});

        }

        public async Task<IEnumerable<VMPatientCondition>> GetPatientConditionsByIdAsync(int PatientID)
        {
            var result = await _data.GetData<VMPatientCondition, dynamic>("spGetPatientConditioByPatientID", new { PatientID });
            return result;
        }

        public async Task<IEnumerable<VMPatientAllergy>> GetPatientAllergiesByIdAsync(int PatientID)
        {
            var result = await _data.GetData<VMPatientAllergy, dynamic>("spGetPatientAllergyByPatientID", new { PatientID });
            return result;
        }

        public  async Task<IEnumerable<VMPatientMedication>> GetPatientMedicationsByIdAsync(int PatientID)
        {
            var result = await _data.GetData<VMPatientMedication, dynamic>("spGetPatientMedicationByPatientID", new { PatientID });
            return result;
        }

        public async Task<IEnumerable<Movement>> GetPatientMovementByIdAsync(int FileID)
        {
            var result = await _data.GetData<Movement, dynamic>("spGetPatientMovementByFileID", new { FileID });
            return result;
        }
        //End of Refer past patient
    }
}
