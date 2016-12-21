using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DieRoll.Net
{
    public class DieRoller
    {
        /// <summary>
        /// Random object for rolling the dice.
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// Regex for getting the number of dice.
        /// 
        /// "1d6" would match on "1", for instance.
        /// </summary>
        private Regex _dieCountRegex = new Regex(@"(.+)(?=d)");

        /// <summary>
        /// Regex for getting the number of sides on each dice.
        /// 
        /// "1d6" would match on "6", for instance.
        /// </summary>
        private Regex _dieSidesRegex = new Regex(@"(?<=d)(\d+)");

        /// <summary>
        /// Regex for getting the bonuses on a roll.
        /// 
        /// "1d6+1" would matchon "+1", for instance.
        /// </summary>
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
            // Get roll info
            var dieCount = int.Parse(_dieCountRegex.Match(format).ToString());
            var dieSides = int.Parse(_dieSidesRegex.Match(format).ToString());
            var bonusString = _bonusRegex.Match(format).ToString();
            
            var result = 0;

            // Iterate the number of dice
            for (var d = 0; d < dieCount; d++)
            {
                // Roll the dice
                result += _random.Next(1, dieSides);
            }

            // Apply bonuses to the current result
            ApplyBonuses(ref result, bonusString);

            return result;
        }

        /// <summary>
        /// Apply the bonuses, if applicable.
        /// 
        /// Runs a regex to find individual bonuses and then iterates them.
        /// </summary>
        /// <param name="n">The result/number to apply bonuses to.</param>
        /// <param name="bonusString">The string containing the bonus info.</param>
        private void ApplyBonuses(ref int n, string bonusString)
        {
            // Match on +1, +2, -10 etc.
            var r = new Regex(@"((?:\+|\-)\d+)");

            // Iterate each match
            foreach (Match opMatch in r.Matches(bonusString))
            {
                // Get the string representation of the match
                var op = opMatch.ToString();

                // Get the math operator of the match (-, +, * etc.)
                var mathOperation = op.First();

                // The number to use the operator on
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
