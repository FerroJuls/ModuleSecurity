using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class View
    {
        private int IdView { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private string CreateAt { get; set; }
        private string UpdateAt { get; set; }
        private string DeleteAt { get; set; }
        private Boolean State { get; set; }

        // Relación con Module
        private Module IdModule { get; set; }
    }
}
