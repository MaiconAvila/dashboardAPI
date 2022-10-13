using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DashboardAPI.Context;
using DashboardAPI.Models;

namespace DashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandsController : ControllerBase
    {
        private readonly DashboardContext _context;

        public DemandsController(DashboardContext context)
        {
            _context = context;
        }

        // GET: api/Demands
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IActionResult> GetDemand(
            [FromServices] DashboardContext context,
            int skip = 0,
            int take = 20)
        {
            var total = await context.Demand.CountAsync();
            var allItems = await context
                .Demand
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return Ok(new { total, skip, take, allItems });
        }

        // GET: api/Demands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Demand>> GetDemand(int id)
        {
            var demand = await _context.Demand.FindAsync(id);

            if (demand == null)
            {
                return NotFound();
            }

            return demand;
        }

        // PUT: api/Demands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemand(int id, Demand demand)
        {
            if (id != demand.Id)
            {
                return BadRequest();
            }

            _context.Entry(demand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemandExists(id))
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

        // POST: api/Demands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDemand(Demand demand)
        {
            _context.Demand.Add(demand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDemand", new { id = demand.Id }, demand);
        }

        // DELETE: api/Demands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemand(int id)
        {
            var demand = await _context.Demand.FindAsync(id);
            if (demand == null)
            {
                return NotFound();
            }

            _context.Demand.Remove(demand);
            await _context.SaveChangesAsync();

            return Ok(demand);
        }

        private bool DemandExists(int id)
        {
            return _context.Demand.Any(e => e.Id == id);
        }
    }
}
