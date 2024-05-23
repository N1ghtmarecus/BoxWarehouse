using Application.Mappings;
using AutoMapper;
using Domain.Entities.Cosmos;

namespace Application.Dto.Cosmos
{
    public class CosmosBoxDto : IMap
    {
        public string? ID { get; set; }
        public string? CutterID { get; set; }
        public int Fefco { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CosmosBox, CosmosBoxDto>();
            profile.CreateMap<CosmosBoxDto, CosmosBox>();
        }

    }
}
