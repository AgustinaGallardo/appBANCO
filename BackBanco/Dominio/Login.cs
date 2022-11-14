using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackBanco.Dominio
{
    public class Login
    {
        public string Usuario { get; set; }
        public int Pass { get; set; }

        public Login()
        {
            Usuario = string.Empty;
            Pass = 0;
        }

        public Login(string usuario, int pass)
        {
            Usuario = usuario;
            Pass = pass;
        }
    }
}
