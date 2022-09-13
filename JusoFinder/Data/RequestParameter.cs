using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JusoFinder.Data
{
    public class RequestParameter
    {
        public int CurrentPage { get; set; }
        public int CountPerPage { get; set; }
        public string ApiKey { get; set; }
        public string Keyword { get; set; }
    }
}
