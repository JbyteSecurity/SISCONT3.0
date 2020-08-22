namespace Presentacion
{
    partial class FrmPlanContable
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
            this.tvPlanContable = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chbCuentaPorCobrar = new System.Windows.Forms.CheckBox();
            this.chbCuentaVentaNaturaleza = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCuentaId = new System.Windows.Forms.TextBox();
            this.btnNuevaCuentaPadre = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.chbCuentaDestino = new System.Windows.Forms.CheckBox();
            this.chbCuentaPorPagar = new System.Windows.Forms.CheckBox();
            this.chbCuentaNaturaleza = new System.Windows.Forms.CheckBox();
            this.chbUso = new System.Windows.Forms.CheckBox();
            this.txtCodigoHijo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtCuentaHijo = new System.Windows.Forms.TextBox();
            this.txtCodigoPadre = new System.Windows.Forms.TextBox();
            this.txtCuentaPadre = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvPlanContable
            // 
            this.tvPlanContable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPlanContable.Location = new System.Drawing.Point(0, 0);
            this.tvPlanContable.Name = "tvPlanContable";
            this.tvPlanContable.Size = new System.Drawing.Size(1000, 749);
            this.tvPlanContable.TabIndex = 13;
            this.tvPlanContable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPlanContable_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tvPlanContable);
            this.panel1.Location = new System.Drawing.Point(295, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 749);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.chbCuentaPorCobrar);
            this.panel2.Controls.Add(this.chbCuentaVentaNaturaleza);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtCuentaId);
            this.panel2.Controls.Add(this.btnNuevaCuentaPadre);
            this.panel2.Controls.Add(this.btnEditar);
            this.panel2.Controls.Add(this.chbCuentaDestino);
            this.panel2.Controls.Add(this.chbCuentaPorPagar);
            this.panel2.Controls.Add(this.chbCuentaNaturaleza);
            this.panel2.Controls.Add(this.chbUso);
            this.panel2.Controls.Add(this.txtCodigoHijo);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnEliminar);
            this.panel2.Controls.Add(this.txtCuentaHijo);
            this.panel2.Controls.Add(this.txtCodigoPadre);
            this.panel2.Controls.Add(this.txtCuentaPadre);
            this.panel2.Controls.Add(this.btnAgregar);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 749);
            this.panel2.TabIndex = 4;
            // 
            // chbCuentaPorCobrar
            // 
            this.chbCuentaPorCobrar.AutoSize = true;
            this.chbCuentaPorCobrar.Location = new System.Drawing.Point(88, 233);
            this.chbCuentaPorCobrar.Name = "chbCuentaPorCobrar";
            this.chbCuentaPorCobrar.Size = new System.Drawing.Size(112, 17);
            this.chbCuentaPorCobrar.TabIndex = 39;
            this.chbCuentaPorCobrar.Text = "Cuenta por Cobrar";
            this.chbCuentaPorCobrar.UseVisualStyleBackColor = true;
            // 
            // chbCuentaVentaNaturaleza
            // 
            this.chbCuentaVentaNaturaleza.AutoSize = true;
            this.chbCuentaVentaNaturaleza.Location = new System.Drawing.Point(88, 210);
            this.chbCuentaVentaNaturaleza.Name = "chbCuentaVentaNaturaleza";
            this.chbCuentaVentaNaturaleza.Size = new System.Drawing.Size(113, 17);
            this.chbCuentaVentaNaturaleza.TabIndex = 38;
            this.chbCuentaVentaNaturaleza.Text = "Ventas Naturaleza";
            this.chbCuentaVentaNaturaleza.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Código";
            // 
            // txtCuentaId
            // 
            this.txtCuentaId.Location = new System.Drawing.Point(33, 268);
            this.txtCuentaId.Name = "txtCuentaId";
            this.txtCuentaId.ReadOnly = true;
            this.txtCuentaId.Size = new System.Drawing.Size(49, 20);
            this.txtCuentaId.TabIndex = 36;
            this.txtCuentaId.Visible = false;
            // 
            // btnNuevaCuentaPadre
            // 
            this.btnNuevaCuentaPadre.Location = new System.Drawing.Point(88, 294);
            this.btnNuevaCuentaPadre.Name = "btnNuevaCuentaPadre";
            this.btnNuevaCuentaPadre.Size = new System.Drawing.Size(177, 23);
            this.btnNuevaCuentaPadre.TabIndex = 34;
            this.btnNuevaCuentaPadre.Text = "Nueva Cuenta Padre";
            this.btnNuevaCuentaPadre.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(186, 265);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(79, 23);
            this.btnEditar.TabIndex = 33;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            // 
            // chbCuentaDestino
            // 
            this.chbCuentaDestino.AutoSize = true;
            this.chbCuentaDestino.Location = new System.Drawing.Point(88, 187);
            this.chbCuentaDestino.Name = "chbCuentaDestino";
            this.chbCuentaDestino.Size = new System.Drawing.Size(99, 17);
            this.chbCuentaDestino.TabIndex = 31;
            this.chbCuentaDestino.Text = "Cuenta Destino";
            this.chbCuentaDestino.UseVisualStyleBackColor = true;
            // 
            // chbCuentaPorPagar
            // 
            this.chbCuentaPorPagar.AutoSize = true;
            this.chbCuentaPorPagar.Location = new System.Drawing.Point(88, 164);
            this.chbCuentaPorPagar.Name = "chbCuentaPorPagar";
            this.chbCuentaPorPagar.Size = new System.Drawing.Size(109, 17);
            this.chbCuentaPorPagar.TabIndex = 29;
            this.chbCuentaPorPagar.Text = "Cuenta por Pagar";
            this.chbCuentaPorPagar.UseVisualStyleBackColor = true;
            // 
            // chbCuentaNaturaleza
            // 
            this.chbCuentaNaturaleza.AutoSize = true;
            this.chbCuentaNaturaleza.Location = new System.Drawing.Point(88, 141);
            this.chbCuentaNaturaleza.Name = "chbCuentaNaturaleza";
            this.chbCuentaNaturaleza.Size = new System.Drawing.Size(114, 17);
            this.chbCuentaNaturaleza.TabIndex = 27;
            this.chbCuentaNaturaleza.Text = "Cuenta Naturaleza";
            this.chbCuentaNaturaleza.UseVisualStyleBackColor = true;
            // 
            // chbUso
            // 
            this.chbUso.AutoSize = true;
            this.chbUso.Location = new System.Drawing.Point(88, 118);
            this.chbUso.Name = "chbUso";
            this.chbUso.Size = new System.Drawing.Size(49, 17);
            this.chbUso.TabIndex = 25;
            this.chbUso.Text = "USO";
            this.chbUso.UseVisualStyleBackColor = true;
            // 
            // txtCodigoHijo
            // 
            this.txtCodigoHijo.Location = new System.Drawing.Point(88, 61);
            this.txtCodigoHijo.Name = "txtCodigoHijo";
            this.txtCodigoHijo.Size = new System.Drawing.Size(177, 20);
            this.txtCodigoHijo.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Cuenta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Código Padre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Cuenta Padre";
            // 
            // btnEliminar
            // 
            this.btnEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEliminar.Location = new System.Drawing.Point(88, 323);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(58, 23);
            this.btnEliminar.TabIndex = 35;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // txtCuentaHijo
            // 
            this.txtCuentaHijo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCuentaHijo.Location = new System.Drawing.Point(88, 87);
            this.txtCuentaHijo.Name = "txtCuentaHijo";
            this.txtCuentaHijo.Size = new System.Drawing.Size(177, 20);
            this.txtCuentaHijo.TabIndex = 24;
            // 
            // txtCodigoPadre
            // 
            this.txtCodigoPadre.Location = new System.Drawing.Point(88, 9);
            this.txtCodigoPadre.Name = "txtCodigoPadre";
            this.txtCodigoPadre.ReadOnly = true;
            this.txtCodigoPadre.Size = new System.Drawing.Size(177, 20);
            this.txtCodigoPadre.TabIndex = 21;
            // 
            // txtCuentaPadre
            // 
            this.txtCuentaPadre.Location = new System.Drawing.Point(88, 35);
            this.txtCuentaPadre.Name = "txtCuentaPadre";
            this.txtCuentaPadre.ReadOnly = true;
            this.txtCuentaPadre.Size = new System.Drawing.Size(177, 20);
            this.txtCuentaPadre.TabIndex = 22;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(88, 265);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(87, 23);
            this.btnAgregar.TabIndex = 32;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // FrmPlanContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 753);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmPlanContable";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "PCGE";
            this.Load += new System.EventHandler(this.FrmPlanContable_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvPlanContable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chbCuentaPorCobrar;
        private System.Windows.Forms.CheckBox chbCuentaVentaNaturaleza;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCuentaId;
        private System.Windows.Forms.Button btnNuevaCuentaPadre;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.CheckBox chbCuentaDestino;
        private System.Windows.Forms.CheckBox chbCuentaPorPagar;
        private System.Windows.Forms.CheckBox chbCuentaNaturaleza;
        private System.Windows.Forms.CheckBox chbUso;
        private System.Windows.Forms.TextBox txtCodigoHijo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtCuentaHijo;
        private System.Windows.Forms.TextBox txtCodigoPadre;
        private System.Windows.Forms.TextBox txtCuentaPadre;
        private System.Windows.Forms.Button btnAgregar;
    }
}