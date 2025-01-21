using beethoven_api.Controllers;
using beethoven_api.Database;
using beethoven_api.Database.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace beethoven_api.Controllers.JobControllers;

public class UserController(BeeDBContext context) : BeeController(context)
{

    [HttpGet]
    [Route("api/users")]
    public virtual IActionResult FetchUsers(){
        try{
            var res =_context.Users.Select(u=>u.ToDTO());
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}