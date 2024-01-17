namespace linhkien_donet.Helper.ApiResults
{
    public class PagingSuccessResult<T>  : PagingApi<T>
    {
        public T Data { get; set; }
        public double TotalPage { get; set; }
        public int TotalRecordAll { get; set; }
        public int TotalRecord { get; set; }
        public PagingSuccessResult(int totalRecordAll, double totalPage, int totalRecord, int pageSize, int pageIndex, T data)
        {
            Message = "Success";
            Success = true;
            TotalPage = totalPage;
            TotalRecord = totalRecord;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            TotalRecordAll = totalRecordAll;

        }
    }
}
