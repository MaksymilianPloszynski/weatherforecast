namespace Weather.Forecast.API.Services.Models;

public class PagedResult<T>
{
    public List<T> Data { get; set; }
    public PaginationModel Pagination { get; set; }

    public PagedResult(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        Pagination = new PaginationModel
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalItems = count,
        };
        Data = new List<T>(items);
    }

    public PagedResult(IEnumerable<T> items, PaginationModel paginationModel)
    {
        Pagination = new PaginationModel
        {
            CurrentPage = paginationModel.CurrentPage,
            PageSize = paginationModel.PageSize,
            TotalItems = paginationModel.TotalItems,
        };
        Data = new List<T>(items);
    }
}