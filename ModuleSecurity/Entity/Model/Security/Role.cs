using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class Role
    {
        private int IdRole { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private string CreateAt { get; set; }
        private string UpdateAt { get; set; }
        private string DeleteAt { get; set; }
        private Boolean State { get; set; }

    }
}
