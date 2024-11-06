using Dapper;
using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.Models.View_Models.Nurse;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Nurse_and_Sister
{
    public class AdministerMedsRepository : IAdministerMedsRepository
    {
        private readonly ISqlDataAccess _data;

        public AdministerMedsRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddAdministerMeds(MedsAdminister meds)

        { 
            try
            {
                await _data.SaveData("spAddAdministeredMedication", new { meds.NurseID, meds.FileID, meds.MedicationID });
   
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> UpdateMedicationAdministered(VMAdministerMeds meds) 
        {
            try
            {
                await _data.SaveData("spUpdateAdministeredMeds", new { meds.MedsAdministerID, meds.NurseID, meds.MedicationID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //public Async Task<bool> UpdateMedicationAdministered(MedsAdminister meds)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> DeleteMedicationAdministered(int medsAdministerID)
        {
            try
            {
                await _data.SaveData("spDeleteMedsAdministered", new { MedsAdministerID = medsAdministerID });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

       

        public async Task<IEnumerable<VMAdministerMeds>> GetAllMedsAdministeredByNurse()
        {
            string query = "spGetAdministeredMedsListByNurse";
           
            return await _data.GetData<VMAdministerMeds, dynamic>(query, new { });
        }

        public async Task<IEnumerable<VMAdministerMeds>> GetMedsAdministeredByFileId(int FileID)
        {

            return await _data.GetData<VMAdministerMeds, dynamic>("spGetAdministeredMedicationsByID", new { FileID });
        }

        public async Task<VMAdministerMeds> GetMedsAdministeredById(int medsAdministerID)
        {

            var result = await _data.GetData<VMAdministerMeds, dynamic>("spGetMedsAdministeredById", new { medsAdministerID});
            return result.FirstOrDefault();
        }

        //public async Task<VMAdministerMeds>GetMedsAdministeredByRole(string Type)
        //{
        //    var result = await _data.GetData<VMAdministerMeds, dynamic>("spGetMedsAdministeredByRole", new {Type, DbType.String });
        //    return result.FirstOrDefault();
  
            
        //}

        public async Task<int> GetMedsAdministerCountAsync(int fileId, int medicationID)
        {
            var result = await _data.GetData<int, dynamic>("spGetMedsAdministerCount", new { fileId, medicationID });
            return result.FirstOrDefault();
        }



    }
    
}
