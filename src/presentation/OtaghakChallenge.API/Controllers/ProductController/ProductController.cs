
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OtaghakChallenge.API.Controllers.Constant;
using OtaghakChallenge.API.Controllers.ProductController.Models;
using OtaghakChallenge.Application.ProductApplication.Commands.AddProduct;
using OtaghakChallenge.Application.ProductApplication.Commands.ModifyProduct;
using OtaghakChallenge.Application.ProductApplication.Commands.RemoveProduct;
using OtaghakChallenge.Application.ProductApplication.Models.Views;
using OtaghakChallenge.Application.ProductApplication.Queries.GetProduct;

using OtaghakChallenge.Domain.Enums;


namespace OtaghakChallenge.API.Controllers.ProductController
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(ApiVersions.V1)]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        public readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger
            , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }




        [HttpGet]
        [Authorize(Roles=Roles.Admin_Cusstomer)]
        [ProducesResponseType(typeof(IEnumerable<ProductView>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductAsync()
        {
            
            IEnumerable<ProductView> result = await _mediator.Send(new GetProductsQuery());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles=Roles.Admin)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAddressAsync(int id)
        {
            await _mediator.Send(new RemoveProductCommand
            {
                Id = id
            });

            return Ok(true);
        }


        [HttpPost("Add")]
        [Authorize(Roles=Roles.Admin)]
        [ProducesResponseType(typeof(ProductView), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAddressAsync([FromBody] AddProductModel model)
        {
            ProductView result = await _mediator.Send(new AddProductCommand
            {
                Name = model.Name,
                Description = model.Description,
                Status=Status.Active,
            });

            return Created($"Product/{result.Id}", result);
        }


        [HttpPut]
        [Authorize(Roles=Roles.Admin)]
        [ProducesResponseType(typeof(ProductView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModifyAddressAsync([FromBody] ModifyProductModel model)
        {
        
            ProductView result = await _mediator.Send(new ModifyProductCommand
            {
                Name = model.Name,
                Description = model.Description,
                Status = model.Status,
                Id = model.Id
            });

            return Ok();
        }

    }
}
