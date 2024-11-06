using Atlegang_Medicare.ViewModels;
using DataAccesslayer.Models.Domains.Doctor;
using DataAccesslayer.Models.Domains.Ward_Administrator;
using DataAccesslayer.Models.View_Models.Doctor;
using DataAccesslayer.SqlDataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Repository.Doctor
{
    public class PatientFileRepository : IPatientFileRepository
    {
        private readonly ISqlDataAccess _db;

        public PatientFileRepository(ISqlDataAccess db)
        {
            _db = db;
        }

       
        //ward_admin and nurse must also consume this repository it is important???

        // Retrieves a list of patients who have been admitted today.
        // This method is crucial for real-time updates on patient admissions.
        public async Task<IEnumerable<VMPatientFile>> GetPatientsAdmittedToday()
        {
            return await _db.GetData<VMPatientFile, dynamic>("spGetPatientsAdmittedToday", new { });
        }

        // Retrieves a patient file by its ID.
        // This method is essential for fetching specific patient details when needed.
        public async Task<VMPatientFile> GetPatientFileById(int FileID)
        {
            var patientFile = await _db.GetData<VMPatientFile, dynamic>("spGetPatientFileByID", new { FileID });
            return patientFile.FirstOrDefault();
        }

        // Retrieves all patient files in the system.
        // Useful for displaying the complete list of patient files to authorized personnel.
        public async Task<IEnumerable<VMPatientFile>> GetPatientFiles()
        {
            return await _db.GetData<VMPatientFile, dynamic>("spGetAllPatientFiles", new { });
        }

        // Retrieves all patient files assigned to a specific doctor.
        // This method must not be removed because it plays a critical role in doctor-specific views.
        public async Task<IEnumerable<VMPatientFile>> GetAllPatientFilesForDoctor(int DoctorID)
        {
            return await _db.GetData<VMPatientFile, dynamic>("spGetAllPatientFilesForDoctor", new { DoctorID });
        }

        // Adds a new patient file to the database.
        // Important for registering new patients admitted to the facility.
        public async Task<bool> AddPatientFilesAsync(PatientFile patientfile)
        {
            try
            {
                await _db.SaveData("spInsertPatientFile", new { patientfile.PatientID, patientfile.BedID, patientfile.DoctorID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Updates an existing patient file's details.
        // Used to modify patient information, such as their status or assigned bed.
        public async Task<bool> UpdatePatientFileAsync(PatientFile patientFile)
        {
            try
            {
                await _db.SaveData("spUpdatePatientFile", new { patientFile.PatientID, patientFile.DischargeDate, patientFile.BedID, patientFile.DoctorID, patientFile.Status });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Deletes a patient file from the database by ID.
        // Ensures that patient records can be removed when necessary.
        public async Task<bool> DeletePatientFileAsync(int id)
        {
            try
            {
                await _db.SaveData("DeletePatientFile", new { id });
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Retrieves a list of patient files related to visits.
        // Used to display or manage patient visits for doctors and nurses.
        public async Task<IEnumerable<VMPatientFile>> GetAllPatientFilesForVisit()
        {
            return await _db.GetData<VMPatientFile, dynamic>("spGetPatientNamesForVisit", new { });
        }

        // Retrieves a list of referred patients.
        // Important for tracking patients who have been referred to other specialists or facilities.
        public async Task<IEnumerable<VMPatient>> GetAllReferredPatientNames()
        {
            return await _db.GetData<VMPatient, dynamic>("spGetReferredPatientNames", new { });
        }

        // Retrieves all patient files that do not have a status.
        // Useful for identifying incomplete or pending patient records.
        public async Task<IEnumerable<VMPatientFile>> GetAllPatientFilewithOutStatusAsync()
        {
            string query = "spGetAllPatientFiles";
            return await _db.GetData<VMPatientFile, dynamic>(query, new { });
        }

        // Retrieves a list of all admitted patient movements.
        // Essential for tracking patient location and status within the facility.
        public async Task<IEnumerable<PatientFile>> GetAlladmittedPatientMovement()
        {
            string query = "spGetadmittedPatientNamesMovement";
            return await _db.GetData<PatientFile, dynamic>(query, new { });
        }

        // Updates a patient file by assigning a new doctor to the patient.
        // Necessary when transferring patient care between doctors.
        public async Task<bool> UpdatePatientFileAsync(VMPatientFile patientFile)
        {
            try
            {
                await _db.SaveData("spUpdateDoctorForPatientFile", new { patientFile.FileID, patientFile.DoctorID });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Updates the status of a patient file (e.g., discharge, transfer).
        // Critical for reflecting changes in the patient's treatment status.
        public async Task<bool> UpdatePatientFileStatusAsync(PatientFile patientfile)
        {
            try
            {
                await _db.SaveData("spUpdatePatientFile", new { patientfile.PatientID, patientfile.DischargeDate, patientfile.BedID, patientfile.Status });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Retrieves a discharge summary for a specific patient file by ID.
        // Used to display details of a patient's discharge process.
        public async Task<VMPatientFile> GetDischargeById(int FileID)
        {
            var patientFile = await _db.GetData<VMPatientFile, dynamic>("spGetPatientFileByID", new { FileID });
            return patientFile.FirstOrDefault();
        }

        // Retrieves all patient files for movement tracking.
        // Important for monitoring patient transfers within the facility.
        public async Task<IEnumerable<VMPatientFile>> GetAllPatientFilesForMovement()
        {
            return await _db.GetData<VMPatientFile, dynamic>("spGetPatientNames", new { });
        }

        // Updates a patient file's discharge status.
        // Ensures that the discharge process is recorded accurately.
        public async Task<bool> UpdateDischargePatientFileStatusAsync(VMPatientFile patientfile)
        {
            try
            {
                await _db.SaveData("spDoctorDischargePatient", new { patientfile.FileID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Retrieves a list of all discharged patients.
        // Useful for generating reports on patient discharges.
        public async Task<IEnumerable<VMPatientFile>> GetDischargeListAsync()
        {
            string query = "spGetAllDischargelist";
            return await _db.GetData<VMPatientFile, dynamic>(query, new { });
        }

        // Updates patient file information upon discharge.
        // Important for finalizing patient records when they leave the facility.
        public async Task<bool> UpdatePatientFileDichargeAsync(VMPatientFile patientfile)
        {
            try
            {
                await _db.SaveData("spUpdatePatientFile", new { patientfile.PatientID, patientfile.BedID, patientfile.FileID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Retrieves a patient file that is ready for discharge by ID.
        // Facilitates the discharge process by providing necessary patient details.
        public async Task<VMPatientFile> GetPatientFileByIdReady(int FileID)
        {
            var patientFile = await _db.GetData<VMPatientFile, dynamic>("spGetPatientFileByIDReady", new { FileID });
            return patientFile.FirstOrDefault();
        }
        
        //for a report
        public async Task<int> GetAdmittedPatientsCount()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalAdmittedandPerDayReport", new { });
            return result.FirstOrDefault();
        }

        public async Task<int> GetDischargedPatientsCount()
        {
            var result = await _db.GetData<int, dynamic>("spGetTotalDischargePerDay", new { }); 
            return result.FirstOrDefault();
        }

        //public async Task<IEnumerable<dynamic>> GetTotalAdmittedandDischargePerDayReportAsync()
        //{
            

        //    string query = "spGetTotalAdmittedandDischargePerDayReport";
        //    return await _db.GetData<dynamic, dynamic>(query, new { });
        //}
    }
}
