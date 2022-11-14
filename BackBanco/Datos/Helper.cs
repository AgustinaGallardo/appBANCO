using BackBanco.Datos;
using BancoBack.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BancoBack.Datos
{
    public class Helper
    {
        private static Helper instancia;
        private string cnnString;
        private SqlConnection cnn;

        public Helper()
        {
            //sofi//Data Source=DESKTOP-THG4KVC\SQLEXPRESS;Initial Catalog=AMBbancoProgII;Integrated Security=True
            //gabi//Data Source=DESKTOP-ART4IB1\SQLEXPRESS;Initial Catalog=AMBbancoProgII;Integrated Security=True
            //agus//Data Source=DESKTOP-DBB4CIB\SQLEXPRESS;Initial Catalog=AMBbancoProgII;Integrated Security=True
            //nico//Data Source=LOCALHOST;Initial Catalog=AMBbancoProgII;Integrated Security=True
            cnnString = @"Data Source=LOCALHOST;Initial Catalog=AMBbancoProgII;Integrated Security=True";
            cnn = new SqlConnection(cnnString);
        }
        public static Helper ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new Helper();
            return instancia;
        }
        public int ObtenerProximo(string sp_nombre, string nombreOutPut)
        {
            cnn.Open();
            SqlCommand cmdProx = new SqlCommand();
            cmdProx.Connection = cnn;
            cmdProx.CommandText=sp_nombre;
            cmdProx.CommandType=CommandType.StoredProcedure;
            SqlParameter OutPut = new SqlParameter();
            OutPut.ParameterName = nombreOutPut;
            OutPut.Direction = ParameterDirection.Output;
            OutPut.DbType=DbType.Int32;
            cmdProx.Parameters.Add(OutPut);
            cmdProx.ExecuteNonQuery();
            cnn.Close();
            return (int)OutPut.Value;
        }
        public DataTable ObtenerDatosSql(string sp_nombre)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            DataTable tabla = new DataTable();
            SqlCommand cmdDatos = new SqlCommand();
            cnn.Open();
            cmdDatos.Connection = cnn;
            cmdDatos.CommandText=sp_nombre;
            cmdDatos.CommandType=CommandType.StoredProcedure;
            tabla.Load(cmdDatos.ExecuteReader());
            cnn.Close();
            return tabla;
        }


        public DataTable ConsultarSql(string sp_nombre,List<Parametro>values)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            DataTable tabla = new DataTable();
            SqlCommand cmdCombo = new SqlCommand();
            cnn.Open();
            cmdCombo.Connection = cnn;
            cmdCombo.CommandText=sp_nombre;
            cmdCombo.CommandType=CommandType.StoredProcedure;
            if (values != null)
            {
                foreach (Parametro par in values)
                {
                    cmdCombo.Parameters.AddWithValue(par.Clave, par.Valor);
                }
            }
            tabla.Load(cmdCombo.ExecuteReader());
            cnn.Close();
            return tabla;
        }

        public bool ConfirmarCliente(Cliente c, string sp_maestro, string sp_detalle, string pOut_nombre, List<Parametro>values)
        {
            bool ok = true;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlTransaction t = null;
            try
            {
                SqlCommand cmdMaestro = new SqlCommand();
                cnn.Open();
                t = cnn.BeginTransaction();

                cmdMaestro.Connection=cnn;
                cmdMaestro.Transaction=t;                
                cmdMaestro.CommandText=sp_maestro;
                cmdMaestro.CommandType=CommandType.StoredProcedure;

                if (values != null)
                {
                    foreach (Parametro par in values)
                    {
                        cmdMaestro.Parameters.AddWithValue(par.Clave, par.Valor);
                    }
                }

                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = pOut_nombre;
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;

                cmdMaestro.Parameters.Add(pOut);
                cmdMaestro.ExecuteNonQuery();

                int cliente_nro = (int)pOut.Value;

                foreach (Cuenta ct in c.lstCuentas)
                {
                    SqlCommand cmdD = new SqlCommand();
                    cmdD.Connection = cnn;
                    cmdD.Transaction = t;
                    cmdD.CommandType = CommandType.StoredProcedure;
                    cmdD.CommandText = sp_detalle;

                    cmdD.Parameters.AddWithValue("@cbu", ct.CBU);
                    cmdD.Parameters.AddWithValue("@id_tipoCuenta", ct.TipoCuenta.IdTipo);
                    cmdD.Parameters.AddWithValue("@saldo", ct.Saldo);
                    cmdD.Parameters.AddWithValue("@ultimoMovimiento", ct.UltimoMovimiento);
                    cmdD.Parameters.AddWithValue("@cod_cliente", cliente_nro);

                    cmdD.ExecuteNonQuery();
                }
                t.Commit();
            }
            catch (Exception)
            {
                if (t != null)
                {
                    t.Rollback();
                    ok=false;
                }
            }
            finally
            {
                if (cnn !=null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return ok;
        }
        public int EjecutarSql(string sp_nombre, List<Parametro> values)
        {
            int afectadas = 0;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmdCombo = new SqlCommand();
            cnn.Open();
            cmdCombo.Connection = cnn;
            cmdCombo.CommandText = sp_nombre;
            cmdCombo.CommandType = CommandType.StoredProcedure;
            if (values != null)
            {
                foreach (Parametro par in values)
                {
                    cmdCombo.Parameters.AddWithValue(par.Clave, par.Valor);
                }
            }
            afectadas = cmdCombo.ExecuteNonQuery();
            cnn.Close();
            return afectadas;
        }
        public int Login(string sp_nombre, List<Parametro> values, string pOut_nombre)
        {
            cnn.Open();
            SqlCommand cmdProx = new SqlCommand();
            cmdProx.Connection = cnn;
            cmdProx.CommandText = sp_nombre;
            cmdProx.CommandType = CommandType.StoredProcedure;
            if (values != null)
            {
                foreach (Parametro par in values)
                {
                    cmdProx.Parameters.AddWithValue(par.Clave, par.Valor);
                }
            }
            SqlParameter OutPut = new SqlParameter();
            OutPut.ParameterName = pOut_nombre;
            OutPut.Direction = ParameterDirection.Output;
            OutPut.DbType = DbType.Int32;
            cmdProx.Parameters.Add(OutPut);
            cmdProx.ExecuteNonQuery();
            cnn.Close();
            return (int)OutPut.Value;
        }
    }
}
