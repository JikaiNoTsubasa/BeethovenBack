using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;

namespace beethoven_api.JobManagers;

public class GlobalParameterManager(BeeDBContext context): BeeManager(context)
{
    public List<GlobalParameter> FetchAllParameters()
    {
        return [.. _context.GlobalParameters.OrderBy(p => p.Name)];
    }

    public GlobalParameter FetchParameter(long id)
    {
        return _context.GlobalParameters.FirstOrDefault(p => p.Id == id) ?? throw new Exception($"Could not find parameter {id}"); ;
    }

    public GlobalParameter FetchParameter(string name)
    {
        return _context.GlobalParameters.FirstOrDefault(p => p.Name.Equals(name)) ?? throw new Exception($"Could not find parameter {name}"); ;
    }

    public T GetParameterValue<T>(string name, T defaultValue)
    {
        var param = FetchParameter(name);
        return param is not null ? (T)Convert.ChangeType(param.Value!, typeof(T)) : defaultValue;
    }

    public GlobalParameter UpdateParameter(long id, string? name = null, string? value = null, string? type = null, string? description = null)
    {
        GlobalParameter p = _context.GlobalParameters.FirstOrDefault(p => p.Id == id) ?? throw new Exception($"Could not find parameter {id}");
        if (name != null) p.Name = name;
        if (value != null) p.Value = value;
        if (type != null) p.Type = type;
        if (description != null) p.Description = description;
        _context.SaveChanges();
        return p;
    }
}
