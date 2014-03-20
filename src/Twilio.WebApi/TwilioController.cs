using System.Web.Http;

namespace Twilio.TwiML.WebApi
{
    /// <summary>
    /// Extends the standard base controller to simplify returning a TwiML response
    /// </summary>
    public class TwilioController : ApiController
    {
        /// <summary>
        /// Returns a property formatted TwiML response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public TwiMLResult TwiML(TwilioResponse response)
        {
            return new TwiMLResult(Request, response);
        }
    }
}
