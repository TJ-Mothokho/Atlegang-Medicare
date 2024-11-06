using DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public interface IMedicationRepository
    {
        Task<IEnumerable<Medication>> GetAllMedicationsAsync();
        Task<Medication> GetMedicationByIdAsync(int medicationID);
        Task<bool> AddMedicationAsync(Medication medication);
        Task<bool> UpdateMedicationAsync(Medication medication);
        Task<bool> DeleteMedicationAsync(int medicationID);

        Task<IEnumerable<Medication>> GetMedicationsByScheduleAsync(int schedule);
        Task<IEnumerable<Medication>> GetMedicationByNameAsync(string medicationName);

        Task<IEnumerable<Medication>> GetMedicationByUserTypeAsync(string medicationName);

        Task<IEnumerable<Medication>> GetAllMedicationsByNurseAsync();

        Task<IEnumerable<Medication>> GetAllMedicationsbBySisterNurseAsync();



    }
}
