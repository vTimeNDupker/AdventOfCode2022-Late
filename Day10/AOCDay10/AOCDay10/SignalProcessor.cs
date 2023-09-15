namespace AOCDay10;

public class SignalProcessor
{
    private static int[] cyclesToRecord = new[] {20, 60, 100, 140, 180, 220};
    static void Cycle(ref int cycleCount, ref int xRegister, ref int sum)
    {
        cycleCount++;
        if (cyclesToRecord.Contains(cycleCount))
        {
            sum += cycleCount * xRegister;
        }
    }
    
    public static int ProcessInstructions(string fileName)
    {
        int cycleCount = 0;
        int xRegister = 1;
        int sum = 0;
        
        var lines = File.ReadLines(fileName);
        foreach (var line in lines)
        { 
            string[] commandParts = line.Split(' ');
            switch (commandParts[0])
            {
                case "noop":
                    Cycle(ref cycleCount, ref xRegister, ref sum);
                    break;
                case "addx":
                    Cycle(ref cycleCount, ref xRegister, ref sum);
                    Cycle(ref cycleCount, ref xRegister, ref sum);
                    xRegister += int.Parse(commandParts[1]);
                    break;
            }
        }

        return sum;
    }

    const int screenWidth = 40;
    
    static void CycleScreen(ref int cycleCount, ref int xRegister, ref string screen)
    {
        if (cycleCount >= screenWidth)
        {
            screen += Environment.NewLine;
            cycleCount = 0;
        }
        if (MathF.Abs(cycleCount - xRegister) <= 1)
            screen += "#";
        else
            screen += ".";
        cycleCount++;
    }
    
    public static void ProcessScreenInstructions(string fileName)
    {
        int cycleCount = 0;
        int xRegister = 1;
        string screen = "";
        
        var lines = File.ReadLines(fileName);
        foreach (var line in lines)
        { 
            string[] commandParts = line.Split(' ');
            switch (commandParts[0])
            {
                case "noop":
                    CycleScreen(ref cycleCount, ref xRegister, ref screen);
                    break;
                case "addx":
                    CycleScreen(ref cycleCount, ref xRegister, ref screen);
                    CycleScreen(ref cycleCount, ref xRegister, ref screen);
                    xRegister += int.Parse(commandParts[1]);
                    break;
            }
        }
        Console.Write(screen);
    }
}