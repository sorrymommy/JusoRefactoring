using JusoFinder.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JusoFinder
{

    public class JusoJsonParser: IJusoParser
    {
        public Result Parse(string content)
        {
            JObject o = JObject.Parse(content);

            return JsonConvert.DeserializeObject<Result>(o.SelectToken("results").ToString());
        }
    }
}
