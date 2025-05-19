using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.Entidades
{
    internal class Receta
    {
        public int ID_Receta { get; set; }
        public string NombreReceta { get; set; }
        public List<int> IDsIngredientes { get; set; }
        public double CaloriasTotales { get; set; }

        //Faltan atributos, como los que pusimos en el diagrama de clases
        //como no eran relevantes para el codigo, no los agregé de momento
        //podes agregarlos pero, con un constructor distinto para que no afecte al funcionamiento de las demás clases

        public Receta()
        {
            IDsIngredientes = new List<int>();
        }

        // Constructor actualizado para incluir CaloriasTotales
        public Receta(int id, string nombre, List<int> idsIngredientes, double caloriasTotales)
        {
            ID_Receta = id;
            NombreReceta = nombre;
            IDsIngredientes = idsIngredientes ?? new List<int>();
            CaloriasTotales = caloriasTotales;
        }
    }
}
