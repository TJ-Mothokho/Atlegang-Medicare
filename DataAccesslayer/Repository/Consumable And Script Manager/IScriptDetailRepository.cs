using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.View_Models.Nurse;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public interface IScriptDetailRepository
    {
        Task<IEnumerable<dynamic>> GetScriptDetailsByScriptIdAsync(int id);
        Task<ScriptDetail> GetScriptDetailByIdAsync(int id);
        Task<bool> AddScriptDetailAsync(ScriptDetail scriptDetail);
    }
}
