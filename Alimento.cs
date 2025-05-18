using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_PlanEat
{
    public class Alimento
    {      
        //PROPIEDADES
        public int Id_Alimento{ get; set; }
        public string NombreAlimento {get; set;}

        //Información nutricional por porción estandar
        public string TipoAlimento { get; set; } // "Verdura", "Fruta", "Lácteo", etc.
        public string RolAlimento { get; set;} //"Base", "Proteina", "Vegetales", "GrasasYExtras"
        public double CaloriasPorPorcion { get; set;} 
        public double ProteinasPorPorcion { get; set;}
        public double CarbohidratosPorPorcion { get; set;}
        public double GrasasPorPorcion {  get; set;}
        public string UnidadMedidaBase {  get; set;} //"g", "ml", "unidad", etc.
        public double? TamañoPorcionEstandarGramos { get; set; } // Nullable si no aplica
        public List<string> MomentosDiaApropiados { get; set; } // "Desayuno", "Almuerzo, "Cena"


        //CONSTRUCTOR VACÍO
        public Alimento()
        {
            MomentosDiaApropiados = new List<string>();
        }

        // CONSTRUCTOR CON PARÁMETROS
        public Alimento(int id_alimento, string nombrealimento, string tipoalimento, string rolalimento, double caloriasporporcion, 
            double proteinasporporcion, double carbohidratosporporcion, double grasasporporcion, string unidadmedidabase, double? tamañoporciongramos, List<string> momentosdia)
        {
            Id_Alimento = id_alimento;
            NombreAlimento = nombrealimento;
            TipoAlimento = tipoalimento;
            RolAlimento = rolalimento;
            CaloriasPorPorcion = caloriasporporcion;
            ProteinasPorPorcion = proteinasporporcion;
            CarbohidratosPorPorcion = carbohidratosporporcion;
            GrasasPorPorcion = grasasporporcion;
            UnidadMedidaBase = unidadmedidabase;
            TamañoPorcionEstandarGramos = tamañoporciongramos;
            MomentosDiaApropiados = momentosdia ?? new List<string>();
        }
    }
}
