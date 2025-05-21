using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.Entidades
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
        public double? TamañoPorcionEstandarGramos { get; set; } // Nulleable si no aplica
        public string TipoAlimento { get; set; } // "Verdura", "Fruta", "Lácteo", etc.

        //Tag para indicar en qué momentos del día es apropiado el alimento
        public List<string> MomentosDiaApropiados { get; set; }
        //Tag para indicar que rol principal tiene el alimento
        public string RolAlimento { get; set; } // "Base", "Proteina", "Vegetales", "GrasasYExtras"

        public Alimento()
        {
            MomentosDiaApropiados = new List<string>();
        }

        public Alimento(int id, string nombre, double caloriasPorPorcion, double proteinasPorPorcion, double carbohidratosPorPorcion, double grasasPorPorcion, string unidadMedidaBase, double? tamañoPorcionEstandarGramos = null, string tipoAlimento = null, List<string> momentosDiaApropiados = null, string rolAlimento = null)
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
            RolAlimento = rolAlimento;
        }

        public bool EsApropiadoPara(string momentoDia)
        {
            return MomentosDiaApropiados.Contains(momentoDia.ToLower());
        }
    }
}

