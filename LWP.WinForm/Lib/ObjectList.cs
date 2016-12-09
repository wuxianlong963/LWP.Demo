using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LWP.WinForm.Lib
{
    public class ObjectList
    {
        public static List<string> GetSampleClassList()
        {
            var pathInfo = new DirectoryInfo(Application.StartupPath + "\\Image\\Sample\\");
            if (pathInfo == null) return null;

            var list = pathInfo.EnumerateDirectories().Select(d=>d.Name).ToList();
            list.Insert(0,"==请选择==");

            return list;
        }

        public static List<string> GetLogoClassList()
        {
            var pathInfo = new DirectoryInfo(Application.StartupPath + "\\Image\\Logo\\");
            if (pathInfo == null) return null;

            var list = pathInfo.EnumerateDirectories().Select(d => d.Name).ToList();
            list.Insert(0, "==请选择==");
            return list;
        }
    }
}
