using Baudrillard.Simulator;
using SimuladorEstacionServicio.Core.Domain;
using SimuladorEstacionServicio.Core.Simulador.MetodosNumericos;
using System;
using System.Collections.Generic;

namespace SimuladorEstacionServicio.Core.Simulador
{
    public class VectorEstadoSimulador : StateVector
    {
        public Evento Evento { get; internal set; }

        public double? RandomProximaLlegadaVehiculo { get; internal set; }
        public TimeSpan? ProximaLlegadaVehiculo { get; internal set; }

        public EstadoSurtidor EstadoSurtidor1 { get; internal set; }
        public RungeKuttaResultado CargaCombustibleSurtidor1 { get; internal set; }
        public TimeSpan? TiempoCargaCombustibleSurtidor1 { get; internal set; }
        public TimeSpan? FinCargaCombustibleSurtidor1 { get; internal set; }
        public double? RandomFinLimpiezaParabrisasSurtidor1 { get; internal set; }
        public TimeSpan? FinLimpiezaParabrisasSurtidor1 { get; internal set; }
        public TimeSpan? FinCobroSurtidor1 { get; internal set; }
        public Queue<Vehiculo> ColaSurtidor1 { get; internal set; }

        public EstadoSurtidor EstadoSurtidor2 { get; internal set; }
        public RungeKuttaResultado CargaCombustibleSurtidor2 { get; internal set; }
        public TimeSpan? TiempoCargaCombustibleSurtidor2 { get; internal set; }
        public TimeSpan? FinCargaCombustibleSurtidor2 { get; internal set; }
        public double? RandomFinLimpiezaParabrisasSurtidor2 { get; internal set; }
        public TimeSpan? FinLimpiezaParabrisasSurtidor2 { get; internal set; }
        public TimeSpan? FinCobroSurtidor2 { get; internal set; }
        public Queue<Vehiculo> ColaSurtidor2 { get; internal set; }

        public int SumatoriaVehiculosAtendidos { get; internal set; }
        public int SumatoriaMotocicletasAtendidas { get; internal set; }
        public int SumatoriaAutomovilesAtendidos { get; internal set; }
        public int SumatoriaCamionetasAtendidas { get; internal set; }
        public TimeSpan SumatoriaTiempoPermanenciaSistema { get; internal set; }
    }
}
