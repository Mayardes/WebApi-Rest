using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Data;
using myapi.Models;
using myapi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Controllers
{
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var category = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(category);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id, [FromServices] DataContext context)
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
                return NotFound(new { message ="Categoria não encontrada"});
            else
                return Ok(category);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Post(
            [FromBody] Category model, 
            [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try 
            { 
                context.Categories.Add(model);
                await context.SaveChangesAsync();

            }catch(Exception E)
            {
                return BadRequest( new { message=E.Message });

            }

            return Ok(model);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Put([FromBody] Category model, int id, [FromServices]DataContext context)
        {
            if(model.Id != id)
                return BadRequest(new { message="Categoria não encontrada!"});
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                try
                {
                    context.Entry<Category>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException E)
                {
                    return BadRequest(new { message =E.Message});
                }
                catch(Exception E)
                {
                    return BadRequest(new { message = E.Message});
                }

                return Ok(model);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete(int id, [FromServices] DataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
                return NotFound(new { message = "Categoria não encontrada"});
            else
            {
                try
                {
                    context.Categories.Remove(category);
                    await context.SaveChangesAsync();
                }
                catch(Exception E)
                {
                    return BadRequest(new { Message=E.Message});
                }
            }
            return Ok(category);
        }
    }
}
