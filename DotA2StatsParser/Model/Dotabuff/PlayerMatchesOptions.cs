using DotA2StatsParser.Controller;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    public class PlayerMatchesOptions : IQueryStringable
    {
        /// <summary>
        /// Gets or sets the hero.
        /// </summary>
        /// <value>
        /// The hero.
        /// </value>
        public Heroes Hero { get; set; }
        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public Modes Mode { get; set; }
        /// <summary>
        /// Gets or sets the skillbracket.
        /// </summary>
        /// <value>
        /// The skillbracket.
        /// </value>
        public Skillbrackets Skillbracket { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Types Type { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public Dates Date { get; set; }
        /// <summary>
        /// Gets or sets the faction.
        /// </summary>
        /// <value>
        /// The faction.
        /// </value>
        public Factions Faction { get; set; }
        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public Regions Region { get; set; }
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public Durations Duration { get; set; }


        /// <summary>
        /// Creates the name value collection.
        /// </summary>
        /// <returns>
        /// A NameValueCollection of all public properties with their values
        /// </returns>
        public NameValueCollection CreateNameValueCollection()
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            PropertyInfo[] propertyInfos = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                nameValueCollection.Add(propertyInfo.Name, propertyInfo.GetValue(this).ToString());
            }
            return nameValueCollection;
        }
    }
}
