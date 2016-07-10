namespace BetSystem.NotDrawableSports
{
    using System;
    using BetSystem.EventDeclarations;

    public abstract class NotDrawableSports : Match
    {
        //constructors
        public NotDrawableSports(string text)
            : base(text)
        {
        }

        //events
        public abstract event ForcedEndMatchEventHandler ForcedEndOfMatch;
    }
}
