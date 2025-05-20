using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_PED.Modelo.Entidades;
using Proyecto_PED.Modelo.BD;

namespace Proyecto_PED.Modelo.LogicaNegocio
{
    internal class GestorDeRecetas
    {
        private readonly RecetaRepositorio _recetaRepositorio;
        private TablaHash _tablaHash;
        private Dictionary<int, Receta> _cacheRecetasPorID;

        public GestorDeRecetas(RecetaRepositorio recetaRepositorio)
        {
            _recetaRepositorio = recetaRepositorio ?? throw new ArgumentNullException(nameof(recetaRepositorio));
            _tablaHash = new TablaHash();
            _cacheRecetasPorID = new Dictionary<int, Receta>();
            ConstruirIndiceInvertido();
        }

        private void ConstruirIndiceInvertido()
        {
            
            List<Receta> todasLasRecetas = _recetaRepositorio.ObtenerTodasLasRecetas();
            foreach (var receta in todasLasRecetas)
            {
                _cacheRecetasPorID[receta.ID_Receta] = receta;
                foreach (var idAlimento in receta.IDsIngredientes)
                {
                    _tablaHash.Add(idAlimento, receta.ID_Receta);
                }
            }
            
        }

        /// <summary>
        /// Encuentra recetas que contienen los alimentos de una pseudoreceta generada
        /// y que caen dentro de un rango calórico similar.
        /// </summary>
        /// <param name="pseudorecetaAlimentos">La lista de objetos Alimento generada por la pseudoreceta.</param>
        /// <returns>Una lista de objetos Receta que coinciden, ordenadas por el número de ingredientes coincidentes, o una lista vacía.</returns>
        public List<Receta> EncontrarRecetasCoincidentes(List<Alimento> pseudorecetaAlimentos)
        {
            if (pseudorecetaAlimentos == null || !pseudorecetaAlimentos.Any())
            {
                //de fallar, devuelve una lista vacia
                return new List<Receta>();
            }

            double caloriasObjetivoPseudoreceta = pseudorecetaAlimentos.Sum(a => a.CaloriasPorPorcion);
            

            // Primer filtro: Coincidencia de ingredientes
            Dictionary<int, int> recetaCoincidencias = new Dictionary<int, int>();
            HashSet<int> pseudorecetaAlimentoIDs = new HashSet<int>(pseudorecetaAlimentos.Select(a => a.ID_Alimento));

            foreach (int idAlimentoPseudoreceta in pseudorecetaAlimentoIDs)
            {
                if (_tablaHash.TryGetValue(idAlimentoPseudoreceta, out List<int> recetasConEsteAlimento))
                {
                    foreach (int idReceta in recetasConEsteAlimento)
                    {
                        if (recetaCoincidencias.ContainsKey(idReceta))
                        {
                            recetaCoincidencias[idReceta]++;
                        }
                        else
                        {
                            recetaCoincidencias[idReceta] = 1;
                        }
                    }
                }
            }

            int minCoincidenciasIngredientes = 2; // Umbral de coincidencia ajustable
                                                 

            // Candidatos después del filtro de ingredientes, ordenados por número de coincidencias
            List<Receta> candidatosPorIngredientes = recetaCoincidencias
                .Where(entry => entry.Value >= minCoincidenciasIngredientes)
                .OrderByDescending(entry => entry.Value)
                .Select(entry => _cacheRecetasPorID[entry.Key])
                .ToList();

            if (!candidatosPorIngredientes.Any())
            {
                //de fallar, devuelve una lista vacia
                return new List<Receta>();
            }

            // Segundo filtro: Rango de calorías
            double toleranciaPorcentaje = 0.15; // 15%
            double minCaloriasReceta = caloriasObjetivoPseudoreceta * (1 - toleranciaPorcentaje);
            double maxCaloriasReceta = caloriasObjetivoPseudoreceta * (1 + toleranciaPorcentaje);

            

            List<Receta> recetasFinales = candidatosPorIngredientes
                .Where(r => r.CaloriasTotales >= minCaloriasReceta && r.CaloriasTotales <= maxCaloriasReceta)
                .ToList();

            

            return recetasFinales;
        }
    }
}
