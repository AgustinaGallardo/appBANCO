using BackBanco.Dominio;
using BancoBack.Datos;
using BancoBack.Dominio;
using BancoBack.Negocio.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBack.Negocio.Implementacion
{
    public class Aplicacion : IAplicacion
    {
        private IDaoCliente dao;
        public Aplicacion()
        {
            dao = new DaoCliente();
        }

        public List<Cliente> ObtenerCliente()
        {
            return dao.ObtenerCliente();
        }

        public Cliente ObtenerClientePorId(int id)
        {
            return dao.ObtenerClientePorId(id);
        }

        public int ObtenerProximo()
        {
            return dao.ObtenerProximo();
        }

        public int ObtenerProximoTipoCuenta()
        {
            return dao.ObtenerProximoTipoCuenta();
        }

        public List<TipoCuenta> ObtenerTipos()
        {
           return dao.ObtenerTipos();
        }
        public bool Save(Cliente oCliente)
        {
            return dao.Save(oCliente);
        }
        public bool Modificar(TipoCuenta tipo)
        {
            return dao.Modificar(tipo);
        }
        public bool Baja(int id)
        {
            return dao.Baja(id);
        }

        public DataTable Reporte(DateTime fechaDesde, DateTime fechaHasta)
        {
            return dao.Reporte(fechaDesde,fechaHasta);
        }

        public bool SaveTipoCuenta(TipoCuenta oTipo)
        {
            return dao.SaveTipoCuenta(oTipo);
        }

        public Cliente ObtenerClientesCuentas(int id)
        {
           return dao.ObtenerClientesCuentas(id);
        }

        public int Login(Login log)
        {
            return dao.Login(log);
        }
    }
}
