using Baudrillard.Attributes;
using Baudrillard.Simulator;
using SimuladorEstacionServicio.Core.Domain;
using SimuladorEstacionServicio.Core.Simulador.MetodosNumericos;
using System;
using System.Collections.Generic;

namespace SimuladorEstacionServicio.Core.Simulador
{
    public class VectorEstadoMostrableSimulador : ShowableStateVector
    {
        [Display(order: 1)]
        public Evento Evento { get; internal set; }

        [Display(title: "RndProxLlegVeh", order: 2)]
        public double? RandomProximaLlegadaVehiculo { get; internal set; }

        [Display(title: "ProxLlegVeh", order: 3)]
        public TimeSpan? ProximaLlegadaVehiculo { get; internal set; }

        [Display(title: "Surtidor1", order: 4)]
        public EstadoSurtidor EstadoSurtidor1 { get; internal set; }

        [Display(hidden: true)]
        public RungeKuttaResultado CargaCombustibleSurtidor1 { get; internal set; }

        [Display(title: "TiempoCargaCombSurt1", order: 5)]
        public TimeSpan? TiempoCargaCombustibleSurtidor1 { get; internal set; }

        [Display(title: "FinCargaCombSurt1", order: 6)]
        public TimeSpan? FinCargaCombustibleSurtidor1 { get; internal set; }

        [Display(title: "RndLimpParabSurt1", order: 7)]
        public double? RandomFinLimpiezaParabrisasSurtidor1 { get; internal set; }

        [Display(title: "FinLimpParabSurt1", order: 8)]
        public TimeSpan? FinLimpiezaParabrisasSurtidor1 { get; internal set; }

        [Display(title: "FinCobroSurt1", order: 9)]
        public TimeSpan? FinCobroSurtidor1 { get; internal set; }

        [Display(hidden: true)]
        public Queue<Vehiculo> ColaSurtidor1 { get; internal set; }

        [Display(title: "Surtidor2", order: 10)]
        public EstadoSurtidor EstadoSurtidor2 { get; internal set; }

        [Display(hidden: true)]
        public RungeKuttaResultado CargaCombustibleSurtidor2 { get; internal set; }

        [Display(title: "TiempoCargaCombSurt2", order: 11)]
        public TimeSpan? TiempoCargaCombustibleSurtidor2 { get; internal set; }

        [Display(title: "FinCargaCombSurt2", order: 12)]
        public TimeSpan? FinCargaCombustibleSurtidor2 { get; internal set; }

        [Display(title: "RndLimpParabSurt2", order: 13)]
        public double? RandomFinLimpiezaParabrisasSurtidor2 { get; internal set; }

        [Display(title: "FinLimpParabSurt2", order: 14)]
        public TimeSpan? FinLimpiezaParabrisasSurtidor2 { get; internal set; }

        [Display(title: "FinCobroSurt2", order: 15)]
        public TimeSpan? FinCobroSurtidor2 { get; internal set; }

        [Display(hidden: true)]
        public Queue<Vehiculo> ColaSurtidor2 { get; internal set; }

        [Display(title: "SumVehAtendidos", order: 16)]
        public int SumatoriaVehiculosAtendidos { get; internal set; }

        [Display(title: "SumTiempoPermSist", order: 17)]
        public TimeSpan SumatoriaTiempoPermanenciaSistema { get; internal set; }
    }
}
