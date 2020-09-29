using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Listing
    {

        public string company { get; set; }
        public string categoria { get; set; }
        public string tipo { get; set; }
        public string posicion { get; set; }
        public string ubicacion { get; set; }
        public string logo { get; set; }
        public DateTime fechaPublicacion { get; set; }
    }
}