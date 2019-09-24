using SimuladorEstacionServicio.Core.Domain;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SimuladorEstacionServicio.WinClient
{
    public partial class QueueForm : Form
    {
        private List<DataGridView> _grids = new List<DataGridView>();

        public Queue<Vehiculo>[] Queues { get; set; }

        public QueueForm()
        {
            InitializeComponent();
        }

        #region Controls
        private void QueueForm_Load(object sender, System.EventArgs e)
        {
            LoadQueues();
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
        private void LoadQueues()
        {
            _grids.Add(dgvQueueSurtidor1);
            _grids.Add(dgvQueueSurtidor2);

            for (int i = 0; i < _grids.Count; i++)
            {
                if (Queues[i] != null)
                {
                    var dtQueue = new DataTable();
                    dtQueue.Columns.Add(new DataColumn("Estado", typeof(string)));
                    dtQueue.Columns.Add(new DataColumn("Llegada", typeof(string)));
                    dtQueue.Columns.Add(new DataColumn("Tipo", typeof(string)));
                    dtQueue.Columns.Add(new DataColumn("LimpiezaParabrisas", typeof(string)));

                    foreach (var item in Queues[i])
                    {
                        var rowVector = dtQueue.NewRow();

                        rowVector["Estado"] = item.Estado;
                        rowVector["Llegada"] = item.Llegada;
                        rowVector["Tipo"] = item.Tipo;
                        rowVector["LimpiezaParabrisas"] = item.SolicitaLimpiezaParabrisas ? "Sí": "No";
                        dtQueue.Rows.Add(rowVector);
                    }

                    _grids[i].DataSource = dtQueue;
                }
            }
        }
        #endregion
    }
}
