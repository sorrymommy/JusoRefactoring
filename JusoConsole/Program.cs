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
            Result result = GetResponse(1, 100, "devU01TX0FVVEgyMDIyMDkxMzE0NTUzMzExMjk2OTI=", "중리");

            if (result.common.errorCode.Equals("0"))
            {
                foreach (Addr addr in result.Juso)
                {
                    Console.WriteLine($"{addr.roadAddrPart1} {addr.roadAddrPart2} {addr.jibunAddr}");
                }
            }
            Console.ReadKey();
        }

        public static Result GetResponse(int currentPage, int countPerPage, string apikey, string keyword)
        {
            string result = string.Empty;
            
            try
            {
                var targetURL = "https://business.juso.go.kr/addrlink/addrLinkApi.do";

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("currentPage"  , currentPage.ToString()  );
                nvc.Add("countPerPage" , countPerPage.ToString() );
                nvc.Add("resultType"   , "json"                  );
                nvc.Add("confmKey"     , apikey                  );
                nvc.Add("keyword"      , keyword                 );

                targetURL = $"{targetURL}?{string.Join("&", nvc.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(nvc[a])))}";

                using (WebClient client = new WebClient())
                {
                    //특정 요청 헤더값을 추가해준다. 
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                    using (Stream data = client.OpenRead(targetURL))
                    using (StreamReader reader = new StreamReader(data))
                    {
                        string s = reader.ReadToEnd();

                        JObject o = JObject.Parse(s);

                        return JsonConvert.DeserializeObject<Result>(o.SelectToken("results").ToString());

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }
    }
}
