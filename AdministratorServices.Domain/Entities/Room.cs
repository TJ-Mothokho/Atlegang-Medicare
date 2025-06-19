using AdministratorServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class Room : BaseModel
    {
        public int Capacity { get; set; } //Bed capacity
        [ForeignKey("Ward")]
        public Guid WardID { get; set; }

        public Ward Ward { get; set; }
        public ICollection<Bed>? Beds { get; set; }
    }
}
