using DotA2StatsParser.Controller.Dotabuff;
using DotA2StatsParser.Controller.Yasp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller
{
    internal partial class MainController
    {
        /// <summary>
        /// Gets the dotabuff mapping controller.
        /// </summary>
        /// <value>
        /// The dotabuff mapping controller.
        /// </value>
        internal DotabuffMappingController DotabuffMappingController { get; private set; }

        /// <summary>
        /// Gets the yasp mapping controller.
        /// </summary>
        /// <value>
        /// The yasp mapping controller.
        /// </value>
        internal YaspMappingController YaspMappingController { get; private set; }

        /// <summary>
        /// Gets the HTML document controller.
        /// </summary>
        /// <value>
        /// The HTML document controller.
        /// </value>
        internal HtmlDocumentController HtmlDocumentController { get; private set; }
        /// <summary>
        /// Gets the query string controller.
        /// </summary>
        /// <value>
        /// The query string controller.
        /// </value>
        internal QueryStringController QueryStringController { get; private set; }

        /// <summary>
        /// Gets the hero controller.
        /// </summary>
        /// <value>
        /// The hero controller.
        /// </value>
        internal HeroController HeroController { get; private set; }
        /// <summary>
        /// Gets the item controller.
        /// </summary>
        /// <value>
        /// The item controller.
        /// </value>
        internal ItemController ItemController { get; private set; }
        /// <summary>
        /// Gets the player controller.
        /// </summary>
        /// <value>
        /// The player controller.
        /// </value>
        internal PlayerController PlayerController { get; private set; }
        /// <summary>
        /// Gets the word cloud controller.
        /// </summary>
        /// <value>
        /// The word cloud controller.
        /// </value>
        internal WordCloudController WordCloudController { get; private set; }
    }
}
