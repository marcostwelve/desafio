using AutoMapper;
using Entities.Models;
using Entities.Models.Dtos;


namespace Repository.Mapper
{
    public class ConfigurationMapper : Profile
    {
        public ConfigurationMapper() 
        {
            CreateMap<DebtTitle, DebtTitleDto>().ReverseMap();
            CreateMap<DebtInstallment, DebtInstallmentDto>().ReverseMap();

        }
    }
}
