using AlturCase.Core.Dto.Request.Product;
using AlturCase.Core.Entities;
using AutoMapper;

namespace AlturCase.Application.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductUpdateDto, ProductEntity>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //TODO: add useridentity update mapping here
        }
    }
}
