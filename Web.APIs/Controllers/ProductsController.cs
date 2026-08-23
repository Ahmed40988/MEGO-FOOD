using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Common;
using Web.Application.Products.Commands.CreateProduct;
using Web.Application.Products.Commands.DeleteProduct;
using Web.Application.Products.Commands.UpdateProduct;
using Web.Application.Products.ProductDTO;
using Web.Application.Products.Queries.GetProductById;
using Web.Application.Products.Queries.GetProductsByCategory;
using Web.Application.Products.Queries.SearchProducts;

namespace Web.APIs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ISender mediator) : ApiController
    {
        private readonly ISender _mediator = mediator;

        /// <summary>
        /// Create New Product
        /// </summary>
        /// <response code="200">Created successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">MenuCategory not found</response>
        /// <response code="409">Product already exists</response>
       [Authorize]
        [HttpPost("Create_Product")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest request)
        {
            var adminId = User.GetUserId();
            var command = new CreateProductCommand(
                request.Name,
                request.Description,
                request.Images,
                request.Price,
                request.MenuCategoryId,
                adminId
                );

            var result = await _mediator.Send(command);

            return result.Match(
                productId => Ok(productId),
                errors => ToProblem(errors));
        }


        /// <summary>
        /// Get all products by MenuCategoryId
        /// </summary>
        [HttpGet("Get_Products_By_Category")]
        public async Task<IActionResult> GetProductsByCategory([FromQuery]RequestFilters filters,[FromQuery] Guid?categoryId)
        {
            var query = new GetProductsByCategoryQuery(filters, categoryId);
            var result = await _mediator.Send(query);

            return result.Match(
                list => Ok(list),
                errors => ToProblem(errors));
        }


        /// <summary>
        /// Get product by Id
        /// </summary>
        [HttpGet("Get_Product_By_ID/{Id}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid Id)
        {
            var query = new GetProductByIdQuery(Id);
            var result = await _mediator.Send(query);

            return result.Match(
                product => Ok(product),
                errors => ToProblem(errors));
        }


        /// <summary>
        /// Delete Product
        /// </summary>
        /// <response code="200">Deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Product not found</response>
        [Authorize]
        [HttpDelete("Delete_Product/{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid Id)
        {
            var adminId = User.GetUserId();

            var command = new DeleteProductCommand(Id, adminId);

            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => ToProblem(errors));
        }


        /// <summary>
        /// Update Product
        /// </summary>
        /// <response code="200">Updated successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Product not found</response>
        [Authorize]
        [HttpPut("Update_Product/{Id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid Id, [FromForm] ProductRequest request)
        {
            var adminId = User.GetUserId();

            var command = new UpdateProductCommand(
                Id,
                request.Name,
                request.Description,
                request.Images,
                request.Price,
                adminId);

            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => ToProblem(errors));
        }

        /// <summary>
        /// Search For Product by name 
        /// </summary>
        /// <response code="200">Products are Return</response>
        /// <response code="404">Not Found Products For This Name</response>
        [HttpGet("search_Product")]
        public async Task<IActionResult> Search( [FromQuery] string keyword, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new SearchProductsQuery(keyword),
                cancellationToken);


            return result.Match(
                list => Ok(list),
                errors => ToProblem(errors));
        }
    }
}
