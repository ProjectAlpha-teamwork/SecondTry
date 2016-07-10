namespace BetSystem.EventDeclarations
{
    using System;
    using Users;
    //EVENT DECLARATIONS
    public class MatchEventArgs : EventArgs
    {
        //properties
        public int ScoredHome { get; set; }
        public int ScoredAway { get; set; }

        //constructors
        public MatchEventArgs(int scoredHome, int scoredAway)
        {
            this.ScoredHome = scoredHome;
            this.ScoredAway = scoredAway;
        }
    }

    public class ScoredEventArgs : MatchEventArgs
    {
        //fields
        private double passedPartOfMatch;

        //properties
        public double PassedPartOfMatch
        {
            get
            {
                return this.passedPartOfMatch;
            }
            set
            {
                Validation.CheckIfDoubleIsInRange(value, 100, 0);
                this.passedPartOfMatch = value;
            }
        }
        public string Team { get; set; }

        //constructors
        public ScoredEventArgs(double passedPartOfMatch, int scoredHome, int scoredAway, string team)
            : base(scoredHome, scoredAway)
        {
            this.PassedPartOfMatch = passedPartOfMatch;
            this.Team = team;
        }
    }

    public class EndOfPartEventArgs : MatchEventArgs
    {
        //fields
        private int gamesHome;
        private int gamesAway;

        //properties
        public int GamesHome
        {
            get
            {
                return this.gamesHome;
            }
            set
            {
                this.gamesHome = value;
            }
        }
        public int GamesAway
        {
            get
            {
                return this.gamesAway;
            }
            set
            {
                this.gamesAway = value;
            }
        }
        public int PassedGames
        {
            get
            {
                return this.GamesHome + this.GamesAway;
            }
        }
        public string Team { get; set; }

        //constructors
        public EndOfPartEventArgs(int gamesHome, int gamesAway, int scoredHome, int scoredAway, string team)
            : base(scoredHome, scoredAway)
        {
            this.GamesHome = gamesHome;
            this.GamesAway = gamesAway;
            this.Team = team;
        }
    }

    public class EndMatchEventArgs : MatchEventArgs
    {
        //constants
        public const string noWinner = "NONE";

        //properties
        public string Home { get; set; }
        public string Away { get; set; }
        public string Winner { get; set; }

        //constructors
        public EndMatchEventArgs(int scoredHome, int scoredAway, string home, string away)
            : base(scoredHome, scoredAway)
        {
            this.Home = home;
            this.Away = away;
            this.Winner = (scoredHome == scoredAway) ? noWinner : (scoredHome > scoredAway) ? home : away;
        }
    }

    public class ForcedEndMatchEventArgs
    {
        //properties
        public string Home { get; set; }
        public string Away { get; set; }
        public string Winner { get; set; }
        public string Message { get; set; }

        //constructors
        public ForcedEndMatchEventArgs(string message, string winner, string home, string away)
        {
            this.Home = home;
            this.Away = away;
            this.Winner = winner;
            this.Message = message;
        }
    }

    // A delegate type for hooking up change notifications.
    public delegate void ScoreChangeEventHandler(object sender, ScoredEventArgs e);
    public delegate void EndMatchEventHandler(object sender, EndMatchEventArgs e);
    public delegate void ForcedEndMatchEventHandler(object sender, ForcedEndMatchEventArgs e);
    public delegate void EndOfPartEventHandler(object sender, EndOfPartEventArgs e);
}
