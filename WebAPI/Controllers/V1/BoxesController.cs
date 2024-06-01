using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [HttpGet("[action]")]
        public IActionResult GetSortFields()
        {
            return Ok(SortingHelper.GetSortField().Select(x => x.Key));
        }

        [SwaggerOperation(Summary = "Retrieves paged boxes")]
        [AllowAnonymous]
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
        [HttpGet("[action]")]
        public IQueryable<BoxDto> GetAll()
        {
            return _boxService.GetAllBoxes();
        }

        [SwaggerOperation(Summary = "Retrieves a specific box by unique cutter ID")]
        [HttpGet("{cutterId}")]
        public async Task<IActionResult> Get(int cutterId)
        {
            var box = await _boxService.GetBoxByCutterIdAsync(cutterId);
            if (box == null)
            {
                return NotFound();
            }

            return Ok(new Response<BoxDto>(box));
        }

        [SwaggerOperation(Summary = "Creates a new box")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBoxDto newBox)
        {
            var box = await _boxService.AddNewBoxAsync(newBox);
            return Created($"api/boxes/{box.CutterID}", new Response<BoxDto>(box));
        }

        [SwaggerOperation(Summary = "Updates an existing box")]
        [HttpPut]
        public async Task<IActionResult> Update(BoxDto updateBox)
        {
            await _boxService.UpdateBoxAsync(updateBox);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Deletes a box by unique cutter ID")]
        [HttpDelete("{cutterId}")]
        public async Task<IActionResult> Delete(int cutterId)
        {
            await _boxService.DeleteBoxAsync(cutterId);
            return NoContent();
        }
    }
}
