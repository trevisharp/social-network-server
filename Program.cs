using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Model;
using Generators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(
    opt => opt.UseInMemoryDatabase("local")
);

var app = builder.Build();

bool started = false;
app.Use(async (ctx, next) =>
{
    if (started)
        await next.Invoke();
    
    var db = ctx.RequestServices.GetService<Context>() ?? throw new Exception();
    var gen = new DefaultGenerator(db);
    gen.Generate();

    started = true;
    await next.Invoke();
});

app.MapGet("/", () => "Hello World!");

app.Run();