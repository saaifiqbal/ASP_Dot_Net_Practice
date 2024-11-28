using asp_net_ecommerce_web_api.DTO;
using asp_net_ecommerce_web_api.Models;
using AutoMapper;

namespace asp_net_ecommerce_web_api.Profiles
{
    public class CategoryProfile: Profile {
        public CategoryProfile(){
            CreateMap<Category, ReadCategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<UpdateCreateDto, Category>();
        }
    }
}