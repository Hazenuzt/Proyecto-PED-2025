using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clases_PlanEat
{
    public class Receta
    {
        //Propiedades
        public int Id_Receta { get; set; }

        public string NombreReceta { get; set; }

        public string Descripción { get; set; }

        public double CaloriasTotales { get; set; }

        public double TiempoPreparación { get; set; }

        public List<int> IDsIngredientes { get; set; }

        //CONSTRUCTOR VACÍO
        public Receta()
        {
            IDsIngredientes = new List<int>();
        }

        //CONSTRUCTOR CON PARAMETROS
        public Receta(int id_receta, string nombrereceta, List<int> idsIngredientes, double caloriastotales)
        {
            Id_Receta = id_receta;
            NombreReceta = nombrereceta;
            IDsIngredientes= idsIngredientes ?? new List<int>();
            CaloriasTotales = caloriastotales;
        }
        public Receta(int id_receta, string nombrereceta, string descripcion, List<int> idsIngredientes, 
            double caloriastotales, double tiempopreparacion)
        {
            Id_Receta = id_receta;
            NombreReceta = nombrereceta;
            Descripción = descripcion;
            IDsIngredientes = idsIngredientes ?? new List<int>();
            CaloriasTotales = caloriastotales;
            TiempoPreparación = tiempopreparacion;
        }

    }
}
