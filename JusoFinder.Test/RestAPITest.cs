using NUnit.Framework;
using RestSharp;

namespace JusoFinder.Test
{
    public class RestAPITest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void JusoAPI_Call_OK()
        {
            var client = new RestClient("https://business.juso.go.kr/addrlink/addrLinkApi.do");
            
            var request = new RestRequest()
                    .AddParameter("currentPage", 1)
                    .AddParameter("countPerPage", 100)
                    .AddParameter("resultType", "json")
                    .AddParameter("confmKey", "devU01TX0FVVEgyMDIyMDkxMzE0NTUzMzExMjk2OTI=")
                    .AddParameter("keyword", "서울특별시 강남구 역삼동");

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}