using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DashboardAPI.Context;
using DashboardAPI.Models;
using DashboardAPI.Services;
using DashboardAPI.Wrappers;

namespace DashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly DashboardContext _context;
        private readonly IUriService uriService;

        public TeamsController(DashboardContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<IActionResult> GetTeam(
            [FromServices] DashboardContext context, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var allItems = await context
                .Team                
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .AsNoTracking()
                .ToListAsync();

            var total = await context.Team.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse(allItems, validFilter, total, uriService, route);
            
            return Ok(pagedReponse);
        }

        // GET: api/Teams/Total
        [HttpGet("Total")]
        public async Task<IActionResult> GetTotalTeams(
            [FromServices] DashboardContext context)
        {
            var items = await context.Team
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context
                .Team
                .FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(new Response<Team>(team));
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, [FromBody] Team team, 
            [FromServices] DashboardContext context)
        {
            var findTeam = await context
                .Team
                .FirstOrDefaultAsync(x => x.Id == id);
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
                //findTeam.Order = team.Order != null ? (List<Order>)team.Order.Select(x => new Order
                //{
                //    Address = x.Address,
                //    CreateAt = x.CreateAt,
                //    DeliveryDate = x.DeliveryDate,
                //    IdTeam = findTeam.Id,
                //    NameTeam = team.Name != null ? team.Name : findTeam.Name,
                //    Product = x.Product.Select(i => new Product
                //    {
                //        Name = i.Name,
                //        Description = i.Description,
                //        Value = i.Value
                //    }).ToList(),
                    
                //}).ToList() : findTeam.Order;

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
