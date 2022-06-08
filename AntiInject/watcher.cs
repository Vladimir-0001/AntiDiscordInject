using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace AntiInject
{

    internal class watcher

    {
        public string path;
        public bool killerstart;
        
        public void GetPath()
        {
            path = @"%LOCALAPPDATA%";
            path = Environment.ExpandEnvironmentVariables(path);
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                if (new DirectoryInfo(dir).Name.ToLower() == "discord")
                {  
                    string[] apps = Directory.GetDirectories(dir);
                    foreach (string app in apps)
                    {
                        if (app.Replace(dir, "").Contains("app"))
                        {
                            path = app;
                        }

                    }
                }
            }

        }
        private void DiscordKiller()
        {

            for (int i = 0; i < 100; i++)
            {
                Process[] pr = Process.GetProcessesByName("Discord");
                foreach (Process p in pr)
                {
                    p.Kill();
                }
                Thread.Sleep(10);
            }
            

        }
        public void intercept()
        {
            Thread killer = new Thread(DiscordKiller);
            killer.Start();
            string text = System.IO.File.ReadAllText(path + @"\modules\discord_desktop_core-3\discord_desktop_core\index.js");
            if (text != "module.exports = require('./core.asar');")
            {
                    System.IO.File.WriteAllText(path + @"\modules\discord_desktop_core-3\discord_desktop_core\index.js", "module.exports = require('./core.asar');");
                    killerstart = false;
            }

            MessageBox.Show("AntiDiscordInject detected attempted injection of a discord virus and blocked it", "AntiInject", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            
        }
        public void Watch()
        {
            GetPath();
            while (true)
            {
                try
                {                    
                    string text = System.IO.File.ReadAllText(path + @"\modules\discord_desktop_core-3\discord_desktop_core\index.js");
                    if (text != "module.exports = require('./core.asar');")
                    {
                        intercept();
                    }
                    Thread.Sleep(20);
                }

                catch
                {
                    continue;
                }
                
                
            }
        }
    }
}
