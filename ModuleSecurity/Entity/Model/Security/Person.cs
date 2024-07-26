using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Security
{
    public class Person
    {
        private int IdPerson { get; set; }
        private string First_name { get; set; }
        private string Last_name { get; set; }
        private string Email { get; set; }
        private string Addres { get; set; }
        private string Type_document { get; set; }
        private int Document { get; set; }
        private DateTime Birth_of_date { get; set; }
        private string CreateAt { get; set; }
        private string UpdateAt { get; set; }
        private string DeleteAt { get; set; }
        private int Phone { get; set; }
        private Boolean State { get; set; }

    }
}
