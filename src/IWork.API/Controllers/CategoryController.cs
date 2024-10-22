using IWork.API.Handlers.CategoryHandler;
using IWork.Domain.Commands.CategoryCommands;
using IWork.Domain.Queries.AdvertisementQuery;
using IWork.Domain.Queries.CategoryQuery;
using IWork.Domain.Requests;
using IWork.Domain.ViewModels;
using IWork.Service.Interfaces;
using IWork.Service.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IWork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllCategories")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("GetCategoryById{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var query = new GetByIdCategoryQuery(Id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost("CreateCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CategoryAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }

        [HttpPut("UpdateCategory{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid Id, [FromBody] CategoryRequest categoryBookRequest)
        {
            var cmd = new CategoryUpdateCommand(Id, categoryBookRequest);
            var response = await _mediator.Send(cmd);
            if (!response) return BadRequest();
            return Ok();
        }

        [HttpDelete("DeleteCategory{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var cmd = new CategoryDeleteCommand(Id);
            var response = await _mediator.Send(cmd);
            if (!response) return BadRequest();
            return Ok();
        }
    }
}
