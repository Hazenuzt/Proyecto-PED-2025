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

            List<Alimento> alimentosCandidatos = ObtenerAlimentosCandidatosPorMomento(momentoDia);
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
            return _gestorDeAlimentos.ObtenerTodosLosAlimentos()
                .Where(a => a.EsApropiadoPara(momentoDia))
                .Where(a => a.CaloriasPorPorcion > 0) // Asegurarse de que el alimento aporte calorías
                .ToList();
        }

        private List<int> EncontrarCombinacionPD(List<Alimento> alimentos, int caloriasObjetivo)
        {
            // dp[i] será la lista de IDs de alimentos que suman exactamente i calorías
            List<List<int>> dp = new List<List<int>>(new List<int>[caloriasObjetivo + 1]);
            dp[0] = new List<int>(); // 0 calorías se logran con una lista vacía

            for (int i = 0; i < alimentos.Count; i++)
            {
                Alimento alimento = alimentos[i];
                int caloriasPorPorcion = (int)Math.Round(alimento.CaloriasPorPorcion);

                // Iterar desde la capacidad de la porción del alimento hasta el objetivo
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
                        // Opcional: Permitir múltiples porciones del mismo alimento
                        // else
                        // {
                        //     List<int> nuevaCombinacion = new List<int>(dp[j - caloriasPorPorcion]);
                        //     nuevaCombinacion.Add(alimento.ID_Alimento);
                        //     // Podemos decidir si queremos almacenar todas las combinaciones o solo una
                        //     if (dp[j].Count > nuevaCombinacion.Count) // Ejemplo de optimización por número de ingredientes
                        //     {
                        //         dp[j] = nuevaCombinacion;
                        //     }
                        // }
                    }
                }
            }

            // Después de iterar, dp[caloriasObjetivo] contendrá una combinación (si existe)
            return dp[caloriasObjetivo];
        }
    }
}
