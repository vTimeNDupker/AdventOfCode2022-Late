using System.Collections;

namespace Day1;

public class CalorieCalc
{
    static public string ReadCaloriesAndFindHighestCount(string fileName)
    {
        var lines = File.ReadLines(fileName);
        List<int> elves = new List<int>();
        elves.Add(0);
        foreach (var line in lines)
        {
            if(string.IsNullOrWhiteSpace(line))
                elves.Add(0);
            else
            {
                int calories;
                if(int.TryParse(line, out calories))
                {
                    // ^1 gets the last item in the list
                    elves[^1] += calories;
                }
                else
                {
                    return "Error: Data contained non numbers.";
                }
            }
        }
        int highestCount = int.MinValue;
        List<int> highestCountIDs = new List<int>();
        for (int i = 0; i < elves.Count; i++)
        {
            if (elves[i] >= highestCount)
            {
                if (elves[i] > highestCount)
                    highestCountIDs.Clear();
                highestCountIDs.Add(i);
                highestCount = elves[i];
            }
        }

        string message = "";
        if (highestCountIDs.Count > 1)
        {
            message += "Multiple elves with highest calories: ";
            foreach (var id in highestCountIDs)
            {
                message += Environment.NewLine + "Elf " + (id+1);
            }
            message += Environment.NewLine + "Highest Calories is " + highestCount;
        }
        else
        {
            message += "Elf with highest calories is Elf " + (highestCountIDs[0] + 1) + " with " + highestCount;
        }
        return message;
    }
    
    static public string ReadCaloriesAndFindTop3Counts(string fileName)
    {
        var lines = File.ReadLines(fileName);
        SortedList<int, int> elves = new SortedList<int, int>();
        elves.Add(0, 0);
        int currentID = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                currentID++;
                elves.Add(currentID, 0);
            }
            else
            {
                int calories;
                if(int.TryParse(line, out calories))
                {
                    // ^1 gets the last item in the list
                    elves[currentID] += calories;
                }
                else
                {
                    return "Error: Data contained non numbers.";
                }
            }
        }
        var list = elves.OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key).ToList();
        string message = "Top 3 elves are: ";
        int count = 0;
        int total = 0;
        foreach (var pair in list)
        {
            message += Environment.NewLine + "Elf " + (pair.Key +1) + " with " + pair.Value + " Cal";
            count++;
            total += pair.Value;
            if (count == 3)
                break;
        }
        message += Environment.NewLine + "Total for all 3 is: " + total;
        return message;
    }
}