using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBack.Dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public List<Cuenta> lstCuentas { get; set; }

        public Cliente()
        {
            IdCliente=0;
            Nombre = "";
            Apellido = "";
            Dni = 0;
            lstCuentas = new List<Cuenta>();
        }
        public Cliente( string nom, string ape, int d, List<Cuenta> Cuentas)
        {
            Nombre = nom;
            Apellido = ape;
            Dni = d;
            lstCuentas = Cuentas;
        }

        public Cliente(int id, string nom, string ape, int d, List<Cuenta> Cuentas)
        {
            IdCliente = id;
            Nombre = nom;
            Apellido = ape;
            Dni = d;
            lstCuentas = Cuentas;
        }
        public void AgregarCuenta(Cuenta nueva)
        {
            lstCuentas.Add(nueva);
        }
        public void EliminarCuenta(int cbu)
        {
            lstCuentas.RemoveAt(cbu);
        }
        public override string ToString()
        {
            return "Cliente: " + Nombre + ", " + Apellido + " dni: " +
                Dni;
        }
    }
}
