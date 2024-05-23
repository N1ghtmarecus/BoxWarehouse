using Application.Dto;
using Application.Dto.Cosmos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers.V2
{
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
                    Posts = boxes,
                    Count = boxes.Count()
                });
        }

        [SwaggerOperation(Summary = "Retrieves a specific box by unique cutter ID")]
        [HttpGet("{cutterId}")]
        public async Task<IActionResult> Get(string cutterId)
        {
            var box = await _boxService.GetBoxByCutterIdAsync(cutterId);
            if (box == null)
            {
                return NotFound();
            }

            return Ok(box);
        }

        [SwaggerOperation(Summary = "Creates a new box")]
        [HttpPost]
        public async Task<IActionResult> Create(CosmosBoxDto newBox)
        {
            var box = await _boxService.AddNewBoxAsync(newBox);
            return Created($"api/boxes/{box.CutterID}", box);
        }

        [SwaggerOperation(Summary = "Updates an existing box")]
        [HttpPut]
        public async Task<IActionResult> Update(CosmosBoxDto updateBox)
        {
            await _boxService.UpdateBoxAsync(updateBox);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Deletes a box by unique cutter ID")]
        [HttpDelete("{cutterId}")]
        public async Task<IActionResult> Delete(string cutterId)
        {
            await _boxService.DeleteBoxAsync(cutterId);
            return NoContent();
        }
    }
}
