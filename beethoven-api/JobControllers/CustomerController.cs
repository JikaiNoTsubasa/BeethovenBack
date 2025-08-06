using beethoven_api.Database.DTO.CustomerModels;
using beethoven_api.JobManagers;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beethoven_api.JobControllers;

[Authorize]
public class CustomerController(CustomerManager manager) : BeeController
{
    protected CustomerManager _manager = manager;

    [HttpGet]
    [Route("api/customers")]
    public virtual IActionResult FetchCustomers(){
        return StatusCode(StatusCodes.Status200OK, _manager.FetchAllCusotmers());
    }

    [HttpPost]
    [Route("api/customer")]
    public virtual IActionResult CreateCustomer([FromForm] RequestCreateCustomer model){
        throw new NotImplementedException();
    }
}
