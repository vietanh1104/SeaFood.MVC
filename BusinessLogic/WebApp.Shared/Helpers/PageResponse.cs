namespace WebApp.Shared.Helpers
{
    public class PageResponse<T>
    {
        public int total { get; set; }
        public int totalPages { get; set; } = 0;
        public int page { get; set; }
        public int pageSize { get; set; }
        
        public IEnumerable<T> data { get; set; } = new List<T>();
    }
}
