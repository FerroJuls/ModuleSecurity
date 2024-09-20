using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool state { get; set; }

        // Relación con Countries
        public int CountriesId { get; set; }
        public string Countries { get; set; }

    }
}
