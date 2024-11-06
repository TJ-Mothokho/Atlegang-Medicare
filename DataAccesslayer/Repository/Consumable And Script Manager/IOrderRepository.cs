using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public interface IOrderRepository
    {
        Task<IEnumerable<dynamic>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<bool> AddOrderAsync(Order order);
        Task<bool> UpdateAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);

        Task<Order> GetOrderAddedAsync(int managerID);

    }
}
