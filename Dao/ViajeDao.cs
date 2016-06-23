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
            string query = "INSERT INTO Viaje(nombreViaje, idCiudadOrigen, idCiudadDestino, fechaDesde, fechaHasta,soloIda,precioTotal,idTransporte,idTemporada) VALUES(@nom, @origen, @destino, @fDesde,@fHasta,@ida,@precio,@trans,@temp)";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"nom", viaje.nombreViaje);
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
                viaje.nombreViaje = dr["nombreViaje"].ToString();
                viaje.origen = dr["nombreOrigen"].ToString();
                viaje.destino = dr["nombreDestino"].ToString();
                viaje.fechaDesde = DateTime.Parse(dr["fechaDesde"].ToString());
                viaje.fechaHasta = DateTime.Parse(dr["fechaHasta"].ToString());
                viaje.precioTotal = float.Parse(dr["precioTotal"].ToString());
                viaje.soloIda = bool.Parse(dr["soloIda"].ToString());
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

        public static List<ViajeEntidad> reporteViaje(int transporte, DateTime? fecha, int destino)
        {
            
            List<ViajeEntidad> listaViaje = new List<ViajeEntidad>();
            string query = "SELECT v.idViaje, v.idCiudadDestino, v.idTransporte, v.fechaDesde, c.idCiudadDestino, c.nombreDestino, t.nombreTransporte, t.idTransporte FROM Viaje v JOIN CiudadDestino c ON (v.idCiudadDestino = c.idCiudadDestino) JOIN Transporte t ON t.idTransporte = v.idTransporte WHERE c.idCiudadDestino LIKE @destino AND v.idTransporte LIKE @transporte AND v.fechaDesde LIKE @fecha";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            if (destino > 0)
            {
                cmd.Parameters.AddWithValue("@destino", destino);
            }
            else {
                cmd.Parameters.AddWithValue("@destino", "%");
            }
            if (fecha == null)
            {
                cmd.Parameters.AddWithValue("@fecha", '%');
            }
            else
            {
                cmd.Parameters.AddWithValue("@fecha", fecha);
            }
            if (transporte == 0)
            {
                cmd.Parameters.AddWithValue("@transporte", "%");
            }
            else
            {
                cmd.Parameters.AddWithValue("@transporte", transporte);
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                ViajeEntidad viaje = new ViajeEntidad();
                viaje.idViaje = int.Parse(dr["idViaje"].ToString());
                viaje.transporte = dr["nombreTransporte"].ToString();                
                viaje.fechaDesde = DateTime.Parse(dr["fechaDesde"].ToString());
                viaje.destino = dr["nombreDestino"].ToString();
                listaViaje.Add(viaje);
            }
            dr.Close();
            cmd.Connection.Close();
            return listaViaje;

        }

        public static ViajeEntidad consultarViajeSeleccionado(int id)
        {
            ViajeEntidad viaje = new ViajeEntidad();
            //List<HotelEntidad> listaHotel = new List<HotelEntidad>();
            string query = "SELECT * FROM Viaje where idViaje = @viaje";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"viaje", id);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                viaje.idViaje = int.Parse(dr["idViaje"].ToString());
                viaje.precioTotal = float.Parse(dr["precioTotal"].ToString());
                viaje.soloIda = bool.Parse(dr["soloIda"].ToString());
                viaje.fechaDesde = DateTime.Parse(dr["fechaDesde"].ToString());
                viaje.fechaHasta = DateTime.Parse(dr["fechaHasta"].ToString());
                viaje.idTemporada = int.Parse(dr["idTemporada"].ToString());
            }
            dr.Close();
            cmd.Connection.Close();

            return viaje;
        }

        public static List<ViajeEntidad> consultarListaViajeSeleccionado(int id)
        {
            List<ViajeEntidad> listaViaje = new List<ViajeEntidad>();
            //List<HotelEntidad> listaHotel = new List<HotelEntidad>();
            string query = "SELECT * FROM Viaje where idViaje = @viaje";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"viaje", id);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ViajeEntidad viaje = new ViajeEntidad();
                viaje.idViaje = int.Parse(dr["idViaje"].ToString());
                viaje.precioTotal = float.Parse(dr["precioTotal"].ToString());
                viaje.soloIda = bool.Parse(dr["soloIda"].ToString());
                viaje.fechaDesde = DateTime.Parse(dr["fechaDesde"].ToString());
                viaje.fechaHasta = DateTime.Parse(dr["fechaHasta"].ToString());
            }
            dr.Close();
            cmd.Connection.Close();

            return listaViaje;
        }

    }
}
