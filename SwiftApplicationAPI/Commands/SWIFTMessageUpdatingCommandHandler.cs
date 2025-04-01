
using System.Text;
using MediatR;
using SwiftApplicationAPI.Models;
using SwiftApplicationAPI.Services;

namespace SwiftApplicationAPI.Commands;
public record SWIFTMessageUpdatingCommand(int id, IFormFile file) : IRequest<int>;

[AutoConstructor]
public partial class SWIFTMessageUpdatingCommandHandler : IRequestHandler<SWIFTMessageUpdatingCommand, int>
{
    private readonly ISwiftParserService swiftParserService;
    private readonly ISwiftMessageRepository swiftMessageRepository;
    private readonly ILogger<SWIFTMessageRetrievingCommandHandler> logger;


    public async Task<int> Handle(SWIFTMessageUpdatingCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Reading from table SWIFTMessage");
        var swiftContent = new StringBuilder();
        using (var reader = new StreamReader(command.file.OpenReadStream()))
        {
            swiftContent.Append(reader.ReadToEnd());
        }

        logger.LogInformation("Reading completed succesfully.Now begging Parsing");

        var parsedResult = await swiftParserService.Parser(swiftContent.ToString());

        var result = await swiftMessageRepository.UpdateById(command.id, parsedResult);

        logger.LogInformation($"Read messages :{result}");

        return result;
    }
}