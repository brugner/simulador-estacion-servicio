namespace SimuladorEstacionServicio.Core.Simulador.MetodosNumericos
{
    public class RungeKuttaVector : Baudrillard.Simulator.NumericMethods.RungeKuttaVector
    {
        public int Paso { get; set; }
        public float t { get; set; }
        public float Ci { get; set; }
        public float K0 { get; set; }
        public float K1 { get; set; }
        public float K2 { get; set; }
        public float K3 { get; set; }
        public float Ci1 { get; set; }
    }
}