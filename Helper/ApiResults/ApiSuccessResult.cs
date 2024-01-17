using linhkien_donet.Entities;

namespace _01_WEBAPI.Helper.ApiResults
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public T Data { get; set; }

        public T Other { get; set; }
        public string Content { get; set; }
        public ApiSuccessResult()
        {
            isSuccess = true;
            Message = "Request Sucess";
        }

        public ApiSuccessResult(T data)
        {
            isSuccess = true;
            Message = "Request Sucess !";
            Data = data;
        }

        public ApiSuccessResult(T data, string message)
        {
            isSuccess = true;
            Message = message;
            Data = data;
        }

        public ApiSuccessResult(T data, T other)
        {
            isSuccess = true;
            Message = "Request Sucess !";
            Data = data;
            Other = other;
        }

        public ApiSuccessResult(string message)
        {
            isSuccess = true;
            Message = message;
        }

        public ApiSuccessResult(string message, string content)
        {
            isSuccess = true;
            Message = message;
            Content = content;
        }

        public ApiSuccessResult(Product productByName)
        {
        }
    }
}
