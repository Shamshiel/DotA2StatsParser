using DotA2StatsParser.Model.Dotabuff.Interfaces;
namespace DotA2StatsParser.Model.Dotabuff
{
    internal class LifetimeStat : ILifetimeStat
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the stat.
        /// </summary>
        /// <value>
        /// The stat.
        /// </value>
        public IStat Stat { get; internal set; }

        internal LifetimeStat()
        {
            
        }

        internal LifetimeStat(string name, string matches, string winRate)
        {
            this.Name = name;
            this.Stat = new Stat(matches, winRate);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Stat);
        }
    }
}
