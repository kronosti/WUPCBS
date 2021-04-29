using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUPCBS.Class
{
    public partial class Resultado
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }

        public Resultado()
        {
            Codigo = 0;
            Descripcion = "";
        }

        public Resultado(long codigo)
        {
            Codigo = (int)codigo;
            Descripcion = "";
        }

        public Resultado(long codigo, string descipcion)
        {
            Codigo = (int)codigo;
            Descripcion = descipcion;
        }
    }
}
