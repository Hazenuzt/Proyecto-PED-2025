using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_PED.Modelo.BD;
using Proyecto_PED.Modelo.Entidades;

namespace Proyecto_PED.Controlador
{
    public class ControladorLogin
    {
        private UsuarioRepositorio usuariorepo = new UsuarioRepositorio();

        public Usuario InicioSesion(string usuario, string contra)
        {
            return usuariorepo.ValidarYObtenerUsuario(usuario, contra);
        }
    }
}
