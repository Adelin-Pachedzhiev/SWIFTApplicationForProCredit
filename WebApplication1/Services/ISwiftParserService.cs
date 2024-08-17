using WebApplication1.Data;

namespace WebApplication1.Services
{
    public interface ISwiftParserService
    {
        MT799Model Parser(string swiffContent);
    }
}
