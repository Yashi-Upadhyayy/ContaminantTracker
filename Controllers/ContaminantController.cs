using Microsoft.AspNetCore.Mvc;
using ContaminantTracker.Models;

namespace ContaminantTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaminantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContaminantsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contaminant>> GetAll()
        {
            return Ok(_context.Contaminants.ToList());
        }

        [HttpPost]
        public ActionResult Add(Contaminant c)
        {
            _context.Contaminants.Add(c);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAll), new { id = c.Id }, c);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Contaminant c)
        {
            var existing = _context.Contaminants.Find(id);
            if (existing == null) return NotFound();

            existing.Name = c.Name;
            existing.Level = c.Level;
            existing.Source = c.Source;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _context.Contaminants.Find(id);
            if (existing == null) return NotFound();

            _context.Contaminants.Remove(existing);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
