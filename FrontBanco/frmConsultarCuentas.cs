using BancoBack.Dominio;
using BancoBack.Negocio.Implementacion;
using BancoBack.Negocio.Interfaz;
using Microsoft.VisualBasic.Logging;
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
    public partial class frmConsultarCuentas : Form
    {
        private int IdCliente;
        private IAplicacion gestor;
        public frmConsultarCuentas(int idCliente)
        {
            InitializeComponent();
            gestor = new Aplicacion();
            this.IdCliente = idCliente;
            txtApellido.Enabled = false;
            txtNombre.Enabled = false;
            txtDNI.Enabled = false;

        }

        private async void frmConsultarCuentas_Load(object sender, EventArgs e)
        {

            lblCliente.Text = "Cliente N° : " + IdCliente.ToString();

            Cliente cliente = gestor.ObtenerClientePorId(IdCliente);
            txtApellido.Text = cliente.Apellido;
            txtNombre.Text = cliente.Nombre;
            txtDNI.Text = cliente.Dni.ToString();
            foreach (Cuenta cuenta in cliente.lstCuentas)
            {
                dgvCuentas.Rows.Add(new object[]
                {
                    cuenta.CBU,
                    cuenta.Saldo,
                    cuenta.UltimoMovimiento,
                    cuenta.TipoCuenta.Tipo
                });
            }
            //await ObtenerCuentas();
        }

        //private async Task ObtenerCuentas() //el cliente nos llega null
        //{
        //    lblCliente.Text = "Cliente N° : " + IdCliente.ToString();
        //    string URL = "http://localhost:5200/clientesCuentas" + IdCliente;
        //    var result = await ClientSingleton.GetInstance().GetAsync(URL);
        //    var cliente = JsonConvert.DeserializeObject<Cliente>(result);
        //    txtApellido.Text = cliente.Apellido;
        //    txtNombre.Text = cliente.Nombre;
        //    txtDNI.Text = cliente.Dni.ToString();
        //    foreach (Cuenta cuenta in cliente.lstCuentas)
        //    {
        //            dgvCuentas.Rows.Add(new object[]
        //            {
        //            cuenta.CBU,
        //            cuenta.Saldo,
        //            cuenta.UltimoMovimiento,
        //            cuenta.TipoCuenta.Tipo
        //            });
        //    }

        //}

       

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }
    }
}
