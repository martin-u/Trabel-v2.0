using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace Dao
{
    public class TipoDocumentoDao:Conexion
    {

        public static List<TipoDocumentoEntidad> consultarTipoDoc()
        {
            List<TipoDocumentoEntidad> listaTipoDoc = new List<TipoDocumentoEntidad>();
            string query = "SELECT * FROM TipoDocumento";
            SqlCommand cmd = new SqlCommand(query, obtenerDB());
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                TipoDocumentoEntidad tipo = new TipoDocumentoEntidad();
                tipo.idTipoDocumento = int.Parse(dr["idTipoDocumento"].ToString());
                tipo.nombre = dr["nombreDocumento"].ToString();
                listaTipoDoc.Add(tipo);
            }
            dr.Close();
            cmd.Connection.Close();

            return listaTipoDoc;
        }
    }
}
