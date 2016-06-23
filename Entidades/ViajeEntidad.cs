using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ViajeEntidad
    {

        public int? idViaje { get; set; }
        public int idCiudadOrigen { get; set; }
        public int idCiudadDestino { get; set; }
        public DateTime fechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public bool? soloIda { get; set; }
        public float precioTotal { get; set; }
        public int idTransporte { get; set; }
        public int idTemporada { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string transporte { get; set; }
        public string nombreViaje { get; set; }
        
    }
}
