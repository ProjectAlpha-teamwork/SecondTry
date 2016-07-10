namespace BetSystem.NotDrawableSports
{
    using System;

    using BetSystem.EventDeclarations;
    using System.Text;
    public class Volleyball : NotDrawableSports
    {
        //constants
        private const int maxGames = 5;
        private const int maxWinningGames = 3;
        private const int normalGameMaxPoints = 25;
        private const int fifthGameMaxPoints = 15;
        private const int minPointDiffToEnd = 2;

        //constructors
        public Volleyball(string text)
            : base(text)
        {
        }

        //events
        public override event ForcedEndMatchEventHandler ForcedEndOfMatch;
        public event EndOfPartEventHandler Scored;

        //methods
        public override void MatchEventFollower()
        {
            int gamesHome = 0;
            int gamesAway = 0;
            int pointsHome = 0;
            int pointsAway = 0;
            StringBuilder strResultInGames = new StringBuilder();

            while (gamesHome < maxWinningGames && gamesAway < maxWinningGames)
            {
                pointsHome = 0;
                pointsAway = 0;
                int game = gamesHome + gamesAway + 1;
                int pointsDiff = 0;
                int maxPoints = (game < maxGames) ? normalGameMaxPoints : fifthGameMaxPoints;

                while ((pointsHome < maxPoints && pointsAway < maxPoints) ||
                        pointsDiff < minPointDiffToEnd)
                {
                    int randNum = RandomGenerator.RandInt(maxPoints);

                    if (randNum <= maxPoints / 2)
                    {
                        //Home has scored
                        pointsHome++;
                    }
                    else
                    {
                        //Away has hit
                        pointsAway++;
                    }

                    pointsDiff = Math.Abs(pointsHome - pointsAway);
                }

                strResultInGames.Append(string.Format("[{0}:{1}] ", pointsHome, pointsAway));
                if (pointsAway > pointsHome)
                {
                    gamesAway++;
                    this.OnScored(gamesHome, gamesAway, pointsHome, pointsAway, this.Away);
                }
                else
                {
                    gamesHome++;
                    this.OnScored(gamesHome, gamesAway, pointsHome, pointsAway, this.Home);
                }
            }

            strResultInGames.Insert(0, string.Format("{0}:{1} (", gamesHome, gamesAway));
            strResultInGames.Append(")");
            //End of match
            this.OnForcedEndOfMatch(strResultInGames.ToString(),
                        (gamesHome > gamesAway) ? this.Home : this.Away,
                        this.Home, this.Away);
        }

        // Invoke the Scored event when a team has won a game
        private void OnScored(int gamesHome, int gamesAway, int scoredHome, int scoredAway, string team)
        {
            if (this.Scored != null)
            {
                this.Scored(this, new EndOfPartEventArgs(gamesHome, gamesAway, scoredHome, scoredAway, team));
            }
        }
        // Invoke the ForcedEndOfMatch event when the match is over due to knockout
        private void OnForcedEndOfMatch(string message, string winner, string home, string away)
        {
            if (this.ForcedEndOfMatch != null)
            {
                StringBuilder msg = new StringBuilder(message);
                this.ForcedEndOfMatch(this, new ForcedEndMatchEventArgs(msg.ToString(), winner, home, away));
            }
        }

        public override void Print()
        {
            Console.WriteLine();
            Console.WriteLine("****************   VOLLEYBALL   ****************");
            base.Print();
            Console.WriteLine();
        }
    }
}
