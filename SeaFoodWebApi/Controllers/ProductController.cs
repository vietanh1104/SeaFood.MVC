using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Query.ProductQuery;

namespace SeaFoodWebApi.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            return Ok(await _mediator.Send(new ProductGetDetailById
            {
                id = id
            }));
        }

    }
}
