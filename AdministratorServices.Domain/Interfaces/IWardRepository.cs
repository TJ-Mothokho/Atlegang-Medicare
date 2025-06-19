using AdministratorServices.Domain.Entities;

namespace AdministratorServices.Domain.Interfaces
{
    public interface IWardRepository
    {
        Task<IEnumerable<Ward>> GetAllWardsAsync();
        Task<Ward> GetWardByIdAsync(Guid id);
        Task<bool> AddWardAsync(Ward ward);
        Task<bool> UpdateWardAsync(Ward ward);
        Task<bool> DeleteWardAsync(Guid id);


        Task<IEnumerable<Bed>> GetAllBedsAsync();
        Task<Bed> GetBedByIdAsync(Guid id);
        Task<bool> AddBedAsync(Bed bed);
        Task<bool> UpdateBedAsync(Bed bed);
        Task<bool> DeleteBedAsync(Guid id);

        Task<IEnumerable<Room>> GetAllRoomsByAsync();
        Task<Room> GetRoomById(Guid id);
        Task<bool> AddRoomAsync(Room room);
        Task<bool> UpdateRoomAsync(Room room);
        Task<bool> DeleteRoomAsync(Guid id);
    }
}
