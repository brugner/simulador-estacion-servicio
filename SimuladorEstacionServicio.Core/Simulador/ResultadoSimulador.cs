using Baudrillard.Simulator;
using System;

namespace SimuladorEstacionServicio.Core.Simulador
{
    public class ResultadoSimulador : SimulationResult
    {
        public TimeSpan SumatoriaTiempoPermanenciaSistema { get; set; }
        public int SumatoriaVehiculosAtendidos { get; set; }
        public TimeSpan TiempoPromedioPermanenciaSistema { get; set; }
        public int SumatoriaMotocicletasAtendidas { get; set; }
        public int SumatoriaAutomovilesAtendidos { get; set; }
        public int SumatoriaCamionetasAtendidas { get; set; }
    }
}
