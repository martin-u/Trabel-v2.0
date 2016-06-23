using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Dao
{
    public class TransporteDao : Conexion
    {
        public static List<TransporteEntidad> consultarTransporte()
        {
            List<TransporteEntidad> TransporteEntidad = new List<TransporteEntidad>();
            string query = "Select * FROM Transporte";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                TransporteEntidad transporte = new TransporteEntidad();
                transporte.idTransporte = int.Parse(dr["idTransporte"].ToString());
                transporte.nombreTransporte = dr["nombreTransporte"].ToString();
                transporte.empresa = dr["empresa"].ToString();
                TransporteEntidad.Add(transporte);
            }
            dr.Close();
            cmd.Connection.Close();
            return TransporteEntidad;
        }
    }
}
