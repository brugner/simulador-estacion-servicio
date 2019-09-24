using Baudrillard.Extensions;
using Baudrillard.Randoms;
using Baudrillard.Simulator;
using Baudrillard.Simulator.NumericMethods;
using SimuladorEstacionServicio.Core.Domain;
using SimuladorEstacionServicio.Core.Simulador.MetodosNumericos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorEstacionServicio.Core.Simulador
{
	public sealed class EstacionServicio : Simulator<ParametrosSimulador, VectorEstadoSimulador, VectorEstadoMostrableSimulador, ResultadoSimulador>, IRungeKuttaManager<RungeKuttaParametros, RungeKuttaResultado, MetodosNumericos.RungeKuttaVector>
	{
		public Dictionary<float, float> _rungeKuttaPreviousValues { get; set; } = new Dictionary<float, float>();
		public Dictionary<float, IList<MetodosNumericos.RungeKuttaVector>> _rungeKuttaPreviousVectors { get; set; } = new Dictionary<float, IList<MetodosNumericos.RungeKuttaVector>>();

		public EstacionServicio(ParametrosSimulador parametros) : base(parametros)
		{

		}

		#region Simulator
		protected override void InitRoutine()
		{
			PreviousStateVector = new VectorEstadoSimulador
			{
				Evento = Evento.Inicio,
				Clock = new TimeSpan(),
				RandomProximaLlegadaVehiculo = 0.49,
				ProximaLlegadaVehiculo = new TimeSpan(0, 0, 0, 29, 0),
				EstadoSurtidor1 = EstadoSurtidor.Ocupado,
				CargaCombustibleSurtidor1 = null,
				TiempoCargaCombustibleSurtidor1 = null,
				FinCargaCombustibleSurtidor1 = new TimeSpan(0, 0, 1, 0, 0),
				RandomFinLimpiezaParabrisasSurtidor1 = 1.73,
				FinLimpiezaParabrisasSurtidor1 = new TimeSpan(0, 0, 1, 49, 0),
				FinCobroSurtidor1 = new TimeSpan(0, 0, 2, 49, 0),
				ColaSurtidor1 = new Queue<Vehiculo>(new Vehiculo[] { new Vehiculo(EstadoVehiculo.EnAtencion, TimeSpan.Zero, TipoVehiculo.Automovil, true) }),
				EstadoSurtidor2 = EstadoSurtidor.Ocupado,
				CargaCombustibleSurtidor2 = null,
				TiempoCargaCombustibleSurtidor2 = null,
				FinCargaCombustibleSurtidor2 = new TimeSpan(0, 0, 0, 40, 0),
				RandomFinLimpiezaParabrisasSurtidor2 = null,
				FinLimpiezaParabrisasSurtidor2 = null,
				FinCobroSurtidor2 = new TimeSpan(0, 0, 1, 40, 0),
				ColaSurtidor2 = new Queue<Vehiculo>(new Vehiculo[] { new Vehiculo(EstadoVehiculo.EnAtencion, TimeSpan.Zero, TipoVehiculo.Motocicleta, false) }),
				SumatoriaVehiculosAtendidos = 0,
				SumatoriaTiempoPermanenciaSistema = TimeSpan.Zero
			};

			CurrentStateVector = new VectorEstadoSimulador();
			ShowableStateVectors = new List<VectorEstadoMostrableSimulador>();
		}

		protected override void TimeRoutine()
		{
			CurrentStateVector.Evento = GetEvento();
			CurrentStateVector.Clock = GetClock();
			CopyStateVector();
		}

		protected override void EventRoutine()
		{
			if (EsLlegadaVehiculo())
			{
				ProgramarSiguienteLlegada();

				if (SurtidorEstaLibre(Entity.Surtidor1))
				{
					Surtidor1AtiendeVehiculo(new Vehiculo(EstadoVehiculo.EnAtencion, CurrentStateVector.Clock));
				}
				else if (SurtidorEstaLibre(Entity.Surtidor2))
				{
					Surtidor2AtiendeVehiculo(new Vehiculo(EstadoVehiculo.EnAtencion, CurrentStateVector.Clock));
				}
				else if (Surtidor1ColaMenorIgualSurtidor2())
				{
					AgregarVehiculoColaSurtidor1();
				}
				else
				{
					AgregarVehiculoColaSurtidor2();
				}
			}

			if (EsFinCargaCombustibleSurtidor1())
			{
				CurrentStateVector.FinCargaCombustibleSurtidor1 = null;
			}

			if (EsFinCargaCombustibleSurtidor2())
			{
				CurrentStateVector.FinCargaCombustibleSurtidor2 = null;
			}

			if (EsFinLimpiezaParabrisasSurtidor1())
			{
				CurrentStateVector.FinLimpiezaParabrisasSurtidor1 = null;
			}

			if (EsFinLimpiezaParabrisasSurtidor2())
			{
				CurrentStateVector.FinLimpiezaParabrisasSurtidor2 = null;
			}

			if (EsFinCobroSurtidor1())
			{
				FinalizarCobroSurtidor1();

				if (ColaSurtidorVacia(Entity.Surtidor1))
				{
					Surtidor1QuedaLibre();
				}
				else
				{
					Surtidor1AtiendeVehiculo();
				}
			}

			if (EsFinCobroSurtidor2())
			{
				FinalizarCobroSurtidor2();

				if (ColaSurtidorVacia(Entity.Surtidor2))
				{
					Surtidor2QuedaLibre();
				}
				else
				{
					Surtidor2AtiendeVehiculo();
				}
			}

			if (CurrentStateVector.ColaSurtidor1.Count(x => x.Estado == EstadoVehiculo.EnAtencion) > 1)
			{

			}

			if (ShowableStateVectors.LastOrDefault()?.ColaSurtidor1.Count(x => x.Estado == EstadoVehiculo.EnAtencion) > 1)
			{

			}
		}

		protected override void CopyStateVector()
		{
			CurrentStateVector.ProximaLlegadaVehiculo = PreviousStateVector.ProximaLlegadaVehiculo;
			CurrentStateVector.EstadoSurtidor1 = PreviousStateVector.EstadoSurtidor1;
			CurrentStateVector.FinCargaCombustibleSurtidor1 = PreviousStateVector.FinCargaCombustibleSurtidor1;
			CurrentStateVector.FinLimpiezaParabrisasSurtidor1 = PreviousStateVector.FinLimpiezaParabrisasSurtidor1;
			CurrentStateVector.FinCobroSurtidor1 = PreviousStateVector.FinCobroSurtidor1;
			CurrentStateVector.ColaSurtidor1 = CopiarColaSurtidor(Entity.Surtidor1);
			CurrentStateVector.EstadoSurtidor2 = PreviousStateVector.EstadoSurtidor2;
			CurrentStateVector.FinCargaCombustibleSurtidor2 = PreviousStateVector.FinCargaCombustibleSurtidor2;
			CurrentStateVector.FinLimpiezaParabrisasSurtidor2 = PreviousStateVector.FinLimpiezaParabrisasSurtidor2;
			CurrentStateVector.FinCobroSurtidor2 = PreviousStateVector.FinCobroSurtidor2;
			CurrentStateVector.ColaSurtidor2 = CopiarColaSurtidor(Entity.Surtidor2);
			CurrentStateVector.SumatoriaVehiculosAtendidos = PreviousStateVector.SumatoriaVehiculosAtendidos;
			CurrentStateVector.SumatoriaTiempoPermanenciaSistema = PreviousStateVector.SumatoriaTiempoPermanenciaSistema;
			CurrentStateVector.SumatoriaMotocicletasAtendidas = PreviousStateVector.SumatoriaMotocicletasAtendidas;
			CurrentStateVector.SumatoriaAutomovilesAtendidos = PreviousStateVector.SumatoriaAutomovilesAtendidos;
			CurrentStateVector.SumatoriaCamionetasAtendidas = PreviousStateVector.SumatoriaCamionetasAtendidas;
		}

		public override ResultadoSimulador GetResults()
		{
			var results = base.GetResults();

			if (PreviousStateVector.SumatoriaVehiculosAtendidos > 0)
			{
				results.SumatoriaTiempoPermanenciaSistema = PreviousStateVector.SumatoriaTiempoPermanenciaSistema;
				results.SumatoriaVehiculosAtendidos = PreviousStateVector.SumatoriaVehiculosAtendidos;
				results.TiempoPromedioPermanenciaSistema = TimeSpan.FromMinutes(PreviousStateVector.SumatoriaTiempoPermanenciaSistema.TotalMinutes / PreviousStateVector.SumatoriaVehiculosAtendidos);

				results.SumatoriaMotocicletasAtendidas = PreviousStateVector.SumatoriaMotocicletasAtendidas;
				results.SumatoriaAutomovilesAtendidos = PreviousStateVector.SumatoriaAutomovilesAtendidos;
				results.SumatoriaCamionetasAtendidas = PreviousStateVector.SumatoriaCamionetasAtendidas;
			}

			return results;
		}

		protected override void AddStateVectorToShowables()
		{
			ShowableStateVectors.Add(new VectorEstadoMostrableSimulador
			{
				Evento = PreviousStateVector.Evento,
				Clock = PreviousStateVector.Clock.StripMilliseconds(),
				RandomProximaLlegadaVehiculo = PreviousStateVector.RandomProximaLlegadaVehiculo.Round(2),
				ProximaLlegadaVehiculo = PreviousStateVector.ProximaLlegadaVehiculo.StripMilliseconds(),
				EstadoSurtidor1 = PreviousStateVector.EstadoSurtidor1,
				CargaCombustibleSurtidor1 = PreviousStateVector.CargaCombustibleSurtidor1,
				TiempoCargaCombustibleSurtidor1 = PreviousStateVector.TiempoCargaCombustibleSurtidor1.StripMilliseconds(),
				FinCargaCombustibleSurtidor1 = PreviousStateVector.FinCargaCombustibleSurtidor1.StripMilliseconds(),
				RandomFinLimpiezaParabrisasSurtidor1 = PreviousStateVector.RandomFinLimpiezaParabrisasSurtidor1.Round(2),
				FinLimpiezaParabrisasSurtidor1 = PreviousStateVector.FinLimpiezaParabrisasSurtidor1.StripMilliseconds(),
				FinCobroSurtidor1 = PreviousStateVector.FinCobroSurtidor1.StripMilliseconds(),
				ColaSurtidor1 = CopiarColaSurtidor(Entity.Surtidor1),
				EstadoSurtidor2 = PreviousStateVector.EstadoSurtidor2,
				CargaCombustibleSurtidor2 = PreviousStateVector.CargaCombustibleSurtidor2,
				TiempoCargaCombustibleSurtidor2 = PreviousStateVector.TiempoCargaCombustibleSurtidor2.StripMilliseconds(),
				FinCargaCombustibleSurtidor2 = PreviousStateVector.FinCargaCombustibleSurtidor2.StripMilliseconds(),
				RandomFinLimpiezaParabrisasSurtidor2 = PreviousStateVector.RandomFinLimpiezaParabrisasSurtidor2.Round(2),
				FinLimpiezaParabrisasSurtidor2 = PreviousStateVector.FinLimpiezaParabrisasSurtidor2.StripMilliseconds(),
				FinCobroSurtidor2 = PreviousStateVector.FinCobroSurtidor2.StripMilliseconds(),
				ColaSurtidor2 = CopiarColaSurtidor(Entity.Surtidor2),
				SumatoriaVehiculosAtendidos = PreviousStateVector.SumatoriaVehiculosAtendidos,
				SumatoriaTiempoPermanenciaSistema = PreviousStateVector.SumatoriaTiempoPermanenciaSistema.StripMilliseconds()
			});

			StripMillisecondsFromLastShowableVectorQueues();
		}
		#endregion

		#region IRungeKuttaManager
		public RungeKuttaResultado RungeKutta(RungeKuttaParametros parameters)
		{
			if (_rungeKuttaPreviousValues.ContainsKey(parameters.CapacidadTanque))
			{
				return new RungeKuttaResultado
				{
					Value = _rungeKuttaPreviousValues[parameters.CapacidadTanque],
					Vectors = _rungeKuttaPreviousVectors[parameters.CapacidadTanque]
				};
			}

			var result = new RungeKuttaResultado();

			var vectorAnterior = new MetodosNumericos.RungeKuttaVector
			{
				Paso = 0,
				t = -parameters.h,
				Ci1 = 0
			};

			do
			{
				var vectorActual = new MetodosNumericos.RungeKuttaVector
				{
					Paso = vectorAnterior.Paso + 1,
					t = vectorAnterior.t + parameters.h,
					Ci = vectorAnterior.Ci1
				};

				vectorActual.K0 = 30 * vectorActual.Ci + 10;
				vectorActual.K1 = 30 * (vectorActual.Ci + (vectorActual.K0 / 2) * parameters.h) + 10;
				vectorActual.K2 = 30 * (vectorActual.Ci + (vectorActual.K1 / 2) * parameters.h) + 10;
				vectorActual.K3 = 30 * (vectorActual.Ci + vectorActual.K2 * parameters.h) + 10;
				vectorActual.Ci1 = vectorActual.Ci + (parameters.h / 6) * (vectorActual.K0 + 2 * vectorActual.K1 + 2 * vectorActual.K2 + vectorActual.K3);

				result.Vectors.Add(vectorActual);
				vectorAnterior = vectorActual;
			} while (vectorAnterior.Ci1 <= parameters.CapacidadTanque);

			// Regla de 3
			// si h = 1 equivale a 10 minutos, entonces value = (10 * h) / 1
			result.Value = 10 * vectorAnterior.t / 1;

			_rungeKuttaPreviousValues.Add(parameters.CapacidadTanque, result.Value);
			_rungeKuttaPreviousVectors.Add(parameters.CapacidadTanque, result.Vectors);

			return result;
		}
		#endregion

		#region Helpers
		private TimeSpan GetClock()
		{
			return GetEntities().Where(x => x.Value.HasValue).OrderBy(x => x.Value).First().Value.Value;
		}

		private Evento GetEvento()
		{
			var entity = GetNextEntity();

			if (entity == Entity.ProximaLlegadaVehiculo)
			{
				return Evento.LlegadaVehiculo;
			}

			if (entity == Entity.Surtidor1)
			{
				var dic1 = new Dictionary<Evento, TimeSpan?>
					 {
						  { Evento.FinCargaCombustibleSurtidor1, PreviousStateVector.FinCargaCombustibleSurtidor1 },
						  { Evento.FinLimpiezaParabrisasSurtidor1, PreviousStateVector.FinLimpiezaParabrisasSurtidor1 },
						  { Evento.FinCobroSurtidor1, PreviousStateVector.FinCobroSurtidor1 }
					 };

				return dic1.Where(x => x.Value.HasValue).OrderBy(x => x.Value).First().Key;
			}

			if (entity == Entity.Surtidor2)
			{
				var dic2 = new Dictionary<Evento, TimeSpan?>
					 {
						  { Evento.FinCargaCombustibleSurtidor2, PreviousStateVector.FinCargaCombustibleSurtidor2 },
						  { Evento.FinLimpiezaParabrisasSurtidor2, PreviousStateVector.FinLimpiezaParabrisasSurtidor2 },
						  { Evento.FinCobroSurtidor2, PreviousStateVector.FinCobroSurtidor2 }
					 };

				return dic2.Where(x => x.Value.HasValue).OrderBy(x => x.Value).First().Key;
			}

			throw new Exception("Evento inválido");
		}

		private Dictionary<Entity, TimeSpan?> GetEntities()
		{
			var entities = new Dictionary<Entity, TimeSpan?>();
			entities.Add(Entity.ProximaLlegadaVehiculo, PreviousStateVector.ProximaLlegadaVehiculo);

			var tiemposSurtidor1 = new List<TimeSpan>();
			if (PreviousStateVector.FinCargaCombustibleSurtidor1.HasValue)
			{
				tiemposSurtidor1.Add(PreviousStateVector.FinCargaCombustibleSurtidor1.Value);
			}
			if (PreviousStateVector.FinLimpiezaParabrisasSurtidor1.HasValue)
			{
				tiemposSurtidor1.Add(PreviousStateVector.FinLimpiezaParabrisasSurtidor1.Value);
			}
			if (PreviousStateVector.FinCobroSurtidor1.HasValue)
			{
				tiemposSurtidor1.Add(PreviousStateVector.FinCobroSurtidor1.Value);
			}

			if (tiemposSurtidor1.Count > 0)
			{
				entities.Add(Entity.Surtidor1, tiemposSurtidor1.Min());
			}

			var tiemposSurtidor2 = new List<TimeSpan>();
			if (PreviousStateVector.FinCargaCombustibleSurtidor2.HasValue)
			{
				tiemposSurtidor2.Add(PreviousStateVector.FinCargaCombustibleSurtidor2.Value);
			}
			if (PreviousStateVector.FinLimpiezaParabrisasSurtidor2.HasValue)
			{
				tiemposSurtidor2.Add(PreviousStateVector.FinLimpiezaParabrisasSurtidor2.Value);
			}
			if (PreviousStateVector.FinCobroSurtidor2.HasValue)
			{
				tiemposSurtidor2.Add(PreviousStateVector.FinCobroSurtidor2.Value);
			}

			if (tiemposSurtidor2.Count > 0)
			{
				entities.Add(Entity.Surtidor2, tiemposSurtidor2.Min());
			}

			return entities;
		}

		private Entity GetNextEntity()
		{
			return GetEntities().Where(x => x.Value.HasValue).OrderBy(x => x.Value).First().Key;
		}

		private bool EsLlegadaVehiculo()
		{
			return CurrentStateVector.Evento == Evento.LlegadaVehiculo;
		}

		private bool EsFinCargaCombustibleSurtidor1()
		{
			return CurrentStateVector.Evento == Evento.FinCargaCombustibleSurtidor1;
		}

		private bool EsFinCargaCombustibleSurtidor2()
		{
			return CurrentStateVector.Evento == Evento.FinCargaCombustibleSurtidor2;
		}

		private bool EsFinLimpiezaParabrisasSurtidor1()
		{
			return CurrentStateVector.Evento == Evento.FinLimpiezaParabrisasSurtidor1;
		}

		private bool EsFinLimpiezaParabrisasSurtidor2()
		{
			return CurrentStateVector.Evento == Evento.FinLimpiezaParabrisasSurtidor2;
		}

		private bool EsFinCobroSurtidor1()
		{
			return CurrentStateVector.Evento == Evento.FinCobroSurtidor1;
		}

		private bool EsFinCobroSurtidor2()
		{
			return CurrentStateVector.Evento == Evento.FinCobroSurtidor2;
		}

		private void Surtidor1AtiendeVehiculo(Vehiculo vehiculo = null)
		{
			if (vehiculo != null)
			{
				CurrentStateVector.ColaSurtidor1.Enqueue(vehiculo);
			}
			else
			{
				CurrentStateVector.ColaSurtidor1.First().Estado = EstadoVehiculo.EnAtencion;
			}

			CurrentStateVector.EstadoSurtidor1 = EstadoSurtidor.Ocupado;
			CurrentStateVector.CargaCombustibleSurtidor1 = RungeKutta(GetRungeKuttaParameters(CurrentStateVector.ColaSurtidor1.First().Tipo));
			CurrentStateVector.TiempoCargaCombustibleSurtidor1 = TimeSpan.FromMinutes(CurrentStateVector.CargaCombustibleSurtidor1.Value);
			CurrentStateVector.FinCargaCombustibleSurtidor1 = CurrentStateVector.Clock + CurrentStateVector.TiempoCargaCombustibleSurtidor1;

			if (CurrentStateVector.ColaSurtidor1.First().AplicaLimpiezaParabrisas())
			{
				CurrentStateVector.RandomFinLimpiezaParabrisasSurtidor1 = GetRandomLimpiezaParabrisas();
				CurrentStateVector.FinLimpiezaParabrisasSurtidor1 = CurrentStateVector.Clock + TimeSpan.FromMinutes(CurrentStateVector.RandomFinLimpiezaParabrisasSurtidor1.Value);
			}

			CurrentStateVector.FinCobroSurtidor1 = GetFinCobroSurtidor(Entity.Surtidor1);
		}

		private void Surtidor2AtiendeVehiculo(Vehiculo vehiculo = null)
		{
			if (vehiculo != null)
			{
				CurrentStateVector.ColaSurtidor2.Enqueue(vehiculo);
			}
			else
			{
				CurrentStateVector.ColaSurtidor2.First().Estado = EstadoVehiculo.EnAtencion;
			}

			CurrentStateVector.EstadoSurtidor2 = EstadoSurtidor.Ocupado;
			CurrentStateVector.CargaCombustibleSurtidor2 = RungeKutta(GetRungeKuttaParameters(CurrentStateVector.ColaSurtidor2.First().Tipo));
			CurrentStateVector.TiempoCargaCombustibleSurtidor2 = TimeSpan.FromMinutes(CurrentStateVector.CargaCombustibleSurtidor2.Value);
			CurrentStateVector.FinCargaCombustibleSurtidor2 = CurrentStateVector.Clock + CurrentStateVector.TiempoCargaCombustibleSurtidor2;

			if (CurrentStateVector.ColaSurtidor2.First().AplicaLimpiezaParabrisas())
			{
				CurrentStateVector.RandomFinLimpiezaParabrisasSurtidor2 = GetRandomLimpiezaParabrisas();
				CurrentStateVector.FinLimpiezaParabrisasSurtidor2 = CurrentStateVector.Clock + TimeSpan.FromMinutes(CurrentStateVector.RandomFinLimpiezaParabrisasSurtidor2.Value);
			}

			CurrentStateVector.FinCobroSurtidor2 = GetFinCobroSurtidor(Entity.Surtidor2);
		}

		private void AgregarVehiculoColaSurtidor1()
		{
			CurrentStateVector.ColaSurtidor1.Enqueue(new Vehiculo(EstadoVehiculo.EnCola, CurrentStateVector.Clock));
		}

		private void AgregarVehiculoColaSurtidor2()
		{
			CurrentStateVector.ColaSurtidor2.Enqueue(new Vehiculo(EstadoVehiculo.EnCola, CurrentStateVector.Clock));
		}

		private void FinalizarCobroSurtidor1()
		{
			var vehiculo = CurrentStateVector.ColaSurtidor1.Dequeue();

			CurrentStateVector.SumatoriaTiempoPermanenciaSistema += CurrentStateVector.Clock - vehiculo.Llegada;
			CurrentStateVector.SumatoriaVehiculosAtendidos++;

			SumarTipoVehiculoAtendido(vehiculo.Tipo);
		}

		private void FinalizarCobroSurtidor2()
		{
			var vehiculo = CurrentStateVector.ColaSurtidor2.Dequeue();

			CurrentStateVector.SumatoriaTiempoPermanenciaSistema += CurrentStateVector.Clock - vehiculo.Llegada;
			CurrentStateVector.SumatoriaVehiculosAtendidos++;

			SumarTipoVehiculoAtendido(vehiculo.Tipo);
		}

		private void SumarTipoVehiculoAtendido(TipoVehiculo tipo)
		{
			if (tipo == TipoVehiculo.Motocicleta)
			{
				CurrentStateVector.SumatoriaMotocicletasAtendidas++;
			}
			else if (tipo == TipoVehiculo.Automovil)
			{
				CurrentStateVector.SumatoriaAutomovilesAtendidos++;
			}
			else
			{
				CurrentStateVector.SumatoriaCamionetasAtendidas++;
			}
		}

		private Queue<Vehiculo> CopiarColaSurtidor(Entity surtidor)
		{
			var q = new Queue<Vehiculo>();

			if (surtidor == Entity.Surtidor1)
			{
				foreach (var item in PreviousStateVector.ColaSurtidor1)
				{
					q.Enqueue(new Vehiculo(item.Estado, item.Llegada, item.Tipo, item.SolicitaLimpiezaParabrisas));
				}
			}
			else
			{
				foreach (var item in PreviousStateVector.ColaSurtidor2)
				{
					q.Enqueue(new Vehiculo(item.Estado, item.Llegada, item.Tipo, item.SolicitaLimpiezaParabrisas));
				}
			}

			return q;
		}

		private void StripMillisecondsFromLastShowableVectorQueues()
		{
			foreach (var item in ShowableStateVectors.Last().ColaSurtidor1)
			{
				item.Llegada = item.Llegada.StripMilliseconds();
			}

			foreach (var item in ShowableStateVectors.Last().ColaSurtidor2)
			{
				item.Llegada = item.Llegada.StripMilliseconds();
			}
		}

		private void ProgramarSiguienteLlegada()
		{
			CurrentStateVector.RandomProximaLlegadaVehiculo = GetRandomProximaLlegadaAutomovil();
			CurrentStateVector.ProximaLlegadaVehiculo = CurrentStateVector.Clock + TimeSpan.FromMinutes(CurrentStateVector.RandomProximaLlegadaVehiculo.Value);
		}

		private double GetRandomProximaLlegadaAutomovil()
		{
			return RandomGenerator.Exponential(Parameters.LlegadaVehiculosExponencialLambda);
		}

		private double GetRandomLimpiezaParabrisas()
		{
			return RandomGenerator.Uniform(Parameters.LimpiezaParabrisasUniformeDesde, Parameters.LimpiezaParabrisasUniformeHasta);
		}

		private bool SurtidorEstaLibre(Entity surtidor)
		{
			if (surtidor == Entity.Surtidor1)
			{
				return CurrentStateVector.EstadoSurtidor1 == EstadoSurtidor.Libre;
			}
			else
			{
				return CurrentStateVector.EstadoSurtidor2 == EstadoSurtidor.Libre;
			}
		}

		private bool Surtidor1ColaMenorIgualSurtidor2()
		{
			return CurrentStateVector.ColaSurtidor1.Count <= CurrentStateVector.ColaSurtidor2.Count;
		}

		private bool ColaSurtidorVacia(Entity surtidor)
		{
			if (surtidor == Entity.Surtidor1)
			{
				return CurrentStateVector.ColaSurtidor1.Count == 0;
			}
			else
			{
				return CurrentStateVector.ColaSurtidor2.Count == 0;
			}
		}

		private void Surtidor1QuedaLibre()
		{
			CurrentStateVector.EstadoSurtidor1 = EstadoSurtidor.Libre;
			CurrentStateVector.TiempoCargaCombustibleSurtidor1 = null;
			CurrentStateVector.FinCargaCombustibleSurtidor1 = null;
			CurrentStateVector.RandomFinLimpiezaParabrisasSurtidor1 = null;
			CurrentStateVector.FinLimpiezaParabrisasSurtidor1 = null;
			CurrentStateVector.FinCobroSurtidor1 = null;
		}

		private void Surtidor2QuedaLibre()
		{
			CurrentStateVector.EstadoSurtidor2 = EstadoSurtidor.Libre;
			CurrentStateVector.TiempoCargaCombustibleSurtidor2 = null;
			CurrentStateVector.FinCargaCombustibleSurtidor2 = null;
			CurrentStateVector.RandomFinLimpiezaParabrisasSurtidor2 = null;
			CurrentStateVector.FinLimpiezaParabrisasSurtidor2 = null;
			CurrentStateVector.FinCobroSurtidor2 = null;
		}

		private TimeSpan GetFinCobroSurtidor(Entity surtidor)
		{
			var list = new List<TimeSpan?>();

			if (surtidor == Entity.Surtidor1)
			{
				list.Add(CurrentStateVector.FinCargaCombustibleSurtidor1);
				list.Add(CurrentStateVector.FinLimpiezaParabrisasSurtidor1);
			}
			else
			{
				list.Add(CurrentStateVector.FinCargaCombustibleSurtidor2);
				list.Add(CurrentStateVector.FinLimpiezaParabrisasSurtidor2);
			}

			return list.Where(x => x.HasValue).Max().Value + TimeSpan.FromMinutes(Parameters.TiempoCobro);
		}

		private RungeKuttaParametros GetRungeKuttaParameters(TipoVehiculo tipoVehiculo)
		{
			var parametrosRungeKutta = new RungeKuttaParametros
			{
				h = 0.05F
			};

			switch (tipoVehiculo)
			{
				case TipoVehiculo.Motocicleta:
					parametrosRungeKutta.CapacidadTanque = Parameters.TanqueMotocicleta;
					break;
				case TipoVehiculo.Automovil:
					parametrosRungeKutta.CapacidadTanque = Parameters.TanqueAutomovil;
					break;
				case TipoVehiculo.Camioneta:
					parametrosRungeKutta.CapacidadTanque = Parameters.TanqueCamioneta;
					break;
				default:
					break;
			}

			return parametrosRungeKutta;
		}
		#endregion
	}
}