using JusoFinder;
using JusoFinder.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JusoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var jusoFinder = new JusoFinder.JusoFinder();
            var result = jusoFinder.Find(new RequestParameter()
            {
                CountPerPage = 100,
                CurrentPage = 1,
                ApiKey = "devU01TX0FVVEgyMDIyMDkxMzE0NTUzMzExMjk2OTI=",
                Keyword = "서울특별시 강남구 역삼동"
            });

            foreach (Addr addr in result)
                Console.WriteLine($"{addr.roadAddrPart1} {addr.roadAddrPart2} {addr.jibunAddr}");

            Console.ReadKey();
        }
    }
}
