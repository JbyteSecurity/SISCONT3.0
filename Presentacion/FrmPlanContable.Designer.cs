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
            this.btnAgregar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtNodoTexto = new System.Windows.Forms.TextBox();
            this.txtNodoHijo = new System.Windows.Forms.TextBox();
            this.txtNodoPadre = new System.Windows.Forms.TextBox();
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
            this.tvPlanContable.TabIndex = 0;
            this.tvPlanContable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPlanContable_AfterSelect);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(87, 81);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.UseVisualStyleBackColor = true;
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
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnEliminar);
            this.panel2.Controls.Add(this.txtNodoTexto);
            this.panel2.Controls.Add(this.txtNodoHijo);
            this.panel2.Controls.Add(this.txtNodoPadre);
            this.panel2.Controls.Add(this.btnAgregar);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 749);
            this.panel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Descripción";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "N° Cuenta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cuenta Padre";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(189, 81);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // txtNodoTexto
            // 
            this.txtNodoTexto.Location = new System.Drawing.Point(87, 55);
            this.txtNodoTexto.Name = "txtNodoTexto";
            this.txtNodoTexto.Size = new System.Drawing.Size(177, 20);
            this.txtNodoTexto.TabIndex = 4;
            // 
            // txtNodoHijo
            // 
            this.txtNodoHijo.Location = new System.Drawing.Point(87, 29);
            this.txtNodoHijo.Name = "txtNodoHijo";
            this.txtNodoHijo.Size = new System.Drawing.Size(177, 20);
            this.txtNodoHijo.TabIndex = 3;
            // 
            // txtNodoPadre
            // 
            this.txtNodoPadre.Location = new System.Drawing.Point(87, 3);
            this.txtNodoPadre.Name = "txtNodoPadre";
            this.txtNodoPadre.Size = new System.Drawing.Size(177, 20);
            this.txtNodoPadre.TabIndex = 2;
            // 
            // FrmPlanContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 753);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmPlanContable";
            this.Text = "FrmPlanContable";
            this.Load += new System.EventHandler(this.FrmPlanContable_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvPlanContable;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNodoTexto;
        private System.Windows.Forms.TextBox txtNodoHijo;
        private System.Windows.Forms.TextBox txtNodoPadre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label3;
    }
}