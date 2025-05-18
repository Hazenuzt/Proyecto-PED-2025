using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    internal class UsuarioRepositorio
    {
        private List<Usuario> _usuarios; // Simulacion TEMPORAL de una base de datos, meramente para desarrollo.

        //En esta clase debe los metodos para RECUPERAR y GUARDAR en la base de datos los elementos de la clase usuario.

        public UsuarioRepositorio()
        {
            // Datos de ejemplo para usuarios con solo su nombre y cantidad de calorias
            _usuarios = new List<Usuario>
        {
            new Usuario("JuanPerez", 2200.0),
            new Usuario("MariaLopez", 1800.0),
            new Usuario("AdminUser",  2500.0) 
        };
        }

        /// <summary>
        /// Intenta obtener un usuario por su nombre de usuario (o ID).
        /// En la clase final debe ser con consultas a la BD
        /// y posiblemente verificación de contraseña.
        /// </summary>
        /// <param name="nombreUsuario">El nombre de usuario para buscar.</param>
        /// <returns>El objeto Usuario si se encuentra, de lo contrario null.</returns>
        public Usuario ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            
            return _usuarios.FirstOrDefault(u => u.Nombre.Equals(nombreUsuario, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
