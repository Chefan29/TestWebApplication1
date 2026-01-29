using Microsoft.AspNetCore.Mvc;

namespace TestWebApplication1.Controllers
{
    [ApiController]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;

        public NotesController(INoteService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var note = _service.GetById(id);
            return note is null ? NotFound() : Ok(note);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateNoteDto dto)
        {
            var (ok, error, note) = _service.Create(dto);

            if (!ok)
                return BadRequest(new { error = error ?? "Invalid request" });

            return Created($"/api/notes/{note!.Id}", note);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] UpdateNoteDto dto)
        {
            var (ok, error, note) = _service.Update(id, dto);

            if (!ok && error is not null)
                return BadRequest(new { error });

            if (note is null)
                return NotFound();

            return Ok(note);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ? NoContent() : NotFound();
        }
    }
}
