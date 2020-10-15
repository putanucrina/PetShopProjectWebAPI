using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Categorie { get; set; }
        public string Rasa { get; set; }
        public float Pret { get; set; }
        public int AnPrimire { get; set; }
    }
}