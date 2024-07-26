using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class RoleView
    {
        private int IdRoleView { get; set; }
        private string CreateAt { get; set; }
        private string UpdateAt { get; set; }
        private string DeleteAt { get; set; }
        private Boolean State { get; set; }

        // Relación con Role y View
        private Role IdRole { get; set; }
        private View IdView { get; set; }
    }
}
