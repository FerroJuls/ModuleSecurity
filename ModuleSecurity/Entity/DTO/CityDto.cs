using Entity.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Estado { get; set; }
        public int StateId { get; set; }
        public string state { get; set; }

    }
}
