namespace SimuladorEstacionServicio.WinClient
{
    partial class QueueForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvQueueSurtidor1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvQueueSurtidor2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueSurtidor1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueSurtidor2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvQueueSurtidor1
            // 
            this.dgvQueueSurtidor1.AllowUserToAddRows = false;
            this.dgvQueueSurtidor1.AllowUserToDeleteRows = false;
            this.dgvQueueSurtidor1.AllowUserToResizeRows = false;
            this.dgvQueueSurtidor1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueueSurtidor1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueueSurtidor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueueSurtidor1.Location = new System.Drawing.Point(3, 34);
            this.dgvQueueSurtidor1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvQueueSurtidor1.Name = "dgvQueueSurtidor1";
            this.dgvQueueSurtidor1.ReadOnly = true;
            this.dgvQueueSurtidor1.RowHeadersVisible = false;
            this.dgvQueueSurtidor1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueueSurtidor1.Size = new System.Drawing.Size(512, 416);
            this.dgvQueueSurtidor1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvQueueSurtidor2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvQueueSurtidor1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1037, 450);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(521, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(513, 30);
            this.label2.TabIndex = 6;
            this.label2.Text = "Surtidor #2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvQueueSurtidor2
            // 
            this.dgvQueueSurtidor2.AllowUserToAddRows = false;
            this.dgvQueueSurtidor2.AllowUserToDeleteRows = false;
            this.dgvQueueSurtidor2.AllowUserToResizeRows = false;
            this.dgvQueueSurtidor2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueueSurtidor2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueueSurtidor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueueSurtidor2.Location = new System.Drawing.Point(521, 34);
            this.dgvQueueSurtidor2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvQueueSurtidor2.Name = "dgvQueueSurtidor2";
            this.dgvQueueSurtidor2.ReadOnly = true;
            this.dgvQueueSurtidor2.RowHeadersVisible = false;
            this.dgvQueueSurtidor2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueueSurtidor2.Size = new System.Drawing.Size(513, 416);
            this.dgvQueueSurtidor2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "Surtidor #1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // QueueForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1037, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Colas";
            this.Load += new System.EventHandler(this.QueueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueSurtidor1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueSurtidor2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvQueueSurtidor1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvQueueSurtidor2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}