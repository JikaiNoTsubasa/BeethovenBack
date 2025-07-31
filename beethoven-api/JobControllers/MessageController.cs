using System;
using beethoven_api.Database;
using beethoven_api.Database.DTO;
using beethoven_api.Global.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.JobControllers;

[Authorize]
public class MessageController: BeeController
{
    [HttpGet]
    [Route("api/mymessages")]
    public virtual IActionResult GetMyMessages(){
        throw new NotImplementedException();
    }
}
