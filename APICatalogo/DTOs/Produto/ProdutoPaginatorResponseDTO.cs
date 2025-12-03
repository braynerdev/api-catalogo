namespace APICatalogo.DTOs.Produto
{
    public class ProdutoPaginatorResponseDTO
    {
        public PaginatorResponseDTO Paginator { get; set; }

        public IEnumerable<ProdutoResponseDTO> Items { get; set; }
    }
}
