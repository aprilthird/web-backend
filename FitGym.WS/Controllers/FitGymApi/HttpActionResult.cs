using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FitGym.WS.Controllers.FitGymApi
{
    public class HttpActionResult<T> : IHttpActionResult
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _statusDescription;
        private readonly T _content;
        
        public HttpStatusCode StatusCode { get { return _statusCode; } }
        public T Content { get { return _content; } }

        public HttpActionResult(HttpStatusCode statusCode, string statusDescription, T content)
        {
            this._statusCode = statusCode;
            this._statusDescription = statusDescription;
            this._content = content;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(_statusCode)
            {
                Content = new ObjectContent<T>(_content, new JsonMediaTypeFormatter())
            };

            return Task.FromResult(response);
        }
    }
}