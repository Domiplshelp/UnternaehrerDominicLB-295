using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnternährerDominicLB_295.Data;
using UnternährerDominicLB_295.Model;

namespace UnternährerDominicLB_295.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Owner2CarsController : ControllerBase
    {
        private readonly CarDbContext _context;
        public Owner2CarsController(CarDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Owner2Car>> Get() => (IEnumerable<Owner2Car>)await _context.Owners2Cars.ToListAsync();

        [HttpGet("GetByCarId/{id}")]
        [ProducesResponseType(typeof(List<Owner2Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCarId(int id)
        {
            var cars = await _context.Owners2Cars.Where(c => c.CarId == id).ToListAsync();
            return !cars.Any() ? NotFound() : Ok(cars);
        }

        [HttpGet("GetByOwnerId/{id}")]
        [ProducesResponseType(typeof(List<Owner2Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByOwnerId(int id)
        {
            var owners = await _context.Owners2Cars.Where(c => c.OwnerId == id).ToListAsync();
            return !owners.Any() ? NotFound() : Ok(owners);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Owner2Car owner2Car)
        {
            if (!ModelState.IsValid || owner2Car.OwnerId == 0 || owner2Car.CarId == 0)
            {
                return BadRequest();
            }

            // Überprüfen, ob OwnerId und CarId existieren
            var ownerExists = await _context.Owners.AnyAsync(o => o.OwnerId == owner2Car.OwnerId);
            var carExists = await _context.Cars.AnyAsync(c => c.CarId == owner2Car.CarId);

            if (!ownerExists || !carExists)
            {
                return BadRequest("OwnerId oder CarId existiert nicht in der Datenbank");
            }

            _context.Owners2Cars.Add(owner2Car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByCarId), new { id = owner2Car.CarId }, owner2Car);
        }


        [HttpDelete("{ownerId}/{carId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int ownerId, int carId)
        {
            var owner2CarToDelete = await _context.Owners2Cars.FirstOrDefaultAsync(oc => oc.OwnerId == ownerId && oc.CarId == carId);

            if (owner2CarToDelete == null)
            {
                return NotFound();
            }

            _context.Owners2Cars.Remove(owner2CarToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
