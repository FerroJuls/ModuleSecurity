using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class User
    {
        private int IdUser { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string CreateAt { get; set; }
        private string UpdateAt { get; set; }
        private string DeleteAt { get; set; }
        private Boolean State { get; set; }

        // Relación con Person
        private Person IdPerson { get; set; }
    }
}
