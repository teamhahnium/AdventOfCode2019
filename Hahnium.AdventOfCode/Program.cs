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
            Console.ResetColor();
            Console.Clear();
            var namespaceParser = new Regex(@"Day(?<DayId>\d+)");
            var discoveredDays =
                from exportedType in typeof(IDay).Assembly.GetExportedTypes()
                where exportedType.GetInterfaces().Any(@interface => @interface == typeof(IDay)) && !exportedType.IsAbstract
                let dayId = int.Parse(namespaceParser.Match(exportedType.Namespace).Groups["DayId"].Value)
                select (dayId, exportedType);

            await Task.WhenAll(discoveredDays.Select(o =>
                RunDay(o.dayId, (IDay)Activator.CreateInstance(o.exportedType))));

            // 2nd run to eliminate JIT issues.
            await Task.WhenAll(discoveredDays.Select(o =>
                RunDay(o.dayId, (IDay)Activator.CreateInstance(o.exportedType))));

            lock (ConsoleSync)
            {
                Console.SetCursorPosition(0, discoveredDays.Count() + 1);
            }
        }

        private static async Task RunDay(int dayNumber, IDay day)
        {
            string dayId = $"Day{dayNumber:00}";
            lock (ConsoleSync)
            {
                Console.SetCursorPosition(0, dayNumber);
                Console.Write(dayId);
            }

            var input = File.ReadAllText($"Calendar/{dayId}/Input.txt");
            day.Parse(input);

            await Task.WhenAll(
                RunPart(day.PartA, day.FunctionalPartA, day.FastPartA, dayNumber, 7),
                RunPart(day.PartB, day.FunctionalPartB, day.FastPartB, dayNumber, (Console.WindowWidth - 7) / 2));
        }

        private static async Task RunPart(
            Func<object> imperative,
            Func<object> functional,
            Func<object> fast,
            int top, int left)
        {
            var pending = new HashSet<Task<(string Result, TimeSpan Duration, char Type)>>
            {
                RunPart(imperative, 'I'),
                RunPart(functional, 'F'),
                RunPart(fast, 'P')
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
                        fastest = completed.Where(o => o.Result != null).MinBy(o => o.Duration).First();
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

                        if (!completed.All(o => o.Result == fastest.Result || o.Result == null))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(left + 4, top);
                            Console.Write($"ERROR!!");
                            Console.ResetColor();
                        }
                        else if (fastest != default)
                        {
                            Console.SetCursorPosition(left + 4, top);
                            Console.Write($"[{fastest.Duration.TotalMilliseconds}] {fastest.Result}");
                        }


                        partId++;
                    }
                }
            }
        }

        private static Task<(string Result, TimeSpan Duration, char Type)> RunPart(
            Func<object> part,
            char type) =>
            Task.Factory.StartNew(() =>
        {
            var timer = Stopwatch.StartNew();
            string result;

            try
            {
                result = part().ToString();
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
