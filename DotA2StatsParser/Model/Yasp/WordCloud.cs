using DotA2StatsParser.Model.Yasp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Yasp
{
    internal class WordCloud : IWordCloud
    {
        public string Word { get; internal set; }

        public int Count { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Word, Count);
        }
    }
}
