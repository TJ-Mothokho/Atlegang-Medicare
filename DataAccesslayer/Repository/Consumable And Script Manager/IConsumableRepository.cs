using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public interface IConsumableRepository
    {
        Task<IEnumerable<dynamic>> GetAllConsumablesAsync();
        Task<IEnumerable<dynamic>> FilterConsumableByIdAsync(int consumableID);
        Task<Consumable> GetConsumableByIdAsync(int consumableID);
        Task<Consumable> GetConsumableByNameAsync(string consumableName);

        Task<bool> AddConsumableAsync(Consumable consumable);
        Task<bool> DeleteConsumableAsync(int consumableID);
        Task<bool> UpdateConsumableAsync(Consumable consumable);

        Task<int> GetWardsBelowThreshold();
        Task<int> GetOrdersPending();
        Task<int> GetNumberOfConsumables();
        Task<int> GetNumberOfSuppliers();
    }
}
