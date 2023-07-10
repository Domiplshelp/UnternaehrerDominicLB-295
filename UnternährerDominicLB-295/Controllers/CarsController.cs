using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnternährerDominicLB_295.Data;
using UnternährerDominicLB_295.Model;

namespace UnternährerDominicLB_295.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarDbContext _context;
        public CarsController(CarDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Car>> Get() =>  await _context.Cars.ToListAsync();

        [HttpGet("CarId")]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            return car == null ? NotFound() : Ok(car);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = car.CarId }, car);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Car car)
        {

            if (id != car.CarId) return BadRequest();
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var issueToDelete = await _context.Cars.FindAsync(id);
            if (issueToDelete == null) return NotFound();
            _context.Cars.Remove(issueToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}


