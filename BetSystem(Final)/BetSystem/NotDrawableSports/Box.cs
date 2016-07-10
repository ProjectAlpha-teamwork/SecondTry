namespace BetSystem.NotDrawableSports
{
    using System;

    using BetSystem.EventDeclarations;
    using System.Text;
    public class Box : NotDrawableSports
    {
        //constants
        private const int rounds = 12;
        private const int roundSeconds = 180;
        private const string msgKnockout = "KNOCKOUT";

        //constructors
        public Box(string text)
            : base(text)
        {
        }

        //events
        public event EndMatchEventHandler EndOfMatch;
        public override event ForcedEndMatchEventHandler ForcedEndOfMatch;
        public event ScoreChangeEventHandler Scored;

        //methods
        public override void MatchEventFollower()
        {
            int scoredHome = 0;
            int scoredAway = 0;
            int maxRand = 3 * roundSeconds;

            for (int round = 0; round < rounds; round++)
            {
                for (int second = 0; second < roundSeconds; second++)
                {
                    int randNum = RandomGenerator.RandInt(maxRand);
                    //Console.WriteLine("randNum = {0}", randNum);
                    if (randNum < roundSeconds / 40)
                    {
                        if (randNum == 1)
                        {//Home has won with Knockout
                            this.OnForcedEndOfMatch(round, second, msgKnockout, this.Home, this.Home, this.Away);
                            return;
                        }

                        //Home has hit
                        scoredHome++;
                        this.OnScored(round, second, scoredHome, scoredAway, this.Home);
                    }
                    else if (39 * roundSeconds / 40 < randNum && randNum < roundSeconds)
                    {
                        if (randNum == roundSeconds - 1)
                        {//Away has won with Knockout
                            this.OnForcedEndOfMatch(round, second, msgKnockout, this.Away, this.Home, this.Away);
                            return;
                        }
                        //Away has hit
                        scoredAway++;
                        this.OnScored(round, second, scoredHome, scoredAway, this.Away);
                    }
                }
            }

            //End of match
            this.OnEndOfMatch(scoredHome, scoredAway, this.Home, this.Away);
        }

        // Invoke the Scored event when a team has scored
        private void OnScored(int round, int second, int scoredHome, int scoredAway, string team)
        {
            if (this.Scored != null)
            {
                this.Scored(this, new ScoredEventArgs((double)100 * (round * roundSeconds + second) / (rounds * roundSeconds), scoredHome, scoredAway, team));
            }
        }
        // Invoke the ForcedEndOfMatch event when the match is over due to knockout
        private void OnForcedEndOfMatch(int round, int second, string message, string winner, string home, string away)
        {
            if (this.ForcedEndOfMatch != null)
            {
                StringBuilder msg = new StringBuilder(message);
                msg.Append(string.Format(" at {0} second of {1} round", second, round + 1));
                this.ForcedEndOfMatch(this, new ForcedEndMatchEventArgs(msg.ToString(), winner, home, away));
            }
        }
        // Invoke the EndOfMatch event when the match is over
        private void OnEndOfMatch(int scoredHome, int scoredAway, string home, string away)
        {
            if (this.EndOfMatch != null)
            {
                this.EndOfMatch(this, new EndMatchEventArgs(scoredHome, scoredAway, home, away));
            }
        }

        public override void Print()
        {
            Console.WriteLine();
            Console.WriteLine("****************   BOX   ****************");
            base.Print();
            Console.WriteLine();
        }
    }
}
