using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Dao
{
    public class TuristaDao:Conexion
    {
        public static void registrarTuristas(TuristaEntidad turista)
        {
            string query = "INSERT INTO Turista(idTipoDocumento, DNI, nombre, apellido, fechaNacimiento) VALUES (@tipo, @dni, @nom, @ape, @fecha)";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"tipo", turista.idTipoDocumento);
            cmd.Parameters.AddWithValue(@"dni", turista.DNI);
            cmd.Parameters.AddWithValue(@"nom", turista.nombre);
            cmd.Parameters.AddWithValue(@"ape", turista.apellido);
            cmd.Parameters.AddWithValue(@"fecha", turista.fechaNacimiento);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }

        public static List<TuristaEntidad> consultarTurista()
        {
            List<TuristaEntidad> listaTurista = new List<TuristaEntidad>();
            string query = "SELECT * FROM Turista t JOIN TipoDocumento d ON (t.idTipoDocumento = d.idTipoDocumento)";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                TuristaEntidad turista = new TuristaEntidad();
                turista.DNI = int.Parse(dr["DNI"].ToString());
                turista.nomTipoDoc = dr["nombreDocumento"].ToString();
                turista.nombre = dr["nombre"].ToString();
                turista.apellido = dr["apellido"].ToString();
                turista.fechaNacimiento = DateTime.Parse(dr["fechaNacimiento"].ToString());
                listaTurista.Add(turista);
            }
            dr.Close();
            cmd.Connection.Close();

            return listaTurista;
        }
    }
}
