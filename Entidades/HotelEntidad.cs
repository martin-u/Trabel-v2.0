using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class HotelEntidad
    {
        public int idHotel { get; set; }
        public string nombre { get; set; }
        public int telefono { get; set; }
        public int cuit { get; set; }
        public int idTemporada { get; set; }
        public float precio { get; set; }
        public int idCategoria { get; set; }
        public int idCiudad { get; set; }

    }
}
