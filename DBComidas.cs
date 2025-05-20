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
        public DBComidas()
        {
            conexion=new ConexionBD();
        }

        public int GuardarPlanComidas(int idUsuario)
        {
            int idPlan;
            using (SqlConnection cn= conexion.ObtenerConexion())
            {
                cn.Open();
                SqlCommand insertPlan = new SqlCommand("INSERT INTO Plan_Comidas(Id_Usuario, Fecha_Generacion) OUTPUT INSERTED.Id_Plan VALUES (@Id_Usuario, GETDATE()) ");
                insertPlan.Parameters.AddWithValue("@Id_Usuario", idUsuario);
                idPlan=(int)insertPlan.ExecuteScalar(); //Devolverá el Id de Plan que generó
            }
            return idPlan;
        }
        public void GuardarRecetas(int idPlan, List<Receta> recetas, string tiempo)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                int opcion = 1;
                foreach(var receta in recetas)
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
            List<Receta> recetas = new List<Receta>();
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                SqlCommand selectReceta = new SqlCommand("SELECT r.Id_Receta, r.Nombre, r.Descripcion, r.Calorias FROM Plan_Receta pr" +
                   "INNER JOIN Receta r ON pr.Id_Receta=r.Id_Receta WHERE pr.Id_Plan=@Id_Plan");
                selectReceta.Parameters.AddWithValue("@Id_Plan", idPlan);
                using (SqlDataReader dr = selectReceta.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Receta receta = new Receta()
                        {
                            ID_Receta = dr.GetInt32(0),
                            NombreReceta = dr.GetString(1),
                            Descripcion = dr.GetString(2),
                            CaloriasTotales = dr.GetInt32(3)
                        };
                        recetas.Add(receta);
                    }
                }
            }
            return recetas;

        }
    }
}
