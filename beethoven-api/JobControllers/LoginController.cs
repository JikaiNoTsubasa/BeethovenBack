using System;
using beethoven_api.Database.DTO.LoginModels;
using beethoven_api.JobManagers;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.JobControllers;

[AllowAnonymous]
public class LoginController(AuthManager manager) : Controller
{
    private static readonly ILog log = LogManager.GetLogger(typeof(LoginController));
    private readonly AuthManager _manager = manager;
    
    [HttpPost]
    [Route("/api/login")]
    public virtual IActionResult Login([FromBody] RequestLogin model){
        try{
            ResponseLogin res = _manager.LoginUser(model.Email, model.Password);
            log.Info($"User {model.Email} logged in");
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            log.Error("Could not login to server",e);
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
