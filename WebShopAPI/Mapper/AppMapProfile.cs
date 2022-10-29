using AutoMapper;
using LibData.Entities;
using WebShopAPI.Models;

namespace WebShopAPI.Mapper
{
    public class AppMapProfile: Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryEntity, CategoryItemViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(cat => $"/images/{cat.Image}"));
        }
    }
}
