using Application.Dto;
using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.SwaggerExamples.Responses.Picture.GET;
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
        /// "Retrieves pictures by unique cutter ID"
        /// </summary>
        /// <response code="200">"Retrieved 'x' pictures for Box with Cutter Id 'ID'."</response>
        /// <response code="404">"No pictures found for Box with Cutter Id 'ID'."</response>
        /// <param name="boxCutterId"></param>
        /// <returns></returns>
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
        /// "Retrieves a picture by unique ID"
        /// </summary>
        /// <response code="200">"Picture 'name' retrieved successfully."</response>
        /// <response code="404">"No picture found for Picture with Id 'ID'."</response>
        /// <param name="id"></param>
        /// <returns></returns>
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

        [SwaggerOperation(Summary = "Adds a picture to a box")]
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

        [SwaggerOperation(Summary = "Updates the main picture of the box")]
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
                Message = $"isMain flag of Picture '{picture.Name}' updated successfully."
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
