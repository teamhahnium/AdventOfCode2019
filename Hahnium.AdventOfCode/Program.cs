using Hahnium.AdventOfCode.Calendar;
using System;
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
                RunPart(day.PartA, input, dayNumber, 7),
                RunPart(day.PartB, input, dayNumber, (Console.WindowWidth - 7) / 2));
        }

        private static async Task RunPart(Func<string, string> partFunction, string input, int top, int left)
        {
            var result = await Task.Factory.StartNew(() => partFunction(input));
            lock (ConsoleSync)
            {
                Console.SetCursorPosition(left, top);
                Console.Write(result);
            }
        }
    }
}
