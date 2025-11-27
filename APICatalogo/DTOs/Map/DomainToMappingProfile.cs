using APICatalogo.DTOs.Categoria;
using APICatalogo.DTOs.Produto;
using APICatalogo.DTOs.Roles;
using APICatalogo.Models;
using APICatalogo.Paginator.Conf;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.DTOs.Map
{
    public class DomainToMappingProfile: Profile
    {
        public DomainToMappingProfile()
        {
            CreateMap<Produtos, ProdutoRequestDTO>().ReverseMap();
            CreateMap<Produtos, ProdutoUpdateRequaestDTO>().ReverseMap();
            CreateMap<Produtos, ProdutoResponseDTO>().ReverseMap();
            CreateMap<Categorias, CategoriaResponseDTO>().ReverseMap();
            CreateMap<Categorias, CategoriaRequestDTO>().ReverseMap();
            CreateMap(typeof(PagdList<>), typeof(PagdList<>));
            CreateMap<IdentityRole, RolesResponseDTO>();
        }
    }
}
