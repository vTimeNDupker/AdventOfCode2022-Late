namespace AOCDay3;

public class RucksackSorter
{
    public class RucksackData
    {
        public List<int> ContainedPrioritiesLeft;
        public List<int> ContainedPrioritiesRight;
        public int PriorityFoundInBoth;
    }
    
    public static int ReadFileAndGetPriorityTotalP1(string fileName)
    {
        var lines = File.ReadLines(fileName);
        List<RucksackData> rucksackDatas = new List<RucksackData>();
        int totalPriorities = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string left = line.Substring(0, line.Length / 2);
            string right = line.Substring(line.Length / 2, line.Length / 2);
            RucksackData rucksack = new RucksackData();
            rucksack.ContainedPrioritiesLeft = new List<int>();
            rucksack.ContainedPrioritiesRight = new List<int>();
            foreach (var ch in left)
            {
                if (char.IsUpper(ch))
                    rucksack.ContainedPrioritiesLeft.Add((int) ch - (int) 'A' + 27);
                else
                    rucksack.ContainedPrioritiesLeft.Add((int) ch - (int) 'a' + 1);
            }
            bool Found = false;
            int PriorityInBoth = -1;
            foreach (var ch in right)
            {
                if (char.IsUpper(ch))
                    rucksack.ContainedPrioritiesRight.Add((int) ch - (int) 'A' + 27);
                else
                    rucksack.ContainedPrioritiesRight.Add((int) ch - (int) 'a' + 1);

                if (rucksack.ContainedPrioritiesLeft.Contains(rucksack.ContainedPrioritiesRight[^1]))
                {
                    Found = true;
                    PriorityInBoth = rucksack.ContainedPrioritiesRight[^1];
                    break;
                }
            }

            if (Found)
            {
                rucksack.PriorityFoundInBoth = PriorityInBoth;
                totalPriorities += PriorityInBoth;
            }
            else
            {
                Console.WriteLine("Error: could not find a priority common to left and right on line: " + (rucksackDatas.Count + 1));
                return 0;
            }
            rucksackDatas.Add(rucksack);
        }

        return totalPriorities;
    }
    public class RucksackGroup
    {
        public List<int> ContainedPrioritiesFirst;
        public List<int> ContainedPrioritiesFirstAndSecond;
        public int PriorityFoundInAll;
    }
    public static int ReadFileAndGetPriorityTotalP2(string fileName)
    {
        var lines = File.ReadLines(fileName);
        List<RucksackGroup> rucksackGroups = new List<RucksackGroup>();
        int totalPriorities = 0;
        
        RucksackGroup currentRucksackGroup = new RucksackGroup();
        currentRucksackGroup.ContainedPrioritiesFirst = new List<int>();
        currentRucksackGroup.ContainedPrioritiesFirstAndSecond = new List<int>();
        int lineNumberInGroup = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine("Error: null line found after group: " + (rucksackGroups.Count + 1));
                return 0;
            }

            switch (lineNumberInGroup)
            {
                case 0:
                    foreach (var ch in line)
                    {
                        if (char.IsUpper(ch))
                            currentRucksackGroup.ContainedPrioritiesFirst.Add((int) ch - (int) 'A' + 27);
                        else
                            currentRucksackGroup.ContainedPrioritiesFirst.Add((int) ch - (int) 'a' + 1);
                    }
                    lineNumberInGroup = 1;
                    break;
                case 1:
                    foreach (var ch in line)
                    {
                        int priority = -1;
                        if (char.IsUpper(ch))
                            priority = (int) ch - (int) 'A' + 27;
                        else
                            priority = (int) ch - (int) 'a' + 1;
                        if(currentRucksackGroup.ContainedPrioritiesFirst.Contains(priority))
                            currentRucksackGroup.ContainedPrioritiesFirstAndSecond.Add(priority);
                    }
                    lineNumberInGroup = 2;
                    break;
                case 2:
                    foreach (var ch in line)
                    {
                        int priority = -1;
                        if (char.IsUpper(ch))
                            priority = (int) ch - (int) 'A' + 27;
                        else
                            priority = (int) ch - (int) 'a' + 1;
                        if (currentRucksackGroup.ContainedPrioritiesFirstAndSecond.Contains(priority))
                        {
                            rucksackGroups.Add(currentRucksackGroup);
                            totalPriorities += priority;
                            currentRucksackGroup = new RucksackGroup();
                            currentRucksackGroup.ContainedPrioritiesFirst = new List<int>();
                            currentRucksackGroup.ContainedPrioritiesFirstAndSecond = new List<int>();
                            break;
                        }
                    }
                    lineNumberInGroup = 0;
                    break;
            }
        }

        return totalPriorities;
    }
}