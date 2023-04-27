using System;
using System.Net;

namespace Shearlegs.Web.APIClient.Models.Exceptions
{
    public class ShearlegsWebAPIRequestException : Exception
    {
        public HttpStatusCode? StatusCode { get; set; }

        public ShearlegsWebAPIRequestException(string message, HttpStatusCode? statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
