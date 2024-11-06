using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.Models.View_Models.WardAdministror;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Doctor
{
    public interface IPatientRepository
    {
        //Add Referral
        Task<bool> AddReferralAsync(Patient patient);
        Task<IEnumerable<dynamic>> GetAllReferralsAsync();
        Task<VMPatient> GetReferralByIdAsync(int UserID);
        Task<bool> UpdateReferralAsync(VMPatient patient);
        Task<bool> DeleteReferralAsync(VMPatient patient);
        //End of Referral


        //Retrieve Referral
        Task<IEnumerable<dynamic>> GetAllRemovedReferralsAsync();
        Task<VMPatient> GetRemovedReferralByIdAsync(int UserID);
        Task<bool> RetrieveReferralAsync(VMPatient patient);
        //End of Retrieve Referral


        //Discharged Patients
        Task<VMPatient> GetDischargedPatientByIdAsync(int UserID);
        Task<IEnumerable<dynamic>> GetAllDischargedPatientsAsync();
        
        //End of Discharged Patients


        //Refer a past patient again to appear aas part of referred patients
        Task<bool> ReferPatientAsync(VMPatient patient);
        //End of Refer past patient
        //Get admitted patient by id
        Task<VMPatient>GetPatientByIdAsync(int userID);
         Task<IEnumerable<VMPatient>> GetPatient();



        //Get Patient Chronic Conditions
        Task<IEnumerable<VMPatientCondition>> GetPatientConditionsByIdAsync(int PatientID);

        //Get Patient Allergies
        Task<IEnumerable<VMPatientAllergy>> GetPatientAllergiesByIdAsync(int PatientID);

        //Get Patient Medication 
        Task<IEnumerable<VMPatientMedication>> GetPatientMedicationsByIdAsync(int PatientID);
        //Get Patient Movement
        Task<IEnumerable<Movement>> GetPatientMovementByIdAsync(int PatientID);

    }
}
