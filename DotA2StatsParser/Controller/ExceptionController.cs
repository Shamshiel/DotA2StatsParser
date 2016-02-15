using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Exceptions;

namespace DotA2StatsParser.Controller
{
    internal class ExceptionController
    {
        internal ExceptionController()
        {
            
        }

        internal void HandleWebException(string customMessage, WebException webException)
        {
            if (webException.Status == WebExceptionStatus.ProtocolError && webException.Response != null)
            {
                HttpWebResponse errorResponse = (HttpWebResponse)webException.Response;

                if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new PlayerNotFoundException(customMessage, webException);
                }
            }
        }
    }
}
