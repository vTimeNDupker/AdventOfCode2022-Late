namespace AOCDay7;

public class DirectoryHandler
{
    class AOCFile
    {
        public AOCFile(string name, int size)
        {
            this.name = name;
            this.size = size;
        }
        public string name;
        public int size;
    }
    class AOCDirectory
    {
        public AOCDirectory(string name, AOCDirectory parent)
        {
            this.name = name;
            this.parent = parent;
            directories = new Dictionary<string, AOCDirectory>();
            files = new Dictionary<string, AOCFile>();
        }
        public AOCDirectory AddNewChildDirectory(string name)
        {
            AOCDirectory newDir = new AOCDirectory(name, this);
            directories.Add(name, newDir);
            return newDir;
        }

        public int TotalSize()
        {
            int total = 0;
            foreach (var file in files)
                total += file.Value.size;
            foreach (var dir in directories)
                total += dir.Value.TotalSize();
            return total;
        }
        
        public int GetTotalBelowSize(int maxSize)
        {
            int size = 0;
            int dirSize = TotalSize();
            if (dirSize <= maxSize)
                size += dirSize;
            foreach (var dir in directories)
                size += dir.Value.GetTotalBelowSize(maxSize);
            return size;
        }
        public int GetSmallestAboveSize(int targetSize)
        {
            int size = Int32.MaxValue;
            int dirSize = TotalSize();
            if (dirSize >= targetSize)
                size = dirSize;
            foreach (var dir in directories)
            {
                int childDirSize = dir.Value.GetSmallestAboveSize(targetSize);
                if (childDirSize < size)
                    size = childDirSize;
            }

            return size;
        }
        
        public string name;
        public AOCDirectory parent;
        public Dictionary<string, AOCDirectory> directories;
        public Dictionary<string, AOCFile> files;
    }
    static AOCDirectory GetAllDirectoriesFromFile(string fileName)
    {
        AOCDirectory originalDirectory = null;
        AOCDirectory currentDirectory = null;
        var lines = File.ReadLines(fileName);
        foreach (var line in lines)
        {
            string[] commandParts = line.Split(' ');
            if (commandParts[0] == "$")
            {
                switch (commandParts[1])
                {
                    case "cd":
                        //initial "/" directory
                        if (currentDirectory == null)
                        {
                            currentDirectory = new AOCDirectory(commandParts[2], null);
                            originalDirectory = currentDirectory;
                        }
                        //move up one
                        else if (commandParts[2] == ".." && currentDirectory.parent != null)
                            currentDirectory = currentDirectory.parent;
                        //go into existing child folder
                        else if (currentDirectory.directories.ContainsKey(commandParts[2]))
                            currentDirectory = currentDirectory.directories[commandParts[2]];
                        //create and go into child folder
                        else 
                            currentDirectory = currentDirectory.AddNewChildDirectory(commandParts[2]);
                        break;
                    case "ls":
                        //list mode
                        break;
                }
            }
            //ls
            else
            {
                if (commandParts[0] == "dir")
                {
                    currentDirectory.AddNewChildDirectory(commandParts[1]);
                }
                //file
                else
                {
                    int size = int.Parse(commandParts[0]);
                    string name = commandParts[1];
                    currentDirectory.files.Add(name, new AOCFile(name, size));
                }
            }
        }

        return originalDirectory;
    }
    
    public static int RunCommandsAndOutputSizesP1(string fileName)
    {
        AOCDirectory originalDirectory = GetAllDirectoriesFromFile(fileName);

        int totalBelowSize = originalDirectory.GetTotalBelowSize(100000);
        return totalBelowSize;
    }
    public static int RunCommandsAndDecideDeletionP2(string fileName, int totalSpace, int requiredSpace)
    {
        AOCDirectory originalDirectory = GetAllDirectoriesFromFile(fileName);
        
        int total = originalDirectory.TotalSize();
        int freeSpace = totalSpace - total;
        int deletionSizeRequired = requiredSpace - freeSpace;
        
        return originalDirectory.GetSmallestAboveSize(deletionSizeRequired);
    }
}