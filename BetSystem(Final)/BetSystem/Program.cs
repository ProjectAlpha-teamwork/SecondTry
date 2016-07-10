namespace BetSystem
{
    using System;

    using NotDrawableSports;
    using DrawableSports;
    using EventDeclarations;
    using Enumerations;
    using Interfaces;
    using Bets;
    using Users;
    class Program
    {
        static void PrintScoreEvent(object sender, ScoredEventArgs args)
        {
            Console.WriteLine("team {0} has scored after {1:f2}% of match has passed", args.Team, args.PassedPartOfMatch);
        }

        static void PrintScoreEvent(object sender, EndOfPartEventArgs args)
        {
            Console.WriteLine("team {0} has won game#{1}. The result in the game is [{2}:{3}]", args.Team, args.PassedGames, args.ScoredHome, args.ScoredAway);
        }

        static void PrintEndOfMatchEvent(object sender, EndMatchEventArgs args)
        {
            Console.WriteLine("{0} has won! The result of match {1} vs {2} is {3} : {4}", args.Winner, args.Home, args.Away, args.ScoredHome, args.ScoredAway);
        }

        static void PrintForcedEndOfMatchEvent(object sender, ForcedEndMatchEventArgs args)
        {
            Console.WriteLine("{0} has won with {1} against {2}", args.Winner, args.Message, (args.Winner == args.Home) ? args.Away : args.Home);
        }

        static void Main()
        {
            //FOOTBALL
            string matchLevskiCSKA = "Ftb0001 | Levski vs CSKA | x - 2.22 | 1 - 3.33 | 2 - 1.11";
            DrawableSports.DrawableSports mLevskiCSKA = new Football(matchLevskiCSKA);
            string matchLitexBeroe = "Ftb0002 | Litex vs Beroe | 1 - 1.2 | 2 - 4 | x - 3";
            DrawableSports.DrawableSports mLitexBeroe = new Football(matchLitexBeroe);

            mLevskiCSKA.Print();
            //Bet on match
            DrawBet betLevskiCSKA = new DrawBet(mLevskiCSKA, 10, "MyUniqueID", DrawPossibleResults.WinAway);
            //Events
            (mLevskiCSKA as Football).Scored += PrintScoreEvent;
            (mLevskiCSKA as Football).Scored += betLevskiCSKA.CloseBetBeforeEnd;
            (mLevskiCSKA as Football).EndOfMatch += PrintEndOfMatchEvent;
            (mLevskiCSKA as Football).EndOfMatch += betLevskiCSKA.CloseBetAfterEnd;
            //simulate match
            mLevskiCSKA.MatchEventFollower();

            mLitexBeroe.Print();
            //Bet on match
            DrawBet betLitexBeroe = new DrawBet(mLitexBeroe, 110, "OtherUniqueID", DrawPossibleResults.WinHome);
            //Events
            (mLitexBeroe as Football).Scored += PrintScoreEvent;
            //(mLitexBeroe as Football).Scored += betLitexBeroe.CloseBetBeforeEnd;
            (mLitexBeroe as Football).EndOfMatch += PrintEndOfMatchEvent;
            (mLitexBeroe as Football).EndOfMatch += betLitexBeroe.CloseBetAfterEnd;
            //simulate match
            mLitexBeroe.MatchEventFollower();


            //BOX
            string matchPulevKlichko = "Bx0001 | Kubrat Pulev vs Vladimir Klichko | 2 - 2.222 | 1 - 6.111";
            Box mPulevKlichko = new Box(matchPulevKlichko);
            mPulevKlichko.Print();
            //Bet on match
            NoDrawBet betPulevKlichko = new NoDrawBet(mPulevKlichko, 1000, "BoxUniqueID", DrawNotPossibleResults.WinAway);
            //Events
            mPulevKlichko.Scored += PrintScoreEvent;
            mPulevKlichko.Scored += betPulevKlichko.CloseBetBeforeEnd;
            mPulevKlichko.EndOfMatch += PrintEndOfMatchEvent;
            mPulevKlichko.EndOfMatch += betPulevKlichko.CloseBetAfterEnd;
            mPulevKlichko.ForcedEndOfMatch += PrintForcedEndOfMatchEvent;
            mPulevKlichko.ForcedEndOfMatch += betPulevKlichko.CloseBetOnForcedEnd;
            //simulate match
            mPulevKlichko.MatchEventFollower();


            //VOLLEYBALL
            string matchBulgariaPoland = "Volley0001 | Bulgaria vs Poland | 2 - 2.222 | 1 - 6.111";
            Volleyball mBulgariaPoland = new Volleyball(matchBulgariaPoland);
            mBulgariaPoland.Print();
            //Bet on match
            NoDrawBet betBulgariaPoland = new NoDrawBet(mBulgariaPoland, 150, "VolleyUniqueID", DrawNotPossibleResults.WinHome);
            //Events
            mBulgariaPoland.Scored += PrintScoreEvent;
            //mBulgariaPoland.Scored += betBulgariaPoland.CloseBetBeforeEnd;
            //mBulgariaPoland.ForcedEndOfMatch += PrintForcedEndOfMatchEvent;
            //mBulgariaPoland.ForcedEndOfMatch += betBulgariaPoland.CloseBetOnForcedEnd;

            //var reg = new Registration("gosho", "Georgi", "Georgiev", "954751894864", 100, Gender.Male, "123456", "123456", "Sliven", new DateTime(1995, 5, 8));

            var gosho = Login.SignIn("gosho", "123456");
            IMatch goshoBetsOn = mBulgariaPoland;

            if (goshoBetsOn is NotDrawableSports.NotDrawableSports)
            {
                NoDrawBet goshoBets = gosho.MakeBet(goshoBetsOn, 100, gosho.UserName, DrawNotPossibleResults.WinAway);
                (goshoBetsOn as NotDrawableSports.NotDrawableSports).ForcedEndOfMatch += goshoBets.CloseBetOnForcedEnd;
            }
            else
            {
                DrawBet goshoBets = gosho.MakeBet(goshoBetsOn, 100, gosho.UserName, DrawPossibleResults.WinAway);
                (goshoBetsOn as DrawableSports.DrawableSports).EndOfMatch += goshoBets.CloseBetAfterEnd;
            }

            //simulate match
            mBulgariaPoland.MatchEventFollower();
        }
    }
}
