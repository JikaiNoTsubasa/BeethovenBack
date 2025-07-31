using System;
using beethoven_api.Database.DTO.LoginModels;
using beethoven_api.JobManagers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.JobControllers;

[AllowAnonymous]
public class LoginController(AuthManager manager) : Controller
{
    private readonly AuthManager _manager = manager;
    
    [HttpPost]
    [Route("/api/login")]
    public virtual IActionResult Login([FromForm] RequestLogin model){
        try{
            ResponseLogin res = _manager.LoginUser(model.Email, model.Password);
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
