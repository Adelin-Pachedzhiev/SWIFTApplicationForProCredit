using WebApplication1.Data;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Services
{
    public class SwiftParserService : ISwiftParserService
    {
        public MT799Model Parser(string swiffContent)
        {
            var blocks = swiffContent.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x=> x.Trim())
                .ToList();

            var mtData = new MT799Model
            {
                BasicHeader = GetBlockContent(blocks, "1"),
                ApplicationHeader = GetBlockContent(blocks, "2"),
                UserHeader = GetBlockContent(blocks, "3"),
                Text = GetBlockContent(blocks, "4"),
                Tailers = GetBlockContent(blocks, "5")
            }; 
            return mtData;
        }

        private string GetBlockContent(List<string> blocks, string identifier)
        {
            var block = blocks
                .Where(x => x.StartsWith(identifier + ":"))
                .FirstOrDefault();

            //removing the identifier
            if (block != null)
            {
                return block.Substring(identifier.Length + 1).Trim();
            }

            return string.Empty;
        }
    }
}
