namespace Hahnium.AdventOfCode.Calendar
{
    public interface IDay
    {
        bool IsTestMode { get; set; }
        void Parse(string input);
        object PartA();
        object PartB();
        object FunctionalPartA();
        object FunctionalPartB();
        object FastPartA();
        object FastPartB();
    }
}
