using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    internal class Alimento
    {
        public int ID_Alimento { get; set; }
        public string NombreAlimento { get; set; }

        // Información nutricional por porción estándar
        public double CaloriasPorPorcion { get; set; }
        public double ProteinasPorPorcion { get; set; }
        public double CarbohidratosPorPorcion { get; set; }
        public double GrasasPorPorcion { get; set; }

        public string UnidadMedidaBase { get; set; } // "g", "ml", "unidad", etc.
        public double? TamañoPorcionEstandarGramos { get; set; } // Para alimentos con porción estándar en gramos (nulleable si no aplica)
        public string TipoAlimento { get; set; } // "Verdura", "Fruta", "Lácteo", etc.

        // Tags para indicar en qué momentos del día es apropiado el alimento
        public List<string> MomentosDiaApropiados { get; set; }

        // Constructor por defecto
        public Alimento()
        {
            MomentosDiaApropiados = new List<string>();
        }

        // Constructor con parámetros para facilitar la creación de instancias
        public Alimento(int id, string nombre, double caloriasPorPorcion, double proteinasPorPorcion, double carbohidratosPorPorcion, double grasasPorPorcion, string unidadMedidaBase, double? tamañoPorcionEstandarGramos = null, string tipoAlimento = null, List<string> momentosDiaApropiados = null)
        {
            ID_Alimento = id;
            NombreAlimento = nombre;
            CaloriasPorPorcion = caloriasPorPorcion;
            ProteinasPorPorcion = proteinasPorPorcion;
            CarbohidratosPorPorcion = carbohidratosPorPorcion;
            GrasasPorPorcion = grasasPorPorcion;
            UnidadMedidaBase = unidadMedidaBase;
            TamañoPorcionEstandarGramos = tamañoPorcionEstandarGramos;
            TipoAlimento = tipoAlimento;
            MomentosDiaApropiados = momentosDiaApropiados ?? new List<string>();
        }



        //Metodo para verificar si es el alimento es adecuado para x tiempo de comida
        public bool EsApropiadoPara(string momentoDia)
        {
            return MomentosDiaApropiados.Contains(momentoDia.ToLower());
        }
    }
}
