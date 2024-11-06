using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public interface IPatientAllergyRepository
    {
        Task<IEnumerable<PatientAllergy>>GetAllPatientAllergyAsync();
        Task<PatientAllergy> GetPatientAllergyByIDAsync(int patientAllergyID);
        Task<bool>AddPatientAllergyAsync (PatientAllergy patientAllergy);
        Task<bool> DeletePatientAllergyAsync(PatientAllergy patientAllergy);
        Task<bool> UpdatePatientAllergyAsync(PatientAllergy patientAllergy);

        Task<IEnumerable<PatientAllergy>> GetAllAllergiesByPatientIDAsync(int patientID);


    }
}
