using AdministratorServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class Bed : BaseModel
    {
        [ForeignKey("Room")]
        public Guid RoomID { get; set; }

        public Room Room { get; set; }
    }
}
