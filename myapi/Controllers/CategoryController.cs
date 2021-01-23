using Microsoft.AspNetCore.Mvc;
using myapi.Models;
using myapi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Controllers
{
    [Route("categoriess")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return new List<Category>();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            return new Category();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Post([FromBody] Category model)
        {
            return new List<Category>();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Put([FromBody] Category model, int id)
        {
            if(model.Id == id)
                return new List<Category>();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            return Ok();
        }
    }
}
