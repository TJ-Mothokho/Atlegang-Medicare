using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public class MedicationRepository : IMedicationRepository
    {
        private readonly ISqlDataAccess _data;

        public MedicationRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddMedicationAsync(Medication medication)
        {
            try
            {
                await _data.SaveData("spAddMedication", new { medication.Name, medication.Schedule, medication.ExpiryDate});
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMedicationAsync(int medicationID)
        {
            try
            {
                await _data.SaveData("spDeleteMedication", new { medicationID });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Medication> GetMedicationByIdAsync(int medicationID)
        {
            var result = await _data.GetData<Medication, dynamic>("spGetMedicationById", new { medicationID });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Medication>> GetAllMedicationsAsync()
        {
            return await _data.GetData<Medication, dynamic>("spGetAllMedications", new { });
        }

        public async Task<bool> UpdateMedicationAsync(Medication medication)
        {
            try
            {
                await _data.SaveData("spUpdateMedication", new { medication.MedicationID, medication.Name, medication.Schedule, medication.ExpiryDate });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Medication>> GetMedicationsByScheduleAsync(int schedule)
        {
            return await _data.GetData<Medication, dynamic>("spGetMedicationsBySchedule", new { schedule });
        }

        public async Task<IEnumerable<Medication>> GetMedicationByNameAsync(string medicationName)
        {
            return await _data.GetData<Medication, dynamic>("spGetMedicationByName", new { medicationName });
        }

        public async Task<IEnumerable<Medication>> GetMedicationByUserTypeAsync(string type)
        {
            return await _data.GetData<Medication, dynamic>("GetMedicationsByScheduleType", new { type });
        }

        public async Task<IEnumerable<Medication>> GetAllMedicationsByNurseAsync()
        {
            return await _data.GetData<Medication, dynamic>("spGetMedsAdministeredByNurse", new { });
        }

        public async Task<IEnumerable<Medication>> GetAllMedicationsbBySisterNurseAsync()
        {
            return await _data.GetData<Medication, dynamic>("spGetMedsAdministeredByNursinSister", new { });
        }

    }
}
