using Application.Dto;
using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebAPI.Attributes;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.SwaggerExamples.Responses.Boxes.Get;
using WebAPI.SwaggerExamples.Responses.Boxes.Post;
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
        /// <response code="400">Sort fields retrieval failed</response>
        /// <returns>The sort fields.</returns>
        [ProducesResponseType(typeof(RetrievesSortFieldsResponseStatus200), StatusCodes.Status200OK)]
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetSortFields()
        {
            try
            {
                return Ok(new
                {
                    response = new Response(true, "Examples of fields that can be sorted"),
                    sorting = SortingHelper.GetSortField().Select(x => x.Key)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(false, ex.Message));
            }
        }

        /// <summary>
        /// Retrieves paged boxes
        /// </summary>
        /// <response code="200">Paged boxes retrieved successfully</response>
        /// <response code="400">Boxes retrieval failed</response>
        /// <response code="404">No boxes found</response>
        /// <param name="paginationFilter">The pagination filter</param>
        /// <param name="sortingFilter">The sorting filter</param>
        /// <param name="filterCutterId">The filter by cutter ID</param>
        /// <returns>The paged boxes</returns>
        [ProducesResponseType(typeof(RetrievesPagedBoxesResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RetrievesPagedBoxesResponseStatus404), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter, [FromQuery] SortingFilter sortingFilter, [FromQuery] string filterCutterId = "")
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(new Response(false, ex.Message));
            }
        }

        /// <summary>
        /// Retrieves all boxes
        /// </summary>
        /// <response code="200">All boxes retrieved successfully</response>
        /// <response code="400">Boxes retrieval failed</response>
        /// <returns>Returns all the boxes</returns>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IQueryable<BoxDto> GetAll()
        {
            try
            {
                return _boxService.GetAllBoxes();
            }
            catch (Exception ex)
            {
                return (IQueryable<BoxDto>)BadRequest(new Response(false, ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a specific box by cutter ID
        /// </summary>
        /// <response code="200">Box retrieved successfully</response>
        /// <response code="400">Box retrieval failed</response>
        /// <response code="404">Box not found</response>
        /// <param name="cutterId">The cutter ID of the box</param>
        /// <returns>Return the specific box</returns>
        [Authorize(Roles = UserRoles.AdminOrManager)]
        [ProducesResponseType(typeof(RetrievesSpecificBoxByCutterIdResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RetrievesSpecificBoxByCutterIdResponseStatus404), StatusCodes.Status404NotFound)]
        [HttpGet("{cutterId}")]
        public async Task<IActionResult> Get(int cutterId)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(new Response(false, ex.Message));
            }
        }

        /// <summary>
        /// Searches boxes by dimension
        /// </summary>
        /// <response code="200">The correct result of the search</response>
        /// <response code="400">Wrong dimension value</response>
        /// <response code="404">No boxes found</response>
        /// <param name="dimension">The dimension to search by (length, width or height)</param>
        /// <param name="dimensionValue">The value of the dimension</param>
        /// <param name="lowerBound">The lower bound of the dimension range</param>
        /// <param name="upperBound">The upper bound of the dimension range</param>
        /// <returns>The result of the search</returns>
        [AllowAnonymous]
        [ProducesResponseType(typeof(SearchesBoxesByDimensionResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SearchesBoxesByDimensionResponseStatus400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SearchesBoxesByDimensionResponseStatus404), StatusCodes.Status404NotFound)]
        [HttpGet("searchBy/{dimension}")]
        public async Task<IActionResult> SearchByDimension(string dimension, int dimensionValue, int lowerBound, int upperBound)
        {
            try
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
                        _ => BadRequest(new Response(false, $"Enter the correct dimension: length, width or height.")),
                    };
                }

                return NotFound(new Response(false, $"No boxes were found with an exact {dimension} of {dimensionValue}mm or within the range {lowerValue}-{upperValue}mm"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(false, ex.Message));
            }
        }

        /// <summary>
        /// Creates a new box
        /// </summary>
        /// <response code="201">Box created successfully</response>
        /// <response code="400">Box creation failed</response>
        /// <param name="newBox">The new box to create</param>
        /// <returns>The created box</returns>
        [ProducesResponseType(typeof(CreatesNewBoxResponseStatus201), StatusCodes.Status201Created)]
        [ValidateFilter]
        [Authorize(Roles = UserRoles.AdminOrManager)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBoxDto newBox)
        {
            try
            {
                var box = await _boxService.AddNewBoxAsync(newBox, User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                return Created($"api/boxes/{box.CutterID}", new Response<BoxDto>(box)
                {
                    Succeeded = true,
                    Message = $"New box with cutter ID {box.CutterID} created successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(false, ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing box
        /// </summary>
        /// <response code="204">Box updated successfully</response>
        /// <response code="400">Box update failed</response>
        /// <param name="updateBox"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.AdminOrManager)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BoxDto updateBox)
        {
            try
            {
                await _boxService.UpdateBoxAsync(updateBox);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(false, ex.Message));
            }
        }

        /// <summary>
        /// Deletes a box by unique cutter ID
        /// </summary>
        /// <response code="204">Box deleted successfully</response>
        /// <response code="400">Box deletion failed</response>
        /// <param name="cutterId"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{cutterId}")]
        public async Task<IActionResult> Delete(int cutterId)
        {
            try
            {
                await _boxService.DeleteBoxAsync(cutterId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response(false, ex.Message));
            }
        }
    }
}
