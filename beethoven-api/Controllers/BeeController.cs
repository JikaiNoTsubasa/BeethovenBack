using beethoven_api.Database;
using beethoven_api.Global.Engine;
using beethoven_api.Global.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace beethoven_api.Controllers;

public class BeeController(BeeDBContext context, BeeEngine engine) : Controller
{
    protected readonly BeeDBContext _context = context;
    protected readonly BeeEngine _engine = engine;
    protected QuerySearch? _search;
    protected QueryPagination? _pagination;
    protected long _loggedUserId;

    override public void OnActionExecuting(ActionExecutingContext context){
        var headers = context.HttpContext.Request.Headers;
        if (headers.ContainsKey("userId")){
            headers.TryGetValue("userId", out var userIdStr);
            _loggedUserId = long.Parse(userIdStr!);
        }

        var query = context.HttpContext.Request.Query;

        foreach(var key in query.Keys){
            query.TryGetValue(key, out var value);
            switch(key){
                case "page":
                    CreatePagination(page: int.Parse(value.ToString() ?? "1"), limit: null);
                    break;
                case "limit":
                    CreatePagination(limit: int.Parse(value.ToString() ?? "1"), page: null);
                    break;
                case "search":
                    _search = new(){
                        Content = value
                    };
                    break;
                default: break;
            }
        }

        if (context.HttpContext.Request.Query.ContainsKey("search")){

        }
        base.OnActionExecuting(context);
    }

    [NonAction]
    private void CreatePagination(int? page, int? limit){
        _pagination ??= new QueryPagination();
        if (page is not null) _pagination.Page = page.Value;
        if (limit is not null) _pagination.Limit = limit.Value;
    }

    [NonAction]
    public virtual ObjectResult StatusCode([ActionResultStatusCode] int statusCode, [ActionResultObjectValue] object? value, QueryMeta? meta = null){
        if (meta is not null){
            var dict = meta.GenerateDictionary();
            foreach (var item in dict){
                HttpContext.Response.Headers.Append(item.Key, item.Value);
            }
        }
        
        return base.StatusCode(statusCode, value);
    }
}