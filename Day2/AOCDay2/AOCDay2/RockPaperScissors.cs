using System.Diagnostics;

namespace AOCDay2;

public enum RPC
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public static class RockPaperScissors
{
    public static int ReadFileAndGetScoreP1(string fileName)
    {
        var lines = File.ReadLines(fileName);
        List<int> elves = new List<int>();
        elves.Add(0);
        int totalScore = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            string[] letters = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            RPC opponentChoice = RPC.Rock;
            switch(letters[0])
            {
                case "A"://rock
                    opponentChoice = RPC.Rock;
                    break;
                case "B"://paper
                    opponentChoice = RPC.Paper;
                    break;
                case "C"://scissors
                    opponentChoice = RPC.Scissors;
                    break;
            }
            RPC yourChoice = RPC.Rock;
            switch(letters[1])
            {
                case "X"://rock
                    yourChoice = RPC.Rock;
                    break;
                case "Y"://paper
                    yourChoice = RPC.Paper;
                    break;
                case "Z"://scissors
                    yourChoice = RPC.Scissors;
                    break;
            }

            totalScore += CheckScore(yourChoice, opponentChoice);
        }

        return totalScore;
    }
    
    public static int ReadFileAndGetScoreP2(string fileName)
    {
        var lines = File.ReadLines(fileName);
        List<int> elves = new List<int>();
        elves.Add(0);
        int totalScore = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            string[] letters = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            RPC opponentChoice = RPC.Rock;
            switch(letters[0])
            {
                case "A"://rock
                    opponentChoice = RPC.Rock;
                    break;
                case "B"://paper
                    opponentChoice = RPC.Paper;
                    break;
                case "C"://scissors
                    opponentChoice = RPC.Scissors;
                    break;
            }
            RPC yourChoice = RPC.Rock;
            switch(letters[1])
            {
                case "X": //lose
                    switch(opponentChoice)
                    {
                        case RPC.Rock:
                            yourChoice = RPC.Scissors;
                            break;
                        case RPC.Paper:
                            yourChoice = RPC.Rock;
                            break;
                        case RPC.Scissors:
                            yourChoice = RPC.Paper;
                            break;
                    }
                    break;
                case "Y"://draw
                    yourChoice = opponentChoice;
                    break;
                case "Z"://win
                    switch(opponentChoice)
                    {
                        case RPC.Rock:
                            yourChoice = RPC.Paper;
                            break;
                        case RPC.Paper:
                            yourChoice = RPC.Scissors;
                            break;
                        case RPC.Scissors:
                            yourChoice = RPC.Rock;
                            break;
                    }
                    break;
            }

            totalScore += CheckScore(yourChoice, opponentChoice);
        }

        return totalScore;
    }

    public static int CheckScore(RPC yourChoice, RPC opponentChoice)
    {
        if (yourChoice == opponentChoice)
            return 3 + (int)yourChoice;
        if (yourChoice == RPC.Rock && opponentChoice == RPC.Scissors ||
            yourChoice == RPC.Paper && opponentChoice == RPC.Rock ||
            yourChoice == RPC.Scissors && opponentChoice == RPC.Paper)
            return 6 + (int)yourChoice;
        return 0 + (int)yourChoice;

    }
}