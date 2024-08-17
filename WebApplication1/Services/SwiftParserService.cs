using System.Text.RegularExpressions;
using WebApplication1.Data;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Services
{
    public class SwiftParserService : ISwiftParserService
    {
        public async Task<MT799Model> Parser(string swiffContent)
        {
            var blockMatches = Regex.Matches(swiffContent, @"\{[0-9]+:[^{}]*?(?:\{[^{}]*\})*\}", RegexOptions.Singleline);
            var blocks = blockMatches.Select(x => x.Value).ToList();

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
                .Where(x => x.StartsWith('{'+identifier + ":"))
                .FirstOrDefault();

            if (block != null)
            {
                return block.Substring(identifier.Length + 1).Trim();
            }

            return string.Empty;
        }
    }
}
