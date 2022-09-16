using JusoFinder.Data;
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
        public void Find_�۵�����Ȯ��_�˻���_�߸��Ե�ĳ��()
        {
            List<Addr> addrs = jusoFinder.Find(new Data.RequestParameter()
            {
                ApiKey = "devU01TX0FVVEgyMDIyMDkxMzE0NTUzMzExMjk2OTI=",
                CountPerPage = 100,
                CurrentPage = 1,
                Keyword = "�߸��Ե�ĳ��"
            });
            
            Assert.IsTrue(addrs.Count > 0);
        }
        
        [Test]
        public void FindAll_�۵�����Ȯ��_�˻���_�߸��Ե�ĳ��()
        {
            List<Addr> addrs = jusoFinder.Find(new Data.RequestParameter()
            {
                ApiKey = "devU01TX0FVVEgyMDIyMDkxMzE0NTUzMzExMjk2OTI=",
                CountPerPage = 100,
                CurrentPage = 1,
                Keyword = "�߸��Ե�ĳ��"
            });

            Assert.IsTrue(addrs.Count > 0);
        }
    }
}