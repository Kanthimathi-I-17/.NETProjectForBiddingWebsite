using Microsoft.EntityFrameworkCore;
using PortalForBiddingDB.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// service for cors origin

builder.Services.AddCors(
    options => options.AddPolicy("FrontEnd", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    }));

// service for MySQL
builder.Services.AddDbContext<PortalForBiddingContext>(
    options =>
    {
        options.UseMySql(builder.Configuration.GetConnectionString("PortalForBiddingDB"),
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
    });

// newtonsoftJson
//builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
//    .AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("FrontEnd");

app.UseAuthorization();

app.MapControllers();

app.Run();
