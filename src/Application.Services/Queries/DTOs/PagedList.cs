namespace Application.Services.Queries.DTOs
{
    public class PagedList<T>
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public List<T> Items { get; set; }
    }
}
