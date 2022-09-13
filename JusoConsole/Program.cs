using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JusoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Addr> addrs = (new JusoFinder()).Find(1, 100, "devU01TX0FVVEgyMDIyMDkxMzE0NTUzMzExMjk2OTI=", "중리");

            foreach (Addr addr in addrs)
            {
                Console.WriteLine($"{addr.roadAddrPart1} {addr.roadAddrPart2} {addr.jibunAddr}");
            }
            
            Console.ReadKey();
        }
    }

    public class JusoFinder
    {
        private string GetUrlWithQueryString(int currentPage, int countPerPage, string apikey, string keyword)
        {
            var targetURL = "https://business.juso.go.kr/addrlink/addrLinkApi.do";

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("currentPage", currentPage.ToString());
            nvc.Add("countPerPage", countPerPage.ToString());
            nvc.Add("resultType", "json");
            nvc.Add("confmKey", apikey);
            nvc.Add("keyword", keyword);

            return $"{targetURL}?{string.Join("&", nvc.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(nvc[a])))}";

        }

        private string GetWebResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            {
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8)){
                    return reader.ReadToEnd();

                }
            }
        }
        public List<Addr> Find(int currentPage, int countPerPage, string apikey, string keyword)
        {
            try
            {
                var targetURL = GetUrlWithQueryString(currentPage, countPerPage, apikey, keyword);

                var result = GetWebResponse(targetURL);

                JObject o = JObject.Parse(result);

                return JsonConvert.DeserializeObject<Result>(o.SelectToken("results").ToString()).Juso;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }
    }
}
