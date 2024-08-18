using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SwiftController : ControllerBase
    {
        private readonly ISwiftParserService swiftParserService;
        private readonly ILogger<SwiftController> logger;

        public SwiftController(ISwiftParserService swiftParserService, ILogger<SwiftController> logger)
        {
            this.swiftParserService = swiftParserService;
            logger = logger;
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
            catch(Exception ex)
            {
                logger.LogError("Something went wrong in calling the method GetSwiftMessage", ex);
                throw new Exception(ex.Message);
            }
        }
        }
    }
