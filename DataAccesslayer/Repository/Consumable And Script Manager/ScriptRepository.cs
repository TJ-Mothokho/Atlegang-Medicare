using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.SqlDataAccess;
using Microsoft.Identity.Client;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly ISqlDataAccess _data;

        public ScriptRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<IEnumerable<dynamic>> GetAllScriptsAsync()
        {
            return await _data.GetData<dynamic, dynamic>("spGetAllScripts", new { });
        }

        public async Task<IEnumerable<Script>> GetAllScriptsByDoctorIdAsync(int doctorID)
        {
            return await _data.GetData<Script, dynamic>("spGetAllScriptsByDoctorId", new { doctorID });
        }

        public async Task<IEnumerable<Script>> GetAllScriptsByManagerIdAsync(int managerID)
        {
            return await _data.GetData<Script, dynamic>("spGetAllScriptsByManagerId", new { managerID });
        }

        public async Task<IEnumerable<Script>> GetScriptForFileIdAsync(int fileID)
        {
            return await _data.GetData<Script, dynamic>("spGetScriptForFileId", new { fileID });
        }

        public async Task<bool> AddScriptAsync(Script script)
        {
            try
            {
                await _data.SaveData<dynamic>("spAddScript", new { script.DoctorID, script.FileID, script.ScriptDate});
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateScriptAsync(Script script)
        {
            try
            {
                await _data.SaveData<dynamic>("spUpdateScript", new { script.ScriptID, script.ManagerID, script.Status });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Script> GetScriptAdded(int doctorID)
        {
            var result = await _data.GetData<Script, dynamic>("spGetScriptAdded", new { doctorID });
            return result.FirstOrDefault();
        }

}}

