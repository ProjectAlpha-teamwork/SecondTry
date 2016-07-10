namespace BetSystem.Interfaces
{
    using EventDeclarations;

    public interface IBet
    {
        //properties
        string ID { get; }
        IMatch Match { get; }
        bool BetClosed { get; }
        decimal BetAmnout { get; }
        decimal MaxPossibleWin { get; }
        decimal AmountWon { get; }

        //methods
        void CloseBetBeforeEnd(object sender, ScoredEventArgs args);
        void CloseBetAfterEnd(object sender, EndMatchEventArgs args);
        void Print();
    }
}
