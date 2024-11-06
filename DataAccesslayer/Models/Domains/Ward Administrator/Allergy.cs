using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Models.Domains.Ward_Administrator
{
    public class Allergy
    {
        /*[AllergyID]
      ,[Description]
      ,[AllergyType]*/
        public int AllergyID { get; set; }
        public string Description { get; set; }
    }
}
