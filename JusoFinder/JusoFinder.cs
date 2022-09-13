using JusoFinder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JusoFinder
{
    public class JusoFinder
    {
        private static readonly string TargetUrl = "https://business.juso.go.kr/addrlink/addrLinkApi.do";
        
        private RestRequest restRequest = new RestRequest();
        private JusoParser jusoParser = new JusoParser();
        private JusoParameterBuilder parameterBuilder = new JusoParameterBuilder();

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

            if (!result.common.errorCode.Equals("0"))
                throw new Exception(result.common.errorMessage);
                

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
                CurrentPage  = 1,
                ApiKey       = requestParameter.ApiKey,
                Keyword      = requestParameter.Keyword
            };
            
            var totalPages = GetTotalPageCount(requestParameter);

            List<Addr> addrs = new List<Addr>();

            for (int i = 1; i <= totalPages; i++)
            {
                tempRequestParameter.CurrentPage = i;
                var result = GetResult(tempRequestParameter);

                result.Juso.ForEach(x => addrs.Add(x));
            }

            return addrs;

        }
        public List<Addr> Find(RequestParameter requestParameter)
        {
            Result result = GetResult(requestParameter);

            return result.Juso;
        }
    }
}
