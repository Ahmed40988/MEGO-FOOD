using MediatR;
using Web.Domain.BaseCategories;

namespace Web.Application.BaseCategories.Queries.GetBaseCategories;

public record GetBaseCategoriesQuery() :IRequest<ErrorOr<List<BaseCategory>>>;
