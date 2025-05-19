using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.Entidades
{
    public class Alimento_No_Gustan
    {
        //Atributos
        private int id_preferencias;
        private int id_alimento;

        //Propiedades
        public int Id_Preferencias
        {
            get { return id_preferencias; }
            set
            {
                id_preferencias = value;
            }
        }

        public int Id_Alimento
        {
            get { return id_alimento; }
            set
            {
                id_alimento = value;
            }
        }
    }
}
