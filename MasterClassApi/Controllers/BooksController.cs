using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterClassApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterClassApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/Books
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Books/5
        [Authorize(Roles ="Admin,Publisher", Policy ="SolidQDomain")]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Books
        [Authorize(Roles ="Admin, Publisher")]
        
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Publisher")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "Admin", Policy = "UADomain")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
