using APICatalogo.Models;
using APICatalogo.Paginator.Conf;

namespace APICatalogo.Paginator.Categoria
{
    public class CategoriaFiterNome : PaginatorParamiters
    {
        public string? Nome { get; set; }
    }
}
