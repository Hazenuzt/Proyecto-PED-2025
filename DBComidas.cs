using Proyecto_PED.Modelo.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.BD
{
    internal class DBComidas
    {
        private readonly ConexionBD conexion; 

        //Constructor 
        public DBComidas()
        {
            conexion=new ConexionBD(); // Inicializa la conexión 
        }

        public int GuardarPlanComidas(int idUsuario)
        {
            int idPlan;
            using (SqlConnection cn= conexion.ObtenerConexion()) //Obtiene la conexion a la DB
            {
                cn.Open(); 
                SqlCommand insertPlan = new SqlCommand("INSERT INTO Plan_Comidas(Id_Usuario, Fecha_Generacion) OUTPUT INSERTED.Id_Plan VALUES (@Id_Usuario, GETDATE()) ");
                insertPlan.Parameters.AddWithValue("@Id_Usuario", idUsuario);
                idPlan=(int)insertPlan.ExecuteScalar(); //Devolverá el Id de Plan que generó
            }
            return idPlan; // Retorna el Id del plan
        }
        public void GuardarRecetas(int idPlan, List<Receta> recetas, string tiempo)
        {
            using (SqlConnection cn = conexion.ObtenerConexion()) 
            {
                cn.Open();// Abre conexión
                int opcion = 1; 
                foreach (var receta in recetas)
                {
                    SqlCommand insertReceta = new SqlCommand("INSERT INTO Plan_Receta(Id_Plan, Id_Receta, Tiempo_Comida, Opcion) VALUES(@Id_Plan, @Id_Receta, @Tiempo_Comida, @Opcion)");
                    insertReceta.Parameters.AddWithValue("@Id_Plan", idPlan);
                    insertReceta.Parameters.AddWithValue("@Id_Receta", receta.ID_Receta);
                    insertReceta.Parameters.AddWithValue("@Tiempo_Comida", tiempo);
                    insertReceta.Parameters.AddWithValue("@Opcion", opcion);
                    insertReceta.ExecuteNonQuery();
                }
                opcion++;
            }
        }

        public List<Receta> ObtenerRecetas(int idPlan)
        {
            List<Receta> recetas = new List<Receta>(); // Lista para almacenar las recetas obtenidas
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                // Consulta para obtener recetas relacionadas 
                SqlCommand selectReceta = new SqlCommand("SELECT r.Id_Receta, r.Nombre, r.Descripcion, r.Calorias FROM Plan_Receta pr" +
                   "INNER JOIN Receta r ON pr.Id_Receta=r.Id_Receta WHERE pr.Id_Plan=@Id_Plan");
                selectReceta.Parameters.AddWithValue("@Id_Plan", idPlan);

                using (SqlDataReader datareader = selectReceta.ExecuteReader()) // Ejecuta la consulta y va leyendo fila por fila
                {
                    while (datareader.Read())
                    {
                        Receta receta = new Receta()
                        {
                            ID_Receta = datareader.GetInt32(0), // Columna 0
                            NombreReceta = datareader.GetString(1), // Columna 1
                            Descripcion = datareader.GetString(2), // Columna 2
                            CaloriasTotales = datareader.GetInt32(3) // Columna 3
                        };
                        recetas.Add(receta); // Agrega la receta a la lista
                    }
                }
            }
            return recetas; // Retorna la lista de recetas obtenidas

        }
    }
}
