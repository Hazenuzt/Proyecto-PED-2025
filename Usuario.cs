using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clases_PlanEat
{
    public class Usuario
    {
        //Atributos para Usuario
        private int id_usuario;
        private string nombre;
        private string apellido;
        private int edad;
        private string genero;
        private double estatura;
        private double peso;
        private string nivelactividad;
        private string estadofisico;
        private string objetivo;
        private string username;
        private string contraseña;

        //Propiedades y validaciones
        public int Id_Usuario
        {
            get { return id_usuario; }
            set
            {
                if (value <= 0) //El ID del usuario debe ser mayor a cero
                {
                    throw new Exception("El ID no es válido");                    
                }
                id_usuario = value;
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if(string.IsNullOrEmpty(value)) //El nombre del usuario no puede estar vacio
                {
                    throw new ArgumentNullException("El nombre no debe estar vacio");
                }
                nombre = value;
            }
        }

        public string Apellido
        {
            get { return apellido; }
            set
            {
                if (string.IsNullOrEmpty(value)) //El apellido del usuario no puede estar vacio
                {
                    throw new ArgumentNullException("El apellido no debe estar vacio");
                }
                nombre = value;
            }
        }

        public int Edad
        {
            get { return edad; }
            set
            {
                if(value <=0|| value>110) //La edad del usuario no puede estar vacio
                {
                    throw new ArgumentOutOfRangeException("Edad fuera del rango válido");
                }
                edad = value;
            }
        }

        public string Genero
        {
            get { return genero; }
            set
            {
                if(string.IsNullOrEmpty(value)) //El genero del usuario no puede estar vacio
                {
                    throw new ArgumentNullException("El genero no es válido");
                }
                genero = value;
            }
        }

        public double Estatura
        {
            get { return estatura; }
            set
            {
                if(value<=0) //El campo de la estatura del usuario no puede quedar vacia
                {
                    throw new ArgumentOutOfRangeException("La estatura no es válida");
                }
                estatura = value;
            }
        }
        public double Peso
        {
            get { return peso; }
            set
            {
                if (value <= 0) // El peso del usuario debe ser mayor a 0
                {
                    throw new ArgumentOutOfRangeException("El peso no es válido");
                }
                peso = value;
            }
        }

        public string Nivel_Actividad
        {
            get { return nivelactividad; }
            set
            {
                if(string.IsNullOrEmpty(value)) // El nivel de actividad física no puede estar vacío
                {
                    throw new ArgumentNullException("El nivel no es válido");
                }
                nivelactividad = value;
            }
        }

        public  string Estado_Fisico
        {
            get { return estadofisico; }
            set
            {
                if (string.IsNullOrEmpty(value)) // El estado físico del usuario no puede estar vacío
                {
                    throw new ArgumentNullException("El estado no es válido");
                }
                estadofisico = value;
            }
        }

        public string Objetivo
        {
            get { return objetivo; }
            set
            {
                if (string.IsNullOrEmpty(value)) // El objetivo del usuario no puede estar vacío
                {
                    throw new ArgumentNullException("El objetivo no es válido");
                }
                objetivo = value;
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (string.IsNullOrEmpty(value)) // El username no puede estar vacío
                {
                    throw new ArgumentNullException("El username no es válido");
                }
                username = value;
            }
        }

        public string Contraseña
        {
            get { return contraseña; }
            set
            {
                if(value.Length<6) //La contraseña será valida si tiene 6 o más caracteres
                {
                    throw new Exception("La contraseña debe tener al menos 6 caracteres");
                }
                contraseña = value;
            }
        }

        // Constructor
        public Usuario(int idusuario, string nombre, string apellido, int edad, string genero, double estatura, double peso,
                       string nivelactividad, string estadofisico, string objetivo, string username, string contraseña)
        {
            Id_Usuario = idusuario;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Genero = genero;
            Estatura = estatura;
            Peso = peso;
            Nivel_Actividad = nivelactividad;
            Estado_Fisico = estadofisico;
            Objetivo = objetivo;
            Username = username;
            Contraseña = contraseña;
        }
    }
}
