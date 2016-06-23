using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades;

namespace Dao
{
    public class UsuarioDao: Conexion
    {

        public static bool consultarUser(string nombre, string clave)
        {
            bool valor = false;
            string query = "Select * FROM Usuario where usuario = @nombre AND contraseña = @clave";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            cmd.Parameters.AddWithValue(@"nombre", nombre);
            cmd.Parameters.AddWithValue(@"clave", clave);
           int count = Convert.ToInt32(cmd.ExecuteScalar());

           if (count == 0)
           {
               valor = false;
           }
           else
           {
               valor = true;
           }           
            cmd.Connection.Close();
            return valor;
        }
    }
}
