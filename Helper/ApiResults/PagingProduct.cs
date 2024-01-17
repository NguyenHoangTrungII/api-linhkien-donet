namespace linhkien_donet.Helper.ApiResults
{
    public class PagingApi<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public string Message { get; set; }
        public bool Success { get; set; }

        public PagingApi()
        {

        }
    }
}
