using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_PED.Modelo.BD
{
    public class ValidacionUsuario
    {
        private ConexionBD conec = new ConexionBD();

        public bool ValidarUsuario(string usuario, string contra)
        {
            using (SqlConnection conn = conec.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    string query = "select count (*) from Usuario where Username = @usuario and Contraseña = @contra";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contra", contra);
                
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
               
            }
        }
    }
}
