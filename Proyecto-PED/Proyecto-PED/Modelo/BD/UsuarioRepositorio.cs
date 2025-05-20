using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_PED.Modelo.Entidades;

namespace Proyecto_PED.Modelo.BD
{
    internal class UsuarioRepositorio
    {
        private string rutaArchivo = "usuarios.txt";
        private List<Usuario> _usuarios;

        public UsuarioRepositorio()
        {
            if (File.Exists(rutaArchivo))//si ya existe 
            {
                _usuarios = RecuperarUsuariosDesdeArchivo(rutaArchivo);
            }
            else
            {
                _usuarios = new List<Usuario>();
            }
        
        }

        // Guarda la lista de usuarios en el archivo indicado
        public void GuardarUsuariosEnArchivo(List<Usuario> usuarios, string rutaArchivo)
        {
            // Usamos StreamWriter para escribir en el archivo 
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                foreach (var usuario in usuarios)
                {
                    // separados por '|',Esto permite almacenar varios datos en una sola línea de texto
                    string linea = string.Join("|",
                        usuario.Id_Usuario,
                        usuario.Nombre,
                        usuario.Apellido,
                        usuario.Edad,
                        usuario.Estatura,
                        usuario.Peso,
                        usuario.Username,
                        usuario.Password,
                        usuario.CantCalorias
                    );
                    sw.WriteLine(linea); // Escribimos la línea en el archivo
                }
            }
        }

        // Recupera la lista de usuarios desde el archivo indicado
        public List<Usuario> RecuperarUsuariosDesdeArchivo(string rutaArchivo)
        {
            var usuarios = new List<Usuario>();
            if (!File.Exists(rutaArchivo))// Si el archivo no existe, retornamos lista vacía
                return usuarios;
            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (var linea in lineas)
            {
                var campos = linea.Split('|');

                // Validamos que la línea tenga el número correcto de campos
                if (campos.Length == 9)
                {
                    try
                    {
                        //  convertir los campos a sus tipos correspondientes
                        int id = int.Parse(campos[0]);
                        string nombre = campos[1];
                        string apellido = campos[2];
                        int edad = int.Parse(campos[3]);
                        double estatura = double.Parse(campos[4]);
                        double peso = double.Parse(campos[5]);
                        string username = campos[6];
                        string password = campos[7];
                        double cantCalorias = double.Parse(campos[8]);

                        // Creamos un nuevo objeto Usuario y lo agregamos a la lista
                        usuarios.Add(new Usuario
                        {
                            Id_Usuario = id,
                            Nombre = nombre,
                            Apellido = apellido,
                            Edad = edad,
                            Estatura = estatura,
                            Peso = peso,
                            Username = username,
                            Password = password,
                            CantCalorias = cantCalorias
                        });
                    }
                    catch
                    {
                        // Si hay algún error en el parseo, ignoramos esa línea y seguimos
                        continue;
                    }
                }
            }

            return usuarios; // Retornamos la lista con los usuarios recuperados
        }



        /* public UsuarioRepositorio()
         {
             // Datos de ejemplo para usuarios con solo su nombre y cantidad de calorias
             _usuarios = new List<Usuario>
         {
             new Usuario("JuanPerez", 2200.0),
             new Usuario("MariaLopez", 1800.0),
             new Usuario("AdminUser",  2500.0) 
         };
         }*/

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
