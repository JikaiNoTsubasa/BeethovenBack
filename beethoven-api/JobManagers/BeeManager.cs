using System;
using beethoven_api.Database;

namespace beethoven_api.JobManagers;

public class BeeManager(BeeDBContext context)
{
    protected BeeDBContext _context = context;
}
