using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Yasp.Interfaces
{
    public interface IWordCloud
    {
        string Word { get; }
        int Count { get; }
    }
}
