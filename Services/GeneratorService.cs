using Model;
using Generators;

namespace Services;

public class GeneratorService(Context db)
{
    bool started = false;

    public void GenerateIfNeeded()
    {
        if (started)
            return;
        started = true;

        var gen = new DefaultGenerator(db);
        gen.Generate();
    }
}