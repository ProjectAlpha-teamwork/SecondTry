namespace BetSystem.Enumerations
{
    using System;

    [Flags]
    public enum DrawPossibleResults
    {
        WinHome = 1,//1
        Draw = 2,//X
        WinAway = 4,//2
        WinHomeorAway = WinHome | WinAway,//12
        WinHomeOrDraw = WinHome | Draw,//1X
        WinAwayOrDraw = Draw | WinAway//X2
    }
    //public enum DrawPossibleResults
    //{
    //   WinHome,//1
    //   Draw,//X
    //   WinAway,//2
    //   WinHomeorAway,//12
    //   WinOrDrawHome,//1X
    //   WinOrDrawAway//X2
    //}
}
