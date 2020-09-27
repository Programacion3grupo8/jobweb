using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jobweb.Models
{
    public class Usuario
    {
        public int id { get; set; }
        [Display(Name = "Username")]
        public string username { get; set; }
        public string password { get; set; }
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
        
    }
}
