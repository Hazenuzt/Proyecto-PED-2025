using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_PED.Modelo
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

        //propiedades de datos tipo Enum
        public Genero Genero { get; set; }
        public NivelActividad Nivel_Actividad { get; set; }
        public Objetivo Objetivo { get; set; }
        public EstadoFisico EstadoFisico { get; set; }


        //método que obtiene los resultados obtenidos por el árbol para su posterior evaluación
        public double tmb1;
        public double tdee1;

        public void CaloriasAsignadas(ArbolDecision arbol)
        {
            var nodoHoja = arbol.EvaluarUsuario(this);
            var resultado = ((double tmb, double tdee))nodoHoja.Evaluar(this);
            tmb1 = resultado.tmb;
            tdee1 = resultado.tdee;

            MessageBox.Show("Los datos obtenidos son: tdee" + tdee1 + " tmb:" + tmb1);
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
}
