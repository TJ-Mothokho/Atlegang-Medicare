using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.SqlDataAccess;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ISqlDataAccess _data;

        public OrderRepository(ISqlDataAccess data)
        {
            _data = data;
        }
        public async Task<IEnumerable<dynamic>> GetAllOrdersAsync()
        {
            return await _data.GetData<dynamic, dynamic>("spGetAllOrders", new { });
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var result = await _data.GetData<Order, dynamic>("spGetOrderById", new { id });
            return result.FirstOrDefault();
        }

        public async Task<Order> GetOrderAddedAsync(int managerID)
        {
            var result = await _data.GetData<Order, dynamic>("spGetOrderAdded", new { managerID });
            return result.FirstOrDefault();
        }

        public async Task<bool> AddOrderAsync(Order order)
        {
            try
            {
                await _data.SaveData("spAddOrder", new { order.OrderDate, order.ManagerID, order.TotalCost });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            try
            {
                await _data.SaveData("spUpdateOrder", new { order.Status});
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            try
            {
                await _data.SaveData("spDeleteOrder", new { id });
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
