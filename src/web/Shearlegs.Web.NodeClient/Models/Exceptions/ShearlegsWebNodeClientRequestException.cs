using System;
using System.Net;

namespace Shearlegs.Web.NodeClient.Models.Exceptions
{
    public class ShearlegsWebNodeClientRequestException : Exception
    {
        public HttpStatusCode? StatusCode { get; set; }

        public ShearlegsWebNodeClientRequestException(string message, HttpStatusCode? statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}