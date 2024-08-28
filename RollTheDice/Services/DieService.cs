using RollTheDice.Interface;
using RollTheDice.Model;

namespace RollTheDice.Services
{
    public class DieService : IDieService
    {
        private readonly Random _rand = new Random();
        public Die GetDie(List<Die> dice, int totalFaceFactor)
        {
            int nRand = _rand.Next(0, totalFaceFactor);

            Die currentDie = null;
            foreach(var d in dice)
            {
                if(nRand < d.FaceFactor)
                {
                    currentDie = d;
                    break;
                }
                nRand = nRand - d.FaceFactor;
            }
            return currentDie;
        }
        public IEnumerable<DiceResult> Roll(List<Die> inputDie1, List<Die> inputDie2, int numberOfRolls)
        {
            int totalFactor1 = 0;
            int totalFactor2 = 0;
            foreach(var d in inputDie1)
            {
                totalFactor1 += d.FaceFactor;
            }
            foreach(var d in inputDie2)
            {
                totalFactor2 += d.FaceFactor;
            }

            List<DiceResult> results = new List<DiceResult>();

            Die currentDie1 = null;
            Die currentDie2 = null;
            for(int i= 0;i < numberOfRolls; i++)
            {
                currentDie1 = GetDie(inputDie1, totalFactor1);
                currentDie2 = GetDie(inputDie2, totalFactor2);
                if(currentDie1 != null && currentDie2 != null)
                {
                    DiceResult result = new DiceResult();
                    result.FirstDieResult = currentDie1;
                    result.SecondDieResult = currentDie2;
                    results.Add(result);
                }
            }

            return results;
        }
    }
}
