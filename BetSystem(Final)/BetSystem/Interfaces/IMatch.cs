namespace BetSystem.Interfaces
{
    using EventDeclarations;

    public interface IMatch
    {
        //properties
        string Home { get; }
        string Away { get; }
        decimal[] Coefficients { get; }
        string ID { get; }

        //events
        //event ScoreChangeEventHandler Scored;
        //event EndMatchEventHandler EndOfMatch;

        //methods
        void GetID(string text);
        void GetTeams(string text);
        decimal[] GetCoefficients(string text);
        void Print();

        //follow the events in match that could change bet status:
        //a team leads with some difference that would make the user close the bet
        //a team leads at some point after match start that would make the user close the bet
        //match ends - bet is closed
        void MatchEventFollower();
    }
}
