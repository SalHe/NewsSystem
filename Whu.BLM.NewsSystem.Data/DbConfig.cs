using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Whu.BLM.NewsSystem.Data
{
    public class DbConfig
    {
        [JsonPropertyName("DbType")]
        public string DbType { get; init; }
        [JsonPropertyName("ConnectionString")]
        public string ConnectionString { get; set; }
        [JsonPropertyName("ServerVersion")]
        public string ServerVersion { get; set; }
    }
}