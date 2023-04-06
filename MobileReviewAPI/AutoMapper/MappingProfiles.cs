using AutoMapper;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Mobile, MobileDto>();
            CreateMap<MobileDto, Mobile>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Owner, OwnerDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<OwnerDto, Owner>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDtoNoReview, Reviewer>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
        }
    }
}
