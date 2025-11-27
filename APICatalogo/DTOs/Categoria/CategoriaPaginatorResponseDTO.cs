namespace APICatalogo.DTOs.Categoria
{
    public class CategoriaPaginatorResponseDTO
    {
        public PaginatorResponseDTO Paginator { get; set; }

        public IEnumerable<CategoriaResponseDTO> Items { get; set; }
    }
}
