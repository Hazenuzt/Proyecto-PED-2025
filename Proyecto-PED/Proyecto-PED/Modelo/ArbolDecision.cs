using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    public class ArbolDecision
    {
        private NodoArbol raiz;

        //constructor de la clase del Arbol
        public ArbolDecision()
        {
            ConstruirArbol();
        }

        //método que construye el árbol de decisión
        private void ConstruirArbol()
        {
            //nodo decisión: NodoObjetivo
            Dictionary<Objetivo, NodoArbol> nodosObjetivoMasculino = CrearNodosObjetivo();
            Dictionary<Objetivo, NodoArbol> nodosObjetivoFemenino = CrearNodosObjetivo();

            //nodo decisión: NodoNivelActvidad
            Dictionary<NivelActividad, NodoArbol> nodosActividadMasculino = CrearNodosActividad(nodosObjetivoMasculino);
            Dictionary<NivelActividad, NodoArbol> nodosActividadFemenino = CrearNodosActividad(nodosObjetivoFemenino);
            
            //nodo raiz, que es el NodoGenero
            Dictionary<Genero, NodoArbol> nodosGenero = new Dictionary<Genero, NodoArbol>()
            {
                {Genero.Masculino, new NodoNivelActividad(nodosActividadMasculino) },
                {Genero.Femenino, new NodoNivelActividad(nodosActividadFemenino) }
            };

            raiz = new NodoGenero(nodosGenero); //definimos el nodo raíz  
        }


        //método para crear los nodos Nivel Actividad, NodoNivelActividad es el siguiente nodo de decisión después del nodoRaiz (nodoGénero)
        private Dictionary<NivelActividad, NodoArbol> CrearNodosActividad(Dictionary<Objetivo, NodoArbol> nodosObjetivo)
        {
            //definimos la tabla Hash la cual su key es NivelActividad y su valor el NodoArbol definido anteriormente
            Dictionary<NivelActividad, NodoArbol> nodos = new Dictionary<NivelActividad, NodoArbol>();

            //se crearán los nodoObjetivos por cada nodoActividad que se cree, siguiendo la estructura del arbol de decisión
            nodos.Add(NivelActividad.Sedentario, new NodoObjetivo(nodosObjetivo));
            nodos.Add(NivelActividad.Actividad_ligera, new NodoObjetivo(nodosObjetivo));
            nodos.Add(NivelActividad.Moderada, new NodoObjetivo(nodosObjetivo));
            nodos.Add(NivelActividad.Intensa, new NodoObjetivo(nodosObjetivo));

            return nodos;
        }


        //método para crear los nodos objetivo, NodoObjetivo es el siguiente nodo de decisión después de el nodo NivelActividad
        //es decir, que este nodo de decisión define el nodo Hoja que saldrá de evaluar en el arbol de decisión
        private Dictionary<Objetivo, NodoArbol> CrearNodosObjetivo()
        {
            //definimos la tabla Hash la cual su key es Objetivo y su valor el NodoArbol definido anteriormente
            Dictionary<Objetivo, NodoArbol> nodos = new Dictionary<Objetivo, NodoArbol>();

            //se creará un nodo hoja por cada objetivo, para ello agregamos valores a la tabla Hash definida antes
            nodos.Add(Objetivo.Perder_grasa, new NodoHoja());
            nodos.Add(Objetivo.Mantener_peso, new NodoHoja());
            nodos.Add(Objetivo.Ganar_musculo, new NodoHoja());
            nodos.Add(Objetivo.Definicion_muscular, new NodoHoja());

            return nodos;
        }


        //método que ejecuta el arbol de decisión para obtener un resultado
        public (double tmb, double tdee) EvaluarUsuario(Usuario nuevoUsuario)
        {
            return ((double, double))raiz.Evaluar(nuevoUsuario);
        }
    }
}
