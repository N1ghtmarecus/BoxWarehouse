using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto
{
    public class PictureDto : IMap
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public byte[]? Image { get; set; }

        public bool IsMain { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Picture, PictureDto>();
            profile.CreateMap<PictureDto, Picture>();
        }
    }
}
