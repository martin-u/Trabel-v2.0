using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Dao
{
    public class Conexion
    {

        protected static SqlConnection obtenerDB()
        {
            string strcn = @"Data Source=MN\;Initial Catalog=MTravel;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(strcn);
            cnn.Open();
            return cnn;
        }
    }
}
