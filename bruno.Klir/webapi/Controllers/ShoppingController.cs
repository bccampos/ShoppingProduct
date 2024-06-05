using Azure.Core;
using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Application.Product;
using bruno.Klir.Application.Product.Commands;
using bruno.Klir.Application.Product.Queries;
using bruno.Klir.Application.Shopping;
using bruno.Klir.Application.Shopping.Commands;
using bruno.Klir.Application.Shopping.Queries;
using bruno.Klir.Contracts.Product;
using bruno.Klir.Contracts.Shopping;
using bruno.Klir.Domain.Exceptions;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Domain.Shopping.ValueObjects;
using bruno.Klir.WebApi.Common;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bruno.Klir.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ShoppingController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("getById/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetShoppingByIdQuery(id);

            var response = await _mediator.Send(query);

            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }

            var firstError = response.Errors[0];
            if (firstError is NotFoundError)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, detail: firstError.Message);
            }

            return Problem($"Unexpected error: {firstError.Message}");
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetShoppingListQuery();

            var response = await _mediator.Send(query);

            return Ok(response.Value);
        }

        [HttpGet("shoppingCalculate/{id:guid}")]
        public async Task<IActionResult> shoppingCalculate([FromRoute] Guid id)
        {
            var command = new CalculateShoppingCommand(id);

            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }

            var firstError = response.Errors[0];
            if (firstError is NotFoundError)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict, detail: firstError.Message);
            }

            return Problem($"Unexpected error: {firstError.Message}");

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ShoppingRequest request)
        {
            //TODO: Add _mapper.Map<CreateShoppingCommand>(request);
            var command = new CreateShoppingCommand
            {
                Items = request.Items.Select(x => new ShoppingItem
                {
                    ProductId = ProductId.Parse(x.ProductId),
                    Quantity = x.Quantity,
                    Price = x.Price,
                })
            };

            CommandResult<Guid> result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(CustomResponse.SuccessResponse(result.Data));
            }
            return Problem($"Unexpected error");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ShoppingUpdateRequest request)
        {
            var command = new UpdateShoppingCommand
            {
                Id = request.Id,
                Items = request.Items.Select(x => new ShoppingItem
                {
                    ShoppingGroupId = ShoppingGroupId.Parse(x.ShoppingId),
                    ProductId = ProductId.Parse(x.ProductId),
                    Quantity = x.Quantity,
                    Price = x.Price,
                })
            };

            Result<ShoppingResult> result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return Problem($"Unexpected error: {result.Errors[0]}");
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new RemoveShoppingCommand(id));

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                throw;
            }
        }
    }
}
