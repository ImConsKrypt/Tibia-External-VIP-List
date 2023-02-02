
using Newtonsoft.Json;
using TEVL.CharacterData;
using TEVL.WorldData;
using TEVL.TibiaConfig;
using TEVL.Updater;

// Initially set update time as 30 (Seconds)
int uTime = 30;

string config = "./Data/Config.json";
ConfData conf;
WData wData;
CData cData;
UpdateData updateData;
string curVersion = "v0.1.2.0-ALPHA";

while (true)
{
    DoInit();

    

    /*
     * Since TibiaDataAPI does not update in "Real Time"
     * it is fairly useless to update faster than approx 30 seconds.
     * 
     * Any faster then this is just sending un-needed traffic to their servers
     * and may result in them blocking your I.P. due to excessive repeated requests.
     * 
     * Formula: Base time * 1000 = Base time in seconds
     * 
     */
    Thread.Sleep(uTime * 1000);
}

void DoInit()
{
    if (!File.Exists(config))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Configuration File Not Found!");
        Console.WriteLine("Attempting to create defaults...");
        Console.WriteLine();
        Console.WriteLine("Please Wait...");
        Thread.Sleep(1500);
        if (!File.Exists(config + ".dist"))
        {
            Console.WriteLine("Defaults Failed....");
            Console.WriteLine("Force Creating Configuration...");
            Thread.Sleep(10000);
            if (!Directory.Exists("./Data"))
            {
                Directory.CreateDirectory("./Data");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Data Directory Has Been Created...");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Attempting to create default Configuration File....");
                Thread.Sleep(500);
                using (StreamWriter sw = new StreamWriter(config))
                {
                    sw.WriteLine("{");
                    sw.WriteLine("  \"Options\": {");
                    sw.WriteLine("      \"ShowLevel\": true,");
                    sw.WriteLine("      \"ShowVocation\": true,");
                    sw.WriteLine("      \"ShowGuild\": true,");
                    sw.WriteLine("      \"UpdateTime\": 30");
                    sw.WriteLine("  },");
                    sw.WriteLine("  \"Players\": {");
                    sw.WriteLine("      \"Friends\": [],");
                    sw.WriteLine("      \"Foes\": [],");
                    sw.WriteLine("      \"Guild\": [],");
                    sw.WriteLine("  },");
                    sw.WriteLine("  \"Worlds\": []");
                    sw.WriteLine("}");
                    sw.Close();
                };
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Default Configuration File Has Been Created!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Please check the manual on how to configure the VIP List....");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You can leave this running and edit the Config File to your specifications,");
                Console.WriteLine("the VIP list dynamically updates upon each refresh cycle and will reflect");
                Console.WriteLine("any changes made to the configuration file so long as its a valid JSON format.");
                Thread.Sleep(20000);

            }
            else
            {
                File.Copy(config + ".dist", config);
            }
        }
        else
        {
            if (!Directory.Exists("./Data"))
            {
                Directory.CreateDirectory("./Data");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Data Directory Has Been Created...");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Attempting to create default Configuration File....");
                Thread.Sleep(500);
                using (StreamWriter sw = new StreamWriter(config))
                {
                    sw.WriteLine("{");
                    sw.WriteLine("  \"Options\": {");
                    sw.WriteLine("      \"ShowLevel\": true,");
                    sw.WriteLine("      \"ShowVocation\": true,");
                    sw.WriteLine("      \"ShowGuild\": true,");
                    sw.WriteLine("      \"UpdateTime\": 30");
                    sw.WriteLine("  },");
                    sw.WriteLine("  \"Players\": {");
                    sw.WriteLine("      \"Friends\": [],");
                    sw.WriteLine("      \"Foes\": [],");
                    sw.WriteLine("      \"Guild\": [],");
                    sw.WriteLine("  },");
                    sw.WriteLine("  \"Worlds\": []");
                    sw.WriteLine("}");
                    sw.Close();
                };
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Default Configuration File Has Been Created!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Please check the manual on how to configure the VIP List....");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You can leave this running and edit the Config File to your specifications,");
                Console.WriteLine("the VIP list dynamically updates upon each refresh cycle and will reflect");
                Console.WriteLine("any changes made to the configuration file so long as its a valid JSON format.");
                Thread.Sleep(20000);

            }
            else
            {
                File.Copy(config + ".dist", config);
            }
        }
    }

    conf = ConfData.FromJson(File.ReadAllText(config));

    if (conf != null && conf.Options.UpdateTime.HasValue)
        uTime = (int)conf.Options.UpdateTime;
    else
        uTime = 30;


    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Tibia External VIP List");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write(" - ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("By ConsKrypt");
    Console.WriteLine();
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write("Last Update");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(": ");
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.Write(DateTime.Now.ToString());
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write(" - Next Update in " + uTime + " seconds!");
    Console.ForegroundColor= ConsoleColor.White;
    Console.WriteLine();
    Console.Write("KEY: ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Friend");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("|");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Guild");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("|");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("Enemy");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
    Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=[ TEVL " + curVersion + " ]=-");
    try {
        for (int w = 0; w < conf.Worlds.Count(); w++)
        {
            string url = "https://api.tibiadata.com/v3/world/" + conf.Worlds[w];
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            Task<string> wJson = client.GetStringAsync(url);
            wJson.Wait(300);
            wData = WData.FromJson(wJson.Result);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("World");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(wData.Worlds.World.Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("    | Players Online: ");
            int onlineP = (int)wData.Worlds.World.PlayersOnline;
            if (onlineP >= 0 && onlineP <= 25)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (onlineP >= 26 && onlineP <= 50)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (onlineP >= 51 && onlineP <= 100)
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            else
                Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(onlineP);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [ Record: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(wData.Worlds.World.RecordPlayers);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            /*Console.Write("Player Name");
            if (conf.Options.ShowLevel.HasValue && conf.Options.ShowLevel.Value == true)
                Console.Write(" \t[Level]");
            if (conf.Options.ShowVocation.HasValue && conf.Options.ShowVocation.Value == true)
                Console.Write("\t[Vocation]");
            if (conf.Options.ShowGuild.HasValue && conf.Options.ShowGuild.Value == true)
                Console.Write("\t \t[Guild]");*/
            string lvl = "";
            string gld = "";
            string voc = "";

            //Console.Write(playerName);
            if (conf.Options.ShowLevel.HasValue && conf.Options.ShowLevel.Value == true)
                lvl = "[Level]";
            if (conf.Options.ShowVocation.HasValue && conf.Options.ShowVocation.Value == true)
                voc = "[Vocation]";
            if (conf.Options.ShowGuild.HasValue && conf.Options.ShowGuild.Value == true)
                gld = "[Guild]";
            Console.WriteLine("{0,10}\t\t{1,5}\t{2,10}\t{3,15}", "[Name]", lvl, voc, gld);
            Console.WriteLine();
            for (int p1 = 0; p1 < conf.Players.Guild.Count(); p1++)
            {

                for (int p2 = 0; p2 < wData.Worlds.World.OnlinePlayers.Count(); p2++)
                {
                    if (wData.Worlds.World.OnlinePlayers[p2].Name.ToLower() == conf.Players.Guild[p1].ToLower())
                    {
                        string urlp = "https://api.tibiadata.com/v3/character/" + Uri.EscapeUriString(wData.Worlds.World.OnlinePlayers[p2].Name);
                        HttpClient client2 = new HttpClient();
                        client2.BaseAddress = new Uri(urlp);
                        Task<string> pJson = client.GetStringAsync(urlp);
                        pJson.Wait(300);
                        cData = CData.FromJson(pJson.Result);
                        string tguild = "";
                        if (cData.Characters.Character.Guild.Name != null)
                            tguild = cData.Characters.Character.Guild.Name + " (" + cData.Characters.Character.Guild.Rank + ")";
                        WriteDetails(wData.Worlds.World.OnlinePlayers[p2].Name, wData.Worlds.World.Name, wData.Worlds.World.OnlinePlayers[p2].Vocation.ToString(), wData.Worlds.World.OnlinePlayers[p2].Level.ToString(), "guild", tguild.ToString());
                    }
                }
            }
            for (int p1 = 0; p1 < conf.Players.Friends.Count(); p1++)
            {
                for (int p2 = 0; p2 < wData.Worlds.World.OnlinePlayers.Count(); p2++)
                {
                    if (wData.Worlds.World.OnlinePlayers[p2].Name.ToLower() == conf.Players.Friends[p1].ToLower())
                    {
                        string urlp2 = "https://api.tibiadata.com/v3/character/" + Uri.EscapeUriString(wData.Worlds.World.OnlinePlayers[p2].Name);
                        HttpClient client3 = new HttpClient();
                        client3.BaseAddress = new Uri(urlp2);
                        Task<string> pJson = client.GetStringAsync(urlp2);
                        pJson.Wait(300);
                        cData = CData.FromJson(pJson.Result);
                        string tguild = "";
                        if (cData.Characters.Character.Guild.Name != null)
                            tguild = cData.Characters.Character.Guild.Name + " (" + cData.Characters.Character.Guild.Rank + ")";
                        WriteDetails(wData.Worlds.World.OnlinePlayers[p2].Name, wData.Worlds.World.Name, wData.Worlds.World.OnlinePlayers[p2].Vocation.ToString(), wData.Worlds.World.OnlinePlayers[p2].Level.ToString(), "friend", tguild.ToString());
                    }
                }
            }
            for (int p1 = 0; p1 < conf.Players.Foes.Count(); p1++)
            {
                for (int p2 = 0; p2 < wData.Worlds.World.OnlinePlayers.Count(); p2++)
                {
                    if (wData.Worlds.World.OnlinePlayers[p2].Name.ToLower() == conf.Players.Foes[p1].ToString().ToLower())
                    {
                        string urlp3 = "https://api.tibiadata.com/v3/character/" + Uri.EscapeUriString(wData.Worlds.World.OnlinePlayers[p2].Name);
                        HttpClient client4 = new HttpClient();
                        client4.BaseAddress = new Uri(urlp3);
                        Task<string> pJson = client.GetStringAsync(urlp3);
                        pJson.Wait(300);
                        cData = CData.FromJson(pJson.Result);
                        string tguild = "";
                        if (cData.Characters.Character.Guild.Name != null)
                            tguild = cData.Characters.Character.Guild.Name + " (" + cData.Characters.Character.Guild.Rank + ")";
                        WriteDetails(wData.Worlds.World.OnlinePlayers[p2].Name, wData.Worlds.World.Name, wData.Worlds.World.OnlinePlayers[p2].Vocation.ToString(), wData.Worlds.World.OnlinePlayers[p2].Level.ToString(), "foe", tguild.ToString());
                    }
                }
            }



            //Console.WriteLine(wData.Worlds.World.Name + " " + wData.Worlds.World.Location);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=[ TEVL " + curVersion + " ]=-");
            
        } 
    }
    catch (Exception ex)
    {

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Tibia External VIP List");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(" - ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("By ConsKrypt");
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("Last Update");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(": ");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(DateTime.Now.ToString());
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(" - Next Update in " + uTime + " seconds!");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=[ TEVL "+ curVersion +" ]=-");
        Console.ForegroundColor = ConsoleColor.Red;
        //Console.WriteLine(ex.Message);
        Console.WriteLine("There was an issue with the data retrieved from TibiaData.com...");
        Console.Write("Trying again in ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(uTime);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(" seconds...");
    }
    CheckForUpdates();
}

void CheckForUpdates()
{
    if(conf.Options.UpdateCheck.HasValue && conf.Options.UpdateCheck == true)
    {

        string url = "https://gist.githubusercontent.com/ImConsKrypt/76b5ebdf7c454acfc397ffea69347c86/raw/36593b6683318ee27fa69c394825fc0968ee90f3/TEVLUpdate.json";
        HttpClient updateCheck = new HttpClient();
        updateCheck.BaseAddress = new Uri(url);
        Task<string> uJson = updateCheck.GetStringAsync(url);
        uJson.Wait(300);
        updateData = UpdateData.FromJson(uJson.Result);
        if("v"+updateData.CurrentVersion.ToString() != curVersion.ToString())
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine("There is a newer version available, Current Version: " + curVersion + ". Available Update Version: " + updateData.CurrentVersion.ToString());
        }
    }
}

void WriteDetails(string playerName, string playerWorld, string playerVocation, string playerLevel, string group, string guild)
{
    
    switch (group.ToLower())
    {
        case "friend":
            Console.ForegroundColor = ConsoleColor.Green;
            break;
        case "foe":
            Console.ForegroundColor = ConsoleColor.DarkRed;
            break;
        case "guild":
            Console.ForegroundColor = ConsoleColor.Cyan;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
    }
    string lvl = "";
    string gld = "";
    string voc = "";
    switch (playerVocation)
    {
        case "MasterSorcerer":
            playerVocation = "MS";
            break;
        case "EliteKnight":
            playerVocation = "EK";
            break;
        case "ElderDruid":
            playerVocation = "ED";
            break;
        case "RoyalPaladin":
            playerVocation = "RP";
            break;
        default:
            break;
    }

    //Console.Write(playerName);
    if (conf.Options.ShowLevel.HasValue && conf.Options.ShowLevel.Value == true)
        lvl = "["+playerLevel+"]";
    if (conf.Options.ShowVocation.HasValue && conf.Options.ShowVocation.Value == true)
        voc = "[" + playerVocation + "]";
    if (conf.Options.ShowGuild.HasValue && conf.Options.ShowGuild.Value == true && guild != "")
        gld = "[" + guild + "]";
    Console.WriteLine("{0,10}\t\t{1,5}\t{2,10}\t{3,25}", playerName, lvl, voc, gld);
    Console.ForegroundColor = ConsoleColor.Gray;
}