using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    internal class GeneradorPseudoRecetas
    {
        private readonly double _caloriasNecesariasDia;
        private readonly GestorDeAlimentos _gestorDeAlimentos; // Necesitamos acceso a los alimentos

        // Constructor: Recibe las calorías diarias necesarias y el GestorDeAlimentos
        public GeneradorPseudorecetas(double caloriasNecesariasDia, GestorDeAlimentos gestorDeAlimentos)
        {
            if (caloriasNecesariasDia <= 0)
            {
                throw new ArgumentException("Las calorías diarias deben ser un valor positivo.", nameof(caloriasNecesariasDia));
            }
            _caloriasNecesariasDia = caloriasNecesariasDia;
            _gestorDeAlimentos = gestorDeAlimentos ?? throw new ArgumentNullException(nameof(gestorDeAlimentos));
        }

        public List<int> GenerarPseudorecetaPorMomento(string momentoDia)
        {
            double caloriasObjetivoComida = CalcularCaloriasPorMomento(momentoDia);
            Console.WriteLine($"\nGeneradorPseudorecetas: Generando pseudoreceta para {momentoDia} con {caloriasObjetivoComida:F0} calorías (usando Programación Dinámica).");

            // Obtener la lista de alimentos candidatos para este momento del día
            List<Alimento> alimentosCandidatos = ObtenerAlimentosCandidatosPorMomento(momentoDia);

            // Llamar al método de programación dinámica
            List<int> pseudoreceta = EncontrarCombinacionPD(alimentosCandidatos, (int)Math.Round(caloriasObjetivoComida));

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
                    Console.WriteLine($"Advertencia: Momento del día '{momentoDia}' no reconocido. Retornando 0 calorías.");
                    return 0;
            }
        }

        private List<Alimento> ObtenerAlimentosCandidatosPorMomento(string momentoDia)
        {
            // Esto es una lógica básica, necesitarás refinarla según tus tipos de alimentos
            switch (momentoDia.ToLower())
            {
                case "desayuno":
                    return _gestorDeAlimentos.ObtenerAlimentosPorTipo("Cereal")
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Lácteo"))
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Fruta"))
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Huevo"))
                           .Where(a => a.TamañoPorcionEstandarGramos.HasValue && a.TamañoPorcionEstandarGramos > 0) // Solo considerar alimentos con porción estándar definida
                           .ToList();
                case "almuerzo":
                    return _gestorDeAlimentos.ObtenerAlimentosPorTipo("Carne")
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Verdura"))
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Legumbre"))
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Cereal"))
                           .Where(a => a.TamañoPorcionEstandarGramos.HasValue && a.TamañoPorcionEstandarGramos > 0)
                           .ToList();
                case "cena":
                    return _gestorDeAlimentos.ObtenerAlimentosPorTipo("Carne")
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Verdura"))
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Pescado"))
                           .Where(a => a.TamañoPorcionEstandarGramos.HasValue && a.TamañoPorcionEstandarGramos > 0)
                           .ToList();
                case "snack":
                    return _gestorDeAlimentos.ObtenerAlimentosPorTipo("Fruta")
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Lácteo"))
                           .Concat(_gestorDeAlimentos.ObtenerAlimentosPorTipo("Nuez"))
                           .Where(a => a.TamañoPorcionEstandarGramos.HasValue && a.TamañoPorcionEstandarGramos > 0)
                           .ToList();
                default:
                    return new List<Alimento>();
            }
        }

        private List<int> EncontrarCombinacionPD(List<Alimento> alimentos, int caloriasObjetivo)
        {
            // dp[i] será una lista de listas de IDs de alimentos que suman i calorías
            List<List<int>> dp = new List<List<int>>(new List<int>[caloriasObjetivo + 1]);
            dp[0] = new List<int>(); // 0 calorías se pueden lograr con una lista vacía

            for (int i = 0; i < alimentos.Count; i++)
            {
                Alimento alimento = alimentos[i];
                if (alimento.TamañoPorcionEstandarGramos.HasValue && alimento.TamañoPorcionEstandarGramos > 0)
                {
                    int caloriasPorPorcion = (int)Math.Round((alimento.CaloriasPor100g / 100) * alimento.TamañoPorcionEstandarGramos.Value);

                    // Iterar desde caloriasPorPorcion hasta caloriasObjetivo
                    for (int j = caloriasPorPorcion; j <= caloriasObjetivo; j++)
                    {
                        if (dp[j - caloriasPorPorcion] != null)
                        {
                            // Si podemos alcanzar j - caloriasPorPorcion, entonces podemos alcanzar j añadiendo este alimento
                            if (dp[j] == null)
                            {
                                dp[j] = new List<int>(dp[j - caloriasPorPorcion]);
                                dp[j].Add(alimento.ID_Alimento);
                            }
                            // Opcional: Considerar múltiples porciones (añadir otra vez el mismo alimento)
                            // if (permitirMultiplesPorciones && dp[j] != null) { ... }
                        }
                    }
                }
            }

            // Después de iterar por todos los alimentos, dp[caloriasObjetivo] contendrá una combinación (si existe)
            return dp[caloriasObjetivo];
        }
    }
}
