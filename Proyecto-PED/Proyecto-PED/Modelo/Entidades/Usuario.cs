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

        //propiedades 
        public int Id_Usuario
        {
            get { return id_usuario; }
            set { this.id_usuario = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }
        public double Estatura
        {
            get { return estatura; }
            set { estatura = value; }
        }

        public double Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        public double CantCalorias 
        { 
            get { return cantCalorias; }
            set { cantCalorias = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        //propiedades de datos tipo Enum
        public Genero Genero { get; set; }
        public NivelActividad Nivel_Actividad { get; set; }
        public Objetivo Objetivo { get; set; }
        public EstadoFisico EstadoFisico { get; set; }

        //constructor
        public Usuario()
        {

        }
        public Usuario(string nom, double cantCalo)
        {
            Nombre = nom;
            cantCalorias = cantCalo;
        }

        //constructor con datos para Usuario
        public Usuario(double peso, double estatura, int edad)
        {
            Peso = peso;
            Edad = edad;
            Estatura = estatura;
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
    public enum EstadoFisico
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
