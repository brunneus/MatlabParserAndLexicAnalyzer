using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatLabCompiler
{
    public class SimbolConverter
    {
        public static string Converter(string simbol)
        {
            if (simbol.Equals("=="))
                return "=";

            if(simbol.Equals("~="))
                return "<>";

            if (simbol.Equals("="))
                return "<-";

            return simbol;
        }
    }
}
