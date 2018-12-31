namespace Lee.Abp.Application.Common.Dto
{
    public class PagedQueryInputDtoBase
    {
        public string SortBy { get; set; } = "ID";
        public string OrderBy { get; set; } = "DESC";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
