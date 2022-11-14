using BackBanco.Dominio;
using BancoBack.Datos;
using BancoBack.Negocio.Implementacion;
using BancoBack.Negocio.Interfaz;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontBanco
{
    public partial class frmLogin : Form
    {
        private IAplicacion gestor;
        public frmLogin()
        {
            InitializeComponent();
            gestor = new Aplicacion();
        }

  

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            await IngresoLogin();
        }
        private async Task IngresoLogin()
        {
            string usuario = txtUsuario.Text;
            int pass = Convert.ToInt32(txtPass.Text);

            Login log = new Login(usuario, pass);
            string Jbody = JsonConvert.SerializeObject(log);
            string URL = "http://localhost:5200/login";
            var result = await ClientSingleton.GetInstance().PostAsync(URL, Jbody);

            if (result.Equals("1"))
            {
                frmPrincipal MenuPrincipal = new frmPrincipal();
                MenuPrincipal.ShowDialog();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña incorrecta", "ERROR", MessageBoxButtons.OK);
                return;
            }
           
        }




        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                btnIngresar_Click(sender, e);
            }
        }

        private void frmLogin_TextChanged(object sender, EventArgs e)
        {
            txtPass.Text = "";
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            txtPass.Text = "";
        }
    }
}
