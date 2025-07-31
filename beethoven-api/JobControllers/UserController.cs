using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.UserModels;
using beethoven_api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.JobControllers;

[Authorize]
public class UserController(UserManager manager) : BeeController
{
    protected UserManager _manager = manager;

    [HttpGet]
    [Route("api/users")]
    public virtual IActionResult FetchAllUsers(){
        var res = _manager.FetchAllUsers().Select(u=>u.ToDTO()).ToList();
        return StatusCode(StatusCodes.Status200OK, res);
    }

    [HttpGet]
    [Route("api/me")]
    public virtual IActionResult FetchMyUser(){
        var res = _manager.FetchUser(_loggedUserId).ToDTO();
        return StatusCode(StatusCodes.Status200OK, res);
    }

    [HttpGet]
    [Route("api/users/{id}")]
    public virtual IActionResult FetchUsers([FromRoute] long id){
        var res = _manager.FetchUser(id).ToDTO();
        return StatusCode(StatusCodes.Status200OK, res);
    }

    [HttpPost]
    [Route("api/users")]
    public virtual IActionResult CreateUser([FromForm] RequestCreateUser model){
        User user = _manager.CreateUser(model.Firstname, model.Lastname, _loggedUserId, model.Email, model.Password, model.Avatar);
        return StatusCode(StatusCodes.Status200OK, user.ToDTO());
    }

    [HttpPut]
    [Route("api/users/{id}")]
    public virtual IActionResult UpdateUser([FromRoute] long id, [FromForm] RequestUpdateUser model){
        try{
            User user = _manager.UpdateUser(id, _loggedUserId, model.Firstname, model.Lastname, model.Email, model.Password, model.Avatar);
            return StatusCode(StatusCodes.Status200OK, user.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
        
    }
}