using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Nurse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public interface IPatientVitalRepository
    {
        public Task<bool> AddPatientVital(PatientVital patientVital);
        //public Task<bool> UpdatePatientVital(PatientVitalsDetailsViewModel patientVital);

        //Task<bool> DeletePatientVital(int patientVitalId);

        //Task<PatientVitalsDetailsViewModel> GetPatientVitalsById(int patientVitalId);

        //Task<IEnumerable<PatientVitalsDetailsViewModel>>GetPatientVitalsByFileId(int fileID);



        Task<IEnumerable<VMPatientVitals>> GetAllPatientVitals();

        Task<PatientVital> GetPatientVitalAdded(int nurseID);





    }
}
