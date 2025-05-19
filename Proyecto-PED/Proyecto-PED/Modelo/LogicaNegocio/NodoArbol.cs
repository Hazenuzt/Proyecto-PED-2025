using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_PED.Modelo.Entidades;

namespace Proyecto_PED.Modelo.LogicaNegocio
{
    //clase padre para definir los nodos de decisión
    public abstract class NodoArbol
    {
        public abstract object Evaluar(Usuario usuario); 
        //se definirá tipo object para poder devolver datos tipo string o double de ser necesario
    }

    //clase hija para definir nodo decisión Género
    public class NodoGenero : NodoArbol
    {
        //atributo de NodoGenero, se maneja como Tabla Hash para poder identificar de manera más sencilla
        //el dato en el arbol
        private Dictionary<Genero, NodoArbol> ramitas;

        //constructor de NodoGenero
        public NodoGenero(Dictionary<Genero, NodoArbol> ramas)
        {
            ramitas = ramas;
        }

        //método heredado
        public override object Evaluar(Usuario usuario)
        {
            //estructura if-else para saber si el dato ingresado por el usuario pertenece o se 
            //identifica como una llave de la tabla Hash que definimos anteriormente como atributo 
            //del nodo decisión Género y si este coincide con los valores definidos para Género ya determinados en enum
            if (ramitas.TryGetValue(usuario.Genero, out NodoArbol siguienteNodo))
            {
                return siguienteNodo.Evaluar(usuario); //recurrencia
            }
            else
            {
                throw new KeyNotFoundException("No existe nodo para el género: {usuario.Genero}");
            }
        }
    }

    //clase hija para definir nodo decisión Nivel de Actividad
    public class NodoNivelActividad : NodoArbol
    {
        private Dictionary<NivelActividad, NodoArbol> ramita;

        public NodoNivelActividad(Dictionary<NivelActividad, NodoArbol> ramitas)
        {
            ramita = ramitas;
        }

        //metodo heredado
        public override object Evaluar(Usuario usuario)
        {
            if (ramita.TryGetValue(usuario.Nivel_Actividad, out NodoArbol siguienteNodo))
            {
                return siguienteNodo.Evaluar(usuario); //recurrencia
            }
            else
            {
                throw new KeyNotFoundException("No existe nodo para el nivel de actividad: {usuario.Nivel_Actividad}");
            }
        }
    }

    //clase hija para definir nodo decisión Objetivo
    public class NodoObjetivo : NodoArbol
    {
        private Dictionary<Objetivo, NodoArbol> ramita;

        public NodoObjetivo(Dictionary<Objetivo, NodoArbol> ramitas)
        {
            ramita = ramitas;
        }

        //metodo heredado
        public override object Evaluar(Usuario usuario)
        {
            if (ramita.TryGetValue(usuario.Objetivo, out NodoArbol siguienteNodo))
            {
                return siguienteNodo.Evaluar(usuario); //recurrencia
            }
            else
            {
                throw new KeyNotFoundException("No existe nodo para el objetivo: {usuario.Objetivo}");
            }
        }
    }

    //clase hija para definir el NODO HOJA del árbol de decisión, el cual contiene los parámetros
    //necesarios para ejecutar el plan de comidas
    public class NodoHoja : NodoArbol
    {
        //metodo heredado
        public override object Evaluar(Usuario usuario)
        {
            double tmb = ObtenerTMB(usuario);
            double tdee = ObtenerTDEE(usuario, tmb);
            double cantcal = ObtenerCantCal(usuario, tdee);

            usuario.CantCalorias = cantcal;

            return usuario.CantCalorias; //retorna la cantidad de calorias que el usuario debe de consumir
        }

        //método para obtener la cantidad de calorias necesarias por usuario
        private double ObtenerCantCal(Usuario usuario, double tdee)
        {
            Random rnd = new Random();
            switch (usuario.Objetivo)
            {
                case Objetivo.Ganar_musculo:
                    return tdee + rnd.Next(250, 501);
                case Objetivo.Perder_grasa:
                    return tdee - rnd.Next(250, 501);
                case Objetivo.Mantener_peso:
                    return tdee;
                case Objetivo.Definicion_muscular:
                    return tdee - rnd.Next(300, 401);
                default:
                    throw new ArgumentException("El valor ingresado no corresponde a los valores asignados");
            }
        }

        //métodos para TMB y TDEE que se utilizarán para el nodo hoja según recorrido
        private double ObtenerTMB (Usuario usuario)
        {
            if (usuario.Genero == Genero.Masculino)
            {
                return 88.36 + (13.4 * usuario.Peso) + (4.8 * usuario.Estatura) - (5.7 * usuario.Edad);
            }
            else if (usuario.Genero == Genero.Femenino)
            {
                return 447.6 + (9.2 * usuario.Peso) + (3.1 * usuario.Estatura) - (4.3 * usuario.Edad);
            }
            else
            {
                throw new ArgumentException("El valor ingresado no corresponde a los valores asignados");
            }
        }

        private double ObtenerTDEE(Usuario usuario, double tmb)
        {
            switch (usuario.Nivel_Actividad)
            {
                case NivelActividad.Sedentario:
                    return tmb * 1.2;
                case NivelActividad.Actividad_ligera:
                    return tmb * 1.375;
                case NivelActividad.Moderada:
                    return tmb * 1.55;
                case NivelActividad.Intensa:
                    return tmb * 1.725;
                case NivelActividad.Muy_intensa:
                    return tmb * 1.9;
                default:
                    throw new ArgumentException("El valor ingresado no corresponde a los valores asignados");
            }
        }

    }

}
