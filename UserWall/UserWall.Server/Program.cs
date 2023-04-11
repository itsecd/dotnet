using System.IO;
using System;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using UserWall;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.AddDbContextFactory<UserWallContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString(nameof(UserWall));
    optionsBuilder.UseMySQL(connectionString);
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(options =>
{
    var docName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var docPath = Path.Combine(AppContext.BaseDirectory, docName);
    options.IncludeXmlComments(docPath);
});

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
