using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace APICatalogo.Paginator.Conf
{
    public class PagdList<T> : List<T> where T : class
    {
        public int CurrentPag { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreview => CurrentPag > 1;
        public bool HasNext => CurrentPag < TotalPages;


        public PagdList(List<T> items,int count, int numberPage, int sizePage)
        {
            TotalCount = count;
            PageSize = sizePage;
            CurrentPag = numberPage;
            TotalPages = (int)Math.Ceiling(count / (double)sizePage);

            AddRange(items);
        }

        public static PagdList<T> ToPagdList(IQueryable<T> source, Expression<Func<T, int>> predicate, string orderBy, int pageNumber, int pageSize)
        {
            var count = source.Count();

            if (orderBy.Equals("DESC", StringComparison.OrdinalIgnoreCase))
            {
                source = source.OrderByDescending(predicate);
            }
            else
            {
                source = source.OrderBy(predicate);
            }

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagdList<T>(items, count, pageNumber, pageSize);
        }
    }
}
