using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JusoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string result = string.Empty;
            try
            {
                var targetURL = "https://business.juso.go.kr/addrlink/addrLinkApi.do";

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("currentPage", "1");
                nvc.Add("countPerPage", "100");
                nvc.Add("resultType", "json");
                nvc.Add("confmKey", "devU01TX0FVVEgyMDIyMDkwNjE2MDU0MzExMjk1NzE");
                nvc.Add("keyword", "중리");

                nvc.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(nvc[a])
                WebClient client = new WebClient();

                //특정 요청 헤더값을 추가해준다. 
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                using (Stream data = client.OpenRead(targetURL))
                {
                    using (StreamReader reader = new StreamReader(data))
                    {
                        string s = reader.ReadToEnd();
                        result = s;

                        reader.Close();
                        data.Close();
                    }
                }

                JObject o = JObject.Parse(result);

                var obj = JsonConvert.DeserializeObject<Result>(o.SelectToken("results").ToString());


            }
            catch (Exception e)
            {
                //통신 실패시 처리로직
                Console.WriteLine(e.ToString());
            }

        }
    }
}
