using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace LWP.WinForm.Lib
{
    public class ImageGenerate
    {
        public static void ResetSize(PictureBox box)
        {
            if (box == null || box.Image == null) return;

            if (box.Image.Width == box.Height) return;

            box.Width = box.Height = box.Width > box.Height ? box.Width : box.Height;

            if (box.Image.Width > box.Image.Height)
            {
                box.Height = box.Width * box.Image.Height / box.Image.Width;
            }
            else
            {
                box.Width = box.Height * box.Image.Width / box.Image.Height;
            }
        }

        public static Image CreateImage(string path, bool returnThumbnail = false, double scale = 0.1)
        {
            try
            {
                if (!File.Exists(path)) return null;

                var img = Image.FromFile(path);
                if (!returnThumbnail) return img;

                var imgSmall =  img.GetThumbnailImage(Convert.ToInt32(img.Size.Width * scale), Convert.ToInt32(img.Size.Height * scale), null, IntPtr.Zero);
                img.Dispose();
                return imgSmall;
            }
            catch (Exception e)
            {
                Log.AddLog(e);
                return null;
            }
        }
    }
}
