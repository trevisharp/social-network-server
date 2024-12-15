using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Model;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(
    opt => opt.UseInMemoryDatabase("inmemorydb")
);
builder.Services.AddSingleton<GeneratorService>();

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    var gen = ctx.RequestServices.GetService<GeneratorService>();
    if (gen is null)
    {
        Console.WriteLine("GeneratorService is not configured.");
        return;
    }

    gen.GenerateIfNeeded();
    await next.Invoke();
});

app.MapGet("/", () => "Hello World!");

app.Run();