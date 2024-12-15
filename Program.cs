using System;
using System.Linq;
using Generators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(
    opt => opt.UseInMemoryDatabase("inmemorydb")
);

var app = builder.Build();

bool start = false;
app.Use(async (ctx, next) =>
{
    var db = ctx.RequestServices.GetService<Context>();
    if (db is null)
    {
        Console.WriteLine("Context is not configured.");
        return;
    }

    if (start)
    {
        await next.Invoke();
        return;
    }

    var gen = new DefaultGenerator(db);
    gen.Generate();

    start = true;
    await next.Invoke();
});

app.MapGet("/user", (
    [FromServices]Context ctx,
    [FromQuery]int page = 0,
    [FromQuery]int limit = 10,
    [FromQuery]string? query = null
    ) =>
{
    var search =
        query is null ? ctx.Users :
        ctx.Users.Where(u => u.Name.Contains(query) || u.Username.Contains(query));
    return search.Skip(page * limit).Take(limit);
});

app.Run();