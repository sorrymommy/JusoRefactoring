using JusoFinder.Data;
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

namespace JusoFinder
{
    public class JusoFinder
    {
        private static readonly string TargetUrl = "https://business.juso.go.kr/addrlink/addrLinkApi.do";
        
        private RestRequest restRequest = new RestRequest();
        private JusoParser jusoParser = new JusoParser();
        private ParameterBuilder parameterBuilder = new ParameterBuilder();

        private string GetTargetUrlWithQueryString(RequestParameter requestParameter)
        {
            var queryString = parameterBuilder.GetQueryString(requestParameter);
            var url = $"{TargetUrl}?{queryString}";

            return url;
        }

        private Result GetResult(RequestParameter requestParameter)
        {
            var targetUrlWithQueryString = GetTargetUrlWithQueryString(requestParameter);
            var responseString = restRequest.GetResponse(targetUrlWithQueryString);

            Result result = jusoParser.Parse(responseString);

            return result;
        }


        private int GetTotalPageCount(RequestParameter requestParameter)
        {
            Result result = GetResult(new RequestParameter() 
            { 
                CountPerPage = 1, 
                CurrentPage  = 1,
                ApiKey       = requestParameter.ApiKey, 
                Keyword      = requestParameter.Keyword
            });
            
            var countPerPage = 100;
            var totalPages = 1;

            if (result.common.totalCount > 0)
            {
                totalPages = result.common.totalCount / countPerPage;
                totalPages += (result.common.totalCount % countPerPage) > 0 ? 1 : 0;
            }

            return totalPages;
        }

        public List<Addr> FindAll(RequestParameter requestParameter)
        {
            RequestParameter tempRequestParameter = new RequestParameter()
            {
                CountPerPage = 100,
                CurrentPage = 1,
                ApiKey = requestParameter.ApiKey,
                Keyword = requestParameter.Keyword
            };
            
            var totalPages = GetTotalPageCount(requestParameter);

            List<Addr> addrs = new List<Addr>();

            for (int i = 1; i <= totalPages; i++)
            {
                tempRequestParameter.CurrentPage = i;
                var result = Find(tempRequestParameter);

                result.ForEach(x => addrs.Add(x));
            }

            return addrs;

        }
        public List<Addr> Find(RequestParameter requestParameter)
        {
            Result result = GetResult(requestParameter);

            return result.Juso;
        }
    }

    public class ParameterBuilder
    {
        public string GetQueryString(RequestParameter requestParameter)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("currentPage"  , requestParameter.CurrentPage.ToString() );
            nvc.Add("countPerPage" , requestParameter.CountPerPage.ToString());
            nvc.Add("resultType"   , "json"                                  );
            nvc.Add("confmKey"     , requestParameter.ApiKey                 );
            nvc.Add("keyword"      , requestParameter.Keyword                );

            return $"{nvc.GetQueryString()}";

        }

    }
    public class JusoParser
    {
        public Result Parse(string content)
        {
            JObject o = JObject.Parse(content);

            return JsonConvert.DeserializeObject<Result>(o.SelectToken("results").ToString());
        }
    }
    
    public class RestRequest
    {
        public string GetResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            {
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
