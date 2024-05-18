using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxesController : ControllerBase
    {
        private readonly IBoxService _boxService;

        public BoxesController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        [SwaggerOperation(Summary = "Retrieves all boxes")]
        [HttpGet]
        public IActionResult Get()
        {
            var boxes = _boxService.GetAllBoxes().OrderBy(b => b.CutterID);
            return Ok(boxes);
        }

        [SwaggerOperation(Summary = "Retrieves a specific box by unique cutter ID")]
        [HttpGet("{cutterId}")]
        public IActionResult Get(int cutterId)
        {
            var box = _boxService.GetBoxByCutterId(cutterId);
            if (box == null)
            {
                return NotFound();
            }

            return Ok(box);
        }

        [SwaggerOperation(Summary = "Creates a new box")]
        [HttpPost]
        public IActionResult Create(BoxDto newBox)
        {
            var box = _boxService.AddNewBox(newBox);
            return Created($"api/boxes/{box.CutterID}", box);
        }

        [SwaggerOperation(Summary = "Updates an existing box")]
        [HttpPut]
        public IActionResult Update(BoxDto updateBox)
        {
            _boxService.UpdateBox(updateBox);
            return NoContent();
        }
    }
}
