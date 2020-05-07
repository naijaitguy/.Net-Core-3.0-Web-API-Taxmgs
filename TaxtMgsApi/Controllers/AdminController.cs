using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxtMgsApi.DAL;
using TaxtMgsApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxtMgsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors("AllowAll")]
    public class AdminController : ControllerBase
    {

        IUnitOfWork IUnitOfWork;
        public AdminController(IUnitOfWork _iunitOfWork ) {

            IUnitOfWork = _iunitOfWork;
        
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("GetAll")]
        public async Task< List<Admin>> Get()
        {
            //return await IUnitOfWork.Admin_Repository.GetAll();
            string Query = "dbo.GET_All_AdminUser {0}";
            object[] Parameter = { 10 };

            return await IUnitOfWork.Admin_Repository.ExecuteQuery(Query, Parameter);
        }
        
        // GET api/<controller>/
        [HttpGet("GetById/{id}")]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {

            if (id == null)

               { return NotFound("No Id Found"); }
               try {
     
               // Admin User =  await IUnitOfWork.Admin_Repository.GetById(id);

                string Query = "GET_ADMINUSER_BYID {0}";
                
                object[] Para = { id };

           var  User = await IUnitOfWork.Admin_Repository.ExecuteQuery(Query,Para);
                if (User.Count() < 1) { return NotFound(); }

                else
                {

                    return Ok(User);
                }
            
            }
            catch ( Exception Ex)
            {
                return  NotFound( Ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add-Admin")]
        public async Task<IActionResult> Post([FromBody] Admin admin)
        {

            string Query_FindEmail = "GET_ADMINUSER_BY_Email {0}";

            object[] Para_FindEmail = {admin.Email };

            string Query_FindUsername = "[GET_ADMINUSER_BY_UserName] {0}";

            object[] Para_FindUsername = {admin.UserName };


            string Query_AddAdmin = "[Add_Admin] {0},{1},{2},{3}";

            object[] Para_Addadmn = { admin.UserName, admin.Password ,admin.Email ,admin.FullName };


            try
                {

                  if (ModelState.IsValid)
                    {

                    // await IUnitOfWork.Admin_Repository.Add(admin);
                    // await IUnitOfWork.Commit();
                    var Result_Email = await IUnitOfWork.Admin_Repository.ExecuteQuery(Query_FindEmail, Para_FindEmail);
                    if (Result_Email.Count() > 0) { return NotFound("Email Already Exist"); }

                    var Result_Username = await IUnitOfWork.Admin_Repository.ExecuteQuery(Query_FindUsername, Para_FindUsername);
                    if(Result_Username.Count()> 0) { return NotFound("UserName  Already Exist"); }

                    var Result_Add = await IUnitOfWork.Admin_Repository.ExecuteCommand(Query_AddAdmin, Para_Addadmn);
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
        [HttpPut("Update-Admin/{id}")]
        [Route("Update-Admin")]
        public async Task<IActionResult> Put(int?  id, [FromBody]Admin admin)
        {
            if(id == null) { return NotFound("empty id"); }

            string Query_Update = "[Update_Admin] {0},{1}, {2}, {3}";
            object[] Parameter_uPDATE = { id, admin.UserName,admin.FullName,admin.Password };

            string Query_FindUser = "[GET_ADMINUSER_BYID] {0}";
            object[] Parameter_Finduser = { id };

            try {

                var Result_FInduser = await IUnitOfWork.Admin_Repository.ExecuteQuery(Query_FindUser, Parameter_Finduser);

                if (Result_FInduser.Count() > 0)
                {
                    var Result_Update = await IUnitOfWork.Admin_Repository.ExecuteCommand(Query_Update, Parameter_uPDATE);

                    if (Result_Update > 0)
                    {

                        return Ok();
                    }
                    else { return NotFound(" User  Not Found"); }
                }
                else { return NotFound(" User  Not Found"); }

                
            }
            catch(Exception)
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
            if (id == null) { return NotFound(); }

          
            string Query_DeleteAdmin = "[Delete_Admin] {0}";
            string Query_FindUser = "[GET_ADMINUSER_BYID] {0}";
            object[] Parameter_Finduser = { id };

            try {

                // await IUnitOfWork.Admin_Repository.Delete(id);
                //await  IUnitOfWork.Commit();
                // return Ok();
                var Result_FInduser = await IUnitOfWork.Admin_Repository.ExecuteQuery(Query_FindUser, Parameter_Finduser);

                if (Result_FInduser.Count() > 0)
                {

                    var Result_Delete = await IUnitOfWork.Admin_Repository.ExecuteCommand(Query_DeleteAdmin, Parameter_Finduser);

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
                return BadRequest( Ex);
            }


        }

        [HttpPost]
        [Route("AdminLogin")]
        public async Task<IActionResult> Post([FromBody] AdminLogin adminLogin) {

            string Query_ = "[LoginAdmin] {0},{1}";
            object[] Parameter = {adminLogin.Email,adminLogin.Password };

            if (ModelState.IsValid) {

                try {

                    var Result = await IUnitOfWork.Admin_Repository.ExecuteQuery(Query_, Parameter);

                    if (Result.Count() > 0) { return Ok(); }
                    else { return BadRequest("invalid user"); }


                }
                catch ( Exception Ex)
                { { return BadRequest( Ex); } }
               
            }
            return BadRequest();
        }
    }
}
