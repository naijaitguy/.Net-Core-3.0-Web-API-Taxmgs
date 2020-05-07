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
    [Produces("Application/json")]
    [ApiController]
    public class TaxRegistrationController : ControllerBase
    {

        IUnitOfWork IUnitOfWork;
        public TaxRegistrationController(IUnitOfWork _iunitOfWork)
        {

            IUnitOfWork = _iunitOfWork;

        }

        // GET: api/<controller>
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<TaxRegistration>> Get()
        {
            //return await IUnitOfWork.Admin_Repository.GetAll();
            string Query = "dbo.GET_All_TaxRegUser {0}";
            object[] Parameter = { 10 };

            return await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query, Parameter);
        }

        // GET api/<controller>/
        [HttpGet("GetById/{id}")]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {

            if (id == null)

            { return NotFound("No Id Found"); }
            try
            {

                // Admin User =  await IUnitOfWork.Admin_Repository.GetById(id);

                string Query = "GetTaxRegByID {0}";

                object[] Para = { id };

                var User = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query, Para);
                if (User.Count() < 1) { return NotFound("User Not Found"); }

                else
                {

                    return Ok(User);
                }

            }
            catch (Exception)
            {
                return NotFound("Update failed");
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> Post([FromBody] TaxRegistration taxRegistration)
        {

            string Query_FindEmail = "GetTaxRegByEmail {0}";

            object[] Para_FindEmail = { taxRegistration.Email };


            string Query_FindUsername = "[GetTaxRegByUserName] {0}";

            object[] Para_FindUsername = { taxRegistration.UserName };

            string Query_FindPhone = "[GetTaxRegByPhone] {0}";

        object[] Para_FindPhone = { taxRegistration.PhoneNo };



            string Query_AddUser = "[AddUser] {0},{1},{2},{3},{4},{5}";

            object[] Para_AddUser = {
                taxRegistration.UserName,
                taxRegistration.Password,
                taxRegistration.Address,
                taxRegistration.PhoneNo,
                taxRegistration.Email,
                taxRegistration.FullName };


            try
            {

                if (ModelState.IsValid)
                {

                    // await IUnitOfWork.Admin_Repository.Add(admin);
                    // await IUnitOfWork.Commit();
                    var Result_Email = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query_FindEmail, Para_FindEmail);
                    if (Result_Email.Count() > 0)
                    {
                         return NotFound("Email Already Exist");

                    
                    }

                    var Result_Username = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query_FindUsername, Para_FindUsername);
                    if (Result_Username.Count() > 0) { return NotFound("UserName  Already Exist"); }

                    var Result_Phone = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query_FindPhone,Para_FindPhone);
                    if (Result_Phone.Count() > 0) { return NotFound("Phone Number  Already Exist"); }

                    var Result_Add = await IUnitOfWork.TaxReg_Repository.ExecuteCommand(Query_AddUser, Para_AddUser);
                    if (Result_Add > 0)

                    {
                        return Ok("Added Successfuly");
                    }

                }
                return NotFound("modelerror");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }

        // PUT api/<controller>/5
        [HttpPut("UpdateUser/{id}")]
        [Route("UpdateUser")]
        public async Task<IActionResult> Put(int? id, [FromBody]TaxRegistration taxRegistration)
        {
            if (id == null) { return NotFound("empty id"); }

            string Query_Update = "[Update_TaxReg] {0},{1}, {2}, {3},{4},{5} ";
            object[] Parameter_uPDATE = { id, taxRegistration.Password, 
                taxRegistration.Address, 
                taxRegistration.PhoneNo,
                taxRegistration.UserName,
                taxRegistration.FullName,
                 
        };
           

            string Query = "GetTaxRegByID {0}";

            object[] Para = { id };
            try
            {

                var Result_FInduser = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query, Para);

                if (Result_FInduser.Count() > 0)
                {
                    var Result_Update = await IUnitOfWork.TaxReg_Repository.ExecuteCommand(Query_Update, Parameter_uPDATE);

                    if (Result_Update > 0)
                    {

                        return Ok();
                    }
                    else { return NotFound(" User  Not Found"); }
                }
                else { return NotFound(" User  Not Found"); }


            }
            catch (Exception)
            {

                IUnitOfWork.RollBack();
                return BadRequest();
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("Delete/{id}")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound("Id Not Found"); }


            string Query_DeleteUser = "[DeleteUser] {0}";
            string Query = "GetTaxRegByID {0}";

            object[] Para = { id };

            try
            {

                // await IUnitOfWork.Admin_Repository.Delete(id);
                //await  IUnitOfWork.Commit();
                // return Ok();
                var Result_FInduser = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query, Para);

                if (Result_FInduser.Count() > 0)
                {

                    var Result_Delete = await IUnitOfWork.TaxReg_Repository.ExecuteCommand(Query_DeleteUser, Para);

                    if (Result_Delete > 0)
                    {

                        return Ok();
                    }
                    else { return NotFound("Could Not Delete"); }

                }
                else { return NotFound("Could Not Delete"); }


            }
            catch (Exception Ex)
            {

                // IUnitOfWork.RollBack();
                return BadRequest(Ex);
            }


        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> Post([FromBody] UserLogin userLogin)
        {

            string Query_ = "[LoginUser] {0},{1}";
            object[] Parameter = { userLogin.Email, userLogin.Password };

            if (ModelState.IsValid)
            {

                try
                {
                    var Result = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query_, Parameter);

                    if (Result.Count() > 0) { return Ok(Result); }
                    else { return BadRequest("invalid user"); }


                }
                catch (Exception )
                { { return BadRequest("Invalid Login"); } }

            }
            return BadRequest();
        }

       
        [HttpPost]
        [Route("FindPhone")]
        public async Task<IActionResult> Post([FromBody] FindUsers model)
        {

            if (model.PhoneNo == null)

            { return NotFound("empty"); }
            try
            {

                // Admin User =  await IUnitOfWork.Admin_Repository.GetById(id);

                string Query = "[GetTaxRegByPhone] {0}";

                object[] Para = { model.PhoneNo };

                var User = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query, Para);
                if (User.Count() < 1) { return NotFound("User Not Found"); }

                else
                {

                    return Ok(User);
                }

            }
            catch (Exception)
            {
                return NotFound("error");
            }
        }




        [HttpPost]
        [Route("PostFindEmail")]
        public async Task<IActionResult> PostFindEmail([FromBody] FindUsers model)
        {

            if (model.Email == null)

            { return NotFound("empty"); }
            try
            {

                // Admin User =  await IUnitOfWork.Admin_Repository.GetById(id);

                string Query = "GetTaxRegByEmail {0}";

                object[] Para = { model.Email };

                var User = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query, Para);
                if (User.Count() < 1) { return NotFound("User Not Found"); }

                else
                {

                    return Ok(User);
                }

            }
            catch (Exception)
            {
                return NotFound("error");
            }
        }


        [HttpPost]
        [Route("PostFindUsername")]
        public async Task<IActionResult> PostFindUserName([FromBody] FindUsers model)
        {

            if (model.UserName == null)

            { return NotFound("empty"); }
            try
            {

                // Admin User =  await IUnitOfWork.Admin_Repository.GetById(id);

                string Query = "[GetTaxRegByUserName] {0}";

                object[] Para = { model.UserName };

                var User = await IUnitOfWork.TaxReg_Repository.ExecuteQuery(Query, Para);
                if (User.Count() < 1) { return NotFound("User Not Found"); }

                else
                {

                    return Ok(User);
                }

            }
            catch (Exception)
            {
                return NotFound("error");
            }
        }



    }
}
