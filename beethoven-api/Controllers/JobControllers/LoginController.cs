using System;
using beethoven_api.Database;
using beethoven_api.Database.DTO.LoginModels;
using beethoven_api.Global.Engine;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.Controllers.JobControllers;

public class LoginController(BeeDBContext context, BeeEngine engine) : BeeController(context, engine)
{
    [HttpPost]
    [Route("/api/login")]
    public virtual IActionResult Login([FromForm] RequestLogin model){
        try{
            ResponseLogin res = _engine.LoginUser(model.Email, model.Password);
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
