using BETareas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add conexion a base de datos
builder.Services.AddSqlServer<TaskContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddDbContext<TaskContext>(p => p.UseInMemoryDatabase("Tareas"));

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

 builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

var app = builder.Build();

app.MapGet("/dbconexion", async([FromServices] TaskContext dbContext) =>
{
   dbContext.Database.EnsureCreated();
   return Results.Ok("Base de datos creada: " + dbContext.Database.IsInMemory());
});

app.MapGet("api/test/products", async([FromServices] TaskContext dbContext) =>
{
   var products = dbContext.Tareas.ToList();
   return Results.Ok(products);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
