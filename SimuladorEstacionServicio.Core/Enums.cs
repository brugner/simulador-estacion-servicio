namespace SimuladorEstacionServicio.Core
{
    public enum Evento
    {
        Inicio,
        LlegadaVehiculo,
        FinCargaCombustibleSurtidor1,
        FinCargaCombustibleSurtidor2,
        FinLimpiezaParabrisasSurtidor1,
        FinLimpiezaParabrisasSurtidor2,
        FinCobroSurtidor1,
        FinCobroSurtidor2
    }

    public enum EstadoSurtidor
    {
        Libre,
        Ocupado
    }

    public enum EstadoVehiculo
    {
        EnAtencion,
        EnCola
    }

    public enum TipoVehiculo
    {
        Motocicleta,
        Automovil,
        Camioneta
    }

    public enum Entity
    {
        ProximaLlegadaVehiculo,
        Surtidor1,
        Surtidor2
    }
}
