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
        public bool visible = false;

        public int scenicScore = 0;
    }

    private static List<List<Tree>> trees;
    private static int height;
    private static int width;
    
    static void CheckLineVisibleFromOutside(int x, int y, int dx, int dy)
    {
        int CurrentHighest = trees[y][x].treeHeight;
        trees[y][x].visible = true;
        while(true)
        {
            x += dx;
            y += dy;
            if (x < 0 || y < 0 || x >= width || y >= height || CurrentHighest >= 9)
                break;
            if (CurrentHighest < trees[y][x].treeHeight)
            {
                trees[y][x].visible = true;
                CurrentHighest = trees[y][x].treeHeight;
            }
        }
    }
    
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
        height = trees[0].Count;
        width = trees.Count;
        for (int y = 0; y < height; y++)
        {
            CheckLineVisibleFromOutside(0, y, +1, 0);
            CheckLineVisibleFromOutside(width-1, y, -1, 0);
        }
        
        for (int x = 0; x < width; x++)
        {
            CheckLineVisibleFromOutside(x, 0, 0, +1);
            CheckLineVisibleFromOutside(x, height-1, 0, -1);
        }

        int count = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (trees[y][x].visible)
                    count++;
            }
        }

        OutputVisibleMap();
        
        return count;
    }

    static int VisibleTotalInDirection(int x, int y, int dx, int dy)
    {
        int currentTreeTotal = 0;
        int originalTreeHeight = trees[y][x].treeHeight;
        while(true)
        {
            x += dx;
            y += dy;
            if (x < 0 || y < 0 || x >= width || y >= height)
                break;
            currentTreeTotal++;
            if (originalTreeHeight <= trees[y][x].treeHeight)
                break;
        }
        return currentTreeTotal;
    }
    
    public static int GetHighestScenicScore()
    {
        int highestScore = -1;
        for (int y = 1; y < height - 1; y++)
        {
            for (int x = 1; x < width - 1; x++)
            {
                int score = 1;
                
                score *= VisibleTotalInDirection(x, y, -1, 0);
                score *= VisibleTotalInDirection(x, y, +1, 0);
                score *= VisibleTotalInDirection(x, y, 0, -1);
                score *= VisibleTotalInDirection(x, y, 0, +1);
                
                if (score > highestScore)
                    highestScore = score;
                trees[y][x].scenicScore = score;
            }
        }

        OutputScenicMap(highestScore);
        
        return highestScore;
    }


    static void OutputVisibleMap()
    {
        for(int y = 0 ; y < height; y++)
        {
            string line = "";
            for(int x = 0 ; x < width; x++)
            {
                if (trees[y][x].visible)
                {
                    line += trees[y][x].treeHeight;
                }
                else
                    line += " ";
            }
            Console.WriteLine(line);
        }
    }
    static void OutputScenicMap(int highestScore)
    {
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
    }
}