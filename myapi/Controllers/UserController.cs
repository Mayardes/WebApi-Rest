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
