using Application.Dto;
using Application.Dto.Cosmos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers.V2
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class BoxesController : ControllerBase
    {
        private readonly ICosmosBoxService _boxService;

        public BoxesController(ICosmosBoxService boxService)
        {
            _boxService = boxService;
        }

        [SwaggerOperation(Summary = "Retrieves all boxes")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var boxes = await _boxService.GetAllBoxesAsync();
            var sortedBoxes = boxes.OrderBy(x => x.CutterID);
            return Ok(
                new
                {
                    Posts = sortedBoxes,
                    Count = sortedBoxes.Count()
                });
        }

        [SwaggerOperation(Summary = "Retrieves a specific box by unique cutter ID")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var box = await _boxService.GetBoxByIdAsync(id);
            if (box == null)
            {
                return NotFound();
            }

            return Ok(box);
        }

        [SwaggerOperation(Summary = "Creates a new box")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCosmosBoxDto newBox)
        {
            var box = await _boxService.AddNewBoxAsync(newBox);
            return Created($"api/boxes/{box.CutterID}", box);
        }

        [SwaggerOperation(Summary = "Updates an existing box")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCosmosBoxDto updateBox)
        {
            await _boxService.UpdateBoxAsync(updateBox);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Deletes a box by unique cutter ID")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _boxService.DeleteBoxAsync(id);
            return NoContent();
        }
    }
}
