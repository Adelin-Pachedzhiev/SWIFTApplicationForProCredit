
using MediatR;
using SwiftApplicationAPI.Models;
using SwiftApplicationAPI.Services;

namespace SwiftApplicationAPI.Commands;
public record SWIFTMessageRetrievingCommand() : IRequest<IEnumerable<MT799Model>>;

[AutoConstructor]
public partial class SWIFTMessageRetrievingCommandHandler : IRequestHandler<SWIFTMessageRetrievingCommand, IEnumerable<MT799Model>>
{

    private readonly ISwiftMessageRepository swiftMessageRepository;
    private readonly ILogger<SWIFTMessageRetrievingCommandHandler> logger;


    public async Task<IEnumerable<MT799Model>> Handle(SWIFTMessageRetrievingCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Reading from table SWIFTMessage");

        var result = await swiftMessageRepository.GetAll();

        logger.LogInformation($"Read messages :{result}");

        return result;
    }
}