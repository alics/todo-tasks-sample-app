using Microsoft.EntityFrameworkCore;
using TodoApplication.Common;
using TodoApplication.Persistence;
using TodoApplication.ReadModel;
using TodoApplication.ServicesConfiguration;
using Framework.Persistence.EF;

namespace TodoApplication.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionFilter>();
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<IDbContextsAssemblyResolver>(new DbContextsAssemblyResolver([
            typeof(TodoAppContext)
        ]));

        services.AddCors();

        services.AddScoped<DbContext, GeneralDbContext>();
        services.AddDbContext<TodoAppContext>(options => options.UseSqlServer(_configuration.GetConnectionString("TodoAppConnectionString")));
        services.AddDbContext<GeneralDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("TodoAppConnectionString")));
        services.AddDbContext<TodoAppReadContext>(options =>
        {
            options.UseSqlServer(_configuration.GetConnectionString("TodoAppConnectionString"));
        });

        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new LongConverter());
            options.SerializerSettings.Converters.Add(new NullableLongConverter());
        });

        services.AddReadModelServices();
        services.AddFrameworkServices();
        services.AddTodoApplicationServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader());

        app.UseAuthorization();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}