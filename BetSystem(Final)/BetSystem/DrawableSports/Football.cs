namespace BetSystem.DrawableSports
{
    using System;

    using BetSystem.EventDeclarations;
    using BetSystem;

    public class Football : DrawableSports
    {
        //constants
        private const int duration = 90;

        //constructors
        public Football(string text)
            : base(text)
        {
        }

        //events
        public override event ScoreChangeEventHandler Scored;
        public override event EndMatchEventHandler EndOfMatch;

        //methods
        public override void MatchEventFollower()
        {
            int scoredHome = 0;
            int scoredAway = 0;

            for (int minute = 0; minute < duration; minute++)
            {
                int randNum = RandomGenerator.RandInt(duration);

                if (randNum < duration / 20)
                {
                    //Home has scored
                    scoredHome++;
                    this.OnScored(minute, scoredHome, scoredAway, this.Home);
                }
                else if (randNum > 19 * duration / 20)
                {
                    //Away has scored
                    scoredAway++;
                    this.OnScored(minute, scoredHome, scoredAway, this.Away);
                }
            }

            //End of match
            this.OnEndOfMatch(scoredHome, scoredAway, this.Home, this.Away);
        }

        // Invoke the Scored event when a team has scored
        private void OnScored(int minute, int scoredHome, int scoredAway, string team)
        {
            if (this.Scored != null)
            {
                this.Scored(this, new ScoredEventArgs((double)100 * minute / duration, scoredHome, scoredAway, team));
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
            Console.WriteLine("****************   FOOTBALL   ****************");
            base.Print();
            Console.WriteLine();
        }
    }
}
