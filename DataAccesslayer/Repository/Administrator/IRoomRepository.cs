using DataAccesslayer.Models.Domains.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Administrator
{
    public interface IRoomRepository
    {
        Task<bool> AddRoomAsync(Room room);
        Task<bool> UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(int RoomID);
        Task<Room> GetRoomByIdAsync(int RoomID);
        Task<IEnumerable<dynamic>> GetAllRoomsAsync();
    }
}
