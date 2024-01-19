namespace Streaker.API.Responses
{
    public class ApiResponse
    {
        public bool IsSucceed => Errors == null;
        public Dictionary<string, List<string>>? Errors { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
    }

    public class ApiPaginatedResponse<T> : ApiResponse<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}
