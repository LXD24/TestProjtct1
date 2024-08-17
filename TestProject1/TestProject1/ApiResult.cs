namespace TestProject1
{

    public class ApiResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string RequestId { get; set; }
    }
}
