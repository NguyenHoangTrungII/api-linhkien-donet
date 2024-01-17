namespace linhkien_donet.Helper.ApiResults
{
    public class PagingFailResult<T> : PagingApi<T>
    {
        public PagingFailResult(string message)
        {
            Message = message;
            Success = false;
        }
    }
}
