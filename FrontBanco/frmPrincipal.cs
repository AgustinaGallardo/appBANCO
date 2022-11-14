using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reporte;

namespace FrontBanco
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void altaClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frmAlta = new Form1();
            frmAlta.Show();
        }

        private void consultarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultasClientes frmConsultar = new frmConsultasClientes();
            frmConsultar.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que quiere salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }

        private void altaTipoDeCuentaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmAltaTipoCuenta frmAltaCuenta = new FrmAltaTipoCuenta();
            frmAltaCuenta.Show();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void reporteSaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReporte frmReporte = new FrmReporte();
            frmReporte.Show();
        }

        private void equiipoDeDesarrolloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcercaDe frmAcerca = new frmAcercaDe();
            frmAcerca.Show();
        }
    }
}
