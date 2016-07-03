using BetSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSystem.Bets
{
    public abstract class Bet
    {
        public IMatch Match { get; }
        public double BetAmnout { get; }
        public double PossibleWin { get; protected set; }
        public double Coefficient { get; protected set; }
        public Bet(IMatch match, double betAmount)
        {
            this.Match = match;
            this.BetAmnout = betAmount;

        }
    }
}
