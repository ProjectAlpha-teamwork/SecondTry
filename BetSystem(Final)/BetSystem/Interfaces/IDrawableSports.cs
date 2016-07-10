namespace BetSystem.Interfaces
{
    public interface IDrawableSports : IMatch
    {
        //properties
        decimal[] GivenCoefficients { get; }
        decimal[] CalculatedCoefficients { get; }
    }
}
