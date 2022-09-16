using JusoFinder.Data;

namespace JusoFinder
{
    public interface IJusoParser
    {
        Result Parse(string content);
    }
}
