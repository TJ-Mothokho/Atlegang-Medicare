using DataAccesslayer.Models.Domains.Administrator;
using DataAccesslayer.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ISqlDataAccess _db;
        public RoomRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddRoomAsync(Room room)
        {
            try
            {
                await _db.SaveData("spInsertRoom", new { room.WardID, room.BedCapacity, room.Status });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRoomAsync(int RoomID)
        {
            try
            {
                await _db.SaveData("spDeleteRoom", new { RoomID });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  async Task<IEnumerable<dynamic>> GetAllRoomsAsync()
        {
            return await _db.GetData<dynamic, dynamic>("spGetAllRooms", new { });
        }

        public  async Task<Room> GetRoomByIdAsync(int RoomID)
        {
            var result = await _db.GetData<Room, dynamic>("spGetRoomById", new { RoomID });
            return result.FirstOrDefault(); 
        }

        public  async Task<bool> UpdateRoomAsync(Room room)
        {
            try
            {
                await _db.SaveData("spUpdateRoom", new { room.RoomID, room.WardID, room.BedCapacity,room.Status });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
