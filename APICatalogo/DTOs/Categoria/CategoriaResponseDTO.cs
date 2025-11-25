using APICatalogo.Models;
using APICatalogo.Validator;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Categoria
{
    public class CategoriaResponseDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? ImagemUrl { get; set; }
    }
}
