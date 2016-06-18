using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace Dao
{
    public class CiudadDestinoDao : Conexion
    {
        public static List<CiudadDestinoEntidad> consultarCiudadDestino()
        {
            List<CiudadDestinoEntidad> listaCiudad = new List<CiudadDestinoEntidad>();
            string query = "Select * FROM CiudadOrigen";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                CiudadDestinoEntidad ciudad = new CiudadDestinoEntidad();
                ciudad.idCiudadDestino = int.Parse(dr["idCiudadDestino"].ToString());
                ciudad.nombreDestino = dr["nombreDestino"].ToString();
                ciudad.idPais = int.Parse(dr["idPais"].ToString());
                listaCiudad.Add(ciudad);
            }
            dr.Close();
            cmd.Connection.Close();
            return listaCiudad;
        }


        public static int nombreCiudad(string nombre)
        {
            CiudadDestinoEntidad idCiudad = new CiudadDestinoEntidad();
            int id = 0;
            string query = "SELECT * FROM CiudadDestino WHERE nombreDestino LIKE @nom";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"nom", nombre);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                id = idCiudad.idCiudadDestino = int.Parse(dr["idCiudadDestino"].ToString());
            }
            dr.Close();
            cmd.Connection.Close();
            return id;
        }
    }
}
