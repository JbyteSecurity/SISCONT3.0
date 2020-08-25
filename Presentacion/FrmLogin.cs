using Negocios;
using System;
using System.Data;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmLogin : Form
    {
        private Usuario usuario = new Usuario();

        private Proveedor proveedor = new Proveedor();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Width = 367;
            this.Height = 338;
            txtUsuario.Focus();
            NetworkState();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void EnterClick(object sender, KeyPressEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            string user = txtUsuario.Text.ToLower();
            string contrasenia = txtContrasenia.Text;
            string ruc = txtRucEmpresa.Text;

            if (user != "" && contrasenia != "" && ruc != "")
            {

                DataTable dataTableLogin = new DataTable();
                DataTable empresa = new DataTable();

                empresa = proveedor.Show(ruc);

                contrasenia = usuario.GetSHA1(contrasenia);

                dataTableLogin = usuario.Login(user, contrasenia);

                if (dataTableLogin != null)
                {
                    if (empresa != null)
                    {
                        string userDB = dataTableLogin.Rows[0]["Usuario"].ToString();
                        string passDB = dataTableLogin.Rows[0]["Contrasenia"].ToString();
                        string nombreDB = dataTableLogin.Rows[0]["Nombre"].ToString();
                        int userIdDB = Convert.ToInt32(dataTableLogin.Rows[0]["IdUsuario"].ToString());

                        if (user.Equals(userDB) && contrasenia.Equals(passDB))
                        {
                            FrmPrincipal frmPrincipal = new FrmPrincipal();
                            frmPrincipal.username = userDB;
                            frmPrincipal.nombreUsuario = nombreDB;
                            frmPrincipal.idUsuario = userIdDB;
                            frmPrincipal.empresa = empresa;
                            frmPrincipal.Show();

                            this.Hide();
                        }
                    } else MessageBox.Show("Ruc invalido", "Inicio de Sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Usuario o Contraseña Incorrecto", "Inicio de Sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Usuario, Contraseña y Ruc son Obligarotios", "Inicio de Sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtUsuarioKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (txtUsuario.Text != "")
                {
                    if (txtContrasenia.Text != "")
                    {
                        if (txtRucEmpresa.Text != "")
                            Login();
                        else txtRucEmpresa.Focus();
                    }
                    else
                        txtContrasenia.Focus();
                }
                else txtUsuario.Focus();
            }
        }

        private void txtContraseniaKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (txtUsuario.Text != "")
                {
                    if (txtContrasenia.Text != "")
                    {
                        if (txtRucEmpresa.Text != "")
                            Login();
                        else txtRucEmpresa.Focus();
                    }
                    else
                        txtContrasenia.Focus();
                }
                else txtUsuario.Focus();
            }
        }
        
        private void txtRucEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (txtUsuario.Text != "")
                {
                    if (txtContrasenia.Text != "")
                    {
                        if (txtRucEmpresa.Text != "")
                            Login();
                        else txtRucEmpresa.Focus();
                    }
                    else
                        txtContrasenia.Focus();
                }
                else txtUsuario.Focus();
            }
        }

        private void NetworkState()
        {
            string Estado = "";
            System.Uri Url = new System.Uri("http://198.12.230.10");
            System.Net.WebRequest WebRequest;
            WebRequest = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse objetoResp;
            try
            {
                objetoResp = WebRequest.GetResponse();
                Estado = "";
                objetoResp.Close();
            }
            catch (Exception e)
            {
                Estado = "Verifique la conexión al servidor";
            }
            lblServerConnectStatus.Text = Estado;
            WebRequest = null;
        }

    }
}
