using BackBanco.Datos;
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
    public class DaoCliente : IDaoCliente
    {
        public List<Cliente> ObtenerCliente()
        {
            List<Cliente> lstClientes = new List<Cliente>();
            string sp_nombre = "sp_clientes_datos";
            List<Cuenta> lstCuentas = new List<Cuenta>();

            DataTable td = Helper.ObtenerInstancia().ObtenerDatosSql(sp_nombre);

            foreach (DataRow dr in td.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.IdCliente = Convert.ToInt32(dr["id_cliente"].ToString());
                cliente.Nombre = dr["nombre"].ToString();
                cliente.Apellido = dr["apellido"].ToString();
                cliente.Dni = Convert.ToInt32(dr["dni"].ToString());

                lstClientes.Add(cliente);
            }
            return lstClientes;
        }
        public Cliente ObtenerClientePorId(int id)
        {
            Cliente cliente = new Cliente();
            string sp_nombre = "sp_cuentas_clientes";
            List<Parametro> lstParametros = new List<Parametro>();
            lstParametros.Add(new Parametro ("@id_cliente",id));   

            DataTable tb = Helper.ObtenerInstancia().ConsultarSql(sp_nombre,lstParametros);
            bool primero = true;

            foreach (DataRow dr in tb.Rows)
            {
                if(primero)
                {
                    cliente.Nombre = dr["nombre"].ToString();
                    cliente.Apellido = dr["apellido"].ToString();
                    cliente.Dni =Convert.ToInt32(dr["dni"].ToString());
                    primero = false;
                }
                int cbu = Convert.ToInt32(dr["cbu"].ToString());
                double saldo = Convert.ToDouble(dr["saldo"].ToString());
                DateTime ultimoMovimiento = Convert.ToDateTime(dr["ultimoMovimiento"].ToString());
                int id_tipoCuenta = Convert.ToInt32(dr["id_tipoCuenta"].ToString());
                string tipo = dr["tipo"].ToString();

                TipoCuenta tipoCuenta = new TipoCuenta(id_tipoCuenta, tipo);
                Cuenta cuenta = new Cuenta(cbu, saldo, ultimoMovimiento, tipoCuenta);

                cliente.AgregarCuenta(cuenta);
            }
            return cliente;
        }
        public int ObtenerProximo()
        {
            string sp_nombre = "ProximoCliente";
            string OutPut = "@next";
            return Helper.ObtenerInstancia().ObtenerProximo(sp_nombre, OutPut);
        }
        public int ObtenerProximoTipoCuenta()
        {
            string sp_nombre = "ProximoTipoCuenta";
            string OutPut = "@next";
            return Helper.ObtenerInstancia().ObtenerProximo(sp_nombre, OutPut);
        }
        public List<TipoCuenta> ObtenerTipos()
        {
            List<TipoCuenta> lst = new List<TipoCuenta>();
            string sp_nombre = "sp_ConsultarCuentas";

            DataTable td = Helper.ObtenerInstancia().ObtenerDatosSql(sp_nombre);

            foreach (DataRow dr in td.Rows)
            {
                int id = Convert.ToInt32(dr["id_tipoCuenta"].ToString());
                string nombre = dr["nombre"].ToString();

                TipoCuenta aux = new TipoCuenta(id, nombre);
                lst.Add(aux);
            }
            return lst;
        }
        public bool Save(Cliente oCliente)
        {
            string sp_maestro = "insertCliente";
            string sp_detalle = "insertCuenta";
            string pOut_nombre = "@cod_cliente";
            List<Parametro> lstParametros = new List<Parametro>();
            lstParametros.Add(new Parametro("@apellido", oCliente.Apellido));
            lstParametros.Add(new Parametro("@nombre", oCliente.Nombre));
            lstParametros.Add(new Parametro("@dni", oCliente.Dni));

            return Helper.ObtenerInstancia().ConfirmarCliente(oCliente, sp_maestro, sp_detalle, pOut_nombre, lstParametros);
        }
        public bool Modificar(TipoCuenta tipo)
        {
            string sp_nombre = "sp_update_modificar_tipos";
            List<Parametro> lstParametros = new List<Parametro>();
            lstParametros.Add(new Parametro("@id", tipo.IdTipo));
            lstParametros.Add(new Parametro("@nombre", tipo.Tipo));
            int afectadas = Helper.ObtenerInstancia().EjecutarSql(sp_nombre, lstParametros);
            return afectadas > 0;
        }
        public bool Baja(int id)
        {
            string sp_nombre = "sp_update_baja_tipos";
            List<Parametro> lstParametros = new List<Parametro>();
            lstParametros.Add(new Parametro("@id", id));
            int afectadas = Helper.ObtenerInstancia().EjecutarSql(sp_nombre, lstParametros);
            return afectadas > 0;
        }

        public DataTable Reporte(DateTime fechaDesde, DateTime fechaHasta)
        {
            string sp_nombre = "sp_saldos_cuentas";
            List<Parametro> lstParametros = new List<Parametro>();
            lstParametros.Add(new Parametro("@fechaDesde", fechaDesde));
            lstParametros.Add(new Parametro("@fechaHasta", fechaHasta));
            DataTable table = Helper.ObtenerInstancia().ConsultarSql(sp_nombre, lstParametros);
            return table;
        }

        public bool SaveTipoCuenta(TipoCuenta oTipo)
        {
            string sp_nombre = "sp_insert_tipo";
            List<Parametro> lstParametros = new List<Parametro>();
            lstParametros.Add(new Parametro("@id_tipoCuenta", oTipo.IdTipo));
            lstParametros.Add(new Parametro("@nombre", oTipo.Tipo));
            int afectadas = Helper.ObtenerInstancia().EjecutarSql(sp_nombre, lstParametros);
            return afectadas > 0;
        }

        public Cliente ObtenerClientesCuentas(int id)
        {
            Cliente cliente = new Cliente();
            List<Cliente> lstClientes = new List<Cliente>();
            string sp_nombre = "sp_cuentas_clientes_api";
            List<Parametro> lstParametros = new List<Parametro>();
            lstParametros.Add(new Parametro("@id_cliente", id));

            DataTable tb = Helper.ObtenerInstancia().ConsultarSql(sp_nombre, lstParametros);
            bool primero = true;

            foreach (DataRow dr in tb.Rows)
            {
                if (primero)
                {
                    cliente.IdCliente = Convert.ToInt32(dr["id_cliente"].ToString());
                    cliente.Nombre = dr["nombre"].ToString();
                    cliente.Apellido = dr["apellido"].ToString();
                    cliente.Dni = Convert.ToInt32(dr["dni"].ToString());
                    primero = false;
                }
                int cbu = Convert.ToInt32(dr["cbu"].ToString());
                double saldo = Convert.ToDouble(dr["saldo"].ToString());
                DateTime ultimoMovimiento = Convert.ToDateTime(dr["ultimoMovimiento"].ToString());
                int id_tipoCuenta = Convert.ToInt32(dr["id_tipoCuenta"].ToString());
                string tipo = dr["tipo"].ToString();

                TipoCuenta tipoCuenta = new TipoCuenta(id_tipoCuenta, tipo);
                Cuenta cuenta = new Cuenta(cbu, saldo, ultimoMovimiento, tipoCuenta);

                cliente.AgregarCuenta(cuenta);
            }
            return cliente;
        }

        public int Login(Login log)
        {
            string sp_nombre = "sp_login";
            List<Parametro> lstParametros = new List<Parametro>();
            lstParametros.Add(new Parametro("@usuario", log.Usuario));
            lstParametros.Add(new Parametro("@pass", log.Pass));
            string pOut = "@resultado";
            int afectadas = Helper.ObtenerInstancia().Login(sp_nombre, lstParametros, pOut);
            return afectadas;
        }
    }
}
