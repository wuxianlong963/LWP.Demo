using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWP.Lib.Unity
{
    public class DataFmt
    {
        public static string GetIncreaseClassNo(string classNo)
        {
            if (string.IsNullOrWhiteSpace(classNo)) return "0001";

            int num = int.Parse(classNo) + 1;

            return num.ToString().PadLeft(4, '0');
        }
    }
}
