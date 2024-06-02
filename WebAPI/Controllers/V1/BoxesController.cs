using Application.Dto;
using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    [ApiController]
    public class BoxesController : ControllerBase
    {
        private readonly IBoxService _boxService;

        public BoxesController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        [SwaggerOperation(Summary = "Retrieves sort fields")]
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetSortFields()
        {
            return Ok(SortingHelper.GetSortField().Select(x => x.Key));
        }

        [SwaggerOperation(Summary = "Retrieves paged boxes")]
        [Authorize(Roles = UserRoles.AdminOrManagerOrUser)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter, [FromQuery] SortingFilter sortingFilter, [FromQuery] string filterCutterId = "")
        {
            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

            var boxes = await _boxService.GetAllBoxesAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize,
                                                           validSortingFilter.SortField!, validSortingFilter.Ascending,
                                                           filterCutterId);

            var totalRecords = await _boxService.GetAllBoxesCountAsync(filterCutterId);

            return Ok(PaginationHelper.CreatePagedReponse(boxes, validPaginationFilter, totalRecords));
        }

        [SwaggerOperation(Summary = "Retrieves all boxes")]
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IQueryable<BoxDto> GetAll()
        {
            return _boxService.GetAllBoxes();
        }

        [SwaggerOperation(Summary = "Retrieves a specific box by cutter ID")]
        [Authorize(Roles = UserRoles.AdminOrManagerOrUser)]
        [HttpGet("{cutterId}")]
        public async Task<IActionResult> Get(int cutterId)
        {
            var box = await _boxService.GetBoxByCutterIdAsync(cutterId);
            if (box == null)
            {
                return NotFound(new Response<bool>()
                {
                    Succeeded = false,
                    Message = $"Box with cutter ID {cutterId} not found"
                });
            }

            return Ok(new Response<BoxDto>(box)
            {
                Succeeded = true,
                Message = $"Box with cutter ID {cutterId} found"
            });
        }

        [SwaggerOperation(Summary = "Searches boxes by length")]
        [AllowAnonymous]
        [HttpGet("searchBy/{length}")]
        public async Task<IActionResult> SearchByLength(int length)
        {
            var boxes = await _boxService.GetBoxesByLengthAsync(length);
            if (boxes != null && boxes.Any())
            {
                return Ok(new Response<IEnumerable<BoxDto>>(boxes)
                {
                    Succeeded = true,
                    Message = $"Found boxes with exact length {length}"
                });
            }

            var lowerBound = length - 20;
            var upperBound = length + 20;
            boxes = await _boxService.GetBoxesByLengthRangeAsync(lowerBound, upperBound);
            if (boxes != null && boxes.Any())
            {
                return Ok(new Response<IEnumerable<BoxDto>>(boxes)
                {
                    Succeeded = true,
                    Message = $"No boxes found with exact length {length}. Found boxes within the range {lowerBound}-{upperBound}"
                });
            }

            return NotFound(new Response<string> { Succeeded = false, Message = $"No boxes found with length {length} or within the range {lowerBound}-{upperBound}" });
        }

        [SwaggerOperation(Summary = "Creates a new box")]
        [Authorize(Roles = UserRoles.AdminOrManager)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBoxDto newBox)
        {
            var box = await _boxService.AddNewBoxAsync(newBox, User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return Created($"api/boxes/{box.CutterID}", new Response<BoxDto>(box)
            {
                Succeeded = true,
                Message = $"New box with cutter ID {box.CutterID} created successfully"
            });
        }

        [SwaggerOperation(Summary = "Updates an existing box")]
        [Authorize(Roles = UserRoles.AdminOrManager)]
        [HttpPut]
        public async Task<IActionResult> Update(BoxDto updateBox)
        {
            await _boxService.UpdateBoxAsync(updateBox);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Deletes a box by unique cutter ID")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{cutterId}")]
        public async Task<IActionResult> Delete(int cutterId)
        {
            await _boxService.DeleteBoxAsync(cutterId);
            return NoContent();
        }
    }
}
