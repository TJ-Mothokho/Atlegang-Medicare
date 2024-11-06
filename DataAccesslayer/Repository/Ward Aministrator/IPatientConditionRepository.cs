using DataAccesslayer.Models.Domains.Ward_Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public interface IPatientConditionRepository
    {

        Task<IEnumerable<PatientCondition>> GetAllPatientConditionsAsync();
        Task<PatientCondition> GetPatientConditionByIdAsync(int PatientConditionID);
        Task<bool> AddPatientConditionAsync(PatientCondition patientCondition);
        Task<bool> UpdatePatientConditionAsync(PatientCondition patientCondition);
        Task<bool> DeletePatientConditionByIdAsync(PatientCondition patientCondition);
    }
}
