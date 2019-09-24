using Baudrillard.Simulator.NumericMethods;

namespace SimuladorEstacionServicio.Core.Simulador.MetodosNumericos
{
    public class RungeKuttaParametros : RungeKuttaParameters
    {
        public float CapacidadTanque { get; internal set; }

        public RungeKuttaParametros()
        {

        }

        public RungeKuttaParametros(float capacidadTanque)
        {
            CapacidadTanque = capacidadTanque;
        }
    }
}