using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ISwiftParserService
    {
        Task <MT799Model> Parser(string swiffContent);
    }
}
