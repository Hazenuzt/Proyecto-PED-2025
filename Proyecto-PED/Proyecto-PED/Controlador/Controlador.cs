using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_PED.Modelo;

namespace Proyecto_PED.Controlador
{
    public class ControladorLogin
    {
        private ValidacionUsuario validacionuser = new ValidacionUsuario();
        public bool InicioSesion(string usuario, string contra)
        {
            return validacionuser.ValidarUsuario(usuario, contra);
        }
    }
}
