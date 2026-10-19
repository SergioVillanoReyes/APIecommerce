using Microsoft.EntityFrameworkCore;
using APIecommerce.Data;
using APIecommerce.Repository.IRepository;
using APIecommerce.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Configuration.GetConnectionString("ConexionSql")
    ?? throw new InvalidOperationException(
        "Configure la cadena de conexión 'ConexionSql' mediante User Secrets o la variable de entorno ConnectionStrings__ConexionSql.");
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(dbConnectionString));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddAutoMapper(_ => { }, typeof(Program).Assembly);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
