using DataAccesslayer.Models.Domains.Ward_Administrator;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Doctor
{
    public class VMPatientFile
    {
        /*PF.FileID, (U.LastName + ' ' + U.FirstName) AS PatientName, FORMAT (PF.ArrivalDate, 'dd, MMMM, yyyy') AS ArrivalDate, Pf.BedID,R.RoomID,
	W.Name AS WardName, (UD.LastName + ' ' + UD.FirstName) AS Doctor*/

        public int FileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string  IDNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        public string PatientName { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public int BedID { get; set; }
        public int RoomID { get; set; }
        public string WardName { get; set; }
        public string PatientMovement { get; set; }
        public string Doctor { get; set; }
        public int DoctorID { get; set; }
        public string Status { get; set; }
        public int PatientID { get; set; }
        public IEnumerable<SelectListItem> Allergies { get; set; }  // To populate the checkbox list
        public IEnumerable<SelectListItem> ChronicConditions { get; set; }  // To populate the checkbox list
        public IEnumerable<PatientCondition> Medication { get; set; }




        //Nurse
        public string InitialVitals {  get; set; }


        
    }
}
