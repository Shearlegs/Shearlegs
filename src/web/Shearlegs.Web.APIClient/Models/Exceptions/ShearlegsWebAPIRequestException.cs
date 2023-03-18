using System;

namespace Shearlegs.Web.APIClient.Models.Exceptions
{
    public class ShearlegsWebAPIRequestException : Exception
    {
        public ShearlegsWebAPIRequestException(string message) : base(message)
        {

        }
    }
}
