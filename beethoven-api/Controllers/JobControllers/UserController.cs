using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.UserModels;
using beethoven_api.Global.Engine;
using beethoven_api.Global.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.Controllers.JobControllers;

[Authorize]
public class UserController(BeeDBContext context, BeeEngine engine) : BeeController(context, engine)
{
    private IQueryable<User> GenerateUserQuery(){
        return _context.Users
            .Include(u=>u.Teams);
        }

    [HttpGet]
    [Route("api/users")]
    public virtual IActionResult FetchUsers(){
        try{
            var res = GenerateUserQuery()
                .Paged(_pagination, out QueryMeta? meta)
                .Select(u=>u.ToDTO());
            return StatusCode(StatusCodes.Status200OK, res, meta);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpGet]
    [Route("api/myuser")]
    public virtual IActionResult FetchMyUser(){
        try{
            var res = GenerateUserQuery().FirstOrDefault(u=>u.Id == _loggedUserId)?.ToDTO();
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpGet]
    [Route("api/user/{id}")]
    public virtual IActionResult FetchUsers([FromRoute] long id){
        try{
            var res = GenerateUserQuery()
                .FirstOrDefault(u=>u.Id == id)?
                .ToDTO();
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpPost]
    [Route("api/user")]
    public virtual IActionResult CreateUser([FromForm] RequestCreateUser model){
        try{
            User user = _engine.CreateUser(model, _loggedUserId);
            return StatusCode(StatusCodes.Status200OK, user.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpPut]
    [Route("api/user/{id}")]
    public virtual IActionResult UpdateUser([FromRoute] long id, [FromForm] RequestUpdateUser model){
        try{
            User user = _context.Users.FirstOrDefault(u=>u.Id == id) ?? throw new Exception("User not found");
            if (model.Firstname is not null) user.Firstname = model.Firstname;
            if (model.Lastname is not null) user.Lastname = model.Lastname;
            if (model.Email is not null) user.Email = model.Email;
            if (model.Password is not null) user.Password = model.Password;
            user.MarkUpdated(_loggedUserId);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, user.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
        
    }
}