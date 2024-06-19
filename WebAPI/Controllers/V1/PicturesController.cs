using Application.Dto;
using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.SwaggerExamples.Responses.Picture.GET;
using WebAPI.SwaggerExamples.Responses.Picture.Post;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.AdminOrManager)]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPictureService _pictureService;
        private readonly IBoxService _boxService;

        public PicturesController(IPictureService pictureService, IBoxService boxService)
        {
            _pictureService = pictureService;
            _boxService = boxService;
        }

        /// <summary>
        /// Retrieves pictures by unique cutter ID
        /// </summary>
        /// <response code="200">"Retrieved 'count' pictures for Box with Cutter Id 'ID'."</response>
        /// <response code="404">"No pictures found for Box with Cutter Id 'ID'."</response>
        /// <param name="boxCutterId">The unique cutter ID of the box</param>
        /// <returns>Returns the pictures for the box with the specified cutter ID</returns>
        [ProducesResponseType(typeof(RetrievesPicturesByCutterIdResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RetrievesPicturesByCutterIdResponseStatus404), StatusCodes.Status404NotFound)]
        [HttpGet("[action]/{boxCutterId}")]
        public async Task<IActionResult> GetByBoxCutterIdAsync(int boxCutterId)
        {
            var pictures = await _pictureService.GetPicturesByBoxCutterIdAsync(boxCutterId);

            if (pictures == null || !pictures.Any())
            {
                return NotFound(new Response(false, $"No pictures found for Box with Cutter Id '{boxCutterId}'."));
            }

            return Ok(new Response<IEnumerable<PictureDto>>(pictures)
            {
                Succeeded = true,
                Message = $"Retrieved {pictures.Count()} pictures for Box with Cutter Id '{boxCutterId}'."
            });
        }

        /// <summary>
        /// Retrieves a picture by unique ID
        /// </summary>
        /// <response code="200">"Picture 'name' retrieved successfully."</response>
        /// <response code="404">"No picture found for Picture with Id 'ID'."</response>
        /// <param name="id">The unique ID of the picture</param>
        /// <returns>Returns the picture with the specified ID</returns>
        [ProducesResponseType(typeof(RetrievesPictureByIdResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RetrievesPictureByIdResponseStatus404), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var picture = await _pictureService.GetPictureByIdAsync(id);

            if (picture == null)
            {
                return NotFound(new Response(false, $"No picture found for Picture with Id '{id}'."));
            }

            return Ok(new Response<PictureDto>(picture)
            {
                Succeeded = true,
                Message = $"Picture '{picture.Name}' retrieved successfully."
            });
        }

        /// <summary>
        /// Adds a picture to a box
        /// </summary>
        /// <response code="201">"Picture 'name' added to the box with cutter id 'ID' successfully."</response>
        /// <response code="400">"Failed to add picture to the box."</response>
        /// <response code="404">"Box with Cutter Id 'ID' does not exist."</response>
        /// <param name="boxCutterId">The unique cutter ID of the box</param>
        /// <param name="file">The picture file to be added</param>
        /// <param name="isMain">Specifies whether the picture is the main picture of the box</param>
        /// <returns>Returns the added picture</returns>
        [ProducesResponseType(typeof(AddsPictureToBoxResponseStatus201), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AddsPictureToBoxResponseStatus400), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AddsPictureToBoxResponseStatus404), StatusCodes.Status404NotFound)]
        [HttpPost("{boxCutterId}")]
        public async Task<IActionResult> AddToBoxAsync(int boxCutterId, IFormFile file, bool isMain = false)
        {
            var box = await _boxService.GetBoxByCutterIdAsync(boxCutterId);

            if (box == null)
            {
                return NotFound(new Response(false, $"Box with Cutter Id '{boxCutterId}' does not exist."));
            }

            var picture = await _pictureService.AddPictureToBoxAsync(boxCutterId, file, isMain);

            if (picture == null)
            {
                return BadRequest(new Response(false, "Failed to add picture to the box."));
            }

            return Created($"api/pictures/{picture.Id}", new Response<PictureDto>(picture)
            {
                Succeeded = true,
                Message = $"Picture '{picture.Name}' added to the box with cutter id {boxCutterId} successfully."
            });
        }

        /// <summary>
        /// Updates the main picture of the box
        /// </summary>
        /// <response code="200">"The main picture of the box has been updated successfully."</response>
        /// <response code="404">"Picture with ID 'ID' does not exist."</response>
        /// <param name="pictureId">The unique ID of the picture</param>
        /// <param name="isMain">Specifies whether the picture is the main picture of the box</param>
        /// <returns>Returns the updated picture</returns>
        [HttpPut("{pictureId}/isMain")]
        public async Task<IActionResult> UpdateIsMainFlagAsync(int pictureId, bool isMain)
        {
            var picture = await _pictureService.GetPictureByIdAsync(pictureId);

            if (picture == null)
            {
                return NotFound(new Response(false, $"Picture with ID '{pictureId}' does not exist."));
            }

            picture.IsMain = isMain;
            await _pictureService.UpdatePictureAsync(picture);

            return Ok(new Response<PictureDto>(picture)
            {
                Succeeded = true,
                Message = $"The main picture of the box has been updated successfully."
            });
        }

        [SwaggerOperation(Summary = "Deletes a picture by unique ID")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var picture = await _pictureService.GetPictureByIdAsync(id);

            if (picture == null)
            {
                return NotFound(new Response(false, $"Picture with ID '{id}' currently does not exist."));
            }

            await _pictureService.DeletePictureAsync(id);

            return Ok(new Response<PictureDto>(picture)
            {
                Succeeded = true,
                Message = $"Picture '{picture.Name}' deleted successfully."
            });
        }
    }
}
