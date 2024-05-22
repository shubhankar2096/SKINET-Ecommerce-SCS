using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;

var builder = WebApplication.CreateBuilder(args); //configures Kestrel server

// Add services to the container.
/*In launchsettings.json, we have specified envvironmentVariable as "ASPNETCORE_ENVIRONMENT": "Development"
which denotes this is Development Environment. Hence, in this case, content written in
appsettings.Development.json has more precedence (or priority) over the content written in
appsettings.json
*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//AddDbContext is extension method
builder.Services.AddDbContext<StoreContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStatusCodePagesWithReExecute("/errors/{}"); //Status Code Middleware for Error/Exception Handling

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //Middleware
    app.UseSwaggerUI(); //Middleware
}

app.UseHttpsRedirection(); //Middleware to redirect http request to https, Forces HTTPS in Application

app.UseStaticFiles(); //Middleware to include static content(e.g. Images, JS, CSS, etc.) in wwwroot

//Middleware to enforce authorization policies used in [Authorize] attribute defined at
//controller or action level
//[Authorize] attribute will not work without this Middleware
app.UseAuthorization();

app.MapControllers(); //To map incoming request to controller actions

// To add specific custom middleware to controllers, request will go through this CustomMiddleware
//class before action method of controller gets called
//app.MapControllers().UseMiddleware<CustomMiddleware>();

//To create and update Database on when App Starts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occured during EF Migration");
    }
}

app.Run();


