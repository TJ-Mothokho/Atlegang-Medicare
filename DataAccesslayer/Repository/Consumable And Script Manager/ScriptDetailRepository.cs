using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.SqlDataAccess;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public class ScriptDetailRepository : IScriptDetailRepository
    {
        private readonly ISqlDataAccess _data;

        public ScriptDetailRepository(ISqlDataAccess data)
        {
            _data = data;
        }


        public async Task<IEnumerable<dynamic>> GetScriptDetailsByScriptIdAsync(int id)
        {
            return await _data.GetData<dynamic, dynamic>("spGetScriptDetailsByScriptId", new { id });
        }

        public async Task<ScriptDetail> GetScriptDetailByIdAsync(int id)
        {
            var result = await _data.GetData<ScriptDetail, dynamic>("spGetScriptDetailById", new { id });
            return result.FirstOrDefault();
        }

        public async Task<bool> AddScriptDetailAsync(ScriptDetail scriptDetails)
        {
            try
            {
                await _data.SaveData("spAddScriptDetail", new { scriptDetails.MedicationID, scriptDetails.ScriptID, scriptDetails.Dosage });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }




        }


        
    }
}
