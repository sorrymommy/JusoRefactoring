using JusoFinder.Data;
using System.Collections.Specialized;

namespace JusoFinder
{
    public class JusoParameterBuilder
    {
        private const string CURRENT_PAGE   = "currentPage";
        private const string COUNT_PER_PAGE = "countPerPage";
        private const string RESULT_TYPE    = "resultType";
        private const string CONFIRM_KEY    = "confmKey";
        private const string KEYWORD        = "keyword";
        private const string JSON           = "json";

        public string GetQueryString(RequestParameter requestParameter)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(CURRENT_PAGE   , requestParameter.CurrentPage.ToString() );
            nvc.Add(COUNT_PER_PAGE , requestParameter.CountPerPage.ToString());
            nvc.Add(RESULT_TYPE    , JSON                                    );
            nvc.Add(CONFIRM_KEY    , requestParameter.ApiKey                 );
            nvc.Add(KEYWORD        , requestParameter.Keyword                );

            return $"{nvc.GetQueryString()}";

        }

    }
}
