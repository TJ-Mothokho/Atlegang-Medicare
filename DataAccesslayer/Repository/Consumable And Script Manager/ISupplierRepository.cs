using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
    }
}
