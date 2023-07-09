using FootballManagement.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpielerController : ControllerBase
    {
        private readonly FussballmannschaftDB _context;

        public SpielerController(FussballmannschaftDB context)
        {
            _context = context;
        }

        // GET: api/Spieler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpielerDTO>>> GetSpieler()
        {
            if (_context.Spieler == null)
            {
                return NotFound();
            }
            return await _context.Spieler.Select(x => SpielerToSpielerDTO(x)).ToListAsync();
        }

        // GET: api/Spieler/Zahl (id)
        [HttpGet("{id}")]
        public async Task<ActionResult<SpielerDTO>> GetSpieler(int id)
        {
            if (_context.Spieler == null)
            {
                return NotFound();
            }
            var spieler = await _context.Spieler.FindAsync(id);

            if (spieler == null)
            {
                return NotFound();
            }

            return SpielerToSpielerDTO(spieler);
        }

        // PUT: api/Spieler/specificNumber
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpieler(int id, SpielerDTO spielerDTO)
        {
            if (id != spielerDTO.Id)
            {
                return BadRequest();
            }
            var spieler = await _context.Spieler.FindAsync(spielerDTO.Id);
            if (spieler == null)
            {
                return NotFound();
            }
            spieler.Nachname = spielerDTO.Nachname;
            spieler.Vorname = spielerDTO.Vorname;

            _context.Entry(spieler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpielerExists(id))
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

        [HttpPost("AddPlayer/{fussballmannschaftId}")]
        public async Task<IActionResult> AddPlayerToMannschaft(int fussballmannschaftId, SpielerDTO spielerDTO)
        {
            var fussballmannschaft = await _context.Fussballmannschaft.FindAsync(fussballmannschaftId);
            if (fussballmannschaft == null)
            {
                return NotFound();
            }

            var spieler = new Spieler
            {
                Nachname = spielerDTO.Nachname,
                Vorname = spielerDTO.Vorname
            };

            fussballmannschaft.Spieler.Add(spieler);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetSpieler),
                new { id = spieler.Id },
                SpielerToSpielerDTO(spieler)
            );
        }


        // POST: api/Spieler
        [HttpPost]
        public async Task<ActionResult<SpielerDTO>> PostSpieler(SpielerDTO spielerDTO)
        {
            if (_context.Spieler == null)
            {
                return Problem("Entity set 'Fussballmannschaft.Spieler'  is null.");
            }
            var spieler = new Spieler
            {
                Nachname = spielerDTO.Nachname,
                Vorname = spielerDTO.Vorname,
            };
            _context.Spieler.Add(spieler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpieler", new { id = spieler.Id }, spielerDTO);
        }

        // DELETE: api/Spieler/specificSpieler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpieler(int id)
        {
            if (_context.Spieler == null)
            {
                return NotFound();
            }
            var spieler = await _context.Spieler.FindAsync(id);
            if (spieler == null)
            {
                return NotFound();
            }

            _context.Spieler.Remove(spieler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpielerExists(int id)
        {
            return (_context.Spieler?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        private static SpielerDTO SpielerToSpielerDTO(Spieler spieler)
        {
            return new SpielerDTO
            {
                Id = spieler.Id,
                Nachname = spieler.Nachname,
                Vorname = spieler.Vorname,

            };
        }
    }
}
