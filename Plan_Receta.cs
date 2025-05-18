using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_PlanEat
{
    public class Plan_Receta
    {
        //Atributos
        private int id_plan;
        private int id_receta;
        private string tiempocomida;
        private string opcion;

        //Propiedades
        public int Id_Plan
        {
            get { return id_plan; }
            set {  id_plan = value; }
        }

        public int Id_Receta
        {
            get { return id_receta; }
            set { id_receta = value; }
        }

        public string Tiempo_Comida
        {
            get { return tiempocomida; }
            set  {tiempocomida = value;}
        }

        public string Opcion
        {
            get { return opcion; }
            set {  opcion = value; }
        }
    }
}
