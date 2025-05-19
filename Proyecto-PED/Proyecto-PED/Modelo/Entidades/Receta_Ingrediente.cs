using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    public class Receta_Ingrediente
    {
        //Atributos
        private int id_receta;
        private int id_alimento;
        private double cantidad;
        private string unidad;

        //Propiedades
        public int Id_Receta
        {
            get { return id_receta; }
            set{ id_receta = value; }
        }

        public int Id_Alimento
        {
            get { return id_alimento; }
            set {id_alimento = value;}
        }

        public double Cantidad
        {
            get { return cantidad; }
            set { cantidad = value;}
        }

        public string Unidad
        {
            get { return unidad; }
            set{ unidad = value; }
        }
    }
}
