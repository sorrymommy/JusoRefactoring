using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JusoFinder;
using JusoFinder.Data;

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
            }
            catch (Exception e)
                //통신 실패시 처리로직
                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
        }

        public List<Addr> FindAll(string apikey, string keyword)
        {
            var tempCountPerPage = 100;
            var totalPages = GetTotalPageCount(apikey, keyword);
            
            List<Addr> addrs = new List<Addr>();

            for (int i = 1; i <= totalPages; i++)
            {
                var result = Find(i, tempCountPerPage, apikey, keyword);
                
                result.ForEach(x => addrs.Add(x));
            }

            return addrs;

        }
        public List<Addr> Find(int currentPage, int countPerPage, string apikey, string keyword)
        {
            Result result = GetResponse(currentPage, countPerPage, apikey, keyword);

            return result.Juso;
        }
    }
}
