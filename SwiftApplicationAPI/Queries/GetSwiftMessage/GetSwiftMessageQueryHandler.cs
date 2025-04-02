using System.IO;
using MediatR;
using SwiftApplicationAPI.Controllers;
using SwiftApplicationAPI.Models;
using SwiftApplicationAPI.Services;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SwiftApplicationAPI.Queries.GetSwiftMessage
{
    public record GetSwiftMessageQuery(IFormFile swiftFile) : IRequest<MT799Model>;

    public class GetSwiftMessageQueryHandler : IRequestHandler<GetSwiftMessageQuery, MT799Model>
    {
        private readonly ISwiftParserService swiftParserService;
        private readonly ILogger<GetSwiftMessageQueryHandler> logger;

        public GetSwiftMessageQueryHandler(ISwiftParserService swiftParserService, ILogger<GetSwiftMessageQueryHandler> logger)
        {
            this.swiftParserService = swiftParserService;
            this.logger = logger;
        }
        public async Task<MT799Model> Handle(GetSwiftMessageQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Reading through the SWIFT File");

            var swiftContent = new StringBuilder();
            using (var reader = new StreamReader(query.swiftFile.OpenReadStream()))
            {
                swiftContent.Append(reader.ReadToEnd());
            }

            logger.LogInformation("Reading completed succesfully.Now begging Parsing");

            var result = await swiftParserService.Parser(swiftContent.ToString());
            return result;
        }
    }
}
