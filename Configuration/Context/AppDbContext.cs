using System;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ASPNET.Configuration.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

}
