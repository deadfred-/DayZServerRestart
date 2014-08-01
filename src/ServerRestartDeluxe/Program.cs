using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ServerRestartDeluxe.classes;
using BattleNET;

namespace ServerRestartDeluxe
{
    public class Program
    {
        static void Main(string[] args)
        {
            string cfgFile;
            Boolean goodFile = false;
            BattlEyeLoginCredentials loginCredentials;
            Config cfg = new Config();

            Dictionary<string, string> ArgPairs = new Dictionary<string, string>();

            if (args.Length == 1)
            {
                string arg1 = args[0];
                // validate we have arguments
                if (arg1.Contains("--configfile=") && arg1.Contains(".xml"))
                {
                    string[] ArgSplit = arg1.Split('=');
                    if (ArgSplit.Length == 2)
                    {
                        // get cfg file
                        cfgFile = ArgSplit[1];

                        // get data from cfg file
                        goodFile = cfg.GetSettingsFromXML(cfgFile);

                    }

                }

            }
            // cli specified args
            // usage: ServerRestartDeluxe.exe server=127.0.0.1 port=2302 password=pw
            else if (args.Length > 1)
            {

                // split up args
                foreach (string inputArg in args)
                {
                    if (inputArg.Contains("="))
                    {
                        string[] pair = inputArg.Split('=');
                        ArgPairs.Add(pair[0], pair[1]);
                    }

                }

                // parse argpairs
                foreach (KeyValuePair<string, string> argPair in ArgPairs)
                {
                    switch (argPair.Key)
                    {
                        case "server":
                            cfg.IP = argPair.Value.ToString();
                            break;
                        case "port":
                            cfg.Port = argPair.Value.ToString();
                            break;
                        case "password":
                            cfg.Password = argPair.Value.ToString();
                            break;
                    }
                }

                // assume good - Fix Later - JG
                goodFile = true;
            }
            if (goodFile == true)
            {
                // establish connection
                loginCredentials = new BattlEyeLoginCredentials();
                loginCredentials.Host = cfg.IP;
                loginCredentials.Password = cfg.Password;
                loginCredentials.Port = Convert.ToInt32(cfg.Port);

                IBattleNET b = new BattlEyeClient(loginCredentials);
                b.MessageReceivedEvent += DumpMessage;
                b.DisconnectEvent += Disconnected;
                b.ReconnectOnPacketLoss(true);
                b.Connect();

                // validate connection
                if (b.IsConnected() == false)
                {
                    Console.WriteLine("No connection starting server");
                    // Process.Start(processPath);

                    // exit
                    return;
                }
                else
                {
                    Console.Title = "DayZ Ultra Server Restart 15 min warning";
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 15 min.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 15 min.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 15 min.");
                    Thread.Sleep(600000);

                    Console.Title = "DayZ Ultra Server Restart 5 minute warning";
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 5 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 5 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 5 minutes.");
                    Thread.Sleep(60000);
                    Console.Title = "DayZ Ultra Server Restart 4 minute warning";
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 4 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 4 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 4 minutes.");
                    Thread.Sleep(60000); // wait 1 min
                    Console.Title = "DayZ Ultra Server Restart 3 minute warning";
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 3 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 3 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 3 minutes.");
                    Thread.Sleep(60000); // wait 1 min
                    Console.Title = "DayZ Ultra Server Restart 2 minute warning";
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 2 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 2 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 2 minutes.");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 2 minutes.");
                    Thread.Sleep(60000); // wait 1 min
                    Console.Title = "DayZ Ultra Server Restart 1 minute warning";
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 1 minute.  Log off now");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 1 minute.  Log off now");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 1 minute.  Log off now");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 1 minute.  Log off now");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 1 minute.  Log off now");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset in 1 minute.  Log off now");

                    // lock server
                    b.SendCommandPacket(EBattlEyeCommand.Lock);

                    Thread.Sleep(60000); // wait 1 min
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset NOW!");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset NOW!");
                    b.SendCommandPacket(EBattlEyeCommand.Say, "-1 Server going down for reset NOW!");
                    Console.WriteLine("Shutdown cmd");
                    //b.SendCommandPacket(EBattlEyeCommand.Shutdown);
                    System.Environment.Exit(0);
                }

                // warnings
            }
            else
            { 
                // exit
                Console.WriteLine("Error reading config file. exiting now...");
                return;
            }

        }

        private static void Disconnected(BattlEyeDisconnectEventArgs args)
        {
            Console.WriteLine(args.Message);
        }

        private static void DumpMessage(BattlEyeMessageEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
