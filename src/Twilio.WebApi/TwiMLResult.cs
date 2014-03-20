using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Routing;
using System.Xml.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace Twilio.TwiML.WebApi
{
	public class TwiMLResult : IHttpActionResult 
	{
		XDocument data;
        HttpRequestMessage request;

		public TwiMLResult(HttpRequestMessage requestMessage, string twiml)
		{
			data = XDocument.Parse(twiml);
            request = requestMessage;
		}

		public TwiMLResult(HttpRequestMessage requestMessage, XDocument twiml)
		{
            request = requestMessage;
			data = twiml;
		}

        public TwiMLResult(HttpRequestMessage requestMessage, TwilioResponse response)
		{
			if (response != null)
				data = response.ToXDocument();
            request = requestMessage;
        }

        public System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            var response = request.CreateResponse(HttpStatusCode.OK);

            if (data == null)
            {
                data = new XDocument(new XElement("Response"));
            }

            response.Content = new StringContent(data.ToString());
            response.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/xml");

            return Task.FromResult(response);
        }
    }
}
