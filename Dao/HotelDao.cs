using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Dao
{
    public class HotelDao:Conexion
    {
        public static List<HotelEntidad> consultarHotel()
        {
            List<HotelEntidad> listaHotel = new List<HotelEntidad>();
            string query = "SELECT * FROM Hotel";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                HotelEntidad hotel = new HotelEntidad();
                hotel.idHotel = int.Parse(dr["idHotel"].ToString());
                hotel.nombre = dr["nombre"].ToString();
                listaHotel.Add(hotel);
            }
            dr.Close();
            cmd.Connection.Close();

            return listaHotel;
        }

        public static HotelEntidad consultarHotelXID(int id)
        {
            HotelEntidad hotel = new HotelEntidad();
            //List<HotelEntidad> listaHotel = new List<HotelEntidad>();
            string query = "SELECT * FROM Hotel where idHotel = @hotel";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"hotel", id);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {                
                hotel.idHotel = int.Parse(dr["idHotel"].ToString());
                hotel.precio = float.Parse(dr["precio"].ToString());                
            }
            dr.Close();
            cmd.Connection.Close();

            return hotel;
        }
    }
}
