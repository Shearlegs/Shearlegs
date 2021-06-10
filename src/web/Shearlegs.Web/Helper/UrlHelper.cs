using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Shearlegs.Web.Helpers
{
    public class UrlHelper
    {
        public static string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            newQueryString.Remove(key);

            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? string.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }
    }
}
