using MediatR;
using SwiftApplicationAPI.Services;

namespace SwiftApplicationAPI.Commands;

public record SWIFTMessageDeletingCommand(int id) : IRequest<int>;

[AutoConstructor]
public partial class SWIFTMessageDeletingCommandHandler : IRequestHandler<SWIFTMessageDeletingCommand, int>
{
    private readonly ISwiftMessageRepository swiftMessageRepository;
    private readonly ILogger<SWIFTMessageRetrievingCommandHandler> logger;

    public async Task<int> Handle(SWIFTMessageDeletingCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Reading from table SWIFTMessage");

        var result = await swiftMessageRepository.DeleteById(command.id);

        logger.LogInformation($"Read messages :{result}");

        return result;
    }
}