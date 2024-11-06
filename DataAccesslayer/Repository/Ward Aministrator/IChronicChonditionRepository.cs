using DataAccesslayer.Models.Domains.Ward_Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public interface IChronicChonditionRepository
    {
        Task<IEnumerable<   ChronicCondition>> GetAllChronicAsync();
        Task<ChronicCondition>GetAllChronicByIdAsync(int ConditionID);
        Task <bool>AddChronicAsync(ChronicCondition chronicCondition);
        Task<bool> DeleteChronicAsync(ChronicCondition chronicCondition);
        Task<bool> UpdateChronicAsync(ChronicCondition chronicCondition);
    }
}
