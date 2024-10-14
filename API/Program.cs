using System.Text;
using API;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AppApplicationServices(builder.Configuration);
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddIdentityExtensions(builder.Configuration);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200/","https://localhost:4200/")
);
app.MapControllers();

app.Run();
