using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSystem.Interfaces
{
    public interface IMatch
    {
        string Home { get; }
        string Away { get; }
        double[] Coefficients { get; }
        string ID { get; }
        string GetID(string text);
        string GetTeams(string text);
        double[] GetCoefficients(string text);
        void Print();
    }
}
