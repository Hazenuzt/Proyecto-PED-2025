using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    //clase padre para definir los nodos de decisión
    public abstract class NodoArbol
    {
        public abstract string Evaluar(Usuario usuario);
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
        public override string Evaluar(Usuario usuario)
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
        public override string Evaluar(Usuario usuario)
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
        public override string Evaluar(Usuario usuario)
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
        public override string Evaluar(Usuario usuario)
        {
            throw new NotImplementedException();//configurar
        }

    }

}
