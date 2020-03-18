namespace Atlantis_Gym
{
    partial class WhatsappConf
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
            this.label1 = new System.Windows.Forms.Label();
            this.textNumero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textCodigoP = new System.Windows.Forms.TextBox();
            this.BotonEnviar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero de WhatsApp";
            // 
            // textNumero
            // 
            this.textNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.textNumero.Location = new System.Drawing.Point(48, 78);
            this.textNumero.Name = "textNumero";
            this.textNumero.Size = new System.Drawing.Size(193, 29);
            this.textNumero.TabIndex = 1;
            this.textNumero.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(319, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Codigo de Pais";
            // 
            // textCodigoP
            // 
            this.textCodigoP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.textCodigoP.Location = new System.Drawing.Point(323, 78);
            this.textCodigoP.Name = "textCodigoP";
            this.textCodigoP.Size = new System.Drawing.Size(134, 29);
            this.textCodigoP.TabIndex = 3;
            // 
            // BotonEnviar
            // 
            this.BotonEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.BotonEnviar.Location = new System.Drawing.Point(549, 71);
            this.BotonEnviar.Name = "BotonEnviar";
            this.BotonEnviar.Size = new System.Drawing.Size(95, 35);
            this.BotonEnviar.TabIndex = 4;
            this.BotonEnviar.Text = "Enviar";
            this.BotonEnviar.UseVisualStyleBackColor = true;
            // 
            // WhatsappConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BotonEnviar);
            this.Controls.Add(this.textCodigoP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textNumero);
            this.Controls.Add(this.label1);
            this.Name = "WhatsappConf";
            this.Text = "WhatsappConf";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textNumero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCodigoP;
        private System.Windows.Forms.Button BotonEnviar;
    }
}