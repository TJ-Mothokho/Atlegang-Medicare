using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class Doctor : User
    {
        [ForeignKey("Specialty")]
        public Guid SpecialtyID { get; set; }


        public Specialty Specialty { get; set; }
    }
}
