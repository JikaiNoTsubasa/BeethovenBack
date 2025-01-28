using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.TicketModels;
using beethoven_api.Global.Engine;
using beethoven_api.Global.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.Controllers.JobControllers;

public class TicketController(BeeDBContext context, BeeEngine engine) : BeeController(context, engine)
{

    private IQueryable<Ticket> GenerateTicketQuery(){
        return _context.Tickets
            .Include(t=>t.Product).ThenInclude(p=>p!.Customer)
            .Include(t=>t.AssignedTo)
            .Include(t=>t.ReviewedBy)
            .Include(t=>t.Status)
            .Include(t=>t.Activities)!.ThenInclude(a=>a.User);
    }

    [HttpGet]
    [Route("api/mytickets")]
    public virtual IActionResult FetchMyTickets(){
        try{
            var res = GenerateTicketQuery()
                .Where(t=>t.AssignedToId == _loggedUserId)
                .Paged(_pagination, out QueryMeta? meta)
                .Select(u=>u.ToDTO());
            return StatusCode(StatusCodes.Status200OK, res, meta);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpGet]
    [Route("api/tickets")]
    public virtual IActionResult FetchAllTickets(){
        try{
            var res = GenerateTicketQuery()
                .Paged(_pagination, out QueryMeta? meta)
                .Select(u=>u.ToDTO());
            return StatusCode(StatusCodes.Status200OK, res, meta);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpGet]
    [Route("api/ticket/{id}")]
    public virtual IActionResult FetchTicketById([FromRoute] long id){
        try{
            var res = GenerateTicketQuery()
                .FirstOrDefault(t=>t.Id == id)?
                .ToDTO();
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpPost]
    [Route("api/ticket")]
    public virtual IActionResult CreateTicket([FromForm] RequestCreateTicket model){
        try{
            Ticket ticket = _engine.CreateTicket(model, _loggedUserId);
            var res = ticket.ToDTO();
            return StatusCode(StatusCodes.Status200OK, res);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
