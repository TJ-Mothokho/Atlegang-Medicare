using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllStocksAsync();
        Task<Stock> GetStockAsync(int id);
        Task<bool> AddStockAsync(Stock stock);
        Task<bool> UpdateStockAsync(Stock stock);
    }
}
