using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.BD
{
    public class ConexionBD
    {
        private string servidor, db, cadena; //atributos para conexion de bd
        public ConexionBD()
        {
            servidor = "localhost";
            db = "PlanEatDB";
            cadena = "Data Source=" + servidor + ";Initial Catalog=" + db + "; Integrated Security = true";
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}
