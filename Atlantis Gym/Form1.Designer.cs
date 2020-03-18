namespace Atlantis_Gym
{
    partial class CapturarHuella
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
            this.BotonGuardar = new System.Windows.Forms.Button();
            this.BotonCerrar = new System.Windows.Forms.Button();
            this.txtHuella = new System.Windows.Forms.TextBox();
            this.btnGuardarBD = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BotonGuardar
            // 
            this.BotonGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonGuardar.Location = new System.Drawing.Point(140, 90);
            this.BotonGuardar.Name = "BotonGuardar";
            this.BotonGuardar.Size = new System.Drawing.Size(161, 49);
            this.BotonGuardar.TabIndex = 1;
            this.BotonGuardar.Text = "CAPTURAR";
            this.BotonGuardar.UseVisualStyleBackColor = true;
            this.BotonGuardar.Click += new System.EventHandler(this.BotonGuardar_Click);
            // 
            // BotonCerrar
            // 
            this.BotonCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonCerrar.ForeColor = System.Drawing.Color.DarkRed;
            this.BotonCerrar.Location = new System.Drawing.Point(140, 238);
            this.BotonCerrar.Name = "BotonCerrar";
            this.BotonCerrar.Size = new System.Drawing.Size(161, 49);
            this.BotonCerrar.TabIndex = 3;
            this.BotonCerrar.Text = "CERRAR";
            this.BotonCerrar.UseVisualStyleBackColor = true;
            this.BotonCerrar.Click += new System.EventHandler(this.BotonCerrar_Click);
            // 
            // txtHuella
            // 
            this.txtHuella.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHuella.Location = new System.Drawing.Point(93, 31);
            this.txtHuella.Name = "txtHuella";
            this.txtHuella.Size = new System.Drawing.Size(247, 26);
            this.txtHuella.TabIndex = 4;
            // 
            // btnGuardarBD
            // 
            this.btnGuardarBD.Enabled = false;
            this.btnGuardarBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarBD.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnGuardarBD.Location = new System.Drawing.Point(140, 163);
            this.btnGuardarBD.Name = "btnGuardarBD";
            this.btnGuardarBD.Size = new System.Drawing.Size(161, 49);
            this.btnGuardarBD.TabIndex = 5;
            this.btnGuardarBD.Text = "GUARDAR";
            this.btnGuardarBD.UseVisualStyleBackColor = true;
            this.btnGuardarBD.Click += new System.EventHandler(this.BtnGuardarBD_Click);
            // 
            // CapturarHuella
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 327);
            this.Controls.Add(this.btnGuardarBD);
            this.Controls.Add(this.txtHuella);
            this.Controls.Add(this.BotonCerrar);
            this.Controls.Add(this.BotonGuardar);
            this.Name = "CapturarHuella";
            this.Text = "Captura de huella";
            this.Load += new System.EventHandler(this.CapturarHuella_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BotonGuardar;
        private System.Windows.Forms.Button BotonCerrar;
        private System.Windows.Forms.TextBox txtHuella;
        private System.Windows.Forms.Button btnGuardarBD;
    }
}