namespace BetSystem.Bets
{
    using System;
    using BetSystem.Interfaces;
    using BetSystem.Enumerations;
    using BetSystem.EventDeclarations;

    public class NoDrawBet : Bet
    {
        //constants
        private const int scoreDiffToClose = 3;
        private const double minTimeToclose = 0.25;
        private const double maxTimeToclose = 0.75;

        //properties
        public DrawNotPossibleResults BetOnResult { get; }

        //constructors
        public NoDrawBet(IMatch match, decimal betAmount, string uniqueID, DrawNotPossibleResults betOnResult)
            : base(match, betAmount, uniqueID)
        {
            this.BetOnResult = betOnResult;
        }

        //methods
        public override void CloseBetBeforeEnd(object sender, ScoredEventArgs args)
        {
            if (sender == this.Match &&
                !this.BetClosed &&
                minTimeToclose < args.PassedPartOfMatch && args.PassedPartOfMatch <= maxTimeToclose &&
                ((args.ScoredAway > (args.ScoredHome + scoreDiffToClose) &&
                  this.BetOnResult == DrawNotPossibleResults.WinAway) ||
                 (args.ScoredHome > (args.ScoredAway + scoreDiffToClose) &&
                  this.BetOnResult == DrawNotPossibleResults.WinHome)))
            {
                this.AmountWon = (this.Match.Coefficients[(int)this.BetOnResult] - 1) * this.BetAmnout / 2;
                this.BetClosed = true;
                Console.WriteLine("The bet({0}) for ({1}) has been closed before the end of match with {2:C} win above the bet amount({3:C})!", this.ID, this.Match.ID, this.AmountWon, this.BetAmnout);
            }
        }

        public void CloseBetBeforeEnd(object sender, EndOfPartEventArgs args)
        {
            if (sender == this.Match &&
                !this.BetClosed &&
                ((args.GamesAway > args.GamesHome &&
                  this.BetOnResult == DrawNotPossibleResults.WinAway) ||
                 (args.GamesHome > args.GamesAway &&
                  this.BetOnResult == DrawNotPossibleResults.WinHome)))
            {
                this.AmountWon = (this.Match.Coefficients[(int)this.BetOnResult] - 1) * this.BetAmnout / 2;
                this.BetClosed = true;
                Console.WriteLine("The bet({0}) for ({1}) has been closed before the end of match with {2:C} win above the bet amount({3:C})!", this.ID, this.Match.ID, this.AmountWon, this.BetAmnout);
            }
        }

        public override void CloseBetAfterEnd(object sender, EndMatchEventArgs args)
        {
            if (sender == this.Match && !this.BetClosed)
            {
                if ((args.Winner == this.Match.Away &&
                     this.BetOnResult == DrawNotPossibleResults.WinAway) ||
                    (args.Winner == this.Match.Home &&
                     this.BetOnResult == DrawNotPossibleResults.WinHome))
                {
                    this.AmountWon = (this.Match.Coefficients[(int)this.BetOnResult] - 1) * this.BetAmnout;
                    this.BetClosed = true;
                    Console.WriteLine("The bet({0}) for ({1}) has been closed due to match ending with {2:C} win above your bet amount({3:C})!", this.ID, this.Match.ID, this.AmountWon, this.BetAmnout);
                }
                else
                {
                    Console.WriteLine("The bet({0}) for ({1}) has been closed due to match ending. You have lost your bet amount({2:C})!", this.ID, this.Match.ID, this.BetAmnout);
                }
            }
        }

        public void CloseBetOnForcedEnd(object sender, ForcedEndMatchEventArgs args)
        {
            if (sender == this.Match && !this.BetClosed)
            {
                if ((args.Winner == this.Match.Away &&
                     this.BetOnResult == DrawNotPossibleResults.WinAway) ||
                    (args.Winner == this.Match.Home &&
                     this.BetOnResult == DrawNotPossibleResults.WinHome))
                {
                    this.AmountWon = (this.Match.Coefficients[(int)this.BetOnResult] - 1) * this.BetAmnout;
                    this.BetClosed = true;
                    Console.WriteLine("The bet({0}) for ({1}) has been closed due to {4} for {5}. You win {2:C} above your bet amount({3:C})!", this.ID, this.Match.ID, this.AmountWon, this.BetAmnout, args.Message, args.Winner);
                }
                else
                {
                    Console.WriteLine("The bet({0}) for ({1}) has been closed due to {3} for {4}. You have lost your bet amount({2:C})!", this.ID, this.Match.ID, this.BetAmnout, args.Message, args.Winner);
                }
            }
        }
    }
}
