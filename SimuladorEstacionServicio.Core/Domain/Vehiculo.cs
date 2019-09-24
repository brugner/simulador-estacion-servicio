using Baudrillard.Extensions;
using Baudrillard.Randoms;
using System;

namespace SimuladorEstacionServicio.Core.Domain
{
    public class Vehiculo
    {
        public EstadoVehiculo Estado { get; set; }
        public TimeSpan Llegada { get; set; }
        public TipoVehiculo Tipo { get; set; }
        public bool SolicitaLimpiezaParabrisas { get; set; }

        public Vehiculo(EstadoVehiculo estado, TimeSpan llegada, TipoVehiculo tipo, bool solicitaLimpiezaParabrisas)
        {
            Estado = estado;
            Llegada = llegada;
            Tipo = tipo;
            SolicitaLimpiezaParabrisas = solicitaLimpiezaParabrisas;
        }

        public Vehiculo(EstadoVehiculo estado, TimeSpan llegada)
        {
            Estado = estado;
            Llegada = llegada;
            Tipo = GenerarTipo();

            if (Tipo != TipoVehiculo.Motocicleta)
            {
                SolicitaLimpiezaParabrisas = GenerarSolicitaLimpiezaParabrisas();
            }
        }

        public bool AplicaLimpiezaParabrisas()
        {
            return Tipo != TipoVehiculo.Motocicleta && SolicitaLimpiezaParabrisas;
        }

        private bool GenerarSolicitaLimpiezaParabrisas()
        {
            return RandomGenerator.Nativo() >= 0.5;
        }

        private TipoVehiculo GenerarTipo()
        {
            var r = RandomGenerator.Nativo();

            if (r < 0.33)
            {
                return TipoVehiculo.Motocicleta;
            }
            else if (r >= 0.33 && r < 0.66)
            {
                return TipoVehiculo.Automovil;
            }
            else
            {
                return TipoVehiculo.Camioneta;
            }
        }

        public override string ToString()
        {
            return $"{Estado} - {Llegada.StripMilliseconds()} - {Tipo} - {SolicitaLimpiezaParabrisas}";
        }
    }
}
