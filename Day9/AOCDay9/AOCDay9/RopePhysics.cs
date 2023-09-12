using System.Numerics;

namespace AOCDay9;

public class RopePhysics
{
    public static int AnimateRope(string fileName, int length)
    {
        Vector2 Max = Vector2.Zero;
        Vector2 Min = Vector2.Zero;
        
        Vector2[] RopePositions = new Vector2[length];
        for(int i = 0 ;i < RopePositions.Length; i++)
            RopePositions[i] = Vector2.Zero;
        var lines = File.ReadLines(fileName);
        List<Vector2> tailPositions = new List<Vector2>();
        tailPositions.Add(Vector2.Zero);
        List<Vector2> headPositions = new List<Vector2>();
        headPositions.Add(Vector2.Zero);
        foreach (var line in lines)
        {
            string[] command = line.Split(' ');
            Vector2 distance = Vector2.Zero;
            switch (@command[0])
            {
                case "R":
                    distance.X = +1;
                    break;
                case "U":
                    distance.Y = +1;
                    break;
                case "L":
                    distance.X = -1;
                    break;
                case "D":
                    distance.Y = -1;
                    break;
            }
            int moves = int.Parse(command[1]);
            for (int i = 0; i < moves; i++)
            {
                RopePositions[0] += distance;
                Move(RopePositions, 1);
                
                if(!tailPositions.Contains(RopePositions[^1]))
                    tailPositions.Add(RopePositions[^1]);

                if (RopePositions[0].X > Max.X)
                    Max.X = RopePositions[0].X;
                if (RopePositions[0].Y > Max.Y)
                    Max.Y = RopePositions[0].Y;
                if (RopePositions[0].X < Min.X)
                    Min.X = RopePositions[0].X;
                if (RopePositions[0].Y < Min.Y)
                    Min.Y = RopePositions[0].Y;
                if(!headPositions.Contains(RopePositions[0]))
                    headPositions.Add(RopePositions[0]);
                //OutputMap(Min, Max, RopePositions);
            }
        }

        OutputHeadTailTotalPositions(Min, Max, tailPositions, headPositions);
        
        return tailPositions.Count;
    }

    static List<Vector2> GetPossibleMovePositions(Vector2 parentPos, Vector2 pos)
    {
        if ((int) pos.X == (int) parentPos.X || (int) pos.Y == (int) parentPos.Y)
        {
            List<Vector2> positions = new List<Vector2>();
            positions.Add(parentPos + new Vector2(1, 0));
            positions.Add(parentPos + new Vector2(-1, 0));
            positions.Add(parentPos + new Vector2(0, 1));
            positions.Add(parentPos + new Vector2(0, -1));
            return positions;
        }
        else
        {
            List<Vector2> GoalPositions = new List<Vector2>();
            GoalPositions.Add(parentPos + new Vector2(1, 0));
            GoalPositions.Add(parentPos + new Vector2(-1, 0));
            GoalPositions.Add(parentPos + new Vector2(0, 1));
            GoalPositions.Add(parentPos + new Vector2(0, -1));
            GoalPositions.Add(parentPos + new Vector2(1, 1));
            GoalPositions.Add(parentPos + new Vector2(-1, -1));
            GoalPositions.Add(parentPos + new Vector2(1, -1));
            GoalPositions.Add(parentPos + new Vector2(-1, 1));
            
            
            List<Vector2> DiagonalMoves = new List<Vector2>();
            DiagonalMoves.Add(pos + new Vector2(1, 1));
            DiagonalMoves.Add(pos + new Vector2(-1, -1));
            DiagonalMoves.Add(pos + new Vector2(1, -1));
            DiagonalMoves.Add(pos + new Vector2(-1, 1));

            
            List<Vector2> positions = new List<Vector2>();
            for (int i = 0; i < GoalPositions.Count; i++)
            {
                if(DiagonalMoves.Contains(GoalPositions[i]))
                    positions.Add(GoalPositions[i]);
            }
            return positions;
        }
    }

    static void Move(Vector2[] RopePositions, int current)
    {
        Vector2 parentPos = RopePositions[current-1];
        Vector2 pos = RopePositions[current];
        
        if (Vector2.Distance(parentPos, pos) >= 2f)
        {
            List<Vector2> positions = GetPossibleMovePositions(parentPos, pos);

            Vector2 closest = Vector2.Zero;
            float lowestDistance = int.MaxValue;
            for (int i = 0; i < positions.Count; i++)
            {
                if (Vector2.Distance(pos, positions[i]) < lowestDistance)
                {
                    lowestDistance = Vector2.Distance(pos, positions[i]);
                    closest = positions[i];
                }
            }
            RopePositions[current] = closest;
        }
        if (current < RopePositions.Length - 1)
            Move(RopePositions, current + 1);
    }

    static void OutputMap(Vector2 Min, Vector2 Max, Vector2[] RopePositions)
    {

        Min.X -= 3;
        Min.Y -= 3;
        Max.X += 3;
        Max.Y += 3;
        
        int width = (int)MathF.Abs(Max.X - Min.X);
        int height = (int)MathF.Abs(Max.Y - Min.Y);
        
        for (int y = height-1; y >= 0; y--)
        {
            string outline = "";
            for (int x = 0; x < width; x++)
            {
                int adjustedX = (int)(Min.X + x);
                int adjustedY = (int)(Min.Y + y);
                Vector2 adjustedV2 = new Vector2(adjustedX, adjustedY);

                bool done = false;
                if (RopePositions[0] == adjustedV2)
                {
                    outline += "H";
                    done = true;
                }
                for (int i = 1; i < RopePositions.Length && !done; i++)
                {
                    if (RopePositions[i] == adjustedV2)
                    {
                        outline += "" + i;
                        done = true;
                    }
                }
                if(!done)
                    outline += ".";
            }
            Console.WriteLine(outline);
        }
        Console.WriteLine("");
        Console.WriteLine("-------------");
        Console.WriteLine("");

    }
    static void OutputHeadTailTotalPositions(Vector2 Min, Vector2 Max, List<Vector2> tailPositions, List<Vector2> headPositions)
    {

        Min.X -= 3;
        Min.Y -= 3;
        Max.X += 3;
        Max.Y += 3;
        
        int width = (int)MathF.Abs(Max.X - Min.X);
        int height = (int)MathF.Abs(Max.Y - Min.Y);
        
        Console.WriteLine("");
        Console.WriteLine("-------------");
        Console.WriteLine("");
        for (int y = height-1; y >= 0; y--)
        {
            string outline = "";
            for (int x = 0; x < width; x++)
            {
                int adjustedX = (int)(Min.X + x);
                int adjustedY = (int)(Min.Y + y);
                
                if (tailPositions.Contains(new Vector2(adjustedX, adjustedY)))
                    outline += "T";
                //else if (headPositions.Contains(new Vector2(adjustedX, adjustedY)))
                //    outline += "H";
                else
                    outline += ".";
            }
            Console.WriteLine(outline);
        }
        Console.WriteLine("");
        Console.WriteLine("-------------");
        Console.WriteLine("");

    }
}