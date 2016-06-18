using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Dao
{
    public class CiudadOrigenDao : Conexion
    {
        public static List<CiudadOrigenEntidad> consultarCiudadOrigen()
        {
            List<CiudadOrigenEntidad> listaCiudad = new List<CiudadOrigenEntidad>();
            string query = "Select * FROM CiudadOrigen";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                CiudadOrigenEntidad ciudad = new CiudadOrigenEntidad();
                ciudad.idCiudadOrigen = int.Parse(dr["idCiudadOrigen"].ToString());
                ciudad.nombreOrigen = dr["nombreOrigen"].ToString();
                ciudad.idPais = int.Parse(dr["idPais"].ToString());
                listaCiudad.Add(ciudad);
            }
            dr.Close();
            cmd.Connection.Close();
            return listaCiudad;
        }

        public static int nombreCiudad(string nombre)
        {
            CiudadOrigenEntidad idCiudad = new CiudadOrigenEntidad();
            int id = 0;
            string query = "SELECT * FROM CiudadOrigen WHERE nombreOrigen LIKE @nom";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"nom", nombre);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                id = idCiudad.idCiudadOrigen = int.Parse(dr["idCiudadOrigen"].ToString());
            }
            dr.Close();
            cmd.Connection.Close();
            return id;
        }
    }
}
