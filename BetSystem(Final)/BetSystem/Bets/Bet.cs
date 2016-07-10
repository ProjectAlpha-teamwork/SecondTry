namespace BetSystem.Bets
{
    using System;
    using System.Linq;

    using BetSystem.Interfaces;
    using EventDeclarations;

    public abstract class Bet : IBet
    {
        //fields
        private string id;

        //properties
        public IMatch Match { get; }
        public bool BetClosed { get; protected set; }
        public decimal BetAmnout { get; }
        public decimal MaxPossibleWin { get; protected set; }
        public decimal AmountWon { get; protected set; }
        public string ID
        {
            get
            {
                return this.id;
            }
        }

        //constructors
        public Bet(IMatch match, decimal betAmount, string uniqueID)
        {
            this.Match = match;
            this.id = this.Match.ID + "_" + uniqueID;
            this.BetAmnout = betAmount;
            this.MaxPossibleWin = this.BetAmnout * (decimal)this.Match.Coefficients.Max();
            this.BetClosed = false;
            this.AmountWon = 0;
        }

        //methods
        public abstract void CloseBetBeforeEnd(object sender, ScoredEventArgs args);
        public abstract void CloseBetAfterEnd(object sender, EndMatchEventArgs args);
        public virtual void Print()
        {
            Console.WriteLine("Amount bet: {0:2C} with maximal possible win: {1:2C}", this.BetAmnout, this.MaxPossibleWin);
        }
    }
}
