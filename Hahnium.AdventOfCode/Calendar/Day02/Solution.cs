using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Hahnium.AdventOfCode.Calendar.Day02
{
    public class Solution : SolutionBase<int[], IEnumerable<int>, ReadOnlyMemory<int>>
    {
        private const int TargetOutput = 19690720;

        public Solution()
            : base(
                  Parsers.Array().Int().ToArray(),
                  Parsers.Array().Int(),
                  Parsers.Array().Int().Memory())
        {
        }

        public override object PartA() => Execute(12, 2);

        public override object PartB()
        {
            int i = 10000 / 2;
            int step = 10000 / 2;

            do
            {
                step /= 2;
                int noun = i / 100;
                int verb = i % 100;
                int result = Execute(noun, verb);

                if (result == TargetOutput)
                {
                    return i;
                }
                else if (result < TargetOutput)
                {
                    i += step;
                }
                else
                {
                    i -= step;
                }
            }
            while (step > 0);

            throw new InvalidOperationException();
        }

        public int Execute(int noun, int verb)
        {
            var ram = (int[])this.imperativeInput.Clone();

            ram[1] = noun;
            ram[2] = verb;

            int p = 0;
            int opcode;

            while ((opcode = ram[p++]) != 99)
            {
                int aa = ram[p++];
                int ab = ram[p++];
                int ac = ram[p++];

                switch (opcode)
                {
                    case 1:
                        ram[ac] = ram[aa] + ram[ab];
                        break;
                    case 2:
                        ram[ac] = ram[aa] * ram[ab];
                        break;
                    default: throw new InvalidOperationException();
                }
            }

            return ram[0];
        }

        public override object FunctionalPartA() => FunctionalExecute(12, 2);

        public override object FunctionalPartB() =>
            (from noun in Enumerable.Range(0, 100)
             from verb in Enumerable.Range(0, 100)
             let result = FunctionalExecute(noun, verb)
             where result == TargetOutput
             select 100 * noun + verb).Single();

        private ImmutableArray<int> InitRam(int noun, int verb)
        {
            var ram = this.functionalInput.ToImmutableArray();
            ram = ram.SetItem(1, noun).SetItem(2, verb);

            return ram;
        }

        public int FunctionalExecute(int noun, int verb) =>
            EnumerableEx.Generate(
                (
                    Op: this.functionalInput.First(),
                    P: 0,
                    Ram: InitRam(noun, verb)
                ),
                state => state.Op != 99,
                state =>
                {
                    int opcode = state.Ram[state.P];

                    if (opcode == 99)
                    {
                        return (opcode, 0, state.Ram);
                    }

                    int a = state.Ram[state.Ram[state.P + 1]];
                    int b = state.Ram[state.Ram[state.P + 2]];
                    var result = opcode switch
                    {
                        1 => a + b,
                        2 => a * b,
                        _ => throw new InvalidOperationException()
                    };

                    return (
                        opcode,
                        state.P + 4,
                        state.Ram.SetItem(state.Ram[state.P + 3], result)
                        );
                },
                state => state.Ram[0]).Last();

        public override object FastPartA() => FastExecute(12, 2);

        public override object FastPartB()
        {
            int i = 10000 / 2;
            int step = 10000 / 2;

            do
            {
                step /= 2;
                int noun = i / 100;
                int verb = i % 100;
                int result = FastExecute(noun, verb);

                if (result == TargetOutput)
                {
                    return i;
                }
                else if (result < TargetOutput)
                {
                    i += step;
                }
                else
                {
                    i -= step;
                }
            }
            while (step > 0);

            throw new InvalidOperationException();
        }

        public unsafe int FastExecute(int noun, int verb)
        {
            var ram = new Memory<int>(new int[this.pointerInput.Length]);
            this.pointerInput.CopyTo(ram);
            using var handle = ram.Pin();
            var address = (int*)handle.Pointer;
            address[1] = noun;
            address[2] = verb;
            var p = address;
            int opcode;

            while ((opcode = *p++) != 99)
            {
                int a = *(address + (*p++));
                int b = *(address + (*p++));
                var ac = address + (*p++);

                switch (opcode)
                {
                    case 1:
                        *ac = a + b;
                        break;
                    case 2:
                        *ac = a * b;
                        break;
                    default: throw new InvalidOperationException();
                }
            }

            return address[0];
        }
    }
}
