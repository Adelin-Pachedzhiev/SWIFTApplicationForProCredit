using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwiftApplicationAPI.Commands;
using SwiftApplicationAPI.Models;
using SwiftApplicationAPI.Queries.GetSwiftMessage;

namespace SwiftApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SwiftController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<SwiftController> logger;

        public SwiftController(IMediator mediator,ILogger<SwiftController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<MT799Model> GetSwiftMessage(IFormFile swiftInput)
        {
            try
            {
                logger.LogInformation("Sending the GetSwiftMessageQuery");
                var result = await mediator.Send(new GetSwiftMessageQuery(swiftInput));
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in calling the method GetSwiftMessage", ex);
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<int> SWIFTMessageInserting(IFormFile swiftInput)
        {
            try
            {
                logger.LogInformation("Sending a SwiftMessageInsertingCommand");
                var result = await mediator.Send(new SWIFTMessageInsertingCommand(swiftInput));
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in calling the method SWIFTMessageInserting", ex);
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IEnumerable<MT799Model>> SWIFTMessage()
        {
            try
            {
                logger.LogInformation("Reading all the SWIFTMessages");
                var result = await mediator.Send(new SWIFTMessageRetrievingCommand());
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in calling the method SWIFTMessage", ex);
                throw new Exception(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<int> SWIFTMessage(int messageId) 
        {
            try
            {
                logger.LogInformation("Sending a SwiftMessageInsertingCommand");
                var result = await mediator.Send(new SWIFTMessageDeletingCommand(messageId));
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in calling the method SWIFTMessageInserting", ex);
                throw new Exception(ex.Message);
            }

        }
        
        [HttpPut]
        public async Task<int> SWIFTMessage(int messageId, IFormFile swiftInput) 
        {
            try
            {
                logger.LogInformation("Sending a SwiftMessageInsertingCommand");
                var result = await mediator.Send(new SWIFTMessageUpdatingCommand(messageId, swiftInput));
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong in calling the method SWIFTMessageInserting", ex);
                throw new Exception(ex.Message);
            }

        }
    }
}
