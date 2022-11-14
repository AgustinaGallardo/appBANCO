using Microsoft.Reporting.WinForms;
using System.Data;
using System.Data.SqlClient;
using BancoBack.Negocio.Interfaz;
using Microsoft.CodeAnalysis;
using BancoBack.Negocio.Implementacion;
using BancoBack.Dominio;

namespace Reporte
{
    public partial class FrmReporte : Form
    {
        private IAplicacion gestor;
        public FrmReporte()
        {
            InitializeComponent();
            gestor = new Aplicacion();
        }

        private void FrmReporte_Load(object sender, EventArgs e)
        {
            reportViewer1.RefreshReport();          
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            DataTable table = gestor.Reporte(dtpDesde.Value,dtpHasta.Value);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", table));
            reportViewer1.RefreshReport();
        }

  
    }
}