using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.WardAdministror;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public class PatientMovementRepository : IPatientMovementRepository
    {
        private readonly ISqlDataAccess _data;
        public PatientMovementRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddMovementAsync(Movement movement)
        {
            try
            {
                await _data.SaveData("spInsertMovement", new { movement.FileID, movement.Description, movement.Status});
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        

        public async Task<bool> DeleteMovementAsync(Movement movement)
        {
            try
            {
                await _data.SaveData("spDeleteMovement", new { movement.MovementID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       

        public async Task<IEnumerable<VMPatientMovement>> GetAllMovementAsync()
        {
            string query = "spGetAllMovement";
            return await _data.GetData<VMPatientMovement, dynamic>(query, new { });
        }

        public async Task<VMPatientMovement> GetMovementByIdAsync(int MovementID)
        {
            var result = await _data.GetData<VMPatientMovement, dynamic>("spGetAllMovementbyID", new { MovementID });
            return result.FirstOrDefault();
        }

       

        public async Task<bool> UpdateMovementAsync(VMPatientMovement movement)
        {
            try
            {
                await _data.SaveData("spUpdateMovement", new { movement.MovementID, movement.Description, movement.Date, movement.Status});
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        //public async Task<IEnumerable<Movement>> GetAlladmittedPatientMovement()
        //{
        //    string query = "spGetadmittedPatientNamesMovement";
        //    return await _data.GetData<Movement, dynamic>(query, new { });
        //}
    }
}
