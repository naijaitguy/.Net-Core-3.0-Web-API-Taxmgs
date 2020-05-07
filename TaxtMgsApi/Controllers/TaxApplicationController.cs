using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxtMgsApi.DAL;using TaxtMgsApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxtMgsApi.Controllers
{
    [Route("api/[controller]")]
    public class TaxApplicationController : ControllerBase
    {

        private IUnitOfWork Repo;

        public TaxApplicationController(IUnitOfWork unitOfWork) { this.Repo = unitOfWork; }
        // GET: api/<controller>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IList<TaxApplication>> Get()
        {
            string Query = "[GET_All_TaxApplication] {0}";
            int count = 10;
            object[] Parameters = { count };

            try
            {
                List<TaxApplication> Result = await Repo.TaxApp_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Result; }
                else { return null; }
            }
            catch (Exception Ex)
            {

                return null;
            }

        }

        // GET api/<controller>/5
        [HttpGet("GetById/{id}")]
        [Route("GetById")]
        public async Task<IActionResult> Get(int? id)
        {

            if(id == null) { return NotFound("id not found"); }

            string Query = " {0}";
            object[] Parameters = { id};

            try
            {
               var Result = await Repo.TaxApp_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Ok(Result); }

                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);
            }


        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddTaxApp")]
        public async Task<IActionResult> Post([FromBody] TaxApplication model)
        {
            

            string Query = "[Add_Taxapplication] {0},{1},{2},{3},{4},{5},{6},{7},{8},{9} ";
            object[] Parameters = {
                model.CompanyAddress,
                model.CompanyPhoneNo,
                model.CompanyWebsite,
                model.CurrentPositon,
                model.CurrentSalary,
                model.Email,
                model.PaymentStatus,
                model.StaffId,
             
                model.Bvn,
                model.CompanyName };

            try
            {
                var Result = await Repo.TaxApp_Repository.ExecuteCommand(Query, Parameters);
                if (Result > 0) { return Ok(); }

                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);
            }


        }
        // PUT api/<controller>/5
        [HttpPut("Update/{id}")]
        [Route("Update")]
        public async Task<IActionResult> Put(int? id, [FromBody]TaxApplication model)
        {



            if (id == null) { return NotFound("id not found"); }

            string Query = " {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10} ";
            object[] Parameters = { id,
                model.CompanyAddress,
                model.CompanyPhoneNo,
                model.CompanyWebsite,
                model.CurrentPositon,
                model.CurrentSalary,
                model.Email,
                model.PaymentStatus,
                model.StaffId,
                model.TaxAmount,
               
                model.Bvn,
                model.CompanyName };

            try
            {
                var Result = await Repo.TaxApp_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Ok(Result); }

                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("Delete/{id}")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {



            if (id == null) { return NotFound("id not found"); }

            string Query = " {0}";
            object[] Parameters = { id };

            try
            {
                var Result = await Repo.TaxApp_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Ok(); }

                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);
            }

        }

        [HttpPost]
        [Route("PostFindByEmail")]
        public async Task<IActionResult> PostFindByEmail([FromBody] FindUsers model)
        {

            if (model.Email == null) { return NotFound("Email not found"); }

            string Query = "[GetTaxAppByEmail] {0}";
            object[] Parameters = { model.Email};

            try
            {
                var Result = await Repo.TaxApp_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Ok(Result); }

                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);
            }


        }

        [HttpPost]
        [Route("PostFindByBVN")]
        public async Task<IActionResult> PostFindByBVN([FromBody]FindUsers model)
        {

            if (model.BVN == null) { return NotFound("BVN not found"); }

            string Query = "[GetTaxAppByBVN] {0}";
            object[] Parameters = { model.BVN };

            try
            {
                var Result = await Repo.TaxApp_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Ok(Result); }

                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);
            }


        }


        [HttpPost]
        [Route("PostFindByTIN")]
        public async Task<IActionResult> PostFindByTIN([FromBody] FindUsers model)
        {

            if (model.TIN == null) { return NotFound("Email not found"); }

            string Query = " [GetTaxAppByTIN] {0}";
            object[] Parameters = { model.TIN };

            try
            {
                var Result = await Repo.TaxApp_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Ok(Result); }

                else { return BadRequest(); }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);
            }


        }


        public async Task<IActionResult> GetCombinInfo([FromBody] TaxRegistration taxRegistration, TaxApplication taxApplication ) {


            string Query = "[GET_All_TaxApplication] {0}";
            int count = 10;
            object[] Parameters = { count };

            try
            {
                List<TaxApplication> Result = await Repo.TaxApp_Repository.ExecuteQuery(Query, Parameters);
                if (Result.Count() > 0) { return Ok( Result) ; }
                else { return null; }
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex);
            }

        }


    }
}
