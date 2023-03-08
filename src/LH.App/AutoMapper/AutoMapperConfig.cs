﻿using AutoMapper;
using LH.App.ViewModels;
using LH.Business.Models;

namespace LH.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<RegistroPontoViewModel, RegistroPonto>().ReverseMap();
        }
    }
}
