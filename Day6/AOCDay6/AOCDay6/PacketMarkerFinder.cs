namespace AOCDay6;

public class PacketMarkerFinder
{
    public static int FindPacketMarker(string fileName, int markerLength)
    {
        var lines = File.ReadLines(fileName);

        List<char> letters = new List<char>();
        int letterID = 1;
        foreach (var line in lines)
        {
            foreach (var letter in line)
            {
                letters.Add(letter);
                if (letters.Count > markerLength)
                {
                    letters.RemoveAt(0);
                }
                if (letters.Count == markerLength)
                {
                    bool hasDuplicates = false;
                    for (int i = 0; i < letters.Count; i++)
                    {
                        for (int j = i+1; j < letters.Count; j++)
                        {
                            if (letters[i] == letters[j])
                            {
                                hasDuplicates = true;
                                break;
                            }
                        }
                    }
                    if (!hasDuplicates)
                    {
                        return letterID;
                    }
                }
                letterID++;
            }
        }
        
        return -1;
    }
}