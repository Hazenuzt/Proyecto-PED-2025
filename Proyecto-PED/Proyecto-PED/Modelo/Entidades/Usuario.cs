using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_PED.Modelo.Entidades
{
    public class Usuario
    {
        //atributos para Usuario
        private int id_usuario;
        private string nombre;
        private string apellido;
        private int edad;
        private double estatura;
        private double peso;
        private string username;
        private string password;
        private double cantCalorias;

        // para el imc 
        public double imcCalcular()
        {
            if (estatura <= 0) return 0;//evitamos diviciones entre cero
            return peso / (estatura * estatura);
        }



        //propiedades 
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
                if (string.IsNullOrEmpty(value)) //El nombre del usuario no puede estar vacio
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
                apellido = value;
            }
        }

        public int Edad
        {
            get { return edad; }
            set 
            {
                if (value <= 0 || value > 110) //La edad del usuario no puede estar vacio
                {
                    throw new ArgumentOutOfRangeException("Edad fuera del rango válido");
                }
                edad = value;
            }
        }
        public double Estatura
        {
            get { return estatura; }
            set 
            {
                if (value <= 0) //El campo de la estatura del usuario no puede quedar vacia
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

        public double CantCalorias 
        { 
            get { return cantCalorias; }
            set { cantCalorias = value; }
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

        public string Password
        {
            get { return password; }
            set 
            {
                if (value.Length < 6) //La contraseña será valida si tiene 6 o más caracteres
                {
                    throw new Exception("La contraseña debe tener al menos 6 caracteres");
                }
                password = value;
            }
        }

        //propiedades de datos tipo Enum
        public Genero Genero { get; set; }
        public NivelActividad Nivel_Actividad { get; set; }
        public Objetivo Objetivo { get; set; }
        public EstadoFisicoUsuario EstadoFisicoUsuario { get; set; }

        //constructor
        public Usuario()
        {

        }
        public Usuario(string nom, double cantCalo)
        {
            Nombre = nom;
            cantCalorias = cantCalo;
        }

        public string Debug()
        {
            return $"Género: {Genero}, Actividad: {Nivel_Actividad}, Objetivo: {Objetivo}\n" +
           $"Peso: {Peso}, Estatura: {Estatura}, Edad: {Edad}, Cantidad de calorías: {CantCalorias}";
        }

    }
    //definimos a Género, Nivel de Actividad y Objetivo como un dato
    //tipo Enum para su procesamiento en el nodo de decisión
    public enum Genero
    {
        Femenino,
        Masculino
    }
    public enum NivelActividad
    {
        Sedentario,
        Actividad_ligera,
        Moderada,
        Intensa,
        Muy_intensa
    }
    public enum Objetivo
    {
        Perder_grasa,
        Mantener_peso,
        Ganar_musculo,
        Definicion_muscular
    }
    public enum EstadoFisicoUsuario
    {
        Delgado,
        Normal,
        Sobrepeso,
        Obeso
    }


    //clase estática para mantener los datos del usuario entre formularios
    public static class DatosGlobales
    {
        public static Usuario usua {  get; set; } = new Usuario();
    }
}
