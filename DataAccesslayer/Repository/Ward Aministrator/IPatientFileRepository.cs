using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Doctor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Doctor
{
    public interface IPatientFileRepository
    {
        //nurse use the following methods to get patient Files
        // Ward Administrator Add more methods that you need
        Task<IEnumerable<VMPatientFile>> GetPatientsAdmittedToday();
        Task<IEnumerable<VMPatientFile>> GetPatientFiles();
        Task<IEnumerable<VMPatientFile>> GetAllPatientFilesForDoctor(int DoctorID);
        Task<IEnumerable<VMPatientFile>> GetAllPatientFilesForVisit();
        Task<IEnumerable<VMPatientFile>> GetAllPatientFilesForMovement();
        Task<IEnumerable<VMPatient>> GetAllReferredPatientNames();
        Task<IEnumerable<PatientFile>> GetAlladmittedPatientMovement();
        Task<VMPatientFile> GetPatientFileById(int FileID);
        Task<VMPatientFile> GetPatientFileByIdReady(int FileID);
        Task<VMPatientFile> GetDischargeById(int FileID);
        Task<bool> UpdateDischargePatientFileStatusAsync(VMPatientFile patientfile);
        Task<bool> DeletePatientFileAsync(int id);
        Task<bool> UpdatePatientFileAsync(VMPatientFile patientFile);
        Task<bool> AddPatientFilesAsync(PatientFile patientfile);
        Task<bool>UpdatePatientFileStatusAsync(PatientFile patientfile);
        Task<bool> UpdatePatientFileDichargeAsync(VMPatientFile patientfile);
        Task<IEnumerable<VMPatientFile>> GetAllPatientFilewithOutStatusAsync();
        Task<IEnumerable<VMPatientFile>> GetDischargeListAsync();

        //report
        //ge admitted patient per day
        //Task<int> GetAdmittedPatientsCount();
        //get discharged patient per day
        //Task<int> GetDischargedPatientsCount();
        // get dischatged and admitted patiebt each day
        //Task<IEnumerable<dynamic> >GetTotalAdmittedandDischargePerDayReportAsync();
        //pass the method to controller
        //Task<PatientFile> GetPatientFileReportAsync();
    }
}
