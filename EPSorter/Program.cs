using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace EPSorter
{
    class Program
    {

        static void Main(string[] args)
        {
            //args = new string[] { @"F:\Series\The 100\S1" };
            if (args != null && args.Any())
            {
                var FolderPath = args[0];
                Sort(FolderPath);
                foreach (var subdire in Directory.GetDirectories(FolderPath, "*", SearchOption.AllDirectories))
                {
                    Sort(subdire);
                }

                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "done.wav"));
                player.PlaySync();
            }
        }
        static void Sort(string FolderPath)
        {
            var rgx = new string[] { @"ep\d+", @"ep\d+", @"e\d+", @"e\d+" };
            foreach (var file in Directory.GetFiles(FolderPath))
            {
                try
                {
                    string FileName = Path.GetFileName(file).ToLower();
                    var epNum = "";
                    foreach (var itemRg in rgx)
                    {
                        if (Regex.IsMatch(FileName, itemRg))
                        {
                            epNum = Regex.Match(Regex.Match(FileName, itemRg).Value, @"\d+").Value;
                            break;
                        }
                    }

                    //if (Regex.IsMatch(FileName, ))
                    //{
                    //    epNum = Regex.Match(FileName, ).Value.Replace("e", "");
                    //}
                    //else
                    //    epNum = Regex.Match(FileName, ).Value;
                    if (!string.IsNullOrEmpty(epNum))
                    {
                        var EpNumInt = 0;

                        if (int.TryParse(epNum, out EpNumInt))
                            if (EpNumInt < 10)
                                epNum = string.Format("{0:00}", EpNumInt);
                        var NewFile = epNum + Path.GetExtension(file);
                        File.Move(file, Path.Combine(FolderPath, NewFile));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

        }
    }
}
