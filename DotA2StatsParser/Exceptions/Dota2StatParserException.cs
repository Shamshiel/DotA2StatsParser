using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Exceptions
{
    class Dota2StatParserException : Exception
    {
        internal Dota2StatParserException(string message)
            : base(message)
        {

        }

        internal Dota2StatParserException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
