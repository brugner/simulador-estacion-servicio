using Baudrillard.Simulator;
using SimuladorEstacionServicio.Core.Domain;
using SimuladorEstacionServicio.Core.Simulador;
using SimuladorEstacionServicio.Core.Simulador.MetodosNumericos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SimuladorEstacionServicio.WinClient
{
    public partial class MainForm : Form
    {
        private const string AppTitle = "Simulador de Estación de Servicio";
        private EstacionServicio _simulador;
        private Queue<Vehiculo> _colaSurtidor1;
        private Queue<Vehiculo> _colaSurtidor2;
        private RungeKuttaResultado _rungeKuttaResultadoSurtidor1;
        private RungeKuttaResultado _rungeKuttaResultadoSurtidor2;

        #region Controls
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = AppTitle + " v" + GetVersion();
            lbAuthor.Text = "Nery Brugnoni - 2019";
            numTiempoSimular.Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyData == Keys.F1)
            {
                mainTabControl.SelectedIndex = 0;
                e.Handled = true;
            }
            else if (e.KeyData == Keys.F2)
            {
                mainTabControl.SelectedIndex = 1;
                e.Handled = true;
            }
            else if (e.KeyData == Keys.F3 && linkQueue.Enabled)
            {
                linkQueue_LinkClicked(null, null);
                e.Handled = true;
            }
            else if (e.KeyData == Keys.F4 && linkRungeKutta.Enabled)
            {
                linkRungeKutta_LinkClicked(null, null);
                e.Handled = true;
            }
            else if (e.KeyData == Keys.F5)
            {
                mainTabControl.SelectedIndex = 0;
                btnSimular_Click(null, null);
                e.Handled = true;
            }
        }

        private async void btnSimular_Click(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                return;
            }

            ClearResults();
            EditParameters(false);

            try
            {
                var parametros = GetParametros();
                _simulador = new EstacionServicio(parametros);
                var resultados = await _simulador.SimulateAsync();

                PresentarResultados(resultados);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nDetalles:\n" + ex.StackTrace, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                var resultados = _simulador.GetResults();
                PresentarResultados(resultados, error: true);
            }
            finally
            {
                EditParameters(true);
            }
        }

        private void dgvVector_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            PrepareQueues(e.RowIndex);
            PrepareRungeKuttaResults(e.RowIndex);
        }

        private void linkQueue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new QueueForm
            {
                Queues = new Queue<Vehiculo>[] { _colaSurtidor1, _colaSurtidor2 }
            };

            form.ShowDialog();
        }

        private void linkRungeKutta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new RungeKuttaForm
            {
                Data = new RungeKuttaResultado[] { _rungeKuttaResultadoSurtidor1, _rungeKuttaResultadoSurtidor2 }
            };

            form.ShowDialog();
        }
        #endregion

        #region Helpers
        private ParametrosSimulador GetParametros()
        {
            return new ParametrosSimulador
            {
                TimeToSimulate = TimeSpan.FromMinutes((int)numTiempoSimular.Value),
                ShowStateVectorsFrom = TimeSpan.FromMinutes((int)numMostrarDesde.Value),
                ShowStateVectorsTo = TimeSpan.FromMinutes((int)numMostrarHasta.Value),
                LlegadaVehiculosExponencialLambda = (float)numLlegadaVehiculosExponencialLambda.Value,
                LimpiezaParabrisasUniformeDesde = (float)numLimpiezaParabrisasUniformeDesde.Value,
                LimpiezaParabrisasUniformeHasta = (float)numLimpiezaParabrisasUniformeHasta.Value,
                TiempoCobro = (int)numTiempoCobro.Value,
                TanqueMotocicleta = (int)numTanqueMotocicleta.Value,
                TanqueAutomovil = (int)numTanqueAutomovil.Value,
                TanqueCamioneta = (int)numTanqueCamioneta.Value
            };
        }

        private void EditParameters(bool state)
        {
            gbConfiguracion.Enabled = state;
            btnSimular.Visible = state;
            pgSimulacion.Visible = !state;
        }

        public void ClearResults()
        {
            lbTiempoSimulado.Text = "";
            lbTiempoPromedioPermanenciaSistema.Text = "";
            lbMostrarDatos.Text = "";
            lbTiempoSimulacion.Text = "";
            dgvVector.DataSource = null;
            lbRowCount.Text = "";
        }

        private void PresentarResultados(SimulationResult results, bool error = false)
        {
            var resultados = results as ResultadoSimulador;

            lbTiempoSimulacion.Invoke(new Action(() => { lbTiempoSimulacion.Text = resultados.SimulationTime.ToString(); }));

            lbTiempoSimulado.Text = "<= " + resultados.SimulatedTime.ToString();
            lbSumatoriaTiempoPermanenciaSistema.Text = resultados.SumatoriaTiempoPermanenciaSistema.ToString();
            lbSumatoriaVehiculosAtendidos.Text = $"{resultados.SumatoriaVehiculosAtendidos} (Motocicletas: {resultados.SumatoriaMotocicletasAtendidas}, Automóviles: {resultados.SumatoriaAutomovilesAtendidos}, Camionetas: {resultados.SumatoriaCamionetasAtendidas})";
            lbTiempoPromedioPermanenciaSistema.Text = resultados.TiempoPromedioPermanenciaSistema.ToString();
            lbMostrarDatos.Text = resultados.ShowableStateVectorsFromTo;
            lbTiempoSimulacion.Text = resultados.SimulationTime.ToString();
            dgvVector.DataSource = _simulador.GetShowableStateVectorsAsDataTable();
            lbRowCount.Text = dgvVector.Rows.Count.ToString();

            foreach (var item in _simulador.GetShowableStateVectorDataGridConfiguration())
            {
                dgvVector.Columns[item.Key].DefaultCellStyle.Alignment = (DataGridViewContentAlignment)item.Value;
            }

            if (error)
            {
                if (dgvVector.Rows.Count > 0)
                {
                    dgvVector.Rows[dgvVector.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                }
            }

            mainTabControl.SelectedIndex = 1;
        }

        private string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private bool IsFormValid()
        {
            if (numMostrarHasta.Value - numMostrarDesde.Value > 200 || numMostrarHasta.Value - numMostrarDesde.Value < 0)
            {
                MessageBox.Show("La diferencia de tiempo a mostrar debe ser mayor a cero y menor o igual a 200", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (numLimpiezaParabrisasUniformeDesde.Value > numLimpiezaParabrisasUniformeHasta.Value)
            {
                MessageBox.Show("'Desde' debe ser menor que 'hasta'", AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private void PrepareRungeKuttaResults(int rowIndex)
        {
            var vectorEstadoMostrable = _simulador.GetShowableStateVectors()[rowIndex];

            _rungeKuttaResultadoSurtidor1 = vectorEstadoMostrable.CargaCombustibleSurtidor1;
            _rungeKuttaResultadoSurtidor2 = vectorEstadoMostrable.CargaCombustibleSurtidor2;

            linkRungeKutta.Enabled = _rungeKuttaResultadoSurtidor1 != null || _rungeKuttaResultadoSurtidor2 != null;
        }

        private void PrepareQueues(int rowIndex)
        {
            _colaSurtidor1 = _simulador.GetShowableStateVectors()[rowIndex].ColaSurtidor1;
            _colaSurtidor2 = _simulador.GetShowableStateVectors()[rowIndex].ColaSurtidor2;

            linkQueue.Enabled = _colaSurtidor1?.Count > 0 || _colaSurtidor2?.Count > 0;
        }
        #endregion
    }
}
