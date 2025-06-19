using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class Admin : User
    {
        public bool isSuperUser { get; set; }
        [ForeignKey("Ward")]
        public Guid? WardID { get; set; }


        public Ward? Ward { get; set; }
    }
}
