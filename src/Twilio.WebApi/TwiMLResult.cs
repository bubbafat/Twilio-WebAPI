using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;

namespace Twilio.TwiML.WebApi
{
	public class TwiMLResult : IHttpActionResult 
	{
		readonly XDocument _data;
        readonly HttpRequestMessage _request;

		public TwiMLResult(HttpRequestMessage requestMessage, string twiml)
            : this(requestMessage, XDocument.Parse(twiml))
		{
		}

		public TwiMLResult(HttpRequestMessage requestMessage, XDocument twiml)
		{
            _request = requestMessage;
		    _data = twiml ?? new XDocument(new XElement("Response"));
		}

        public TwiMLResult(HttpRequestMessage requestMessage, TwilioResponse response)
        {
            _data = response != null 
                ? response.ToXDocument() 
                : new XDocument(new XElement("Response"));

            _request = requestMessage;
        }

	    public Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(HttpStatusCode.OK);

            response.Content = new StringContent(_data.ToString());
            response.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/xml");

            return Task.FromResult(response);
        }
    }
}
