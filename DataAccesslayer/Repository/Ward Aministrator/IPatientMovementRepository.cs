using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.WardAdministror;

namespace DataAccesslayer.Repository.Ward_Aministrator
{
    public interface IPatientMovementRepository
    { 
      Task<IEnumerable<VMPatientMovement>> GetAllMovementAsync();
        //Task<IEnumerable<Movement>> GetAlladmittedPatientMovement();

        Task<VMPatientMovement> GetMovementByIdAsync(int MovementID);
        Task<bool>AddMovementAsync(Movement movement);
        Task<bool>DeleteMovementAsync(Movement movement);
        Task<bool>UpdateMovementAsync(VMPatientMovement movement);
    }
}
