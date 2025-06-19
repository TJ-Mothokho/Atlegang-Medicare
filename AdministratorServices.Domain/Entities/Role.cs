using AdministratorServices.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorServices.Domain.Entities
{
    public class Role : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<User>? Users { get; set; }
    }
}
