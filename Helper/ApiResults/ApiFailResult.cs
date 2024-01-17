namespace _01_WEBAPI.Helper.ApiResults
{
    public class ApiFailResult<T> : ApiResult<T>
    {
        public ApiFailResult()
        {
            isSuccess = false;
        }

        public ApiFailResult(string message)
        {
            isSuccess = false;
            Message = message;
        }
    }
}
