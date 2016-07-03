using BetSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSystem
{
    public abstract class Match : IMatch
    {
        private string away;
        private string home;
        private string id;
        private double[] coefficients;
        public Match()
        {

        }
        public string Away
        {
            get
            {
                return this.away;
            }
            protected set { }
        }

        public double[] Coefficients
        {
            get
            {
                return this.coefficients;
            }
            protected set { }
        }

        public string Home
        {
            get
            {
                return this.home;
            }
            protected set { }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
            protected set { }
        }

        public virtual double[]  GetCoefficients(string text)
        {
            throw new NotImplementedException();
        }

        public string GetID(string text)
        {
            throw new NotImplementedException();
        }

        public string GetTeams(string text)
        {
            throw new NotImplementedException();
        }

        public virtual void Print()
        {
            Console.WriteLine("Match ID: {0}", this.ID);
            Console.WriteLine("{0} vs {1}", this.Home, this.Away);
        }
    }
}
