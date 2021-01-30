using Microsoft.AspNetCore.Authorization;
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
    [Route("v1/product")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var product = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
            return Ok(product);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>>Get(int id, [FromServices] DataContext context)
        {
            var product = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
                return NotFound(new { message = "Produto não encontrado!"});
            else
            {
                return Ok(product);
            }
        }

        [HttpGet]
        [Route("category/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetByCategory(int id, [FromServices] DataContext context)
        {
            var product = await context.Products.Include(x => x.Category).Where(x => x.CategoryId == id).AsNoTracking().ToListAsync();
            if(product == null)
                return NotFound(new { message = "Categoria não encontrada"});
            else
                return Ok(product);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles ="Manager")]
        public async Task<ActionResult<Product>> Post([FromBody] Product model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                try
                {
                    context.Products.Add(model);
                    await context.SaveChangesAsync();
                    return Ok(model);
                }catch(Exception E)
                {
                    return BadRequest(new { messsage = E.Message});
                }
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles ="Manager")]
        public async Task<ActionResult<Product>>Put(int id, [FromBody]Product model, [FromServices]DataContext context)
        {
            if(id != model.Id)
                return BadRequest(new { message = "Produto inválido"});
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                try
                {
                    context.Entry<Product>(model).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Ok(model);
                }catch(DbUpdateConcurrencyException E)
                {
                    return BadRequest(new { message = E.Message});
                }catch(Exception E)
                {
                    return BadRequest(new { message = E.Message});
                }
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles ="Manager")]
        public async Task<ActionResult<Product>> Delete (int id, [FromServices]DataContext context){
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
                return NotFound(new { message = "Produto não encontrado"});
            else
            {
                try
                {
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                    return Ok(product);
                }catch(DbUpdateConcurrencyException E)
                {
                    return BadRequest(new { message = E.Message});
                }catch(Exception E)
                {
                    return BadRequest(new { message = E.Message});
                }
            }
        }

    }
}
