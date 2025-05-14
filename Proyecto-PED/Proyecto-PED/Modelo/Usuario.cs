using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    public class Usuario
    {
        //SOLO ES PROVISIONAL EEEEEE 

        //atributos para Usuario
        private int id_usuario;
        private string nombre;
        private int edad;
        private string genero;
        private double estatura;
        private double peso;
        private string nivel_actividad;
        private string objetivo;
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

        //propiedades definidas para TMB y TDEE que se utilizarán para el nodo hoja según recorrido
        public double TMB
        {
            get
            {
                if (Genero == Genero.Masculino)
                {
                    return (88.36 + (13.4 * Peso) + (4.8 * Estatura) - (5.7 * Edad));
                }
                else if (Genero == Genero.Femenino)
                {
                    return (447.6 + (9.2 * Peso) + (3.1 * Estatura) - (4.3 * Edad));
                }
                else
                {
                    throw new ArgumentException("El valor ingresado no corresponde a los valores asignados");
                }
            }
        }

        public double TDEE
        {
            get
            {
                if (Nivel_Actividad == NivelActividad.Sedentario)
                {
                    return TMB * 1.2;
                }
                else if (Nivel_Actividad == NivelActividad.Actividad_ligera)
                {
                    return TMB * 1.375;
                }
                else if (Nivel_Actividad == NivelActividad.Moderada)
                {
                    return TMB * 1.55;
                }
                else if (Nivel_Actividad == NivelActividad.Muy_intensa)
                {
                    return TMB * 1.725;
                }
                else if (Nivel_Actividad == NivelActividad.Muy_intensa)
                {
                    return TMB * 1.9;
                }
                else
                {
                    throw new ArgumentException("El valor ingresado no corresponde a los valores asignados");
                }
            }
        }

        //propiedades de datos tipo Enum
        public Genero Genero { get; set; }
        public NivelActividad Nivel_Actividad { get; set; }
        public Objetivo Objetivo { get; set; }

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
}
