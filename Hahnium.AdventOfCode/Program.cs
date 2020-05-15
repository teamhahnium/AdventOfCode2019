using Hahnium.AdventOfCode.Calendar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hahnium.AdventOfCode
{
    class Program
    {
        private static object ConsoleSync = new object();

        static async Task Main(string[] args)
        {
            var namespaceParser = new Regex(@"Day(?<DayId>\d+)");
            var discoveredDays =
                from exportedType in typeof(IDay).Assembly.GetExportedTypes()
                where exportedType.GetInterfaces().Any(@interface => @interface == typeof(IDay)) && !exportedType.IsAbstract
                let dayId = int.Parse(namespaceParser.Match(exportedType.Namespace).Groups["DayId"].Value)
                select RunDay(dayId, (IDay)Activator.CreateInstance(exportedType));
            await Task.WhenAll(discoveredDays);
        }

        private static async Task RunDay(int dayNumber, IDay day)
        {
            string dayId = $"Day{dayNumber:00}";
            lock (ConsoleSync)
            {
                Console.CursorTop = dayNumber;
                Console.Write(dayId);
            }

            var input = File.ReadAllText($"Calendar/{dayId}/Input.txt");

            await Task.WhenAll(
                RunPart(day.PartA, day.FunctionalPartA, day.FastPartA, input, dayNumber, 7),
                RunPart(day.PartB, day.FunctionalPartB, day.FastPartB, input, dayNumber, (Console.WindowWidth - 7) / 2));
        }

        private static async Task RunPart(
            Func<string, string> imperative,
            Func<string, string> functional,
            Func<string, string> fast,
            string input, int top, int left)
        {
            var pending = new HashSet<Task<(string Result, TimeSpan Duration, char Type)>>
            {
                RunPart(imperative, input, 'I'),
                RunPart(functional, input, 'F'),
                RunPart(fast, input, 'P')
            };

            var completed = new SortedSet<(string Result, TimeSpan Duration, char Type)>(
                Comparer<(string Result, TimeSpan Duration, char Type)>.Create(
                    (x, y) => x.Type.CompareTo(y.Type)));

            while (pending.Any())
            {
                var complete = await Task.WhenAny(pending);
                pending.Remove(complete);
                completed.Add(await complete);

                lock (ConsoleSync)
                {
                    int partId = 0;
                    (string Result, TimeSpan Duration, char Type) fastest;

                    if (completed.Any(o => o.Result != null))
                    {
                        fastest = completed.Where(o => o.Result != null).MaxBy(o => o.Duration).First();
                    }
                    else
                    {
                        fastest = default;
                    }

                    foreach (var part in completed)
                    {
                        Console.SetCursorPosition(left + partId, top);
                        if (part.Result == null)
                        {
                            //Failure
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                        }
                        else if (part == fastest)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.Write(part.Type);
                        Console.ResetColor();

                        if (fastest != default)
                        {
                            Console.SetCursorPosition(left + 4, top);
                            Console.Write($"{fastest.Result} [{fastest.Duration.TotalMilliseconds}]");
                        }

                        partId++;
                    }
                }
            }
        }

        private static Task<(string Result, TimeSpan Duration, char Type)> RunPart(
            Func<string, string> part,
            string input,
            char type) =>
            Task.Factory.StartNew(() =>
        {
            var timer = Stopwatch.StartNew();
            string result;

            try
            {
                result = part(input);
            }
            catch
            {
                result = null;
            }
            timer.Stop();
            return (result, timer.Elapsed, type);
        });
    }
}
