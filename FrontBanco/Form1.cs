using BancoBack.Datos;
using BancoBack.Dominio;
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
    public partial class Form1 : Form
    {
        private IAplicacion gestor;
        private Cliente nuevo;
        public Form1()
        {
            InitializeComponent();
            gestor = new Aplicacion();
            nuevo= new Cliente();
        }

        private async void AltaCliente_Load(object sender, EventArgs e)
        {
            ObtenerProximo();
            await CargarComboAsync();
            Limpiar();
        }

        private void Limpiar()
        {
            txtApellido.Text="";
            txtNombre.Text="";
            txtDni.Text="";
            txtcbu.Text="";
            txtSaldo.Text="";
            dtpUltimoMov.Value=DateTime.Today;
            dgvClientes.Rows.Clear();
        }
        private async Task CargarComboAsync()
        {            
            string URL = "http://localhost:5200/tipocuenta";
            var result =  await ClientSingleton.GetInstance().GetAsync(URL);           
            var lstTiposCuestas =  JsonConvert.DeserializeObject<List<TipoCuenta>>(result);
            //carga combo
            cboTipoCuenta.DataSource = lstTiposCuestas;
            cboTipoCuenta.DisplayMember = "Tipo";
            cboTipoCuenta.ValueMember = "IdTipo";            
            cboTipoCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void ObtenerProximo()
        {
            int Next = gestor.ObtenerProximo();

            if (Next > 0)
            {
                lblProximoCliente.Text = "Cliente N°: " + Next.ToString();
            }
            else
            {
                MessageBox.Show("No se puede obtener el proximo cliente", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(txtSaldo.Text == "")
            {
                MessageBox.Show("Ingrese el saldo de la cuenta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(dtpUltimoMov.Value != DateTime.Today)
            {
                MessageBox.Show("Ingrese una fecha valida", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtcbu.Text == "")
            {
                MessageBox.Show("Ingrese el CBU de la cuenta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach(DataGridViewRow item in dgvClientes.Rows)
            {
                if(item.Cells["colTipo"].Value.ToString().Equals(cboTipoCuenta.Text))
                {
                    MessageBox.Show("El tipo de cuenta: " + cboTipoCuenta.Text + " ya esta cargado", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }               
            }
            TipoCuenta tp = (TipoCuenta)cboTipoCuenta.SelectedItem;

            double saldo = Convert.ToDouble (txtSaldo.Text);
            int cbu = Convert.ToInt32(txtcbu.Text);
            DateTime fecha = Convert.ToDateTime(dtpUltimoMov.Value);

            Cuenta cuenta = new Cuenta(cbu,saldo,fecha,tp);
            nuevo.AgregarCuenta(cuenta);

            dgvClientes.Rows.Add(cuenta.CBU, cuenta.TipoCuenta.Tipo, cuenta.Saldo, cuenta.UltimoMovimiento);

        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            
            if (txtApellido.Text == "")
            {
                MessageBox.Show("Ingrese el apellido", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtDni.Text == "")
            {
                MessageBox.Show("Ingrese el DNI", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await GuardarClienteAsync();
        }

        private async Task GuardarClienteAsync()
        {

            nuevo.Apellido = txtApellido.Text;
            nuevo.Nombre = txtNombre.Text;
            nuevo.Dni = Convert.ToInt32(txtDni.Text);
            string Jbody = JsonConvert.SerializeObject(nuevo);
            string URL = "http://localhost:5200/cliente";
            var result = await ClientSingleton.GetInstance().PostAsync(URL, Jbody);

            if (result.Equals("true"))
            {
                MessageBox.Show("Se inserto con exito", "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No se pudo insertar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvClientes.CurrentCell.ColumnIndex == 4)
            {
                nuevo.EliminarCuenta(dgvClientes.CurrentRow.Index);
                dgvClientes.Rows.Remove(dgvClientes.CurrentRow);                
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }

        private void cboTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
