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
    public class OrdersController : ControllerBase
    {
        private readonly DashboardContext _context;

        public OrdersController(DashboardContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet("{skip:int}/{take:int}")]
        public async Task<IActionResult> GetOrder(
            [FromServices] DashboardContext context,
            int skip = 0,
            int take = 20)
        {
            var total = await context.Order.CountAsync();
            var allItems = await context
                .Order
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return Ok(new { total, skip, take, allItems });
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

            return order;
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

            findOrder.Id = findOrder.Id;
            findOrder.CreateAt = findOrder.CreateAt;
            findOrder.DeliveryDate = findOrder.DeliveryDate;
            findOrder.Address = order.Address != null ? order.Address : findOrder.Address;
            findOrder.IdProduct = order.IdProduct != 0 ? order.IdProduct : findOrder.IdProduct;

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
