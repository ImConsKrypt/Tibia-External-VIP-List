
using System.Net;
using TibiaExternalVIP;
using TibiaData;

while (true)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Gray;
    int uTime = 5000;
    TibiConfig tConf;
    TData tData;
    Console.WriteLine("Tibia External VIP List By ConsKrypt");
    try
    {
        tConf = TibiConfig.FromJson(File.ReadAllText("./Data/Config.json"));
        uTime = (int)tConf.Options.UpdateTime;
        Console.Title = "Tibia External VIP List: Monitoring " + tConf.Worlds.Count() + " Servers.";
        for (int iW = 0; iW < tConf.Worlds.Count(); iW++)
        {
            string curWorldJson = new WebClient().DownloadString("https://api.tibiadata.com/v3/world/" + tConf.Worlds[iW]);
            tData = TData.FromJson(curWorldJson);
            int count = 0;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------");
            Console.Write("There are currently ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(tData.Worlds.World.PlayersOnline);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Players Online in ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(tData.Worlds.World.Name);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" [ ");

            switch (tData.Worlds.World.PvpType.ToLower())
            {
                case "optional pvp":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Optional PVP");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("PVP Enabled");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" ] ");
            Console.WriteLine();
            for (int i = 0; i < tData.Worlds.World.PlayersOnline; i++)
            {
                for (int i2 = 0; i2 < tConf.Players.Enemies.Count(); i2++)
                {
                    if (tData.Worlds.World.OnlinePlayers[i].Name.ToLower() == tConf.Players.Enemies[i2].ToLower())
                    {
                        count++;
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
                        Console.WriteLine();
                    }
                }
                for (int i3 = 0; i3 < tConf.Players.Friends.Count(); i3++)
                {
                    if (tData.Worlds.World.OnlinePlayers[i].Name.ToLower() == tConf.Players.Friends[i3].ToLower())
                    {
                        count++;
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
                        Console.WriteLine();
                    }
                }
            }
            if (count == 0)
            {
                count++;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("No Tracked Players Online In " + tData.Worlds.World.Name +"!"); ;
                Console.WriteLine();
            }
        }
    } catch
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ERROR PARSING JSON.... TRYING AGAIN IN " + uTime + " SECONDS!");
    }
    Thread.Sleep(uTime*1000);
}
