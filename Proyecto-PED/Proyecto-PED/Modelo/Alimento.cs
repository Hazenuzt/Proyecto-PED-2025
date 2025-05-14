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
        public double CaloriasPor100g { get; set; }
        public double ProteinasPor100g { get; set; }
        public double CarbohidratosPor100g { get; set; }
        public double GrasasPor100g { get; set; }
        public string UnidadMedidaBase { get; set; } // "g", "ml", "unidad"
        public double? TamañoPorcionEstandarGramos { get; set; } // Nullable porque no todos tienen porción estándar clara
        public string TipoAlimento { get; set; } // "Verdura", "Carne", "Cereal", etc.

        public Alimento() { } // Constructor por defecto

        public Alimento(int id, string nombre, double calorias, double proteinas, double carbohidratos, double grasas, string unidadBase, double? tamañoPorcion = null, string tipo = null)
        {
            ID_Alimento = id;
            NombreAlimento = nombre;
            CaloriasPor100g = calorias;
            ProteinasPor100g = proteinas;
            CarbohidratosPor100g = carbohidratos;
            GrasasPor100g = grasas;
            UnidadMedidaBase = unidadBase;
            TamañoPorcionEstandarGramos = tamañoPorcion;
            TipoAlimento = tipo;
        }
    }
}
