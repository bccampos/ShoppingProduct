using bruno.Klir.Application.Common.Errors;
using bruno.Klir.Application.Product;
using bruno.Klir.Application.Product.Commands;
using bruno.Klir.Application.Product.Queries;
using bruno.Klir.Contracts.Product;
using bruno.Klir.Domain.Exceptions;
using bruno.Klir.WebApi.Common;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bruno.Klir.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("getById/{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var query = new GetProductByIdQuery(id);

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
            var query = new GetProductListQuery();

            var response = await _mediator.Send(query);

            return Ok(CustomResponse.SuccessResponse(response));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var command = _mapper.Map<CreateProductCommand>(request);

            Result<ProductResult> result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(_mapper.Map<ProductResponse>(result.Value));
            }

            //var firstError = authResult.Errors[0];
            //if (firstError is DuplicateEmailError)
            //{
            //    return Problem(statusCode: StatusCodes.Status409Conflict, detail: firstError.Message);
            //}

            return Problem($"Unexpected error: {result.Errors[0]}");
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);

            Result<ProductResult> result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(_mapper.Map<ProductResponse>(result.Value));
            }

            //TODO: Not found

            return Problem($"Unexpected error: {result.Errors[0]}");
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand(id));

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

        [HttpPost("setPromotion")]
        public async Task<IActionResult> SetPromotionAsync([FromBody] ProductSetPromotionRequest request)
        {
            try
            {
                var command = _mapper.Map<SetProductPromotionCommand>(request);

                await _mediator.Send(command);

                return Ok();
            }
            catch
            {
                //TODO: Improving log
                throw;
            }
        }

        [HttpPost("removePromotion")]
        public async Task<IActionResult> RemovePromotionAsync([FromBody] RemoveProductPromotionRequest request)
        {
            try
            {
                var command = _mapper.Map<RemoveProductPromotionCommand>(request);

                await _mediator.Send(command);


                return Ok();
            }
            catch
            {
                throw;
            }
        }
    }
}
