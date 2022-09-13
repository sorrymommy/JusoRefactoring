using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JusoFinder
{
    public static class NameValueCollectionExtension
    {
        public static string GetQueryString(this NameValueCollection nvc)
        {
            return string.Join("&", nvc.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(nvc[a])));
        }
    }
}
