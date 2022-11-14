using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBack.Dominio
{
    public class Cuenta
    {
        public int CBU { get; set; }
        public double Saldo { get; set; }
        public DateTime UltimoMovimiento { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
       
        public Cuenta()
        {
            CBU = 0;
            Saldo = 0;
            UltimoMovimiento = DateTime.Today;
            TipoCuenta = new TipoCuenta();
           
        }
        public Cuenta(int cbu, double sal, DateTime ultimo, TipoCuenta tip)
        {
            CBU = cbu;
            Saldo = sal;
            UltimoMovimiento = ultimo;
            TipoCuenta = tip;
         
        }
        public override string ToString()
        {
            return "cbu: " + CBU + ", Saldo: " + Saldo + "$";
        }
    }

}

