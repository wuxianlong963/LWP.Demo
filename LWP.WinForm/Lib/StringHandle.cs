using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWP.WinForm.Lib
{
    public class StringHandle
    {
        public static string RemoveExtension(string fileNmae)
        {
            if (string.IsNullOrWhiteSpace(fileNmae)) return "";

            return fileNmae.Substring(0, fileNmae.LastIndexOf(".")).Trim('_');
        }
    }
}
