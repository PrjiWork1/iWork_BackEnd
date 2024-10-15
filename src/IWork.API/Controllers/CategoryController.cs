using IWork.Domain.Commands.CategoryCommands;
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
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService, IMediator mediator)
        {
            _mediator = mediator;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAll();
            var categories = response.Select(category => new CategoryViewModel(
                category.Id, category.Description, category.IsActive));
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var response = await _categoryService.GetById(Id);
            var categories = new CategoryViewModel(
                response.Id, response.Description, response.IsActive);
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CategoryAddCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response) return BadRequest();
            return CreatedAtRoute(response, response);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid Id, [FromBody] CategoryRequest categoryBookRequest)
        {
            var cmd = new CategoryUpdateCommand(Id, categoryBookRequest);
            var response = await _mediator.Send(cmd);
            if (!response) return BadRequest();
            return Ok();
        }

        [HttpDelete("{Id}")]
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
