using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Exceptions
{
    internal class PlayerNotFoundException : Dota2StatParserException
    {
        internal PlayerNotFoundException(string message)
            : base(message)
        {

        }

        internal PlayerNotFoundException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
