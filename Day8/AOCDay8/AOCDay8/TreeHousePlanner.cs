namespace AOCDay8;

public class TreeHousePlanner
{
    public class Tree
    {
        public Tree(int treeHeight)
        {
            this.treeHeight = treeHeight;
        }
        public int treeHeight;
        public bool visibleFromTop = false;
        public bool visibleFromBottom = false;
        public bool visibleFromLeft = false;
        public bool visibleFromRight = false;

        public int scenicScore = 0;

        public bool visibleFromAny => visibleFromTop || visibleFromBottom || visibleFromLeft || visibleFromRight;
    }

    private static List<List<Tree>> trees;
    public static int GetVisibleTreesFromFile(string fileName)
    {
        trees = new List<List<Tree>>();
        var lines = File.ReadLines(fileName);
        foreach (var line in lines)
        {
            trees.Add(new List<Tree>());
            foreach (var treeHeight in line)
            {
                Tree tree = new Tree(int.Parse("" + treeHeight));
                trees[^1].Add(tree);
            }
        }
        int height = trees[0].Count;
        int width = trees.Count;
        for (int y = 0; y < height; y++)
        {
            trees[y][0].visibleFromLeft = true;
            trees[y][width-1].visibleFromRight = true;
        }
        for (int x = 0; x < width; x++)
        {
            trees[0][x].visibleFromTop = true;
            trees[height-1][x].visibleFromBottom = true;
        }
        for (int y = 1; y < height; y++)
        {
            int CurrentHighest = trees[y][0].treeHeight;
            for (int x = 1; x < width && CurrentHighest < 9; x++)
            {
                if (trees[y][x].treeHeight > CurrentHighest)
                {
                    CurrentHighest = trees[y][x].treeHeight;
                    trees[y][x].visibleFromLeft = true;
                }
            }
        }
        
        for (int y = 1; y < height; y++)
        {
            int CurrentHighest = trees[y][width - 1].treeHeight;
            for (int x = width - 2; x >= 0 && CurrentHighest < 9; x--)
            {
                if (trees[y][x].treeHeight > CurrentHighest)
                {
                    CurrentHighest = trees[y][x].treeHeight;
                    trees[y][x].visibleFromRight = true;
                }
            }
        }
        
        for (int x = 1; x < width; x++)
        {
            int CurrentHighest = trees[0][x].treeHeight;
            for (int y = 1; y < height && CurrentHighest < 9; y++)
            {
                if (trees[y][x].treeHeight > CurrentHighest)
                {
                    CurrentHighest = trees[y][x].treeHeight;
                    trees[y][x].visibleFromTop = true;
                }
            }
        }
        
        for (int x = 1; x < width; x++)
        {
            int CurrentHighest = trees[height - 1][x].treeHeight;
            for (int y = height - 2; y >= 0 && CurrentHighest < 9; y--)
            {
                if (trees[y][x].treeHeight > CurrentHighest)
                {
                    CurrentHighest = trees[y][x].treeHeight;
                    trees[y][x].visibleFromBottom = true;
                }
            }
        }

        int count = 0;
        
        for(int y = 0 ; y < height; y++)
        {
            string line = "";
            for(int x = 0 ; x < width; x++)
            {
                if (trees[y][x].visibleFromAny)
                {
                    count++;
                    line += trees[y][x].treeHeight;
                }
                else
                    line += " ";
            }
            Console.WriteLine(line);
        }

        return count;
    }

    public static int GetHighestScenicScore()
    {
        int height = trees[0].Count;
        int width = trees.Count;
        int highestScore = -1;
        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                int score = 1;
                //go left
                int currentTreeTotal = 0;
                for (int treeLeftX = x-1; treeLeftX >= 0; treeLeftX--)
                {
                    currentTreeTotal++;
                    if (trees[y][x].treeHeight <= trees[y][treeLeftX].treeHeight)
                        break;
                }
                score *= currentTreeTotal;
                
                //go right
                currentTreeTotal = 0;
                for (int treeRightX = x+1; treeRightX < width; treeRightX++)
                {
                    currentTreeTotal++;
                    if (trees[y][x].treeHeight <= trees[y][treeRightX].treeHeight)
                        break;
                }
                score *= currentTreeTotal;
                
                //go up
                currentTreeTotal = 0;
                for (int treeUpY = y-1; treeUpY >= 0; treeUpY--)
                {
                    currentTreeTotal++;
                    if (trees[y][x].treeHeight <= trees[treeUpY][x].treeHeight)
                        break;
                }
                score *= currentTreeTotal;
                
                //go down
                currentTreeTotal = 0;
                for (int treeDownY = y+1; treeDownY < height; treeDownY++)
                {
                    currentTreeTotal++;
                    if (trees[y][x].treeHeight <= trees[treeDownY][x].treeHeight)
                        break;
                }
                score *= currentTreeTotal;
                if (score > highestScore)
                    highestScore = score;
                trees[y][x].scenicScore = score;
            }
        }
        /*for(int y = 0 ; y < height; y++)
        {
            string line = "";
            for(int x = 0 ; x < width; x++)
            {
                if (trees[y][x].scenicScore == highestScore)
                    line += "&";
                else if (trees[y][x].scenicScore > 9)
                    line += "9";
                else if(trees[y][x].scenicScore == 1)
                    line += " ";
                else
                    line += trees[y][x].scenicScore;
            }
            Console.WriteLine(line);
        }*/
        
        for(int y = 0 ; y < height; y++)
        {
            string line = "";
            bool THISONE = false;
            for(int x = 0 ; x < width; x++)
            {
                if (trees[y][x].scenicScore == highestScore)
                {
                    line += "&";
                    THISONE = true;
                }
                else
                    line += trees[y][x].treeHeight;
            }

            if (THISONE)
                line += "<<<<<<<<<<<<<<<<<<<";
            Console.WriteLine(line);
        }
        return highestScore;
    }
}