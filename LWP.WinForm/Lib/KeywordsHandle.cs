using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LWP.WinForm.Lib
{
    public class KeywordsHandle
    {
        public static int Seed = 0;

        public static List<string> keywords
        {
            get
            {
                if (_keys == null || _keys.Count <= 0)
                    LoadKeywordsFromFile();

                return _keys;
            }
        }

        private static List<string> _keys { get; set; }

        private static void LoadKeywordsFromFile()
        {
            _keys = new List<string>();

            var st = new StreamReader(Application.StartupPath + "\\Keywords\\k1.txt");
            _keys.Add(st.ReadToEnd());
            st.Close();
            st.Dispose();
            st = new StreamReader(Application.StartupPath + "\\Keywords\\k2.txt");
            _keys.Add(st.ReadToEnd());
            st.Close();
            st.Dispose();
            st = new StreamReader(Application.StartupPath + "\\Keywords\\k3.txt");
            _keys.Add(st.ReadToEnd());
            st.Close();
            st.Dispose();
            st = new StreamReader(Application.StartupPath + "\\Keywords\\k4.txt");
            _keys.Add(st.ReadToEnd());
            st.Close();
            st.Dispose();
            st = new StreamReader(Application.StartupPath + "\\Keywords\\k5.txt");
            _keys.Add(st.ReadToEnd());
            st.Close();
            st.Dispose();
        }

        public static void ReflashKeys()
        {
            LoadKeywordsFromFile();
        }

        public static string GetRadomKeywords()
        {
            var keys = "";
            foreach (string item in keywords)
            {
                var matchs = (new Regex(@"(?<=\{).*?(?=\})")).Matches(item);
                if (matchs == null || matchs.Count <= 0) continue;
                
                foreach (Match match in matchs)
                {
                    var keyList = match.Value.Split(' ');
                    var random = new Random(Seed);
                    keys += keyList[random.Next(0, keyList.Length - 1)] + " ";
                    Seed = Seed + 1;
                }
            }


            return keys;
        }
    }
}
