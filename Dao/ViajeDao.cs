using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using Entidades;

namespace Dao
{
    public class ViajeDao : Conexion
    {

        public static void registrarViaje(ViajeEntidad viaje)
        {
            string query = "INSERT INTO Viaje(idCiudadOrigen, idCiudadDestino, fechaDesde, fechaHasta,soloIda,precioTotal,idTransporte,idTemporada) VALUES(@origen, @destino, @fDesde,@fHasta,@ida,@precio,@trans,@temp)";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"origen", viaje.idCiudadOrigen);
            cmd.Parameters.AddWithValue(@"destino", viaje.idCiudadDestino);
            cmd.Parameters.AddWithValue(@"fDesde", viaje.fechaDesde);
            cmd.Parameters.AddWithValue(@"fHasta", viaje.fechaHasta);
            cmd.Parameters.AddWithValue(@"ida", viaje.soloIda);
            cmd.Parameters.AddWithValue(@"precio", viaje.precioTotal);
            cmd.Parameters.AddWithValue(@"trans", viaje.idTransporte);
            cmd.Parameters.AddWithValue(@"temp", viaje.idTemporada);
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public static List<ViajeEntidad> consultarViaje()
        {
            List<ViajeEntidad> listaViaje = new List<ViajeEntidad>();
            string query = "SELECT * FROM Viaje v JOIN CiudadOrigen c ON (v.idCiudadOrigen = c.idCiudadOrigen) JOIN CiudadDestino cd ON (cd.idCiudadDestino = v.idCiudadDestino)";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ViajeEntidad viaje = new ViajeEntidad();
                viaje.idViaje = int.Parse(dr["idViaje"].ToString());
                viaje.origen = dr["nombreOrigen"].ToString();
                viaje.destino = dr["nombreDestino"].ToString();
                viaje.fechaDesde = DateTime.Parse(dr["fechaDesde"].ToString());
                listaViaje.Add(viaje);
            }
            dr.Close();
            cmd.Connection.Close();

            return listaViaje;
        }

        public static ViajeEntidad consultarViajeXID(int id)
        {
            ViajeEntidad viaje = null;
            string query = "SELECT * FROM Viaje v JOIN CiudadOrigen c ON (v.idCiudadOrigen = c.idCiudadOrigen) JOIN CiudadDestino cd ON (cd.idCiudadDestino = v.idCiudadDestino) where idViaje = @id";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"id", id);
            SqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                viaje = new ViajeEntidad();
                viaje.idViaje = int.Parse(dr["idViaje"].ToString());
                viaje.origen = dr["nombreOrigen"].ToString();
                viaje.destino = dr["nombreDestino"].ToString();
                viaje.fechaDesde = DateTime.Parse(dr["fechaDesde"].ToString());
                viaje.fechaHasta = DateTime.Parse(dr["fechahasta"].ToString());
                viaje.soloIda = bool.Parse(dr["soloIda"].ToString());
                viaje.precioTotal = float.Parse(dr["precioTotal"].ToString());
                viaje.idTransporte = int.Parse(dr["idTransporte"].ToString());
                viaje.idTemporada = int.Parse(dr["idTemporada"].ToString());

            }
            dr.Close();
            cmd.Connection.Close();

            return viaje;
        }

        public static void eliminarViaje(int id)
        {
            string query = "DELETE from Viaje where idViaje = @id";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"id", id);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static void actualizarViaje(ViajeEntidad viaje)
        {
            string query = "UPDATE Viaje set idCiudadOrigen = @origen, idCiudadDestino = @destino, fechaDesde = @desde, fechaHasta = @hasta, soloIda = @ida, precioTotal = @precio, idTransporte = @transporte, idTemporada = @temp where idViaje = @id";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"id", viaje.idViaje.Value);
            cmd.Parameters.AddWithValue(@"origen", viaje.idCiudadOrigen);
            cmd.Parameters.AddWithValue(@"destino", viaje.idCiudadDestino);
            cmd.Parameters.AddWithValue(@"desde", viaje.fechaDesde);
            cmd.Parameters.AddWithValue(@"hasta", viaje.fechaHasta);
            cmd.Parameters.AddWithValue(@"ida", viaje.soloIda);
            cmd.Parameters.AddWithValue(@"precio", viaje.precioTotal);
            cmd.Parameters.AddWithValue(@"temp", viaje.idTemporada);
            cmd.Parameters.AddWithValue(@"transporte", viaje.idTransporte);

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static List<ViajeEntidad> reporteViaje(int transporte, bool ida, DateTime fecha, int destino)
        {
            List<ViajeEntidad> listaViaje = new List<ViajeEntidad>();
            string query = "SELECT v.idCiudadDestino, v.soloIda, v.fechaDesde, c.idCiudadDestino, c.nombreDestino FROM Viaje v JOIN CiudadDestino c ON (v.idCiudadDestino = c.idCiudadDestino) WHERE v.idCiudadDestino = -1 OR v.idCiudadDestino = @destino";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"destino", destino);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ViajeEntidad viaje = new ViajeEntidad();
                viaje.idViaje = int.Parse(dr["idViaje"].ToString());
                viaje.idTransporte = int.Parse(dr["idTransporte"].ToString());
                viaje.soloIda = bool.Parse(dr["soloIda"].ToString());
                viaje.fechaDesde = DateTime.Parse(dr["fechaDesde"].ToString());
                viaje.idCiudadDestino = int.Parse(dr["idCiudadDestino"].ToString());
                listaViaje.Add(viaje);
            }
            dr.Close();
            cmd.Connection.Close();
            return listaViaje;

        }

    }
}
