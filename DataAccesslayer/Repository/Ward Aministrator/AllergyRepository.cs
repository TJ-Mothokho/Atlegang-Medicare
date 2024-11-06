using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesslayer.Repository.Ward_Aministrator;
using DataAccesslayer.Repository.Ward_Aministrator;

namespace DataAccesslayer.Repository.Ward_Aministrator
{

    public class AllergyRepository : IAllergyRepository
    {
        private readonly ISqlDataAccess _data;
        public AllergyRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddAllergyAsync(Allergy allergy)
        {
            try
            {
                await _data.SaveData("spInsertAllergy", new
                {
                    allergy.Description
                    
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Allergy>> GetAllAllergyAsync()
        {
            string query = "spGetAllAllergy";
            return await _data.GetData<Allergy, dynamic>(query, new { });
        }

        public async Task<Allergy?> GetALLAllergybyIDAsync(int allergyID)
        {
            IEnumerable<Allergy> result = await _data.GetData<Allergy, dynamic>
               ("spGetAllAllergyByID", new {allergyiId =allergyID});
              return result.FirstOrDefault();
        
        }


       
    }
}
