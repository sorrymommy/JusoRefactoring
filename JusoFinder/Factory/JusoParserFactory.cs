using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JusoFinder
{
    public class JusoParserFactory
    {
        public static IJusoParser GetJusoParser(JusoParserType jusoParser)
        {
            switch (jusoParser)
            {
                case JusoParserType.JsonJusoParser:
                    return new JusoJsonParser();
                default:
                    return new JusoJsonParser();
            }
        }
    }
}
