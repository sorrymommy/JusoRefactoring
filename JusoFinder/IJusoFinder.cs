using JusoFinder.Data;
using System.Collections.Generic;

namespace JusoFinder
{
    public interface IJusoFinder
    {
        List<Addr> FindAll(RequestParameter requestParameter);
        List<Addr> Find(RequestParameter requestParameter);
    }
}
