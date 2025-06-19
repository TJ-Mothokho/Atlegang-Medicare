using AdministratorServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class Ward : BaseModel
    {
        public string Name { get; set; }

        public ICollection<Admin>? Admins { get; set; }
        public ICollection<Room>? Rooms { get; set; }
    }
}
