using AutoMapper;
using LH.App.ViewModels;
using LH.Business.Models;

namespace LH.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RegistroPontoViewModel, RegistroPonto>().ReverseMap();
        }
    }
}
