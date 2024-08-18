using SwiftApplicationAPI.Models;

namespace SwiftApplicationAPI.Services
{
    public interface ISwiftParserService
    {
        Task <MT799Model> Parser(string swiffContent);
    }
}
