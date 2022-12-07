
using System.Net;
using TibiaData;
using NameData;

while (true)
{
    Console.Clear();
    List<string> names;
    Console.ForegroundColor = ConsoleColor.Gray;
    //Currently statically locked to Solidera - Will add some configuration at a later date if requested otherwise just edit the line below to use the Server of Choise
    string webURL = "https://api.tibiadata.com/v3/world/Solidera";
    string nameList = "./Data/Names.json";

    string jsonStrings = new WebClient().DownloadString(webURL).ToString();
    string nameString = File.ReadAllText(nameList);
    TData tData;
    NData nData;
    try
    {
        nameString = File.ReadAllText(nameList);
        tData = TData.FromJson(jsonStrings);
        nData = NData.FromJson(nameString);
        int count = 0;
        Console.WriteLine("Updating For World " + tData.Worlds.World.Name + "... " + DateTime.Now);

        Console.Write("There Are Currently ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(tData.Worlds.World.PlayersOnline);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(" Players Online!");
        Console.WriteLine();

        for (int i = 0; i < tData.Worlds.World.PlayersOnline; i++)
        {
            for (int i2 = 0; i2 < nData.Enemies.Count(); i2++)
            {
                if (tData.Worlds.World.OnlinePlayers[i].Name.ToLower() == nData.Enemies[i2].ToLower())
                {
                    count++;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(tData.Worlds.World.OnlinePlayers[i].Name + " ");
                    Console.ForegroundColor = Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[ ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(tData.Worlds.World.OnlinePlayers[i].Vocation);
                    Console.ForegroundColor = Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" | ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(tData.Worlds.World.OnlinePlayers[i].Level);
                    Console.ForegroundColor = Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ]");
                }
            }
            for (int i3 = 0; i3 < nData.Friends.Count(); i3++)
            {
                if (tData.Worlds.World.OnlinePlayers[i].Name.ToLower() == nData.Friends[i3].ToLower())
                {
                    count++;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(tData.Worlds.World.OnlinePlayers[i].Name + " ");
                    Console.ForegroundColor = Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[ ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(tData.Worlds.World.OnlinePlayers[i].Vocation);
                    Console.ForegroundColor = Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" | ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(tData.Worlds.World.OnlinePlayers[i].Level);
                    Console.ForegroundColor = Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ]");
                }
            }
        }
        if (count == 0)
        {
            count++;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("No Tracked Players Online!");
        }
    } catch
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERROR PARSING JSON.... TRYING AGAIN IN 5 SECONDS!");
    }
    Thread.Sleep(5000);
}
