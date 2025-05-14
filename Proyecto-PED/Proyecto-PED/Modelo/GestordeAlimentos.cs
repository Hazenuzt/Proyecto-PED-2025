using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_PED.Modelo
{
    internal class GestordeAlimentos
    {
        private Dictionary<int, Alimento> _cacheAlimentosPorID;
        private AlimentoRepositorio _alimentoRepositorio;

        public GestorDeAlimentos(AlimentoRepositorio alimentoRepositorio)
        {
            _alimentoRepositorio = alimentoRepositorio ?? throw new ArgumentNullException(nameof(alimentoRepositorio));
            _cacheAlimentosPorID = new Dictionary<int, Alimento>();
            CargarAlimentosIniciales();
        }

        private void CargarAlimentosIniciales()
        {
            Console.WriteLine("GestorDeAlimentos: Cargando alimentos desde la base de datos al modelo en memoria...");
            List<Alimento> todosLosAlimentos = _alimentoRepositorio.ObtenerTodosLosAlimentos();
            foreach (var alimento in todosLosAlimentos)
            {
                _cacheAlimentosPorID[alimento.ID_Alimento] = alimento;
            }
            Console.WriteLine($"GestorDeAlimentos: Carga inicial de {_cacheAlimentosPorID.Count} alimentos completada.");
        }

        public Alimento ObtenerAlimentoPorID(int id)
        {
            _cacheAlimentosPorID.TryGetValue(id, out Alimento alimento);
            return alimento;
        }

        public List<Alimento> ObtenerTodosLosAlimentos()
        {
            return _cacheAlimentosPorID.Values.ToList();
        }

        // Nuevo método para obtener alimentos por tipo
        public List<Alimento> ObtenerAlimentosPorTipo(string tipoAlimento)
        {
            return _cacheAlimentosPorID.Values.Where(a => a.TipoAlimento?.ToLower() == tipoAlimento?.ToLower()).ToList();
        }

    }
}
