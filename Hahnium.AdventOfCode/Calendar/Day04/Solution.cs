using System;
using System.Linq;

namespace Hahnium.AdventOfCode.Calendar.Day04
{
    public class Solution : SolutionBase<(int Start, int End), (int Start, int End), (int Start, int End)>
    {
        private const int TargetOutput = 19690720;

        private static (int Start, int End) ParseRange(string input)
        {
            var tokens = input.Split('-');
            return (int.Parse(tokens[0]), int.Parse(tokens[1]));
        }

        public Solution()
            : base(ParseRange, ParseRange, ParseRange)
        {
        }

        public bool IsValidPassword(int password)
        {
            var lastDigit = password % 10;
            password /= 10;
            bool hasDoubleDigits = false;

            for (int i = 0; i < 5; i++)
            {
                var digit = password % 10;
                password /= 10;

                if (lastDigit == digit)
                {
                    hasDoubleDigits = true;
                }
                else if (lastDigit < digit)
                {
                    return false;
                }

                lastDigit = digit;
            }

            return hasDoubleDigits;
        }

        public bool IsValidPasswordB(int password)
        {
            var lastDigit = password % 10;
            password /= 10;
            bool hasDoubleDigits = false;
            int dupeCount = 0;

            for (int i = 0; i < 5; i++)
            {
                var digit = password % 10;
                password /= 10;

                if (lastDigit == digit)
                {
                    dupeCount++;
                }
                else if (lastDigit < digit)
                {
                    return false;
                }
                else
                {
                    if (dupeCount == 1)
                    {
                        hasDoubleDigits = true;
                    }

                    dupeCount = 0;
                }

                lastDigit = digit;
            }

            if (dupeCount == 1)
            {
                hasDoubleDigits = true;
            }

            return hasDoubleDigits;
        }

        // Filtering approach
        public int CountPasswords(Func<int, bool> passwordChecker)
        {
            int validPasswords = 0;

            for (int i = this.imperativeInput.Start; i < this.imperativeInput.End; i++)
            {
                if (passwordChecker(i))
                {
                    validPasswords++;
                }
            }

            return validPasswords;
        }

        public override object PartA() => CountPasswords(IsValidPassword);

        public override object PartB() => CountPasswords(IsValidPasswordB);

        // YOLO one liner approach
        public override object FunctionalPartA() =>
            Enumerable.Range(this.functionalInput.Start, this.functionalInput.End - this.functionalInput.Start).Count(IsValidPassword);

        public override object FunctionalPartB() =>
            Enumerable.Range(this.functionalInput.Start, this.functionalInput.End - this.functionalInput.Start).Count(IsValidPasswordB);

        // Generative approach
        public int CountGeneratedPasswords(Func<int, bool> passwordChecker)
        {
            int passwordCount = 0;
            int a = this.pointerInput.Start / 100000 % 10;
            int b = this.pointerInput.Start / 10000 % 10;
            int c = this.pointerInput.Start / 1000 % 10;
            int d = this.pointerInput.Start / 100 % 10;
            int e = this.pointerInput.Start / 10 % 10;
            int f = this.pointerInput.Start / 1 % 10;

            for (; a < 10; a++)
            {
                for (; b < 10; b++)
                {
                    for (; c < 10; c++)
                    {
                        for (; d < 10; d++)
                        {
                            for (; e < 10; e++)
                            {
                                for (; f < 10; f++)
                                {
                                    int password = (a * 100000) + (b * 10000) + (c * 1000) + (d * 100) + (e * 10) + (f * 1);
                                    if (password > this.pointerInput.End)
                                    {
                                        return passwordCount;
                                    }
                                    if (passwordChecker(password))
                                    {
                                        passwordCount++;
                                    }
                                }
                                f = e;
                            }
                            e = d;
                        }
                        d = c;
                    }
                    c = b;
                }
                b = a;
            }

            return passwordCount;
        }

        public override object FastPartA() => CountGeneratedPasswords(IsValidPassword);

        public override object FastPartB() => CountGeneratedPasswords(IsValidPasswordB);
    }
}
