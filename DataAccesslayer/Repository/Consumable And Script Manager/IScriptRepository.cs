using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.View_Models.Nurse;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public interface IScriptRepository
    {

        Task<IEnumerable<dynamic>> GetAllScriptsAsync();
        Task<IEnumerable<Script>> GetAllScriptsByDoctorIdAsync(int doctorID);
        Task<IEnumerable<Script>> GetAllScriptsByManagerIdAsync(int managerID);
        Task<IEnumerable<Script>> GetScriptForFileIdAsync(int fileID);

        Task<Script> GetScriptAdded(int doctorID);


        Task<bool> AddScriptAsync(Script script);
        Task<bool> UpdateScriptAsync(Script script);
    }
}
