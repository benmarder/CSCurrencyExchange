using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class ExchangeException : Exception
    {
        public ExchangeException(string eMsg):base(eMsg) {}
    }
}
