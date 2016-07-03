using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetSystem.Interfaces;
using BetSystem.Enumerations;

namespace BetSystem.Bets
{
    public class DrawBet : Bet
    {
        public DrawPossibleResults Result { get; }
        public DrawBet(IMatch match, double betAmount, DrawPossibleResults result) 
            : base(match,betAmount)
        {
            this.Result = result;
            this.Coefficient = this.Match.Coefficients[(int)result];
            this.PossibleWin = this.Coefficient * this.BetAmnout;
        }
    }
}
