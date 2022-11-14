using BackBanco.Dominio;
using BancoBack.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBack.Datos
{
    public interface IDaoCliente
    {
        int ObtenerProximo();
        int ObtenerProximoTipoCuenta();
        List<TipoCuenta> ObtenerTipos();
        bool Save(Cliente oCliente);
        List<Cliente> ObtenerCliente();
        Cliente ObtenerClientePorId(int id);
        bool Modificar(TipoCuenta tipo);
        bool Baja(int id);
        DataTable Reporte(DateTime fechaDesde, DateTime fechaHasta);
        bool SaveTipoCuenta(TipoCuenta oTipo);
        Cliente ObtenerClientesCuentas(int id);
        int Login(Login log);

    }
}
