using System;
using beethoven_api.Database;
using beethoven_api.Database.DTO;
using beethoven_api.Global.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.Controllers.JobControllers;

[Authorize]
public class MessageController(BeeDBContext context, BeeEngine engine) : BeeController(context, engine)
{
    [HttpGet]
    [Route("api/mymessages")]
    public virtual IActionResult GetMyMessages(){
        try{
            var res = _context.Messages
                .Include(m=>m.SourceUser)
                .Include(m=>m.TargetUser)
                .Where(m=>m.TargetUserId == _loggedUserId).Select(m=>m.ToDTO()).ToList();
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
