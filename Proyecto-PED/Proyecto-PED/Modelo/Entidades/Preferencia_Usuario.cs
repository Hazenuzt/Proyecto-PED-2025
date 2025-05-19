using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.Entidades
{
    public class Preferencia_Usuario
    {
        //Atributos
        private int id_preferencias;
        private int id_usuario;

        //Propiedades
        public int Id_Preferencias
        {
            get { return id_preferencias; }
            set {id_preferencias = value; }
        }

        public int Id_Usuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }

    }
}
