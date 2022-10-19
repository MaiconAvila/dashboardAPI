using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DashboardAPI.Context;
using DashboardAPI.Models;
using DashboardAPI.Wrappers;
using DashboardAPI.Services;
using System.Data;

namespace DashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DashboardContext _context;
        private readonly IUriService uriService;

        public OrdersController(DashboardContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> GetOrder(
            [FromServices] DashboardContext context, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var allItems = await context
                .Order
                .Include(x => x.Product)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .AsNoTracking()
                .ToListAsync();

            var total = await context.Order.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Order>(allItems, validFilter, total, uriService, route);

            return Ok(pagedReponse);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(new Response<Order>(order));
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] Order order, [FromServices] DashboardContext context)
        {
            var findOrder = await context.Order.FirstOrDefaultAsync(x => x.Id == id);
            if (findOrder == null)
            {
                return BadRequest();
            }

            findOrder.Address = order.Address != null ? order.Address : findOrder.Address;
            findOrder.DeliveryDate = findOrder.DeliveryDate;
            findOrder.Product = findOrder.Product = order.Product != null ? (List<Product>)order.Product.Select(x => new Product
            {
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            }).ToList(): findOrder.Product;

            context.Entry(findOrder).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(findOrder);
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
