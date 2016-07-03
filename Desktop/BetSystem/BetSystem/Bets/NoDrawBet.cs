using System;
using BetSystem.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetSystem.Interfaces;

namespace BetSystem.Bets
{
    public class NoDrawBet: Bet
    {
        public DrawNotPossibleResults Result { get; }
        public NoDrawBet(IMatch match, double betAmount, DrawNotPossibleResults result) 
            : base(match,betAmount)
        {
            this.Result = result;
            this.Coefficient = this.Match.Coefficients[(int)result];
            this.PossibleWin = this.Coefficient * this.BetAmnout;
        }
    }
}
