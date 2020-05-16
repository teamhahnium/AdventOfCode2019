using System;

namespace Hahnium.AdventOfCode.Calendar
{
    public abstract class SolutionBase<TImperative, TFunctional, TPointer> : IDay
    {
        private Func<string, TImperative> imperativeParser;
        private Func<string, TFunctional> functionalParser;
        private Func<string, TPointer> pointerParser;
        protected TImperative imperativeInput;
        protected TFunctional functionalInput;
        protected TPointer pointerInput;

        protected SolutionBase(
            Func<string, TImperative> imperativeParser,
            Func<string, TFunctional> functionalParser,
            Func<string, TPointer> pointerParser)
        {
            this.imperativeParser = imperativeParser;
            this.functionalParser = functionalParser;
            this.pointerParser = pointerParser;
        }

        public bool IsTestMode { get; set; } = false;

        public void Parse(string input)
        {
            imperativeInput = imperativeParser(input);
            functionalInput = functionalParser(input);
            pointerInput = pointerParser(input);
        }

        public abstract object PartA();

        public abstract object PartB();

        public abstract object FunctionalPartA();

        public abstract object FunctionalPartB();

        public abstract object FastPartA();

        public abstract object FastPartB();
    }
}
