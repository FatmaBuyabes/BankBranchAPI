namespace BankBranchAPI.Models
{
    public class PageListResult<T>
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecords { get; set; }

    }

    public static class PagingExtensions
    {
        public static PageListResult<T> ToPageList<T>(this IQueryable<T> query, int pageNumber = 1, int pageSize = 50)
        {
            int totalRecords = query.Count();
            var pagedData = query.Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();

            return new PageListResult<T>
            {
                Data = pagedData,
                CurrentPage = pageNumber,
                TotalPage = (int)Math.Ceiling((double)totalRecords / pageSize),
                TotalRecords = totalRecords
            };
        }
    }
}
