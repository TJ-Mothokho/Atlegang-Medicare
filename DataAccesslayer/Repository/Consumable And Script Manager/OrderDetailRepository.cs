using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ISqlDataAccess _data;

        public OrderDetailRepository(ISqlDataAccess data)
        {
            _data = data;
        }

        public async Task<bool> AddOrderDetailAsync(OrderDetail orderDetail)
        {
            try
            {
                if(orderDetail.Message != null)
                {
                    await _data.SaveData("spAddOrderDetails", new { orderDetail.ConsumableID, orderDetail.Quantity, orderDetail.OrderID, orderDetail.Message });
                    return true;
                }
                else
                {
                    orderDetail.Message = " ";
                    await _data.SaveData("spAddOrderDetails", new { orderDetail.ConsumableID, orderDetail.Quantity, orderDetail.OrderID, orderDetail.Message });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<dynamic>> GetOrderDetailsByOrderID(int orderID)
        {
            var result = await _data.GetData<dynamic, dynamic>("spGetOrderDetailsByOrderID", new { orderID });
            return result;
        }
    }
}
