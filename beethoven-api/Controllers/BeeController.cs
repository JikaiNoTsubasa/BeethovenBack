using beethoven_api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace beethoven_api.Controllers;

public class BeeController(BeeDBContext context) : Controller
{
    protected readonly BeeDBContext _context = context;
    protected long _loggedUserId;

    override public void OnActionExecuting(ActionExecutingContext context){
        var headers = context.HttpContext.Request.Headers;
        if (headers.ContainsKey("userId")){
            headers.TryGetValue("userId", out var userIdStr);
            _loggedUserId = long.Parse(userIdStr!);
        }
        base.OnActionExecuting(context);
    }
}