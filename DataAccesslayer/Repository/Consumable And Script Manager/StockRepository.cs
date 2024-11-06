using DataAccesslayer.SqlDataAccess;
using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public class StockRepository : IStockRepository
    {
        private readonly ISqlDataAccess _data;

        public StockRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddStockAsync(Stock stock)
        {
            try
            {
                await _data.SaveData<dynamic>("spAddStock", new { stock.WardID, stock.ConsumableID, stock.Quantity });
                return true;
            }
            catch
            {
                return false;
            }
            //spAddStock
        }

        public async Task<IEnumerable<Stock>> GetAllStocksAsync()
        {
            return await _data.GetData<Stock, dynamic>("spGetAllStock", new { });
        }

        public async Task<Stock> GetStockAsync(int id)
        {
            var Stock = await _data.GetData<Stock, dynamic>("spGetStockById", new { id });
            return Stock.FirstOrDefault();
        }

        public async Task<bool> UpdateStockAsync(Stock stock)
        {
            try
            {
                await _data.SaveData<dynamic>("spUpdateStock", new { stock.WardStockID, stock.WardID, stock.ConsumableID, stock.Quantity});
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
