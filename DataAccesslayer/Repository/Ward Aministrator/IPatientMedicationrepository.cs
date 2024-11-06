using DataAccesslayer.Models.Domains.Ward_Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public interface IPatientMedicationRepository
    {
        Task<IEnumerable<PatientMedication>> GetAllPatientMedicationAsync();
        Task<PatientMedication> GetPatientMedicationByIdAsync(int PatientMedsID);
        Task<bool> AddPatientMedicationAsync(PatientMedication patientMedication);
    }
}
