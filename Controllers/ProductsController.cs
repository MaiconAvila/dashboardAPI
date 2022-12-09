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
    public class ProductsController : ControllerBase
    {
        private readonly DashboardContext _context;
        private readonly IUriService uriService;

        public ProductsController(DashboardContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult> GetProduct(
            [FromServices] DashboardContext context, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var allItems = await context
                .Product
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .AsNoTracking()
                .ToListAsync();

            var total = await context.Product.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse(allItems, validFilter, total, uriService, route);

            if (pagedReponse == null)
            {
                return NotFound();
            }

            return Ok(pagedReponse);
        }

        // GET: api/Products/Total
        [HttpGet("Total")]
        public async Task<IActionResult> GetTotalProducts(
            [FromServices] DashboardContext context)
        {
            var items = await context.Product
                .ToListAsync();

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context
                .Product
                .FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new Response<Product>(product));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] Product product, [FromServices] DashboardContext context)
        {
            var findProduct = await context
                .Product
                .FirstOrDefaultAsync(x => x.Id == id);
            if (findProduct == null)
            {
                return BadRequest();
            }


            try
            {
                findProduct.Id = findProduct.Id;
                findProduct.Name = product.Name != null ? product.Name : findProduct.Name;
                findProduct.Description = product.Description != null ? product.Description : findProduct.Description;
                findProduct.Value = product.Value != 0 ? product.Value : findProduct.Value;

                context.Entry(findProduct).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(findProduct);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context
                .Product
                .FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
