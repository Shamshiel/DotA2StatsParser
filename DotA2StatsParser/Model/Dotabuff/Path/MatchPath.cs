using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Path
{
    internal class MatchPath
    {
        internal string Id { get; set; }
        internal string Hero { get; set; }
        internal string Result { get; set; }
        internal string TimeAgo { get; set; }
        internal string Type { get; set; }
        internal string Mode { get; set; }
        internal string Skillbracket { get; set; }
        internal string Duration { get; set; }
        internal string Kda { get; set; }
        internal string IdAttribute { get; set; }
        internal string IdReplace { get; set; }
    }
}
