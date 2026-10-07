using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LescanoErickT2.Models
{
    public class Alumno
    {

        public int dni { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string carrera { get; set; }
        public string ciclo { get; set; }


        public Alumno() { }
        public Alumno(int id, string name, string lasname, string profession, string ciclo) { }
    }
}