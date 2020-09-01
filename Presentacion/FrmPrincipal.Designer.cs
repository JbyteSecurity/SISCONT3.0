namespace Presentacion
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.MenuItemPLE = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMantenimiento = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSubItemProveedores = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSubItemTipoDeCambio = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSubItemDetracción = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSubItemPlanContable = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPermisos = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSubItemUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSubItemRoles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.UsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ssPrincipal = new System.Windows.Forms.StatusStrip();
            this.tssConexion = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuItemLeasing = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.ssPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemPLE,
            this.MenuItemLeasing,
            this.MenuItemMantenimiento,
            this.MenuItemPermisos,
            this.toolStripMenuItem2,
            this.UsuarioToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1382, 24);
            this.mainMenu.TabIndex = 5;
            this.mainMenu.Text = "menuStrip1";
            // 
            // MenuItemPLE
            // 
            this.MenuItemPLE.Name = "MenuItemPLE";
            this.MenuItemPLE.Size = new System.Drawing.Size(38, 20);
            this.MenuItemPLE.Text = "PLE";
            this.MenuItemPLE.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // MenuItemMantenimiento
            // 
            this.MenuItemMantenimiento.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSubItemProveedores,
            this.MenuSubItemTipoDeCambio,
            this.MenuSubItemDetracción,
            this.MenuSubItemPlanContable});
            this.MenuItemMantenimiento.Name = "MenuItemMantenimiento";
            this.MenuItemMantenimiento.Size = new System.Drawing.Size(101, 20);
            this.MenuItemMantenimiento.Text = "Mantenimiento";
            // 
            // MenuSubItemProveedores
            // 
            this.MenuSubItemProveedores.Name = "MenuSubItemProveedores";
            this.MenuSubItemProveedores.Size = new System.Drawing.Size(180, 22);
            this.MenuSubItemProveedores.Text = "Proveedores";
            this.MenuSubItemProveedores.Click += new System.EventHandler(this.menuItemProveedores_Click);
            // 
            // MenuSubItemTipoDeCambio
            // 
            this.MenuSubItemTipoDeCambio.Name = "MenuSubItemTipoDeCambio";
            this.MenuSubItemTipoDeCambio.Size = new System.Drawing.Size(180, 22);
            this.MenuSubItemTipoDeCambio.Text = "Tipo de Cambio";
            this.MenuSubItemTipoDeCambio.Click += new System.EventHandler(this.tipoDeCambioToolStripMenuItem_Click);
            // 
            // MenuSubItemDetracción
            // 
            this.MenuSubItemDetracción.Name = "MenuSubItemDetracción";
            this.MenuSubItemDetracción.Size = new System.Drawing.Size(180, 22);
            this.MenuSubItemDetracción.Text = "Detracción";
            this.MenuSubItemDetracción.Click += new System.EventHandler(this.detracciónToolStripMenuItem_Click);
            // 
            // MenuSubItemPlanContable
            // 
            this.MenuSubItemPlanContable.Name = "MenuSubItemPlanContable";
            this.MenuSubItemPlanContable.Size = new System.Drawing.Size(180, 22);
            this.MenuSubItemPlanContable.Text = "PCGE 2020";
            this.MenuSubItemPlanContable.Click += new System.EventHandler(this.menuItemPlanContable_Click);
            // 
            // MenuItemPermisos
            // 
            this.MenuItemPermisos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSubItemUsuario,
            this.MenuSubItemRoles});
            this.MenuItemPermisos.Name = "MenuItemPermisos";
            this.MenuItemPermisos.Size = new System.Drawing.Size(67, 20);
            this.MenuItemPermisos.Text = "Permisos";
            // 
            // MenuSubItemUsuario
            // 
            this.MenuSubItemUsuario.Name = "MenuSubItemUsuario";
            this.MenuSubItemUsuario.Size = new System.Drawing.Size(180, 22);
            this.MenuSubItemUsuario.Text = "Usuarios";
            this.MenuSubItemUsuario.Click += new System.EventHandler(this.mantenimientoToolStripMenuItem1_Click);
            // 
            // MenuSubItemRoles
            // 
            this.MenuSubItemRoles.Name = "MenuSubItemRoles";
            this.MenuSubItemRoles.Size = new System.Drawing.Size(180, 22);
            this.MenuSubItemRoles.Text = "Roles";
            this.MenuSubItemRoles.Click += new System.EventHandler(this.rolesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem2.Text = "|";
            // 
            // UsuarioToolStripMenuItem
            // 
            this.UsuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.UsuarioToolStripMenuItem.Name = "UsuarioToolStripMenuItem";
            this.UsuarioToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.UsuarioToolStripMenuItem.Click += new System.EventHandler(this.UsuarioToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click_1);
            // 
            // ssPrincipal
            // 
            this.ssPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssConexion});
            this.ssPrincipal.Location = new System.Drawing.Point(0, 665);
            this.ssPrincipal.Name = "ssPrincipal";
            this.ssPrincipal.Size = new System.Drawing.Size(1382, 22);
            this.ssPrincipal.TabIndex = 7;
            this.ssPrincipal.Text = "statusStrip1";
            // 
            // tssConexion
            // 
            this.tssConexion.Name = "tssConexion";
            this.tssConexion.Size = new System.Drawing.Size(0, 17);
            // 
            // MenuItemLeasing
            // 
            this.MenuItemLeasing.Name = "MenuItemLeasing";
            this.MenuItemLeasing.Size = new System.Drawing.Size(59, 20);
            this.MenuItemLeasing.Text = "Leasing";
            this.MenuItemLeasing.Click += new System.EventHandler(this.MenuItemLeasing_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 687);
            this.Controls.Add(this.ssPrincipal);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SISCONT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ssPrincipal.ResumeLayout(false);
            this.ssPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPLE;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMantenimiento;
        private System.Windows.Forms.ToolStripMenuItem MenuSubItemProveedores;
        private System.Windows.Forms.ToolStripMenuItem MenuSubItemTipoDeCambio;
        private System.Windows.Forms.ToolStripMenuItem UsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuSubItemDetracción;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPermisos;
        private System.Windows.Forms.ToolStripMenuItem MenuSubItemUsuario;
        private System.Windows.Forms.ToolStripMenuItem MenuSubItemRoles;
        private System.Windows.Forms.StatusStrip ssPrincipal;
        private System.Windows.Forms.ToolStripStatusLabel tssConexion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuSubItemPlanContable;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLeasing;
    }
}

