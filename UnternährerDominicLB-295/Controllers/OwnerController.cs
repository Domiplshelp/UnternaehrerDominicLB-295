using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnternährerDominicLB_295.Data;
using UnternährerDominicLB_295.Model;

namespace UnternährerDominicLB_295.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly CarDbContext _context;
        public OwnerController(CarDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Owner>> Get() => await _context.Owners.ToListAsync();

        [HttpGet("OwnerId")]
        [ProducesResponseType(typeof(Owner), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            return owner == null ? NotFound() : Ok(owner);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = owner.OwnerId }, owner);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Owner owner)
        {

            if (id != owner.OwnerId) return BadRequest();
            _context.Entry(owner).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var issueToDelete = await _context.Owners.FindAsync(id);
            if (issueToDelete == null) return NotFound();
            _context.Owners.Remove(issueToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

