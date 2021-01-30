using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Data;
using myapi.Models;
using myapi.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Controllers
{
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post([FromBody]User model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                model.Role = "Admin";
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }catch(DBConcurrencyException E)
            {
                return BadRequest(new { message = E.Message});
            }catch(Exception E)
            {
                return BadRequest(new { message = E.Message});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles ="Manager")]
        public async Task<ActionResult<User>> Put (int id, [FromServices] DataContext context, [FromBody]User model)
        {
            if(id != model.Id)
                return BadRequest(new { message="Elementos são diferentes"});
            else
                try
                {
                    context.Entry<User>(model).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Ok(model);

                }catch( DBConcurrencyException E)
                {
                    return BadRequest(new { message = E.Message});
                }catch(Exception E)
                {
                    return BadRequest(new { E.Message});
                }
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles ="Manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            return await context.Users.AsNoTracking().ToListAsync();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody]User model)
        {
            var user = await context
                .Users.AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if(user == null)
                return NotFound(new { message = "Usuário ou senha inválidos"});
            
            var token = TokenService.GenerateToken(user);
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
