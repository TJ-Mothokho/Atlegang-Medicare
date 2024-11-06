using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.SqlDataAccess;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ISqlDataAccess _data;

        public SupplierRepository(ISqlDataAccess data)
        {
            _data = data;
        }
        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _data.GetData<Supplier, dynamic>("spGetAllSuppliers", new { });
        }
    }
}
