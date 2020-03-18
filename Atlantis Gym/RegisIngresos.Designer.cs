namespace Atlantis_Gym
{
    partial class RegisIngresos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtgLista = new System.Windows.Forms.DataGridView();
            this.LVisor = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnRefres = new System.Windows.Forms.Button();
            this.pictureBoxVERDE = new System.Windows.Forms.PictureBox();
            this.pictureBoxROJO = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVERDE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxROJO)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgLista
            // 
            this.dtgLista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgLista.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgLista.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dtgLista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgLista.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkCyan;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgLista.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLista.GridColor = System.Drawing.Color.DarkGray;
            this.dtgLista.Location = new System.Drawing.Point(12, 9);
            this.dtgLista.Name = "dtgLista";
            this.dtgLista.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dtgLista.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Chartreuse;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            this.dtgLista.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgLista.Size = new System.Drawing.Size(693, 496);
            this.dtgLista.TabIndex = 0;
            this.dtgLista.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DtgLista_MouseClick);
            // 
            // LVisor
            // 
            this.LVisor.AutoSize = true;
            this.LVisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVisor.ForeColor = System.Drawing.Color.White;
            this.LVisor.Location = new System.Drawing.Point(12, 508);
            this.LVisor.Name = "LVisor";
            this.LVisor.Size = new System.Drawing.Size(45, 16);
            this.LVisor.TabIndex = 1;
            this.LVisor.Text = "label1";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.SkyBlue;
            this.btnCerrar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCerrar.Location = new System.Drawing.Point(605, 511);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(68, 23);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // btnRefres
            // 
            this.btnRefres.BackColor = System.Drawing.Color.SkyBlue;
            this.btnRefres.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefres.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRefres.Location = new System.Drawing.Point(520, 511);
            this.btnRefres.Name = "btnRefres";
            this.btnRefres.Size = new System.Drawing.Size(68, 23);
            this.btnRefres.TabIndex = 4;
            this.btnRefres.Text = "Refresh";
            this.btnRefres.UseVisualStyleBackColor = false;
            this.btnRefres.Click += new System.EventHandler(this.BtnRefres_Click);
            // 
            // pictureBoxVERDE
            // 
            this.pictureBoxVERDE.Image = global::Atlantis_Gym.Properties.Resources.verde;
            this.pictureBoxVERDE.Location = new System.Drawing.Point(300, 515);
            this.pictureBoxVERDE.Name = "pictureBoxVERDE";
            this.pictureBoxVERDE.Size = new System.Drawing.Size(83, 57);
            this.pictureBoxVERDE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxVERDE.TabIndex = 5;
            this.pictureBoxVERDE.TabStop = false;
            this.pictureBoxVERDE.Visible = false;
            this.pictureBoxVERDE.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // pictureBoxROJO
            // 
            this.pictureBoxROJO.Image = global::Atlantis_Gym.Properties.Resources.detener;
            this.pictureBoxROJO.Location = new System.Drawing.Point(418, 515);
            this.pictureBoxROJO.Name = "pictureBoxROJO";
            this.pictureBoxROJO.Size = new System.Drawing.Size(83, 57);
            this.pictureBoxROJO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxROJO.TabIndex = 6;
            this.pictureBoxROJO.TabStop = false;
            this.pictureBoxROJO.Visible = false;
            // 
            // RegisIngresos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(717, 584);
            this.Controls.Add(this.pictureBoxROJO);
            this.Controls.Add(this.pictureBoxVERDE);
            this.Controls.Add(this.btnRefres);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.LVisor);
            this.Controls.Add(this.dtgLista);
            this.Name = "RegisIngresos";
            this.Text = "Registros de Ingresos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegisIngresos_FormClosed);
            this.Load += new System.EventHandler(this.RegisIngresos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVERDE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxROJO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgLista;
        private System.Windows.Forms.Label LVisor;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnRefres;
        private System.Windows.Forms.PictureBox pictureBoxVERDE;
        private System.Windows.Forms.PictureBox pictureBoxROJO;
    }
}