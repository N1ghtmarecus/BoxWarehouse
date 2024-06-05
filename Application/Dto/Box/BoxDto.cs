using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto
{
    public class BoxDto : IMap
    {
        public int CutterID { get; set; }
        public int Fefco { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime CreationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Box, BoxDto>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.Created));
        }
    }
}
