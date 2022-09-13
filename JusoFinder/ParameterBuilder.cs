using JusoFinder.Data;
using System.Collections.Specialized;

namespace JusoFinder
{
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
}
