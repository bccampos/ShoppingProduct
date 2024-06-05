namespace bruno.Klir.Application.Product.Queries
{
    public record PagedResult<T>
    {
        public PagedResult()
        {
            List = new List<T>();
        }

        public IEnumerable<T> List { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
