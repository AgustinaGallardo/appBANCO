using BackBanco.Dominio;
using BancoBack.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBack.Negocio.Interfaz
{
    public interface IAplicacion
    {
        int ObtenerProximo();
        int ObtenerProximoTipoCuenta();
        List<TipoCuenta> ObtenerTipos();
        List<Cliente> ObtenerCliente();
        bool Save(Cliente oCliente);
        Cliente ObtenerClientePorId(int id);
        bool Modificar(TipoCuenta tipo);
        bool Baja(int id);
        DataTable Reporte(DateTime fechaDesde, DateTime fechaHasta);
        bool SaveTipoCuenta(TipoCuenta oTipo);
        Cliente ObtenerClientesCuentas(int id);
        int Login(Login log);
    }
}
