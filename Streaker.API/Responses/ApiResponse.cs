using Streaker.DAL.Utilities;

namespace Streaker.API.Responses
{
    public class ApiResponse
    {
        public bool IsSucceed => Errors == null;
        public Dictionary<string, List<string>>? Errors { get; set; }
    }

    public class ApiResponse<T>(T data) : ApiResponse
    {
        public T? Data { get; set; } = data;
    }

    public class ApiPaginatedResponse<T>(PaginatedList<T> paginatedData) : ApiResponse
    {
        public List<T> Data { get; set; } = [.. paginatedData];
        public int PageIndex { get; set; } = paginatedData.PageIndex;
        public int TotalPages { get; set; } = paginatedData.TotalPages;
        public int TotalCount { get; set; } = paginatedData.TotalCount;
        public bool HasPrevious { get; set; } = paginatedData.HasPrevious;
        public bool HasNext { get; set; } = paginatedData.HasNext;
    }
}
