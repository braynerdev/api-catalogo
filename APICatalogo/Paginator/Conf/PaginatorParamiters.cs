namespace APICatalogo.Paginator.Conf
{
    public abstract class PaginatorParamiters
    {
        const int MaxSize = 99;
        public int NumberPage { get; set; } = 1;
        private int _pageSize = MaxSize;
        public string OrderBy { get; set; } = "ASC";

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxSize ? MaxSize : value;
        }
    }
}
