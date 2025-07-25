using AutoMapper;
using Nzwalks_api.Models.DTO.Requestes;
using Nzwalks_api.Models.Domain;
using Nzwalks_api.Models.DTO;
namespace Nzwalks_api.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>()
                .ReverseMap();
            CreateMap<AddRegionRequestDto, 
                Region>()
                .ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>()
                .ReverseMap();
            CreateMap<Walk, AddWalkRequestDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();
            CreateMap<Walk, WalkDto>()
                .ReverseMap();
            CreateMap<Diffculty, DiffcultyDto>().ReverseMap();
            CreateMap<ImageUploadRequestDto, Image>()
                .ForMember(
                    dest => dest.FileExtension,
                    opt => opt.MapFrom(src => Path.GetExtension(src.File.FileName))
                )
                .ForMember(
                    dest => dest.FileSizeInBytes,
                    opt => opt.MapFrom(src => src.File.Length)
                )
                .ForMember(
                dest=>dest.Id,
                opt => opt.MapFrom(src =>Guid.NewGuid()));
        }
    }
}
