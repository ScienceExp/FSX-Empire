using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FSX_EMPIRE
{
    //not really used yet (not sure if it will be used...)
    class Airgraft_cfg
    {
        static List<String> GetAllFiles(String directory)
        {
            return Directory.GetFiles(directory, "aircraft.cfg", SearchOption.AllDirectories).ToList();
        }

        public static string LoadFileFromTitle(string title)
        {
            List<string> files = GetAllFiles(G.FSXAircraftPath);
            foreach (string file in files)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (IniFile.KeyExists(file, "title", "fltsim." + i.ToString()))
                    {
                        string f = IniFile.ReadKey(file, "title", "fltsim." + i.ToString());
                        if (f == title)
                        {
                            G.FSXPlaneName = IniFile.ReadKey(file, "sim", "fltsim." + i.ToString());
                            return file;
                        }
                    }
                    else
                        break;
                }
            }
            return null;
        }
    }
}
