using AutoMapper;
using InventorySystem.Data.Entity;
using InventorySystem.Data.Model;
using InventorySystem.Web.ViewModel.Brand;
using InventorySystem.Web.ViewModel.Building;
using InventorySystem.Web.ViewModel.Series;

namespace InventorySystem.Web.ViewModel.MapperProfiles
{
    public class EditFormProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BuildingEntity, BuildingFormViewModel>()
                .ForMember(dest => dest.Brands, opt => opt.Ignore())
                .ForMember(dest => dest.Series, opt => opt.Ignore())
                .ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Mapper.Map<PriceModel>(src)));
            
            Mapper.CreateMap<BuildingFormViewModel, PriceModel>()
                .ForMember(dest => dest.Paid, opt => opt.MapFrom(src => src.PricePaid))
                .ForMember(dest => dest.CanadianDollar, opt => opt.MapFrom(src => src.PriceCanadianDollar))
                .ForMember(dest => dest.UsDollar, opt => opt.MapFrom(src => src.PriceUsDollar));

            Mapper.CreateMap<BrandEntity, BrandFormViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());

            Mapper.CreateMap<SeriesEntity, SeriesFormViewModel>()
                .ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
