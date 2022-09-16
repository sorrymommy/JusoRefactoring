using NUnit.Framework;
using System.Collections.Generic;

namespace JusoFinder.Test
{
    public class JusoFinderTest
    {
        private IJusoFinder jusoFinder;
        [SetUp]
        public void Setup()
        {
            jusoFinder = new JusoFinder();
        }

        [Test]
        public void Find_작동정상확인_검색어_중리롯데캐슬()
        {
            List<Addr> addrs = jusoFinder.Find(new Data.RequestParameter()
            {
                ApiKey = "devU01TX0FVVEgyMDIyMDkxMzE0NTUzMzExMjk2OTI=",
                CountPerPage = 100,
                CurrentPage = 1,
                Keyword = "중리롯데캐슬"
            });
            
            Assert.IsTrue(addrs.Count > 0);
        }
        
        [Test]
        public void FindAll_작동정상확인_검색어_중리롯데캐슬()
        {
            List<Addr> addrs = jusoFinder.Find(new Data.RequestParameter()
            {
                ApiKey = "devU01TX0FVVEgyMDIyMDkxMzE0NTUzMzExMjk2OTI=",
                CountPerPage = 100,
                CurrentPage = 1,
                Keyword = "중리롯데캐슬"
            });

            Assert.IsTrue(addrs.Count > 0);
        }
    }
}