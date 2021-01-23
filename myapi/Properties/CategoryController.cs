using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace myapi.Properties
{
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Olá Mundo";
        }

        [HttpGet]
        [Route("{id:int}")]
        public string GetById(int id)
        {
            return "Olá mundo";
        }

        [HttpPost]
        [Route("")]
        public string Post()
        {
            return "Olá Mundo";
        }

        [HttpPut]
        [Route("")]
        public string Put()
        {
            return "Olá Mundo";
        }

        [HttpDelete]
        [Route("")]
        public string Delete()
        {
            return "Olá Mundo";
        }

    }
}
