﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Nurse_and_Nursing_Sister
{
    public class PatientVital
    {

        public int PatientVitalsID { get; set; }
        public int FileID { get; set; }
        public DateTime Date { get; set; }
        public int NurseID { get; set; }
    }

}

