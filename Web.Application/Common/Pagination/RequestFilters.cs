namespace Web.Application.Common
{
    public class RequestFilters
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SearchValue { get; init; }
        public string? SortColumn { get; init; }
        public string? SortDirection { get; init; } = "ASC";
        public bool? TopRaing { get; init; }
    }
    public record AddtionalRequestFilters
    {
        // Filters from UI
        public string? Location { get; set; }
        public bool? OpenNow { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public List<string>? Services { get; set; }
    }

    public class RestaurantFilters : RequestFilters
    {
        public string? UserId { get; set; }
        public bool? OpenNow { get; set; }
        public bool? FastDelivery { get; set; }
        public bool? Nearest { get; set; }
        public bool? Offers { get; set; }
        public bool? FreeDelivery { get; set; }




    }
}
