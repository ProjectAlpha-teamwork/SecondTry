namespace BetSystem
{
    using System;
    using System.Linq;
    using System.Text;
    using EventDeclarations;
    using BetSystem.Interfaces;

    public abstract class Match : IMatch
    {
        //constants
        protected const decimal minCoefficientValue = 1;
        private const string btwnTeams = " vs ";

        //fields
        private string away;
        private string home;
        private string id;
        private decimal[] coefficients;

        //properties
        public string Away
        {
            get
            {
                return this.away;
            }
            protected set
            {
                this.away = value;
            }
        }

        public decimal[] Coefficients
        {
            get
            {
                return this.coefficients;
            }
            protected set
            {
                this.coefficients = value;
            }
        }

        public string Home
        {
            get
            {
                return this.home;
            }
            protected set
            {
                this.home = value;
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
            protected set
            {
                this.id = value;
            }
        }

        //constructors
        public Match(string text)
        {
            this.GetTeams(text);
            this.GetID(text);
            this.Coefficients = this.GetCoefficients(text);
        }

        //methods
        public virtual decimal[] GetCoefficients(string text)
        {
            string[] arrText = text.Split('|');
            if (arrText.Length < 4)
            {
                throw new ArgumentException("Not enough coefficients have been given");
            }

            var arrCoefficients = new ArraySegment<string>(arrText, 2, arrText.Length - 2).ToArray();
            string[] keys = new string[arrCoefficients.Length];
            decimal[] result = new decimal[arrCoefficients.Length];
            for (int i = 0; i < arrCoefficients.Length; i++)
            {
                var curCoef = arrCoefficients[i].Trim().Split('-');
                keys[i] = curCoef[0].Trim();//1 X 2
                try
                {
                    result[i] = decimal.Parse(curCoef[1].Trim());//Coefficient
                    if (result[i] < 1)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    throw new ArgumentException("All coefficients should be positive doubles greater or equal to 1");
                }
            }

            //sort in order 1(first) X(mid if there) 2(last)
            //Check for 1
            int positionOfOne = Array.IndexOf(keys, "1");
            var key = keys[0];
            var coef = result[0];
            keys[0] = "1";
            keys[positionOfOne] = key;
            result[0] = result[positionOfOne];
            result[positionOfOne] = coef;
            //Check for 2
            int positionOfTwo = Array.IndexOf(keys, "2");
            key = keys[keys.Length - 1];
            keys[keys.Length - 1] = "2";
            keys[positionOfTwo] = key;
            coef = result[keys.Length - 1];
            result[keys.Length - 1] = result[positionOfTwo];
            result[positionOfTwo] = coef;

            return result;
        }

        public void GetID(string text)
        {
            this.ID = text.Substring(0, text.IndexOf('|')).Trim();
        }

        public void GetTeams(string text)
        {
            string strTeams = text.Split('|')[1]
                                  .Trim();

            int index = strTeams.IndexOf(btwnTeams);
            this.Home = strTeams.Substring(0, index);
            this.Away = strTeams.Substring(index + btwnTeams.Length);
        }

        public virtual void Print()
        {
            int i = 0;
            Console.WriteLine("Match ID: {0}", this.ID);
            Console.WriteLine("{0}{2}{1}", this.Home.ToUpper(), this.Away.ToUpper(), btwnTeams);

            StringBuilder strGivenCoefs = new StringBuilder("Given Coefficients for: 1 (");
            strGivenCoefs.Append(this.Coefficients[i]);
            strGivenCoefs.Append("); ");
            i++;
            if (this.Coefficients.Length > 2)
            {
                strGivenCoefs.Append("X (");
                strGivenCoefs.Append(this.Coefficients[i]);
                strGivenCoefs.Append("); ");
                i++;
            }
            strGivenCoefs.Append("2 (");
            strGivenCoefs.Append(this.Coefficients[i]);
            strGivenCoefs.Append("); ");

            Console.WriteLine(strGivenCoefs);
        }

        public abstract void MatchEventFollower();
    }
}
