using Dapper;
using SwiftApplicationAPI.Data;
using System.Data;
using SwiftApplicationAPI.Models;

namespace SwiftApplicationAPI.Services
{
    public class SwiftMessageRepository : ISwiftMessageRepository
    {
        private readonly SWIFTMessagesDataContext dataContext;
        private readonly ILogger<SwiftMessageRepository> logger;

        public SwiftMessageRepository(SWIFTMessagesDataContext dataContext, ILogger<SwiftMessageRepository> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task<int> Create(MT799Model mT799Model)
        {
            logger.LogInformation("Creating connection with the dbContext");
            using var connection = dataContext.CreateConnection();
            var sql = """
                          INSERT INTO SWIFTMessage (BasicHeader, ApplicationHeader, UserHeader, Text, Tailers)
                          VALUES (@BasicHeader, @ApplicationHeader, @UserHeader, @Text, @Tailers)
                      """;
            return await connection.ExecuteAsync(sql, mT799Model);
        }

        public async Task<IEnumerable<MT799Model>> GetAll()
        {
            logger.LogInformation("Creating connection with the dbContext");
            using var connection = dataContext.CreateConnection();
            var sql = """
                         SELECT Id, BasicHeader, ApplicationHeader, UserHeader, Text, Tailers
                         FROM SWIFTMessage
                      """;
            return await connection.QueryAsync<MT799Model>(sql);
        }

        public async Task<int> DeleteById(int id)
        {
            using var connection = dataContext.CreateConnection();

            return await connection.ExecuteAsync($"DELETE FROM SWIFTMessage WHERE Id = {id}");
        }

        public async Task<int> UpdateById(int id, MT799Model mT799Model)
        {
            using var connection = dataContext.CreateConnection();
            var sql = """
                          UPDATE SWIFTMessage
                          SET BasicHeader = @BasicHeader, ApplicationHeader = @ApplicationHeader, UserHeader = @UserHeader, Text = @Text, Tailers = @Tailers
                          WHERE Id = @id
                      """;
            return await connection.ExecuteAsync(sql, new { id, mT799Model });
        }
    }
}