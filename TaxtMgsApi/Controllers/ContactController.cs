using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxtMgsApi.DAL;
using TaxtMgsApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxtMgsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("Json/application")]
    public class ContactController : ControllerBase
    {

        private IUnitOfWork Repo;
        
        public ContactController( IUnitOfWork unitOfWork) { this.Repo = unitOfWork; }

        // GET: api/<controller>
        [HttpGet]
        public async Task< List<ContactRecord>> Get()
        {
            string Query = "";
            int count = 10;
            object[] Parameters = { count };

            try {
               List<ContactRecord> Result = await Repo.Contact_Repository.ExecuteQuery(Query, Parameters);
                if(Result.Count() > 0) { return Result; }
                else { return null; }
            }
            catch (Exception Ex)
            {

                return null;
            }

         
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task< IActionResult>  Post([FromBody] ContactRecord model)
        {

            string Query = "   {0}, {1}, {2}, {3}";
            
            object[] Parameters = { model.Comment, model.Email, model.Name, model.Phone};

            try
            {
                var Result = await Repo.Contact_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Ok( Result); }
                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);

            }


        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
