using AOCDay7;

int result1 = DirectoryHandler.RunCommandsAndOutputSizesP1("Input.txt");
int result2 = DirectoryHandler.RunCommandsAndDecideDeletionP2("Input.txt", 70000000, 30000000);
Console.WriteLine("Result P1: " + result1);
Console.WriteLine("Result P2: " + result2);
Console.ReadKey();