using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.SqlDataAccess;

namespace DataAccesslayer.Repository.Consumable_And_Script_Manager
{
    public class ConsumableRepository : IConsumableRepository
    {
        private readonly ISqlDataAccess _data;

        public ConsumableRepository(ISqlDataAccess data)
        {
            _data = data;
        }
        public async Task<IEnumerable<dynamic>> GetAllConsumablesAsync()
        {
            return await _data.GetData<dynamic, dynamic>("spGetAllConsumables", new { });
        }

        public async Task<IEnumerable<dynamic>> FilterConsumableByIdAsync(int consumableID)
        {
            return await _data.GetData<dynamic, dynamic>("spFilterConsumableById", new { consumableID });
        }
        public async Task<Consumable> GetConsumableByIdAsync(int consumableID)
        {
            var result = await _data.GetData<Consumable, dynamic>("spGetConsumableById", new { consumableID });
            return result.FirstOrDefault();
        }

        public async Task<Consumable> GetConsumableByNameAsync(string consumableName)
        {
            var result = await _data.GetData<Consumable, dynamic>("spGetConsumableByName", new { consumableName });
            return result.FirstOrDefault(); ;
        }

        public async Task<bool> AddConsumableAsync(Consumable consumable)
        {
            try
            {
                await _data.SaveData("spAddConsumable", new { ConsumableName = consumable.Name, Quantity = consumable.QuantityOnHand, OnHandThreshold = consumable.OnHandThreshold, Cost = consumable.Cost, SupplierID = consumable.SupplierID, WardThreshold = consumable.WardThreshold});
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateConsumableAsync(Consumable consumable)
        {
            try
            {
                await _data.SaveData("spUpdateConsumable", new { ConsumableID = consumable.ConsumableID, ConsumableName = consumable.Name, Quantity = consumable.QuantityOnHand, OnHandThreshold = consumable.OnHandThreshold, Cost = consumable.Cost, SupplierID = consumable.SupplierID, WardThreshold = consumable.WardThreshold });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteConsumableAsync(int consumableID)
        {
            try
            {
                await _data.SaveData("spDeleteConsumable", new { consumableID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> GetWardsBelowThreshold()
        {
            var result = await _data.GetData<int, dynamic>("spGetWardsBelowThreshold", new {});
            return result.FirstOrDefault();
        }

        public async Task<int> GetOrdersPending()
        {
            var result = await _data.GetData<int, dynamic>("spGetOrdersPending", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetNumberOfConsumables()
        {
            var result = await _data.GetData<int, dynamic>("spGetNumberOfConsumables", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetNumberOfSuppliers()
        {
            var result = await _data.GetData<int, dynamic>("spGetNumberOfSuppliers", new { });
            return result.FirstOrDefault();
        }
    }
}
