namespace BetSystem.Bets
{
    using System;
    using System.Linq;

    using BetSystem.Interfaces;
    using BetSystem.Enumerations;
    using BetSystem.EventDeclarations;

    public class DrawBet : Bet
    {
        public DrawPossibleResults BetOnResult { get; }

        //constructors
        public DrawBet(IMatch match, decimal betAmount, string uniqueID, DrawPossibleResults betOnResult)
            : base(match, betAmount, uniqueID)
        {
            this.BetOnResult = betOnResult;
        }

        //methods
        private decimal[] ReorderCoefToPossibleResults()
        {
            //Match.Coefficients are in order [1 X 2 12 1X X2]
            //In enumeration DrawPossibleResults the possible results when 
            //ordered by their int values correspond to order [1 X 1X 2 12 X2]
            //This method returns 
            int len = this.Match.Coefficients.Length;
            decimal[] arrCoef = new decimal[len];
            Array.Copy(this.Match.Coefficients, arrCoef, len);
            //swap middle elements
            decimal temp = arrCoef[4];
            arrCoef[4] = arrCoef[3];
            arrCoef[3] = arrCoef[2];
            arrCoef[2] = temp;

            return arrCoef;
        }

        public override void CloseBetBeforeEnd(object sender, ScoredEventArgs args)
        {//if half of match duration has passed and preferred team wins - close bet with 50% of possible win
            if (sender == this.Match &&
                !this.BetClosed &&
                (args.PassedPartOfMatch <= 0.5) &&
                ((args.ScoredAway > args.ScoredHome &&
                (this.BetOnResult & DrawPossibleResults.WinAway) == DrawPossibleResults.WinAway) ||
                (args.ScoredAway < args.ScoredHome &&
                (this.BetOnResult & DrawPossibleResults.WinHome) == DrawPossibleResults.WinHome) ||
                (args.ScoredAway == args.ScoredHome &&
                (this.BetOnResult & DrawPossibleResults.Draw) == DrawPossibleResults.Draw)))
            {
                decimal[] arrCoef = ReorderCoefToPossibleResults();

                this.AmountWon = (arrCoef[(int)this.BetOnResult - 1] - 1) * this.BetAmnout / 2;
                this.BetClosed = true;
                Console.WriteLine("The bet({0}) for ({1}) has been closed before the end of match with {2:C} win above the bet amount({3:C})!", this.ID, this.Match.ID, this.AmountWon, this.BetAmnout);
            }
        }

        public override void CloseBetAfterEnd(object sender, EndMatchEventArgs args)
        {
            if (sender == this.Match && !this.BetClosed)
            {
                if ((args.Winner == this.Match.Away &&
                    (this.BetOnResult & DrawPossibleResults.WinAway) == DrawPossibleResults.WinAway) ||
                    (args.Winner == this.Match.Home &&
                    (this.BetOnResult & DrawPossibleResults.WinHome) == DrawPossibleResults.WinHome) ||
                    (args.Winner == EndMatchEventArgs.noWinner &&
                    (this.BetOnResult & DrawPossibleResults.Draw) == DrawPossibleResults.Draw))
                {
                    decimal[] arrCoef = ReorderCoefToPossibleResults();

                    this.AmountWon = (arrCoef[(int)this.BetOnResult - 1] - 1) * this.BetAmnout;
                    this.BetClosed = true;
                    Console.WriteLine("The bet({0}) for ({1}) has been closed due to match ending with {2:C} win above your bet amount({3:C})!",this.ID, this.Match.ID, this.AmountWon, this.BetAmnout);
                }
                else
                {
                    Console.WriteLine("The bet({0}) for ({1}) has been closed due to match ending. You have lost your bet amount({2:C})!", this.ID, this.Match.ID, this.BetAmnout);
                }
            }
        }
    }
}
