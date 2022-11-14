using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBack.Dominio
{
    public class TipoCuenta
    {
        public int IdTipo { get; set; }
        public string Tipo { get; set; }

        public TipoCuenta(int id, string tip)
        {
            Tipo = tip;
            IdTipo = id;
        }
      
        public TipoCuenta()
        {
            IdTipo = 0;
            Tipo = "";            
        }
        public override string ToString()
        {
            return Tipo;
        }
    }
}
