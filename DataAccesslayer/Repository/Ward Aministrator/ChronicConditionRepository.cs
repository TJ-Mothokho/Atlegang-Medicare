using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public class ChronicConditionRepository :IChronicChonditionRepository
    {
        private readonly ISqlDataAccess _data;

        public ChronicConditionRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddChronicAsync(ChronicCondition chronicCondition)
        {
            try
            {
                await _data.SaveData("spInsertCondition", new {chronicCondition.ConditionID, chronicCondition.ChronicType });
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Task<bool> DeleteChronicAsync(ChronicCondition chronicCondition)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChronicCondition>> GetAllChronicAsync()
        {
            string query = "spGetAllChronicCondition";
            return await _data.GetData<ChronicCondition, dynamic>(query, new { });
        }

        public async Task<ChronicCondition> GetAllChronicByIdAsync(int ConditionID)
        {
            IEnumerable<ChronicCondition> result = await _data.GetData<ChronicCondition, dynamic>
               ("spGetAllChronicConditionByID", new { ConditionID = ConditionID });
            return result.FirstOrDefault();

        }



        public Task<bool> UpdateChronicAsync(ChronicCondition chronicCondition)
        {
            throw new NotImplementedException();
        }
    }
}
