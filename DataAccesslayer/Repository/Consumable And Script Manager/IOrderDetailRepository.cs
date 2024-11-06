using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public interface IOrderDetailRepository
    {
        Task<bool> AddOrderDetailAsync(OrderDetail orderDetail);
        Task<IEnumerable<dynamic>> GetOrderDetailsByOrderID(int orderID);
    }
}
