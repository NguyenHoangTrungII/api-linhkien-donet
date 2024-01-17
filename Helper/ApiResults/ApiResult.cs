namespace _01_WEBAPI.Helper.ApiResults
{
    public class ApiResult<T>
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }

        public ApiResult()
        {


        }
    }
}
