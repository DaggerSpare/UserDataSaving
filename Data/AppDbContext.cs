
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using  Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace AppDbContext.Data;

using System.Collections.Generic;
using DataIO.Models;
using Microsoft.EntityFrameworkCore.Design;

public class ApplicationDbContext : DbContext


{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }


    public DbSet<UserData> UserData { get; set; } = null!;
    
}



public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        string connectionString = "Server=localhost,1433;Database=DataIO;User Id=sa;Password=ab12AB34;";
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(connectionString);
        return new ApplicationDbContext(builder.Options);

    }
}