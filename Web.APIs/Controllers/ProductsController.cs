using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Products.Commands.CreateProduct;
using Web.Application.Products.Commands.DeleteProduct;
using Web.Application.Products.Commands.UpdateProduct;
using Web.Application.Products.ProductDTO;
using Web.Application.Products.Queries.GetProductById;
using Web.Application.Products.Queries.GetProductsByCategory;

namespace Web.APIs.Controllers
{
    [Authorize]
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
        [HttpPost("Create_Product")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest request)
        {
            var adminId = User.GetUserId();
            var command = new CreateProductCommand(
                request.Name,
                request.Description,
                request.Image,
                request.Price,
                request.MenuCategoryId,
                adminId
                );

            var result = await _mediator.Send(command);

            return result.Match(
                productId => Ok(productId),
                errors => ToProblem(errors));
        }


        ///// <summary>
        ///// Get all products by MenuCategoryId
        ///// </summary>
        //[HttpGet("Get_Products_By_Category")]
        //public async Task<IActionResult> GetProductsByCategory([FromQuery] Guid categoryId)
        //{
        //    var query = new GetProductsByCategoryQuery(categoryId);
        //    var result = await _mediator.Send(query);

        //    return result.Match(
        //        list => Ok(list),
        //        errors => ToProblem(errors));
        //}


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
        [HttpPut("Update_Product/{Id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid Id, [FromForm] ProductRequest request)
        {
            var adminId = User.GetUserId();

            var command = new UpdateProductCommand(
                Id,
                request.Name,
                request.Description,
                request.Image,
                request.Price,
                adminId);

            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => ToProblem(errors));
        }
    }
}
