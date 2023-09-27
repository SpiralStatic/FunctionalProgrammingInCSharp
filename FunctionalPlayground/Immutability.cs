namespace FunctionalPlayground;

public static class Immutability
{
    const int AttackIncrease = 15;

    public static void Run ()
    {
        // Mutate
        Console.WriteLine("Function that mutates");
        var powerLevelMutate = new PowerLevelClass(10, 20);
        Console.WriteLine($"Original: {powerLevelMutate.Attack}");

        var increaseAttackMutate = (PowerLevelClass currentPowerLevel, int attackIncrease) =>
        {
            currentPowerLevel.Attack += attackIncrease;
            return currentPowerLevel;
        };

        var mutated = increaseAttackMutate(powerLevelMutate, AttackIncrease);
        Console.WriteLine($"Increased: {mutated.Attack}");
        Console.WriteLine($"Original: {powerLevelMutate.Attack}\n");

        // Non Mutate
        Console.WriteLine("Function that doesn't mutate");
        var powerLevelNonMutate = new PowerLevelClass(10, 20);
        Console.WriteLine($"Original: {powerLevelNonMutate.Attack}");

        var increaseAttackNonMutate = (PowerLevelClass currentPowerLevel, int attackIncrease) =>
        {
            var newAttackLevel = currentPowerLevel.Attack + attackIncrease;
            return new PowerLevelClass(newAttackLevel, currentPowerLevel.Defense);
        };

        var nonMutated = increaseAttackNonMutate(powerLevelNonMutate, AttackIncrease);
        Console.WriteLine($"Increased: {nonMutated.Attack}");
        Console.WriteLine($"Original: {powerLevelNonMutate.Attack}\n");
    }

    public static void RunRecord()
    {
        // Record
        Console.WriteLine("Function that uses record");
        var powerLevelRecord = new PowerLevelRecord(10, 20);
        Console.WriteLine($"Original: {powerLevelRecord.Attack}");

        //var increaseAttackMutate = (PowerLevelRecord currentPowerLevel, int attackIncrease) =>
        //{
        //    currentPowerLevel.Attack += attackIncrease;
        //    return currentPowerLevel;
        //};

        var increaseAttackNonMutate = (PowerLevelRecord currentPowerLevel, int attackIncrease) =>
        {
            return currentPowerLevel with { Attack = currentPowerLevel.Attack + attackIncrease };
        };

        var nonMutated = increaseAttackNonMutate(powerLevelRecord, AttackIncrease);
        Console.WriteLine($"Increased: {nonMutated.Attack}");
        Console.WriteLine($"Original: {powerLevelRecord.Attack}\n");
    }

    public class PowerLevelClass
    {
        public PowerLevelClass(int attack, int defense)
        {
            Attack = attack;
            Defense = defense;
        }

        public int Attack { get; set; }
        public int Defense { get; set; }
    }

    public record PowerLevelRecord(int Attack, int Defense);
}
