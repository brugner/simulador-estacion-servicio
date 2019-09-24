using SimuladorEstacionServicio.Core.Simulador.MetodosNumericos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SimuladorEstacionServicio.WinClient
{
    public partial class RungeKuttaForm : Form
    {
        private List<DataGridView> _grids = new List<DataGridView>();

        public RungeKuttaResultado[] Data { get; set; }

        public RungeKuttaForm()
        {
            InitializeComponent();
        }

        #region Controls
        private void RungeKuttaForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyData == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }
        #endregion

        #region Helpers
        private void LoadData()
        {
            _grids.Add(dgvRungeKuttaVectorsSurtidor1);
            _grids.Add(dgvRungeKuttaVectorsSurtidor2);

            for (int i = 0; i < _grids.Count; i++)
            {
                if (Data[i] != null)
                {
                    var dtRungeKutta = new DataTable();
                    dtRungeKutta.Columns.Add(new DataColumn("#", typeof(string)));
                    dtRungeKutta.Columns.Add(new DataColumn("t", typeof(string)));
                    dtRungeKutta.Columns.Add(new DataColumn("Ci", typeof(string)));
                    dtRungeKutta.Columns.Add(new DataColumn("k0", typeof(string)));
                    dtRungeKutta.Columns.Add(new DataColumn("k1", typeof(string)));
                    dtRungeKutta.Columns.Add(new DataColumn("k2", typeof(string)));
                    dtRungeKutta.Columns.Add(new DataColumn("k3", typeof(string)));
                    dtRungeKutta.Columns.Add(new DataColumn("Ci + 1", typeof(string)));

                    foreach (var vector in Data[i].Vectors)
                    {
                        var rowVector = dtRungeKutta.NewRow();
                        rowVector["#"] = vector.Paso;
                        rowVector["t"] = vector.t;
                        rowVector["Ci"] = vector.Ci;
                        rowVector["k0"] = vector.K0;
                        rowVector["k1"] = vector.K1;
                        rowVector["k2"] = vector.K2;
                        rowVector["k3"] = vector.K3;
                        rowVector["Ci + 1"] = vector.Ci1;
                        dtRungeKutta.Rows.Add(rowVector);
                    }

                    _grids[i].DataSource = dtRungeKutta;
                }
            }
        }
        #endregion
    }
}
