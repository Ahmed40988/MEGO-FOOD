//using Microsoft.AspNetCore.Mvc;
//using Web.Application.Products.Commands.CreateProduct;
//using Web.Application.Products.Commands.DeleteProduct;
//using Web.Application.Products.Commands.UpdateProduct;
//using Web.Application.Products.Queries.GetProduct;
//using Web.Application.Products.Queries.ListProduct;

//namespace Web.APIs.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class ProductsController : ApiController
//{
//    private readonly ISender _mediator;

//    public ProductsController(ISender mediator)
//    {
//        _mediator = mediator;
//    }

//    [HttpPost]
//    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
//    {
//        var userId = User.GetUserId();
//        var command = new CreateProductCommand(request.Name, request.Description, request.ImageUrl, request.Price, request.MenuCategoryId, userId);
//        var result = await _mediator.Send(command);

//        return result.Match(
//            id => Ok(id),
//            errors => ToProblem(errors)
//        );
//    }

//    [HttpGet("{id:guid}")]
//    public async Task<IActionResult> GetProductById(Guid id)
//    {
//        var query = new GetProductQuery(id);
//        var result = await _mediator.Send(query);

//        return result.Match(
//            product => Ok(product),
//            errors => ToProblem(errors)
//        );
//    }

//    [HttpGet]
//    public async Task<IActionResult> ListProducts()
//    {
//        var query = new ListProductQuery();
//        var result = await _mediator.Send(query);

//        return result.Match(
//            products => Ok(products),
//            errors => ToProblem(errors)
//        );
//    }

//    [HttpPut("{id:guid}")]
//    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest request)
//    {
//        var userId = User.GetUserId();
//        var command = new UpdateProductCommand(id, request.Name, request.Description, request.ImageUrl, request.Price, request.MenuCategoryId, userId);
//        var result = await _mediator.Send(command);

//        return result.Match(
//            _ => Ok(),
//            errors => ToProblem(errors)
//        );
//    }

//    [HttpDelete("{id:guid}")]
//    public async Task<IActionResult> DeleteProduct(Guid id)
//    {
//        var userId = User.GetUserId();
//        var command = new DeleteProductCommand(id, userId);
//        var result = await _mediator.Send(command);

//        return result.Match(
//            _ => Ok(),
//            errors => ToProblem(errors)
//        );
//    }
//}