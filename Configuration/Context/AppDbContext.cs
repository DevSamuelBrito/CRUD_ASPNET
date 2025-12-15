using System;
using CRUD_ASPNET.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ASPNET.Configuration.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Tasks> Tasks { get; set; }

}
