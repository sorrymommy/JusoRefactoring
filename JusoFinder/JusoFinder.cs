using JusoFinder.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JusoFinder
{
    public class JusoFinder : IJusoFinder
    {
        private const string CURRENT_PAGE = "currentPage";
        private const string COUNT_PER_PAGE = "countPerPage";
        private const string RESULT_TYPE = "resultType";
        private const string CONFIRM_KEY = "confmKey";
        private const string KEYWORD = "keyword";
        private const string JSON = "json";
        private const string TARGET_URL = "https://business.juso.go.kr/addrlink/addrLinkApi.do";
        
        private IJusoParser jusoParser => JusoParserFactory.GetJusoParser(JusoParserType.JsonJusoParser);

        private Result GetResult(RequestParameter requestParameter)
        {
            var client = new RestClient(TARGET_URL);
            var request = new RestRequest()
                    .AddParameter(CURRENT_PAGE, requestParameter.CurrentPage)
                    .AddParameter(COUNT_PER_PAGE, requestParameter.CountPerPage)
                    .AddParameter(RESULT_TYPE, JSON)
                    .AddParameter(CONFIRM_KEY, requestParameter.ApiKey)
                    .AddParameter(KEYWORD, requestParameter.Keyword);

            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"{response.StatusCode}{response.StatusDescription}");

            Result result = jusoParser.Parse(response.Content);

            if (!result.common.errorCode.Equals("0"))
                throw new Exception(result.common.errorMessage);

            if (!result.common.errorCode.Equals("0"))
                throw new Exception(result.common.errorMessage);


            return result;
        }


        private int GetTotalPageCount(RequestParameter requestParameter)
        {
            Result result = GetResult(new RequestParameter() 
            { 
                CountPerPage = 1, 
                CurrentPage = 1,
                ApiKey = requestParameter.ApiKey,
                Keyword = requestParameter.Keyword
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
                CountPerPage = 1,
                CurrentPage = 1,
                ApiKey = requestParameter.ApiKey,
                Keyword = requestParameter.Keyword
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
