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
    public partial class frmConsultasClientes : Form
    {
        private IAplicacion  gestor;
        public frmConsultasClientes()
        {
            InitializeComponent();
            gestor = new Aplicacion();
        }
        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            await ObtenerClientes();
        }
        private async Task ObtenerClientes()
        {
            string URL = "http://localhost:5200/clientes";
            var result = await ClientSingleton.GetInstance().GetAsync(URL);
            var lstClientes = JsonConvert.DeserializeObject<List<Cliente>>(result);
            foreach (Cliente cliente in lstClientes)
            {
                dgvCliente.Rows.Add(new object[]
                {
                    cliente.IdCliente,
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.Dni
                });
            }
        }

        private void dgvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCliente.CurrentCell.ColumnIndex == 4)
            {
                int id = int.Parse(dgvCliente.CurrentRow.Cells["colId"].Value.ToString());
                new frmConsultarCuentas(id).ShowDialog();
            }
        }
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }
    }
}
