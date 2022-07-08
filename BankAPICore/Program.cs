using BankAPI.DataAccess.Data;
using BankAPI.DataAccess.IConfiguration;
using BankAPI.DataAccess.IRepositories;
using BankAPI.DataAccess.Repositories;
using BankAPICore.Data;
using BankAPICore.IData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BankDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BankConnection")));

ConfigureServices(builder.Services);


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<BankDbContext>();
//    context.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    #region DataServices
    services.AddScoped<IClienteDataService, ClienteDataService>();
    services.AddScoped<IPersonaDataService, PersonaDataService>();
    services.AddScoped<ICuentaDataService, CuentaDataService>();

    #endregion

    #region Repositories
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    #endregion
}