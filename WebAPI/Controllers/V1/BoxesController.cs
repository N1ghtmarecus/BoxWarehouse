using Application.Dto;
using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using WebAPI.Attributes;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.SwaggerExamples.Responses.Boxes.Get;
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

        /// <summary>
        /// Retrieves the sort fields.
        /// </summary>
        /// <response code="200">Examples of fields that can be sorted</response>
        /// <returns>The sort fields.</returns>
        [ProducesResponseType(typeof(RetrievesSortFieldsResponseStatus200), StatusCodes.Status200OK)]
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetSortFields()
        {
            return Ok(new
            {
                response = new Response(true, "Examples of fields that can be sorted"),
                sorting = SortingHelper.GetSortField().Select(x => x.Key)
            });
        }

        /// <summary>
        /// Retrieves paged boxes.
        /// </summary>
        /// <response code="200">Paged boxes retrieved successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">No boxes found</response>
        /// <param name="paginationFilter">The pagination filter.</param>
        /// <param name="sortingFilter">The sorting filter.</param>
        /// <param name="filterCutterId">The filter by cutter ID.</param>
        /// <returns>The paged boxes.</returns>
        [ProducesResponseType(typeof(RetrievesPagedBoxesResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RetrievesPagedBoxesResponseStatus401), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(RetrievesPagedBoxesResponseStatus404), StatusCodes.Status404NotFound)]
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

            if (boxes == null || !boxes.Any())
            {
                return NotFound(new Response(false, "No boxes found."));
            }

            return Ok(new
            {
                response = new Response(true, "Paged boxes retrieved successfully."),
                pagination = PaginationHelper.CreatePagedReponse(boxes, validPaginationFilter, totalRecords)
            });
        }

        /// <summary>
        /// Retrieves all boxes.
        /// </summary>
        /// <returns>Returns all the boxes.</returns>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IQueryable<BoxDto> GetAll()
        {
            return _boxService.GetAllBoxes();
        }

        /// <summary>
        /// Retrieves a specific box by cutter ID.
        /// </summary>
        /// <response code="200">Box with cutter ID 'ID' retrieved successfully!</response>
        /// <response code="404">Box with cutter ID 'cutterId' not found</response>
        /// <param name="cutterId">The cutter ID of the box.</param>
        /// <returns>The specific box.</returns>
        [Authorize(Roles = UserRoles.AdminOrManager)]
        [HttpGet("{cutterId}")]
        public async Task<IActionResult> Get(int cutterId)
        {
            var box = await _boxService.GetBoxByCutterIdAsync(cutterId);
            if (box == null)
            {
                return NotFound(new Response(false, $"Box with cutter ID {cutterId} not found."));
            }

            return Ok(new Response<BoxDto>(box)
            {
                Succeeded = true,
                Message = $"Box with cutter ID {cutterId} retrieved successfully!"
            });
        }

        [SwaggerOperation(Summary = "Searches boxes by dimension")]
        [AllowAnonymous]
        [HttpGet("searchBy/{dimension}")]
        public async Task<IActionResult> SearchByDimension(string dimension, int dimensionValue, int lowerBound, int upperBound)
        {
            var boxes = await _boxService.GetBoxesByDimensionAsync(dimension, dimensionValue);
            if (boxes != null && boxes.Any())
            {
                return Ok(new Response<IEnumerable<BoxDto>>(boxes)
                {
                    Succeeded = true,
                    Message = $"Found {boxes.Count()} boxes with an exact {dimension} of {dimensionValue}mm"
                });
            }

            var lowerValue = Math.Max(dimensionValue - lowerBound, 0);
            var upperValue = Math.Max(dimensionValue + upperBound, 0);

            boxes = await _boxService.GetBoxesByDimensionRangeAsync(dimension, lowerValue, upperValue);

            if ((boxes != null || !dimension.IsNullOrEmpty()))
            {
                var message = $"No boxes were found with an exact {dimension} of {dimensionValue}mm. Found {boxes!.Count()} boxes within the range {lowerValue}-{upperValue}mm";

                return dimension switch
                {
                    "length" => Ok(new Response<IEnumerable<BoxDto>>(boxes!.OrderBy(b => b.Length))
                    {
                        Succeeded = true,
                        Message = message
                    }),
                    "width" => Ok(new Response<IEnumerable<BoxDto>>(boxes!.OrderBy(b => b.Width))
                    {
                        Succeeded = true,
                        Message = message
                    }),
                    "height" => Ok(new Response<IEnumerable<BoxDto>>(boxes!.OrderBy(b => b.Height))
                    {
                        Succeeded = true,
                        Message = message
                    }),
                    _ => Ok(new Response(false, $"Enter the correct dimension: 'length', 'width' or 'height'.")),
                };
            }

            return NotFound(new Response(false, $"No boxes were found with an exact {dimension} of {dimensionValue}mm or within the range {lowerValue}-{upperValue}mm"));
        }

        [ValidateFilter]
        [SwaggerOperation(Summary = "Creates a new box")]
        [Authorize(Roles = UserRoles.AdminOrManager)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBoxDto newBox)
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
        public async Task<IActionResult> Update([FromBody] BoxDto updateBox)
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
