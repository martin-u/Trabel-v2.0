using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TuristaEntidad
    {
        public int idTipoDocumento { get; set; }
        public int DNI { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int telefono { get; set; }
        public string email { get; set; }
        public DateTime fechaNacimiento { get; set; }

    }
}
