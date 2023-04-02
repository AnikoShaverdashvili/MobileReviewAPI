using AutoMapper;
using MobileReviewAPI.DTO;
using MobileReviewAPI.Models;

namespace MobileReviewAPI.AutoMapper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Mobile, MobileDto>();
        }
    }
}
