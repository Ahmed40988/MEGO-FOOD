using Web.Application.Common;
using Web.Application.Common.Pagination;
using Web.Application.Restaurants.Contracts;

namespace Web.Application.Restaurants.Queries.ListRestaurant
{
    public record ListRestaurantQuerys(RequestFilters Filters) : IRequest<ErrorOr<PaginatedList<RestaurantResponce>>>;
}
