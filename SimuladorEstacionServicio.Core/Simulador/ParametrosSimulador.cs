using Baudrillard.Simulator;

namespace SimuladorEstacionServicio.Core.Simulador
{
    public class ParametrosSimulador : SimulationParameters
    {
        public float LlegadaVehiculosExponencialLambda { get; set; }
        public float LimpiezaParabrisasUniformeDesde { get; set; }
        public float LimpiezaParabrisasUniformeHasta { get; set; }
        public int TiempoCobro { get; set; }
        public int TanqueMotocicleta { get; set; }
        public int TanqueAutomovil { get; set; }
        public int TanqueCamioneta { get; set; }

        public ParametrosSimulador()
        {

        }
    }
}
