namespace AOCDay4;

public class AssignmentComparer
{
    public static int ReadFileAndCompareAssignmentsP1(string fileName)
    {
        var lines = File.ReadLines(fileName);
        int totalFullyContainedIDs = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            var assignments = line.Split(new[]{',', '-'}, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int assignmentLeftLower = int.Parse(assignments[0]);
            int assignmentLeftUpper = int.Parse(assignments[1]);
            int assignmentRightLower = int.Parse(assignments[2]);
            int assignmentRightUpper = int.Parse(assignments[3]);

            if ((assignmentLeftLower >= assignmentRightLower && assignmentLeftUpper <= assignmentRightUpper) ||
                (assignmentRightLower >= assignmentLeftLower && assignmentRightUpper <= assignmentLeftUpper))
                totalFullyContainedIDs++;
        }

        return totalFullyContainedIDs;
    }
    public static int ReadFileAndCompareAssignmentsP2(string fileName)
    {
        var lines = File.ReadLines(fileName);
        int totalFullyContainedIDs = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            var assignments = line.Split(new[]{',', '-'}, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int assignmentLeftLower = int.Parse(assignments[0]);
            int assignmentLeftUpper = int.Parse(assignments[1]);
            int assignmentRightLower = int.Parse(assignments[2]);
            int assignmentRightUpper = int.Parse(assignments[3]);

            if ((assignmentLeftUpper >= assignmentRightLower && assignmentLeftLower <= assignmentRightUpper) ||
                (assignmentRightUpper >= assignmentLeftLower && assignmentRightLower <= assignmentLeftUpper))
                totalFullyContainedIDs++;
        }

        return totalFullyContainedIDs;
    }
}