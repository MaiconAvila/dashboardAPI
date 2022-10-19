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
    public class TeamsController : ControllerBase
    {
        private readonly DashboardContext _context;

        public TeamsController(DashboardContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IActionResult> GetTeam(
            [FromServices] DashboardContext context,
            int skip = 0,
            int take = 20)
        {
            var total = await context.Team.CountAsync();
            var allItems = await context
                .Team
                .Include(x => x.Order)
                .ThenInclude(x => x.Product)
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return Ok(new { total, skip, take, allItems });
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Team.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, [FromBody] Team team, 
            [FromServices] DashboardContext context)
        {
            var findTeam = await context.Team.FirstOrDefaultAsync(x => x.Id == id);
            if (findTeam == null)
            {
                return BadRequest();
            }

            try
            {
                findTeam.Id = findTeam.Id;
                findTeam.Name = team.Name != null ? team.Name : findTeam.Name;
                findTeam.Description = team.Description != null ? team.Description : findTeam.Description;
                findTeam.LicensePlate = team.LicensePlate != null ? team.LicensePlate : findTeam.LicensePlate;
                findTeam.Order = team.Order != null ? (List<Order>)team.Order.Select(x => new Order
                {
                    Address = x.Address,
                    CreateAt = x.CreateAt,
                    DeliveryDate = x.DeliveryDate,
                    IdTeam = findTeam.Id,
                    NameTeam = team.Name != null ? team.Name : findTeam.Name,
                    Product = x.Product.Select(i => new Product
                    {
                        Name = i.Name,
                        Description = i.Description,
                        Value = i.Value
                    }).ToList(),
                    
                }).ToList() : findTeam.Order;

                context.Team.Update(findTeam);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(findTeam);
        }

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        public async Task<IActionResult> PostTeam(Team team)
        {
            _context.Team.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Team.Remove(team);
            await _context.SaveChangesAsync();

            return Ok(team);
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.Id == id);
        }
    }
}
