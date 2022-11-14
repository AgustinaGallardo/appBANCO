using BancoBack.Negocio.Interfaz;
using BancoBack.Negocio.Implementacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using BancoBack.Dominio;

namespace FrontBanco
{
    public partial class FrmAltaTipoCuenta : Form
    {
        private IAplicacion gestor;
        private int aux = 0;
        private TipoCuenta nuevo;
        public FrmAltaTipoCuenta()
        {
            InitializeComponent();
            gestor = new Aplicacion();
            nuevo = new TipoCuenta();
        }

        private async void FrmAltaTipoCuenta_Load(object sender, EventArgs e)
        {
            await CargarComboAsync();
            txtEditar.Enabled = false;
            TxtProximotipo.Enabled = false;
            BtnCancelar.Enabled = false;
            button1.Enabled = false;
        }
         
        private async void button1_Click(object sender, EventArgs e)
        {
            if(txtEditar.Text != "")
            { 
                if (aux == 1)
                {
                    await ModificarTipo();
                    LimpiarFrm();
                }
                if (aux == 2)
                {
                    await NuevoTipo();
                    LimpiarFrm();
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el nombre de la cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private async Task NuevoTipo()
        {
            nuevo.IdTipo = Convert.ToInt32(TxtProximotipo.Text);
            nuevo.Tipo = txtEditar.Text;
            string Jbody = JsonConvert.SerializeObject(nuevo);
            string URL = "http://localhost:5200/alta/tipoCuenta";
            var result = await ClientSingleton.GetInstance().PostAsync(URL, Jbody);

            if (result.Equals("true"))
            {
                MessageBox.Show("Se inserto con exito", "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
            else
            {
                MessageBox.Show("No se pudo insertar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async Task CargarComboAsync()
        {
            string URL = "http://localhost:5200/tipocuenta";
            var result = await ClientSingleton.GetInstance().GetAsync(URL);
            var lstTiposCuestas = JsonConvert.DeserializeObject<List<TipoCuenta>>(result);
            //carga combo
            cboTipoCuenta.DataSource = lstTiposCuestas;
            cboTipoCuenta.DisplayMember = "Tipo";
            cboTipoCuenta.ValueMember = "IdTipo";
            cboTipoCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }

        private void cboTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoCuenta otipo = (TipoCuenta)cboTipoCuenta.SelectedItem;
            TxtProximotipo.Text = otipo.IdTipo.ToString();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {            
            txtEditar.Enabled = true;
            txtEditar.Focus();
            cboTipoCuenta.Enabled = false;
            BtnBorrar.Enabled = false;
            BtnNuevo.Enabled = false;
            BtnEditar.Enabled = false;
            BtnCancelar.Enabled = true;
            button1.Enabled = true;
            aux = 1;
            

        }

        private async Task ModificarTipo()
        {
            TipoCuenta tipo = new TipoCuenta();

            tipo.IdTipo = Convert.ToInt32(TxtProximotipo.Text);
            tipo.Tipo   = txtEditar.Text;

            string Jbody = JsonConvert.SerializeObject(tipo);
            string URL = "http://localhost:5200/tipocuenta";
            var result = await ClientSingleton.GetInstance().PutAsync(URL, Jbody);

            if (result.Equals("true"))
            {
                MessageBox.Show("Se modifico con exito!!!", "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("No se pudo modificar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private async void BtnBorrar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Esta seguro que desea borrar el Tipo de Cuenta", "CUIDADO", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                await BajaTipo();
                LimpiarFrm();
            }
            
        }

        private async Task BajaTipo()
        {            
            int IdTipo = Convert.ToInt32(TxtProximotipo.Text);            
            string Jbody = JsonConvert.SerializeObject(IdTipo);
            string URL = "http://localhost:5200/api/Banco/"+IdTipo;
            var result = await ClientSingleton.GetInstance().PutAsync(URL, Jbody);
            if (result.Equals("true"))
            {
                MessageBox.Show("Se elimino con exito!", "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("No se pudo eliminar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BtnNuevo_Click_1(object sender, EventArgs e)
        {
            cboTipoCuenta.Enabled = false;
            txtEditar.Enabled = true;
            TxtProximotipo.Text = gestor.ObtenerProximoTipoCuenta().ToString();/// este proximo lo tenemos que cargar por api
            cboTipoCuenta.Visible = false;
            BtnBorrar.Enabled = false;
            BtnEditar.Enabled = false;
            BtnNuevo.Enabled = false;
            BtnCancelar.Enabled = true;
            button1.Enabled = true;
            aux = 2;
            txtEditar.Focus();
            
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFrm();
            
        }

        private void LimpiarFrm()
        {
            BtnBorrar.Enabled = true;
            BtnNuevo.Enabled = true;
            BtnEditar.Enabled = true;
            txtEditar.Enabled = false;
            txtEditar.Text = "";
            cboTipoCuenta.SelectedIndex = 0;
            cboTipoCuenta.Visible = true;
            cboTipoCuenta.Enabled = true;
            button1.Enabled = false;
            BtnCancelar.Enabled = false;
            CargarComboAsync();

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }
    }
}
