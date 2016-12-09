using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWP.WinForm.Lib
{
    public class Log
    {
        public static void AddLog(string title,string content)
        {
        }

        public static void AddLog(Exception e)
        {
            AddLog(e.Message, e.ToString());
        }
    }
}
