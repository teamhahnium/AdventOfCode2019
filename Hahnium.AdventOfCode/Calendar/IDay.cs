namespace Hahnium.AdventOfCode.Calendar
{
    public interface IDay
    {
        void Parse(string input);
        object PartA();
        object PartB();
        object FunctionalPartA();
        object FunctionalPartB();
        object FastPartA();
        object FastPartB();
    }
}
