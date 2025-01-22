using System;

namespace beethoven_api.Global.Query;

public class QueryMeta
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = -1;
    public int Limit { get; set; } = -1;
    public int PageCount {
            get{
                if(Limit <= 0 || Total <= 0 || PageSize <= 0)
                    return 1;
                return (int)Math.Ceiling(Total / (double)Limit);
            }
        }
    public int Total { get; set; } = -1;

    public Dictionary<string, string> GenerateDictionary()
    {
        Dictionary<string, string> dict = new()
        {
            { "page", Page.ToString()},
            { "page-size", PageSize.ToString() },
            { "page-count", PageCount.ToString() },
            { "total", Total.ToString() }
        };
        return dict;
    }
}
