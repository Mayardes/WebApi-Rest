using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Data;
using myapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Controllers
{
    [Route("product")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var product = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            return Ok(product);
        }
    }
}
