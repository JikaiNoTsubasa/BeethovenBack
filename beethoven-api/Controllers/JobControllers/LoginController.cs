using System;
using beethoven_api.Database;
using beethoven_api.Database.DTO.LoginModels;
using beethoven_api.Global.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.Controllers.JobControllers;

[AllowAnonymous]
public class LoginController(BeeEngine engine) : Controller
{
    private readonly BeeEngine _engine = engine;
    
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
