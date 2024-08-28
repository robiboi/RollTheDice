using RollTheDice.Model;

namespace RollTheDice.Interface
{
    public interface IDieService
    {
        IEnumerable<DiceResult> Roll(List<Die> inputDie1, List<Die> inputDie2, int numberOfRolls);
    }
}
