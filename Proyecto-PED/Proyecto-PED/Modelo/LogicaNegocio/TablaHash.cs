using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo.LogicaNegocio
{
    // Clase interna para representar un par clave-valor dentro de la cubeta de la tabla hash
    // Esto es útil si una misma "cubeta" puede contener múltiples IDs de alimentos con sus listas de recetas.
    // Para este caso específico de índice invertido (AlimentoID -> List<RecetaID>),
    // el KeyValuePair de .NET es suficiente para el diccionario en GestorDeRecetas.
    // Sin embargo, si quisieras implementar una tabla hash completamente desde cero sin usar Dictionary,
    // necesitarías una estructura similar a esta para cada nodo en la lista enlazada de la cubeta.
    // Por simplicidad, y dado que la base de la tabla hash (el array de cubetas) será un array de List<int>,
    // no necesitamos esta clase auxiliar si cada cubeta solo almacena la lista de recetas directamente.


    internal class TablaHash
    {

        // El tamaño de la tabla hash. Un número primo es a menudo una buena elección.
        private readonly int _capacity;
        // El array de "cubetas". Cada cubeta contendrá una lista de IDs de receta.
        // Usaremos int para el ID del alimento como clave, y List<int> para la lista de IDs de receta.
        private List<Tuple<int, List<int>>>[] _buckets; // Cada cubeta es una lista de Tuplas (ID_Alimento, Lista_IDs_Receta)

        public TablaHash(int capacity = 101) // Capacidad inicial, un primo para mejor distribución
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "La capacidad de la tabla hash debe ser un número positivo.");
            }
            _capacity = capacity;
            _buckets = new List<Tuple<int, List<int>>>[_capacity];
            // Inicializar cada cubeta con una lista vacía para evitar NullReferenceExceptions
            for (int i = 0; i < _capacity; i++)
            {
                _buckets[i] = new List<Tuple<int, List<int>>>();
            }
        }

        // Función hash simple (método de la división)
        private int GetBucketIndex(int key)
        {
            // Usamos el valor absoluto para manejar posibles IDs negativos si los hubiera (aunque no debería ser el caso aquí)
            return Math.Abs(key % _capacity);
        }

        /// <summary>
        /// Añade una relación de AlimentoID a RecetaID a la tabla hash.
        /// Si el AlimentoID ya existe, el RecetaID se añade a su lista.
        /// Si el AlimentoID no existe, se crea una nueva entrada.
        /// </summary>
        /// <param name="alimentoId">El ID del alimento (la clave).</param>
        /// <param name="recetaId">El ID de la receta que contiene este alimento (el valor a añadir a la lista).</param>
        public void Add(int alimentoId, int recetaId)
        {
            int index = GetBucketIndex(alimentoId);
            var bucket = _buckets[index];

            // Buscar si ya existe una tupla con este alimentoId en la cubeta
            Tuple<int, List<int>> entry = bucket.FirstOrDefault(t => t.Item1 == alimentoId);

            if (entry != null)
            {
                // Si la entrada existe, añadir el recetaId si no está ya presente
                if (!entry.Item2.Contains(recetaId))
                {
                    entry.Item2.Add(recetaId);
                }
            }
            else
            {
                // Si la entrada no existe, crear una nueva tupla y añadirla a la cubeta
                List<int> newRecetaList = new List<int> { recetaId };
                bucket.Add(Tuple.Create(alimentoId, newRecetaList));
            }
        }

        /// <summary>
        /// Obtiene la lista de IDs de receta para un AlimentoID dado.
        /// </summary>
        /// <param name="alimentoId">El ID del alimento a buscar.</param>
        /// <param name="recetas">La lista de IDs de receta si se encuentra la clave.</param>
        /// <returns>True si la clave existe en la tabla hash, false en caso contrario.</returns>
        public bool TryGetValue(int alimentoId, out List<int> recetas)
        {
            int index = GetBucketIndex(alimentoId);
            var bucket = _buckets[index];

            // Buscar la tupla con el alimentoId en la cubeta
            Tuple<int, List<int>> entry = bucket.FirstOrDefault(t => t.Item1 == alimentoId);

            if (entry != null)
            {
                recetas = entry.Item2;
                return true;
            }
            else
            {
                recetas = null;
                return false;
            }
        }

        // Uso opcional: Para depuración o para obtener todas las claves (IDs de Alimentos)
        public IEnumerable<int> GetAllAlimentoIds()
        {
            foreach (var bucket in _buckets)
            {
                foreach (var entry in bucket)
                {
                    yield return entry.Item1;
                }
            }
        }
    }
}
