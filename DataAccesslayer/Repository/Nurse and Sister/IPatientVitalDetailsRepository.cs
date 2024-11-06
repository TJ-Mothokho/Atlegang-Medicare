using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Nurse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public interface IPatientVitalDetailsRepository
    {
        Task<bool> AddPatientVitalDetailsAsync(PatientVitalDetails patientVitalDetails);


        Task<PatientVitalsDetailsViewModel> GetPatientVitalDetailsById(int patientVitalsDetailsID);

        Task<IEnumerable<PatientVitalsDetailsViewModel>> GetPatientVitalDetailsByFileId(int fileID);

        Task<IEnumerable<PatientVitalsDetailsViewModel>> GetAllPatientVitalDetails();

    }
}
