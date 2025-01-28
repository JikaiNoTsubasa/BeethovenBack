using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Database.DTO;
using beethoven_api.Database.DTO.ProductModels;
using beethoven_api.Global.Engine;
using beethoven_api.Global.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.Controllers.JobControllers;

[Authorize]
public class ProductController(BeeDBContext context, BeeEngine engine) : BeeController(context, engine)
{
    [HttpGet]
    [Route("api/products")]
    public virtual IActionResult FetchUsers(){
        try{
            var res =_context.Products
                .Include(p=>p.Customer)
                .Include(p=>p.SLA)
                .Paged(_pagination, out QueryMeta? meta)
                .Select(c=>c.ToDTO());
            return StatusCode(StatusCodes.Status200OK, res, meta);
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    [HttpPost]
    [Route("api/product")]
    public virtual IActionResult CreateProduct([FromForm] RequestCreateProduct model){
        try{
            Product product = _engine.CreateProduct(model, _loggedUserId);
            return StatusCode(StatusCodes.Status200OK, product.ToDTO());
        }catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
