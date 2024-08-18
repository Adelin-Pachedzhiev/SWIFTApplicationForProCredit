using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace SwiftApplicationAPI.Data
{
    public class SWIFTMessagesDataContext
    {
        private readonly IConfiguration configuration;

        public SWIFTMessagesDataContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(configuration.GetConnectionString("SwiftDatabaseSqlLiteString"));
        }

        public async Task Init()
        { 
        
            using var connection =  CreateConnection();
            var sql = """
                CREATE TABLE IF NOT EXISTS 
                SWIFTMessage (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    BasicHeader TEXT,
                    ApplicationHeader TEXT,
                    UserHeader TEXT,
                    Text TEXT,
                    Tailers TEXT
                );
            """;
            await connection.ExecuteAsync(sql);
        }

    }
}
