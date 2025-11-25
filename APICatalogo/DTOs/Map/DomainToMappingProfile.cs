using APICatalogo.DTOs.Categoria;
using APICatalogo.DTOs.Produto;
using APICatalogo.Models;
using AutoMapper;

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
        }
    }
}
