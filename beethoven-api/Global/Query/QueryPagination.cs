using System;

namespace beethoven_api.Global.Query;

public class QueryPagination
{
    public int Page {get; set;} = 1;
    public int Limit {get; set;} = 1;
    public int PageSize {get; set;} = 1;
    public int Total {get; set;} = 0;

    public int PageCount {
        get{
            if(Limit <= 0 || Total <= 0 || PageSize <= 0)
                return 1;
            return (int)Math.Ceiling(Total / (double)Limit);
        }
    }
}
