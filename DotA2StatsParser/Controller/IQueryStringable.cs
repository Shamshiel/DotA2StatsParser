using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller
{
    interface IQueryStringable
    {
        /// <summary>
        /// Creates the name value collection.
        /// </summary>
        /// <returns>A NameValueCollection of all public properties with their values</returns>
        NameValueCollection CreateNameValueCollection();
    }
}
