using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.Models.View_Models.WardAdministror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public interface IPatientTreatmentRepository
    {
        Task<bool> AddPatientTreatment(PatientTreatment patientTreatment);

        Task<bool> UpdatePatientTreatment(VMPatientTreatment patientTreatment);

        //Task<bool> DeletePatientTreatment(PatientTreatment  patientTreatment);

        Task<VMPatientTreatment> GetPatientTreatmentById(int patientTreatmentID);
        Task<IEnumerable<VMPatientTreatment>> GetPatientTreatmentByFileId(int FileID);
        Task<IEnumerable<VMPatientTreatment>> GetAllPatientTreatments();

        //Doctor Get Patient Treatments by File ID
        Task<IEnumerable<VMPatientTreatment>> GetPatientTreatmentByIdAsync(int FileID);
        //Doctor Get Patient Vitals by File ID
        Task<IEnumerable<PatientVitalsDetailsViewModel>> GetPatientVitalsByIdAsync(int FileID);

    }
}
