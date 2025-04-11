using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var path = "ScriptHookV.log";

        if (!File.Exists(path))
        {
            Console.WriteLine("can not find log file");
            return;
        }

        var lines = File.ReadAllLines(path);

        int errorCount = 0, infoCount = 0, debugCount = 0;
        List<string> scripts = new();

        foreach (var line in lines)
        {
            if (line.Contains("[ERROR]")) errorCount++;
            else if (line.Contains("[INFO]")) infoCount++;
            else if (line.Contains("[DEBUG]")) debugCount++;

            if (line.Contains("Started script"))
            {
                var split = line.Split('\'');
                if (split.Length > 1)
                    scripts.Add(split[1]);
            }
        }

        var lastLines = lines.Skip(Math.Max(0, lines.Length - 10));

        Console.WriteLine($"\n[+] Log sum:");
        Console.WriteLine($"    Errors     : {errorCount}");
        Console.WriteLine($"    Information: {infoCount}");
        Console.WriteLine($"    Debug        : {debugCount}");

        Console.WriteLine("\n[+] Loaded Scriptler:");
        foreach (var script in scripts.Distinct())
            Console.WriteLine($"    - {script}");

        Console.WriteLine("\n[+] Last 10 line:");
        foreach (var line in lastLines)
            Console.WriteLine($"    {line}");
        Console.WriteLine($"\n[DEBUG] current directory: {Directory.GetCurrentDirectory()}");
    }
}
