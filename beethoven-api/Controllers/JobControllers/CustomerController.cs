using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.Global.Engine;
using beethoven_api.Global.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.Controllers.JobControllers;

public class CustomerController(BeeDBContext context, BeeEngine engine) : BeeController(context, engine)
{
    [HttpGet]
    [Route("api/customers")]
    public virtual IActionResult FetchCustomers(){
        try{
            var res =_context.Customers
                .Include(c=>c.Products)
                .Paged(_pagination, out QueryMeta? meta)
                .Select(c=>c.ToDTO());
            return StatusCode(StatusCodes.Status200OK, res, meta);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpPost]
    [Route("api/customer")]
    public virtual IActionResult CreateCustomer([FromForm] RequestCreateCustomer model){
        try{
            Customer customer = _engine.CreateCustomer(model, _loggedUserId);
            return StatusCode(StatusCodes.Status200OK, customer.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
