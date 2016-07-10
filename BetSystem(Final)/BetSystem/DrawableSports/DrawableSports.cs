namespace BetSystem.DrawableSports
{
    using System;
    using System.Linq;
    using System.Text;

    using Interfaces;
    using BetSystem.EventDeclarations;
    
    public abstract class DrawableSports : Match, IDrawableSports
    {
        //fields
        private decimal[] givenCoefficients;
        private decimal[] calculatedCoefficients;

        //properties
        public decimal[] GivenCoefficients
        {
            get
            {
                return this.givenCoefficients;
            }
            private set
            {
                this.givenCoefficients = value;
            }
        }
        public decimal[] CalculatedCoefficients
        {
            get
            {
                return this.calculatedCoefficients;
            }
            set
            {
                this.calculatedCoefficients = value;
            }
        }

        //constructors
        public DrawableSports(string text) : base(text)
        {
        }

        //events
        public abstract event ScoreChangeEventHandler Scored;
        public abstract event EndMatchEventHandler EndOfMatch;

        //methods
        public override decimal[] GetCoefficients(string text)
        {
            this.GivenCoefficients = base.GetCoefficients(text);

            this.AddComplexCoefficients();//add calculated coefficients

            return this.GivenCoefficients.Concat(CalculatedCoefficients).ToArray();
        }

        private void AddComplexCoefficients()
        {
            var coefficientArr = new decimal[3];

            coefficientArr[0] = CalcNewCoefficient(this.givenCoefficients[0], this.givenCoefficients[2]);//12
            coefficientArr[1] = CalcNewCoefficient(this.givenCoefficients[0], this.givenCoefficients[1]);//1X
            coefficientArr[2] = CalcNewCoefficient(this.givenCoefficients[1], this.givenCoefficients[2]);//X2

            this.calculatedCoefficients = coefficientArr;
        }

        private static decimal CalcNewCoefficient(decimal coef1, decimal coef2)
        {
            return minCoefficientValue + (Math.Min(coef1, coef2) - minCoefficientValue) / 5;
        }

        public override void Print()
        {
            base.Print();

            StringBuilder strCalcCoefs = new StringBuilder("Calculated Coefficients in order 12 1X X2: ");
            for (int i = 0; i < this.CalculatedCoefficients.Length; i++)
            {
                strCalcCoefs.Append(this.CalculatedCoefficients[i]);
                strCalcCoefs.Append(" ");
            }
            Console.WriteLine(strCalcCoefs);
        }
    }
}
