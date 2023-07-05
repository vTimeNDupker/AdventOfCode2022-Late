namespace AOCDay5;

public class CrateSorter
{
    enum ReadState
    {
        ReadingCrates,
        ReadingNumbers,
        ReadingMoves
    }
    enum OrderWhenMoving
    {
        Same,
        Reverse
    }
    public static string ReadFileAndSortCratesP1(string fileName)
    {
        return ReadFileAndSortCrates(fileName, OrderWhenMoving.Reverse);
    }
    
    public static string ReadFileAndSortCratesP2(string fileName)
    {
        return ReadFileAndSortCrates(fileName, OrderWhenMoving.Same);
    }

    static string ReadFileAndSortCrates(string fileName, OrderWhenMoving orderWhenMoving)
    {
        List<List<char>> crateStacks = new List<List<char>>();

        ReadState currentReadState = ReadState.ReadingCrates;
        
        var lines = File.ReadLines(fileName);
        
        foreach (var line in lines)
        {
            switch (currentReadState)
            {
                case ReadState.ReadingCrates:
                    if (!line.Contains('['))
                    {
                        currentReadState = ReadState.ReadingNumbers;
                        continue;
                        //end of crates
                    }

                    int StackID = 0;
                    string lineCopy = (string)line.Clone();
                    while (lineCopy.Length > 0)
                    {
                        string potentialCrate = lineCopy.Substring(0, 3);
                        if(crateStacks.Count < StackID+1)
                            crateStacks.Add(new List<char>());
                        if (!potentialCrate.Contains('['))
                        {
                            //nothing on this stack yet
                        }
                        else
                        {
                            crateStacks[StackID].Add(potentialCrate[1]);
                        }
                        StackID++;
                        if (lineCopy.Length <= 4)
                            break;
                        //cut off the empty space or crate we just checked and the space after it.
                        lineCopy = lineCopy.Substring(4);
                    }

                    break;
                case ReadState.ReadingNumbers:
                    //line should be white space
                    currentReadState = ReadState.ReadingMoves;
                    continue;
                case ReadState.ReadingMoves:
                    string[] splitWords = new[] {"move", "from", "to"};
                    string[] moveNumbers = line.Split(splitWords,
                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    int crateCount = int.Parse(moveNumbers[0]);
                    int fromStackID = int.Parse(moveNumbers[1]) - 1;
                    int toStackID = int.Parse(moveNumbers[2]) - 1;

                    List<char> cratesToMove = crateStacks[fromStackID].GetRange(0, crateCount);
                    crateStacks[fromStackID].RemoveRange(0, crateCount);

                    if (orderWhenMoving == OrderWhenMoving.Reverse)
                    {
                        foreach (char crate in cratesToMove)
                        {
                            crateStacks[toStackID].Insert(0, crate);
                        }
                    }
                    else if(orderWhenMoving == OrderWhenMoving.Same)
                    {
                        crateStacks[toStackID].InsertRange(0, cratesToMove);
                    }
                    break;
            }
        }

        string result = "";
        foreach (List<char> stack in crateStacks)
        {
            result += stack[0];
        }

        return result;
    }
}