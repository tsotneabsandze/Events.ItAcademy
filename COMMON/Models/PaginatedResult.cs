namespace Common.Models
{
    public class PaginatedResult<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public T Content { get; set; }
    }
}