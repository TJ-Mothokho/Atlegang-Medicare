using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.View_Models.Nurse
{
    public class VMAdministerMeds
    {
        //SELECT MA.MedsAdministerID,(Us.FirstName + ' '+ Us.LastName)AS NurseName, (U.FirstName +' '+ U.LastName) AS PatientName, M.[Name] As MedicationName, M.Schedule, MA.[Date]

        public int MedsAdministerID { get; set; }
        public int FileID { get; set; }

        public string PatientName { get; set; }

        public int NurseID { get; set; }

        public string NurseName { get; set; }

        public int MedicationID { get; set; }

        public string MedicationName { get; set; }

        public int Schedule { get; set; }


        public DateTime Date { get; set; }

        public int TotalMedicationsAdministered { get; set; }

        public string LastAdministeredMedication { get; set; }

        public DateTime LastAdministeredMedicationDate { get;  set; }




    }
}
