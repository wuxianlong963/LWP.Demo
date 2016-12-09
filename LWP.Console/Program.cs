using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing;

namespace LWP.Console1
{
    class Program
    {
        static void Main()
        {

            begin();
            return;
            //定义动作组
            TempClass tc = new TempClass();
            //定义动作组
            List<Action> actions = new List<Action>();
            for (tc.copy = 0; tc.copy < 10; tc.copy++)
            {
                //tc.copy = counter;
                actions.Add(tc.TempMethod);
            }
            //执行动作
            foreach (Action action in actions) action();
            System.Console.ReadLine();
        }

        class TempClass
        {
            public int copy;
            public void TempMethod()
            {
                System.Console.WriteLine(copy);
            }
        }

        private static void begin()
        {
            System.Console.WriteLine("Download image from 'http://cn.bing.com'.");
            Program dsw = new Program();
            string tempImage = Path.GetTempPath() + "\\" + "bing.jpg";
            dsw.GetImageFromBing(tempImage);
            dsw.SetDestPicture(tempImage);

            System.Console.WriteLine("Desktop background image set successfully!");
            System.Console.Write("exit");
        }

        private void GetImageFromBing(String imagePath)
        {
            string url = "http://cn.bing.com/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.GetEncoding("utf-8");
            StreamReader sr = new StreamReader(stream, encode);
            string html = null;
            char[] readbuffer = new char[256];
            int n = sr.Read(readbuffer, 0, 256);
            while (n > 0)
            {
                string str = new string(readbuffer, 0, n);
                html += str;
                n = sr.Read(readbuffer, 0, 256);
            }
            //StreamWriter streamw = File.CreateText(@"C:\test3.txt");
            //streamw.WriteLine(html);
            //streamw.Close();

            //string pattern = "height: 267px; background-image: url\\(\\.+\\); opacity: 1;";  
            //url:'\/fd\/hpk2\/Hayden_ZH-CN1124177866.jpg',id:'bgDiv'  
            string pattern = "(?<=g_img=\\{url:\\s?\").*?(?=\",id:'bgDiv')";
            Match match = Regex.Match(html, pattern);
            if (match.Success)
            {
                string imageUrl = match.Value;
                //imageUrl = url + imageUrl.Replace("\\", "");
                System.Console.WriteLine("Set image '" + imageUrl + "' as the Desktop background.");
                downloadImage(imageUrl, imagePath);
            }
            else
            {
                System.Console.WriteLine("Can't find image.");
                //System.Environment.Exit(-1);
            }
        }

        private void downloadImage(String url, String imagePath)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.ContentType = "image/jpeg";
                Stream stream = request.GetResponse().GetResponseStream();
               
                FileStream fstr = new FileStream(imagePath, FileMode.OpenOrCreate, FileAccess.Write);
                int count = 0;
                do
                {
                    byte[] mbyte = new byte[1024];
                    count = stream.Read(mbyte, 0, 1024);
                    fstr.Write(mbyte, 0, count);

                } while (stream.CanRead && count > 0);
                stream.Close();
                fstr.Close();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Get image from bing failed. Exit");
                //System.Environment.Exit(-1);
            }

        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
            int uAction,
            int uParam,
            string lpvParam,
            int fuWinIni
            );

        /// <summary>  
        /// 设置背景图片  
        /// </summary>  
        /// <param name="picture">图片路径</param>  
        private void SetDestPicture(string picture)
        {
            if (File.Exists(picture))
            {
                if (Path.GetExtension(picture).ToLower() != "bmp")
                {
                    // 其它格式文件先转换为bmp再设置  
                    string tempFile = @"D:\test.bmp";
                    Image image = Image.FromFile(picture);
                    image.Save(tempFile, System.Drawing.Imaging.ImageFormat.Bmp);
                    picture = tempFile;
                    setBMPAsDesktop(picture);
                    File.Delete(tempFile);
                }
                else
                {
                    setBMPAsDesktop(picture);
                }

            }
        }

        /// <summary>  
        /// 设置BMP格式的背景图片  
        /// </summary>  
        /// <param name="bmp">图片路径</param>  
        private void setBMPAsDesktop(string bmp)
        {
            SystemParametersInfo(20, 0, bmp, 0x2);
        }  
    }
}
