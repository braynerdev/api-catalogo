using APICatalogo.Paginator.Conf;

namespace APICatalogo.Paginator.Categoria
{
    public class CategoriasPaginator : PaginatorParamiters
    {
        public string? Name {  get; init; }

        public CategoriasPaginator(string? name)
        {
            Name = name;
        }
    }
}
