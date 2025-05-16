using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    internal class GeneradorPseudorecetas
    {
        private readonly double _caloriasNecesariasDia;
        private readonly GestorDeAlimentos _gestorDeAlimentos;

        public GeneradorPseudorecetas(double caloriasNecesariasDia, GestorDeAlimentos gestorDeAlimentos)
        {
            if (caloriasNecesariasDia <= 0)
            {
                throw new ArgumentException("Las calorías diarias deben ser un valor positivo.", nameof(caloriasNecesariasDia));
            }
            _caloriasNecesariasDia = caloriasNecesariasDia;
            _gestorDeAlimentos = gestorDeAlimentos ?? throw new ArgumentNullException(nameof(gestorDeAlimentos));
        }

        /// <summary>
        /// Genera una pseudoreceta (lista de Alimentos) para un momento del día dado.
        /// </summary>
        /// <param name="momentoDia">El momento del día (ej. "desayuno", "almuerzo").</param>
        /// <returns>Una lista de Alimentos que forman la pseudoreceta, o una lista vacía si no se puede generar.</returns>
        public List<Alimento> GenerarPseudorecetaPorMomento(string momentoDia)
        {
            double caloriasObjetivoComida = CalcularCaloriasPorMomento(momentoDia);
            

            List<Alimento> alimentosCandidatos = ObtenerAlimentosCandidatosPorMomento(momentoDia);
            if (!alimentosCandidatos.Any())
            {
                
                return new List<Alimento>();
            }

            List<Alimento> pseudoreceta = GenerarPseudorecetaHeuristica(alimentosCandidatos, caloriasObjetivoComida, momentoDia);

            return pseudoreceta;
        }

        private double CalcularCaloriasPorMomento(string momentoDia)
        {
            switch (momentoDia.ToLower())
            {
                case "desayuno":
                case "cena":
                    return _caloriasNecesariasDia * 0.25;
                case "almuerzo":
                    return _caloriasNecesariasDia * 0.30;
                case "snack":
                    return _caloriasNecesariasDia * 0.10;
                default:
                    
                    return 0;
            }
        }

        private List<Alimento> ObtenerAlimentosCandidatosPorMomento(string momentoDia)
        {
            return _gestorDeAlimentos.ObtenerTodosLosAlimentos()
                .Where(a => a.EsApropiadoPara(momentoDia))
                .Where(a => a.CaloriasPorPorcion > 0)
                .ToList();
        }

        private List<Alimento> GenerarPseudorecetaHeuristica(List<Alimento> alimentosCandidatos, double caloriasObjetivo, string momentoDia)
        {
            List<Alimento> pseudorecetaFinal = new List<Alimento>();
            double caloriasActuales = 0;
            HashSet<string> rolesPresentes = new HashSet<string>();

            double toleranciaInferior = caloriasObjetivo * 0.90;
            double toleranciaSuperior = caloriasObjetivo * 1.10;

            int minRolesNecesarios;
            if (momentoDia.ToLower() == "snack")
            {
                minRolesNecesarios = 1;
            }
            else
            {
                minRolesNecesarios = 3;
            }

            alimentosCandidatos = alimentosCandidatos.OrderBy(a => Guid.NewGuid()).ToList();

            List<string> rolesAForzarInicialmente = new List<string>();
            if (momentoDia.ToLower() != "snack")
            {
                rolesAForzarInicialmente = new List<string> { "Proteina", "Base", "Vegetales" };
            }

            foreach (string rol in rolesAForzarInicialmente)
            {
                if (pseudorecetaFinal.Count >= 5 || rolesPresentes.Contains(rol)) continue;

                Alimento alimentoSeleccionado = alimentosCandidatos
                    .FirstOrDefault(a => a.RolAlimento == rol &&
                                         !pseudorecetaFinal.Contains(a) &&
                                         (pseudorecetaFinal.Count < 5)
                                        );

                if (alimentoSeleccionado != null)
                {
                    pseudorecetaFinal.Add(alimentoSeleccionado);
                    caloriasActuales += alimentoSeleccionado.CaloriasPorPorcion;
                    rolesPresentes.Add(alimentoSeleccionado.RolAlimento);
                    
                }
            }

            var alimentosRestantes = alimentosCandidatos
                .Where(a => !pseudorecetaFinal.Contains(a))
                .OrderBy(a => Guid.NewGuid())
                .ToList();

            foreach (Alimento alimento in alimentosRestantes)
            {
                if (pseudorecetaFinal.Count >= 5) break;
                if (caloriasActuales >= toleranciaInferior && caloriasActuales <= toleranciaSuperior && rolesPresentes.Count >= minRolesNecesarios)
                {
                    break;
                }

                if (caloriasActuales + alimento.CaloriasPorPorcion <= caloriasObjetivo + (caloriasObjetivo * 0.20))
                {
                    pseudorecetaFinal.Add(alimento);
                    caloriasActuales += alimento.CaloriasPorPorcion;
                    rolesPresentes.Add(alimento.RolAlimento);
                    

                    if (pseudorecetaFinal.Count >= 3 && rolesPresentes.Count >= minRolesNecesarios && caloriasActuales >= toleranciaInferior)
                    {
                        if (caloriasActuales <= toleranciaSuperior) break;
                    }
                }
            }

            bool cumpleMinimoRoles = rolesPresentes.Count >= minRolesNecesarios;
            bool cumpleMaxIngredientes = pseudorecetaFinal.Count <= 5;
            bool cumpleCalorias = caloriasActuales >= toleranciaInferior && caloriasActuales <= toleranciaSuperior;

            

            if (cumpleMinimoRoles && cumpleMaxIngredientes && cumpleCalorias)
            {
                
                return pseudorecetaFinal;
            }
            else
            {
                
                return new List<Alimento>(); // Devuelve una lista vacía si no cumple los criterios.
            }
        }
    }
}
