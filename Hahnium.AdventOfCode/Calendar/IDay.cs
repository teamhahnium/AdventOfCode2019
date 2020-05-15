using System;
using System.Collections.Generic;
using System.Text;

namespace Hahnium.AdventOfCode.Calendar
{
    public interface IDay
    {
        string PartA(string input);
        string PartB(string input);
        string FunctionalPartA(string input);
        string FunctionalPartB(string input);
        string FastPartA(string input);
        string FastPartB(string input);
    }
}
