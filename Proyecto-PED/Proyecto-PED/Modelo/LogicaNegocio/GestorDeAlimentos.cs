using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_PED.Modelo.LogicaNegocio;
using Proyecto_PED.Modelo.Entidades;
using Proyecto_PED.Modelo.BD;

namespace Proyecto_PED.Modelo.LogicaNegocio
{
    internal class GestorDeAlimentos
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
            Console.WriteLine("GestorDeAlimentos: Cargando alimentos desde el repositorio al modelo en memoria...");
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
    }
}
