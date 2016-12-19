using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DieRoll.Net
{
    public class DieRoller
    {
        private readonly Random _random;

        private Regex _dieCountRegex = new Regex(@"(.+)(?=d)");

        private Regex _dieSidesRegex = new Regex(@"(?<=d)(\d+)");

        private Regex _bonusRegex = new Regex(@"((?:\+|\-).*)");

        /// <summary>
        /// Constructor.
        /// </summary>
        public DieRoller()
        {
            _random = new Random();
        }

        /// <summary>
        /// Resolve the given dice notation and return the result.
        /// </summary>
        /// <param name="format"></param>
        /// <returns>The result of the rolling.</returns>
        public int Roll(string format)
        {
            var dieCount = int.Parse(_dieCountRegex.Match(format).ToString());
            var dieSides = int.Parse(_dieSidesRegex.Match(format).ToString());
            var bonusString = _bonusRegex.Match(format).ToString();

            var result = 0;

            for (var d = 0; d < dieCount; d++)
            {
                result += _random.Next(1, dieSides);
            }

            ApplyBonuses(ref result, bonusString);

            return result;
        }

        private void ApplyBonuses(ref int n, string bonusString)
        {
            var r = new Regex(@"((?:\+|\-)\d+)");

            foreach (Match opMatch in r.Matches(bonusString))
            {
                var op = opMatch.ToString();

                var mathOperation = op.First();
                var num = int.Parse(op.Substring(1));

                switch (mathOperation)
                {
                    case '+':
                        n += num;
                        break;
                    case '-':
                        n -= num;
                        break;
                    case '*':
                        n *= num;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
