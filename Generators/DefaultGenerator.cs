using System;
using System.IO;

using Model;

namespace Generators;

public class DefaultGenerator(Context context)
{
    readonly string[] names = File.ReadAllLines("Data/names.txt");
    readonly string[] surnames = File.ReadAllLines("Data/surnames.txt");
    readonly string[] keys = File.ReadAllLines("Data/keys.txt");

    public void Generate()
    {
        Console.WriteLine("Generating Data:");

        for (int i = 0; i < 1000; i++)
        {
            context.Add(GenerateUser());
            Progress(i, 1000, 0, 20f);
        }
        
        context.SaveChanges();
        Progress(1, 1, 90, 100);
    }

    void Progress(int step, int maxStep, float minProg, float maxProg)
    {
        var progress = minProg + maxProg * step / maxStep;
        var bar = LoadBar(progress);
        Console.WriteLine(bar);
        Console.CursorTop--;
    }

    string LoadBar(float progress)
    {
        var total = 0;
        var bar = "";
        while (progress - total > 4)
        {
            bar += "█";
            total += 4;
        }

        if (progress - total > 3)
        {
            bar += "▓";
            return bar;
        }
        
        if (progress - total > 2)
        {
            bar += "▒";
            return bar;
        }
        
        if (progress - total > 1)
        {
            bar += "░";
            return bar;
        }

        return bar;
    }

    User GenerateUser()
    {
        var name = names.Next();
        name += surnames.Next();
        if (30.Prob())
            name += surnames.Next();

        var username = keys.Next();
        int prob = 85;
        while (prob.Prob())
        {
            username += keys.Next();
            prob -= 35;
        }

        var bio = "";

        var days = 365 * 1950;
        var birth = DateOnly.FromDayNumber(
            days + Random.Shared.Next(365 * 50)
        );

        var create = DateOnly.FromDayNumber(
            birth.DayNumber + Random.Shared.Next(365 * 20)
        );

        return new User {
            Id = Guid.NewGuid(),
            Name = name,
            Username = username,
            Bio = bio,
            BirthDay = birth,
            CreateAccountDate = create,
            Avatar = null,
            Banner = null,
            Followers = [],
            Following = [],
            Posts = []
        };
    }
}

public static class ExtensionGenerators
{
    public static bool Prob(this int chance)
        => Random.Shared.Next(100) < chance;

    public static T Next<T>(this T[] array)
        => array[Random.Shared.Next(array.Length)];
}