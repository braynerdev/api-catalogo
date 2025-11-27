namespace APICatalogo.DTOs
{
    public class PaginatorResponseDTO
    {
        public int CurrentPag { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreview { get; set; }
        public bool HasNext { get; set; }
    }
}
