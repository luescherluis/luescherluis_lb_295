using FootballManagement.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FussballmannschaftController : ControllerBase
    {
        private readonly FussballmannschaftDB _context;

        public FussballmannschaftController(FussballmannschaftDB context)
        {
            _context = context;
        }

        // GET: api/Fussballmannschaft
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FussballmannschaftDTO>>> GetFussballmannschaft()
        {
            if (_context.Fussballmannschaft == null)
            {
                return NotFound();
            }
            return await _context.Fussballmannschaft.Select(p => FussballmannschaftToFussballmannschaftDTO(p)).ToListAsync();
        }

        // GET: api/Fussballmannschaft/specificId
        [HttpGet("{id}")]
        public async Task<ActionResult<FussballmannschaftDTO>> GetFussballmannschaft(int id)
        {
            if (_context.Fussballmannschaft == null)
            {
                return NotFound();
            }
            var fussballmannschaft = await _context.Fussballmannschaft.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (fussballmannschaft == null)
            {
                return NotFound();
            }

            return FussballmannschaftToFussballmannschaftDTO(fussballmannschaft);
        }

        // PUT: api/Fussballmannschaft/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFussballmannschaft(int id, FussballmannschaftDTO fussballmannschaftDTO)
        {
            if (id != fussballmannschaftDTO.Id)
            {
                return BadRequest();
            }

            var fussballmannschaft = await _context.Fussballmannschaft.FindAsync(fussballmannschaftDTO.Id);
            if (fussballmannschaft == null)
            {
                return NotFound();
            }
            fussballmannschaft.Name = fussballmannschaftDTO.Name;

            _context.Entry(fussballmannschaft).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FussballmannschaftExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Fussballmannschaft
        [HttpPost]
        public async Task<ActionResult<FussballmannschaftDTO>> PostFussballmannschaft(FussballmannschaftDTO fussballmannschaftDTO)
        {
            if (_context.Fussballmannschaft == null)
            {
                return Problem("Entity set 'Fussballmannschagt.fussballmannschaft'  is null.");
            }

            var fussballmannschaft = new Fussballmannschaft { Name = fussballmannschaftDTO.Name };
            _context.Fussballmannschaft.Add(fussballmannschaft);
            await _context.SaveChangesAsync();
            fussballmannschaftDTO.Id = fussballmannschaft.Id;

            return CreatedAtAction(
                nameof(GetFussballmannschaft),
                new { id = fussballmannschaft.Id },
                FussballmannschaftToFussballmannschaftDTO(fussballmannschaft)
            );
        }

        // DELETE: api/Fussballmannschaft/specificId
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFussballmannschaft(int id)
        {
            if (_context.Fussballmannschaft == null)
            {
                return NotFound();
            }
            var fussballmannschaft = await _context.Fussballmannschaft.FindAsync(id);
            if (fussballmannschaft == null)
            {
                return NotFound();
            }

            _context.Fussballmannschaft.Remove(fussballmannschaft);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FussballmannschaftExists(int id)
        {
            return (_context.Fussballmannschaft?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static FussballmannschaftDTO FussballmannschaftToFussballmannschaftDTO(Fussballmannschaft fussballmannschaft)
        {
            return new FussballmannschaftDTO
            {
                Id = fussballmannschaft.Id,
                Name = fussballmannschaft.Name,
            };
        }

        private static Fussballmannschaft FussballmannschaftDTOToFussballmannschaft(FussballmannschaftDTO fussballmannschaftDTO)
        {
            return new Fussballmannschaft
            {
                Id = fussballmannschaftDTO.Id,
                Name = fussballmannschaftDTO.Name,
            };
        }

    }
}