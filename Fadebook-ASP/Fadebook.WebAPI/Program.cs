using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Services;
using Fadebook.Application.Services.Interfaces;
using Fadebook.Domain.Exceptions;
using Fadebook.Infracstructure.Data;
using Fadebook.Infracstructure.Repositories;
using Fadebook.WebAPI.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using static Fadebook.WebAPI.Extensions.ServiceExtension;
using static Fadebook.WebAPI.Extensions.AppExtension;
using Fadebook.Infracstructure;
using Fadebook.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), 
    b=> b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(typeof(ApplicationReference).Assembly, typeof(InfrastructureReference).Assembly);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureDependencies();
builder.Services.ConfigureJwt(builder.Configuration);
var app = builder.Build();
app.ConfigureErrorHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
