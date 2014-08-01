using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Threading;
using BattleNET;

namespace ServerRestartDeluxe.classes
{
    public class Config
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public int RestartWarningTimer { get; set; }

        

        public Boolean GetSettingsFromXML(string cfgFilePath)
        {
            Boolean returnValue = true;
            XmlDocument xd = new XmlDocument();


            // load XML file
            try
            {
                xd.Load(cfgFilePath);
                XmlNodeList beNodes = xd.SelectNodes("config/BattlEye");

                // itterate through nodes
                foreach (XmlNode beNode in beNodes)
                {
                    foreach (XmlNode optNode in beNode)
                    {
                        switch (optNode.Name)
                        {
                            case "ip":
                                this.IP = optNode.Value;
                                break;

                            case "port":
                                this.Port = optNode.Value;
                                break;
                            case "password":
                                this.Password = optNode.Value;
                                break;
                            case "RestartWarningTimer":
                                this.RestartWarningTimer = Convert.ToInt32(optNode.Value);
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading CFG File");
                returnValue = false;
            }
            return returnValue;         
        }
    }
}
