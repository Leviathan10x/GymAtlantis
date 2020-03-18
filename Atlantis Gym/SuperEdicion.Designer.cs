namespace Atlantis_Gym
{
    partial class SuperEdicion
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
            this.components = new System.ComponentModel.Container();
            this.textDocCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textTipoIngreso = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimeFechaIngre = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimeFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.textDocUsuario = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textTotal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.datos_Reporte = new Atlantis_Gym.Datos_Reporte();
            this.iNGRESOSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iNGRESOSTableAdapter = new Atlantis_Gym.Datos_ReporteTableAdapters.INGRESOSTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.datos_Reporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iNGRESOSBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textDocCliente
            // 
            this.textDocCliente.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDocCliente.Location = new System.Drawing.Point(52, 86);
            this.textDocCliente.Name = "textDocCliente";
            this.textDocCliente.Size = new System.Drawing.Size(163, 27);
            this.textDocCliente.TabIndex = 0;
            this.textDocCliente.TextChanged += new System.EventHandler(this.TextDocCliente_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.Location = new System.Drawing.Point(49, 30);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(27, 18);
            this.labelID.TabIndex = 2;
            this.labelID.Text = "ID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(91, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 21);
            this.button1.TabIndex = 3;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Documento del Ciente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(49, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tipo de ingreso";
            // 
            // textTipoIngreso
            // 
            this.textTipoIngreso.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTipoIngreso.Location = new System.Drawing.Point(52, 154);
            this.textTipoIngreso.Name = "textTipoIngreso";
            this.textTipoIngreso.Size = new System.Drawing.Size(163, 27);
            this.textTipoIngreso.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(49, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha Ingreso";
            // 
            // dateTimeFechaIngre
            // 
            this.dateTimeFechaIngre.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeFechaIngre.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeFechaIngre.Location = new System.Drawing.Point(52, 237);
            this.dateTimeFechaIngre.Name = "dateTimeFechaIngre";
            this.dateTimeFechaIngre.Size = new System.Drawing.Size(162, 26);
            this.dateTimeFechaIngre.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(310, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Fecha Inicio";
            // 
            // dateTimeFechaInicio
            // 
            this.dateTimeFechaInicio.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeFechaInicio.Location = new System.Drawing.Point(313, 87);
            this.dateTimeFechaInicio.Name = "dateTimeFechaInicio";
            this.dateTimeFechaInicio.Size = new System.Drawing.Size(162, 26);
            this.dateTimeFechaInicio.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(310, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Documento Usuario";
            // 
            // textDocUsuario
            // 
            this.textDocUsuario.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDocUsuario.Location = new System.Drawing.Point(313, 154);
            this.textDocUsuario.Name = "textDocUsuario";
            this.textDocUsuario.Size = new System.Drawing.Size(163, 27);
            this.textDocUsuario.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(310, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "Total";
            // 
            // textTotal
            // 
            this.textTotal.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotal.Location = new System.Drawing.Point(313, 227);
            this.textTotal.Name = "textTotal";
            this.textTotal.Size = new System.Drawing.Size(134, 27);
            this.textTotal.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(289, 230);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 18);
            this.label8.TabIndex = 15;
            this.label8.Text = "$";
            this.label8.Click += new System.EventHandler(this.Label8_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(91, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 32);
            this.button2.TabIndex = 16;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(313, 323);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 32);
            this.button3.TabIndex = 17;
            this.button3.Text = "Cerrar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // datos_Reporte
            // 
            this.datos_Reporte.DataSetName = "Datos_Reporte";
            this.datos_Reporte.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // iNGRESOSBindingSource
            // 
            this.iNGRESOSBindingSource.DataMember = "INGRESOS";
            this.iNGRESOSBindingSource.DataSource = this.datos_Reporte;
            // 
            // iNGRESOSTableAdapter
            // 
            this.iNGRESOSTableAdapter.ClearBeforeFill = true;
            // 
            // SuperEdicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textTotal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textDocUsuario);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dateTimeFechaInicio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimeFechaIngre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textTipoIngreso);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textDocCliente);
            this.Name = "SuperEdicion";
            this.Text = "SuperEdicion";
            this.Load += new System.EventHandler(this.SuperEdicion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datos_Reporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iNGRESOSBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textDocCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Datos_Reporte datos_Reporte;
        private System.Windows.Forms.BindingSource iNGRESOSBindingSource;
        private Datos_ReporteTableAdapters.INGRESOSTableAdapter iNGRESOSTableAdapter;
        private System.Windows.Forms.TextBox textTipoIngreso;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimeFechaIngre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimeFechaInicio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textDocUsuario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textTotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}