using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftApplicationAPI.Services;
using System.Text;
using SwiftApplicationAPI.Models;
using SwiftApplicationAPI.Services;

namespace SwiftApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SwiftController : ControllerBase
    {
        private readonly ISwiftParserService swiftParserService;
        private readonly ISwiftMessageRepository swiftMessageRepository;
        private readonly ILogger<SwiftController> logger;

        public SwiftController(ISwiftParserService swiftParserService, ISwiftMessageRepository swiftMessageRepository, ILogger<SwiftController> logger)
        {
            this.swiftParserService = swiftParserService;
            this.swiftMessageRepository = swiftMessageRepository;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<MT799Model> GetSwiftMessage(IFormFile swiftInput)
        {
            try
            {
                logger.LogInformation("Reading through the SWIFT File");

                var swiftContent = new StringBuilder();
                using (var reader = new StreamReader(swiftInput.OpenReadStream()))
                {
                    swiftContent.Append(reader.ReadToEnd());
                }

                logger.LogInformation("Reading completed succesfully.Now begging Parsing");

                var result = await swiftParserService.Parser(swiftContent.ToString());
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
                logger.LogInformation("Reading through the SWIFT File");

                var swiftContent = new StringBuilder();
                using (var reader = new StreamReader(swiftInput.OpenReadStream()))
                {
                    swiftContent.Append(reader.ReadToEnd());
                }

                logger.LogInformation("Reading completed succesfully.Now begging Parsing");

                var parsedResult = await swiftParserService.Parser(swiftContent.ToString());

                logger.LogInformation("Inserting in the table");
                var result = await swiftMessageRepository.Create(parsedResult);
                logger.LogInformation($"Inserting completed, numbers of row affected :{result}");

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
