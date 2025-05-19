using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    public class Plan_Comidas
    {
        //Atributos
        private int id_plan;
        private int id_usuario;
        private DateTime fechageneracion;

        //Propiedades
        public int Id_Plan
        {
            get { return id_plan; }
            set{ id_plan = value; }
        }

        public int Id_Usuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }

        public DateTime Fecha_Generacion
        {
            get { return fechageneracion; }
            set  { fechageneracion = value;}
        }
    }
}
