using System.IO;
using DotNetEnv;

public static class TestEnv
{
    static TestEnv()
    {
        // Traverse up until we find the .env file
        var current = Directory.GetCurrentDirectory();
        while (current != null && !File.Exists(Path.Combine(current, ".env")))
        {
            current = Directory.GetParent(current)?.FullName;
        }

        if (current == null)
            throw new FileNotFoundException(".env file not found in any parent directory.");

        Env.Load(Path.Combine(current, ".env"));
    }

    public static string Get(string key)
    {
        var value = Env.GetString(key);
        if (string.IsNullOrEmpty(value))
            throw new InvalidOperationException($"{key} is not set.");
        return value;
    }
}
