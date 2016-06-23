using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Dao
{
    public class ReservaDao:Conexion
    {
      //  [idReserva]
      //,[fechaReserva]
      //,[idEstado]
      //,[fechaIda]
      //,[fechaVuelta]
      //,[precioTotal]
      //,[idViaje]
      //,[idHotel]
      //,[idDetalleReserva]

        public static void registrarReserva(ReservaEntidad reserva, List<TuristaEntidad> ListaTurista)
        {
            //1. Abrir la conexion
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=MN\;Initial Catalog=MTravel;Integrated Security=True";
            cn.Open();
            // Creamos un objeto para la transaccion
            SqlTransaction tran = cn.BeginTransaction();
            try
            {
                //2. Crear el objeto command para ejecutar el insert
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Insert into Reserva (fechaReserva, idEstado, fechaIda, fechaVuelta, precioTotal, idViaje, idHotel) 
                                    VALUES (@fecRes, @estado, @fecIda, @fecVuelta, @precio, @viaje, @hotel);select Scope_Identity() as ID";

                cmd.Parameters.AddWithValue("@fecRes",reserva.fechaReserva);
                cmd.Parameters.AddWithValue("@estado", reserva.idEstado);
                cmd.Parameters.AddWithValue("@fecIda", reserva.fechaIda);
                cmd.Parameters.AddWithValue("@fecVuelta", reserva.fechaVuelta);
                cmd.Parameters.AddWithValue("@precio", reserva.precioTotal);
                cmd.Parameters.AddWithValue("@viaje", reserva.idViaje);
                cmd.Parameters.AddWithValue("@hotel", reserva.idHotel);
                cmd.Transaction = tran;
                
                //agrego el detalle
                reserva.idReserva = Convert.ToInt32(cmd.ExecuteScalar());
                foreach (TuristaEntidad tur in ListaTurista)
                {
                    tur.idReserva = reserva.idReserva;
                    //Realizo el insert de los telefonos
                    SqlCommand cmdTu = new SqlCommand();
                    cmdTu.Connection = cn;
                    cmdTu.CommandText = @"Insert into Turista(idReserva, nombre, DNI, apellido,idTipoDocumento,fechaNacimiento)
                                            VALUES (@reserva, @nom, @dni, @ape, @tipo, @fecha); Select Scope_Identity() as ID";
                    cmdTu.Parameters.AddWithValue("@reserva", tur.idReserva);
                    cmdTu.Parameters.AddWithValue("@nom", tur.nombre);
                    cmdTu.Parameters.AddWithValue("@ape", tur.apellido);
                    cmdTu.Parameters.AddWithValue("@tipo", tur.idTipoDocumento);
                    cmdTu.Parameters.AddWithValue("@fecha", tur.fechaNacimiento);
                    cmdTu.Parameters.AddWithValue("@dni", tur.DNI);
                    cmdTu.Transaction = tran;
                    tur.idTurista = Convert.ToInt32(cmdTu.ExecuteScalar());
                }
                tran.Commit();
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                throw new Exception("Error al guardar reserva. " + ex.Message);
            }
            finally
            {
                //Cerrar siempre la conexion
                cn.Close();
            } 
        }



    }
}
