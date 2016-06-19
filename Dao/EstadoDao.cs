using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;


namespace Dao
{
    public class EstadoDao: Conexion
    {
        public static List<EstadoEntidad> consultarEstado()
        {
            List<EstadoEntidad> listaEstado = new List<EstadoEntidad>();
            string query = "SELECT * FROM Estado";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                EstadoEntidad estado = new EstadoEntidad();
                estado.idEstado = int.Parse(dr["idEstado"].ToString());
                estado.nombre = dr["nombre"].ToString();
                listaEstado.Add(estado);
            }
            dr.Close();
            cmd.Connection.Close();

            return listaEstado;
        }
    }
}
