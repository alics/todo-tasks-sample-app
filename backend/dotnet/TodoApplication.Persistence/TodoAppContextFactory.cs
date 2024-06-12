using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoApplication.Persistence;

//$env:TodoAppConnectionString = "Data Source=ali-cs.database.windows.net;Initial Catalog=Todo;User ID=CloudSA244d6c3c;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Authentication=ActiveDirectoryInteractive;Application Intent=ReadWrite;Multi Subnet Failover=False"; Update-Database
//$env:TodoAppConnectionString = "Server=tcp:ali-cs.database.windows.net,1433;Initial Catalog=Todo;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default"; Add-Migration
//$env:TodoAppConnectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Todo;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; Add-Migration
//$env:TodoAppConnectionString = "Data Source=(localdb)\ProjectModels;Initial Catalog=TodoApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; dotnet ef migrations add  ""  
public class TodoAppContextFactory : IDesignTimeDbContextFactory<TodoAppContext>
{
    public TodoAppContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("TodoAppConnectionString");

        if (string.IsNullOrEmpty(connectionString)) throw new InvalidOperationException("");

        var options = new DbContextOptionsBuilder<TodoAppContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new TodoAppContext(options);
    }
}
