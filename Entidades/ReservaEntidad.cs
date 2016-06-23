using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ReservaEntidad
    {

        public int idReserva { get; set; }
        public DateTime fechaReserva { get; set; }
        public int idEstado { get; set; }
        public DateTime fechaIda { get; set; }
        public DateTime? fechaVuelta { get; set; }
        public float precioTotal { get; set; }
        public int idViaje { get; set; }
        public int idHotel { get; set; }
        //public int idDetalleReserva { get; set; }
    }
}
