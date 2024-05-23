using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace Domain.Entities.Cosmos
{
    [CosmosCollection("Boxes")]
    public class CosmosBox
    {
        [CosmosPartitionKey]
        [JsonProperty]
        public string? ID { get; set; }
        public string? CutterID { get; set; }
        public int Fefco { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
