using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Dao
{
    public class TemporadaDao : Conexion
    {
        public static List<TemporadaEntidad> consultarTemporada()
        {
            List<TemporadaEntidad> listaTemporada = new List<TemporadaEntidad>();
            string query = "Select * FROM Temporada";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                TemporadaEntidad temporada = new TemporadaEntidad();
                temporada.idTemporada = int.Parse(dr["idTemporada"].ToString());
                temporada.nombre = dr["nombre"].ToString();
                listaTemporada.Add(temporada);
            }
            dr.Close();
            cmd.Connection.Close();
            return listaTemporada;
        }
    }
}
