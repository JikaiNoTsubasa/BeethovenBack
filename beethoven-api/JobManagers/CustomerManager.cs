using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;

namespace beethoven_api.JobManagers;

public class CustomerManager(BeeDBContext context) : BeeManager(context)
{
    private IQueryable<Customer> GenerateCustomerQuery()
    {
        return _context.Customers;
    }

    public List<Customer> FetchAllCusotmers()
    {
        return [..GenerateCustomerQuery().OrderBy(p => p.Name)];
    }
}
