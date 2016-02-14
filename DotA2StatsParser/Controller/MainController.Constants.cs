using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller
{
    internal partial class MainController
    {
        internal const string HTML_A_TAG = "a";
        internal const string HTML_IMG_TAG = "img";
        internal const string HTML_SPAN_TAG = "span";
        internal const string HTML_DIV_TAG = "div";

        internal const string HTML_ATTRIBUTE_SRC = "src";
        internal const string HTML_ATTRIBUTE_DATETIME = "datetime";
        internal const string HTML_ATTRIBUTE_HREF = "href";
        internal const string HTML_ATTRIBUTE_CLASS = "class";

        internal const string HTML_TABLE_TR = "tr";
        internal const string HTML_TABLE_TH = "th";

        internal const string TIMESPAN_FORMAT = "hhmmss";

        internal const string UNDEFINED = "Undefined";

        internal const string MAPPING_PARSER = "Parser";
        internal const string MAPPING_DOTABUFF = "Dotabuff";
        internal const string MAPPING_NAME = "Name";
    }
}
