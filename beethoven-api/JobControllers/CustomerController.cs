using beethoven_api.Database.DTO.CustomerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.JobControllers;

[Authorize]
public class CustomerController : BeeController
{
    [HttpGet]
    [Route("api/customers")]
    public virtual IActionResult FetchCustomers(){
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("api/customer")]
    public virtual IActionResult CreateCustomer([FromForm] RequestCreateCustomer model){
        throw new NotImplementedException();
    }
}
