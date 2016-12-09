using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using LWP.WinForm.Lib;
using System.Text.RegularExpressions;
using System.Diagnostics;
using LWP.Lib;

namespace LWP.WinForm
{
    public partial class Main : Form
    {
        public Main()
        {
            test();
            InitializeComponent();
            comboBox1.DataSource = ObjectList.GetSampleClassList();
            comboBox2.DataSource = ObjectList.GetLogoClassList();
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Parent = pictureBox1;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }

        public void test()
        {
            long ts = DateTime.Now.Ticks;
            var productClass = new ProductClass()
            {
                Name = "test name 00102",
                ParentId = 0
            };

            (new ProductClassManager()).AddProductClass(productClass);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var pathSample = Application.StartupPath + "\\Image\\Sample\\" + comboBox1.SelectedValue + "\\";
            var pathLogo = Application.StartupPath + "\\Image\\Logo\\" + comboBox2.SelectedValue + "\\";
            var folder = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            var savePath = Application.StartupPath + "\\Product\\" + folder;

            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
            ExcelHandle.CreateExcelFile(savePath + "\\ProductList.xls");

            foreach (ListViewItem item in listView1.CheckedItems)
            {
                try
                {
                    var img = SaveImage(pathSample + item.Name, pathLogo + pictureBox2.Name);
                    if (img == null) continue;
                    var saveFullPath = savePath + "\\" + StringHandle.RemoveExtension(item.Name) + "_" + StringHandle.RemoveExtension(listView2.SelectedItems[0].Name) + ".jpg";
                    img.Save(saveFullPath);
                    img.Dispose();

                    ExcelHandle.WriteToExcel(savePath + "\\ProductList.xls", StringHandle.RemoveExtension(item.Name) + "_" + StringHandle.RemoveExtension(listView2.SelectedItems[0].Name) + ".jpg", DateTime.Now.ToString(), KeywordsHandle.GetRadomKeywords());
                }
                catch (Exception ex)
                {
                    Log.AddLog(ex);
                }
            }

            System.Diagnostics.Process.Start(savePath);
        }

        public Image SaveImage(string sampleImage, string logoImage)
        {
            Image imgBack = ImageGenerate.CreateImage(sampleImage);
            Image img = ImageGenerate.CreateImage(logoImage);
            if (imgBack == null || img == null) return null;

            Graphics g = Graphics.FromImage(imgBack);

            //g.DrawImage(imgBack, 0, 0, 148, 124);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高); 
            //g.FillRectangle(System.Drawing.Brushes.Black, 16, 16, (int)112 + 2, ((int)73 + 2));//相片四周刷一层黑色边框

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);
            var w = pictureBox2.Width * imgBack.Width / pictureBox1.Width;
            var h = pictureBox2.Height * imgBack.Height / pictureBox1.Height;
            int x = pictureBox2.Location.X * imgBack.Width / pictureBox1.Width;
            int y = pictureBox2.Location.Y * imgBack.Height / pictureBox1.Height;
            g.DrawImage(img, x, y, w, h);
            img.Dispose();
            GC.Collect();
            return imgBack;
        }

        public Image GetRotateImage(Image img, float angle)
        {
            angle = angle % 360;//弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = img.Width;
            int h = img.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Image dsImage = new Bitmap(W, H, img.PixelFormat);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //计算偏移量
                Point Offset = new Point((W - w) / 2, (H - h) / 2);
                //构造图像显示区域：让图像的中心与窗口的中心点一致
                Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
                Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                g.TranslateTransform(center.X, center.Y);
                g.RotateTransform(360 - angle);
                //恢复图像在水平和垂直方向的平移
                g.TranslateTransform(-center.X, -center.Y);
                g.DrawImage(img, rect);
                //重至绘图的所有变换
                g.ResetTransform();
                g.Save();
            }
            return dsImage;
        }

        #region Mouse drag logo

        int xPos;
        int yPos;
        bool MoveFlag;
        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            MoveFlag = true;//已经按下.
            xPos = e.X;//当前x坐标.
            yPos = e.Y;//当前y坐标.
        }
        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            MoveFlag = false;
        }
        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveFlag)
            {
                pictureBox2.Left += Convert.ToInt16(e.X - xPos);//设置x坐标.
                pictureBox2.Top += Convert.ToInt16(e.Y - yPos);//设置y坐标.
            }
        }

        #endregion

        #region Keyboard move logo

        public void Keyboard_click(object sender, KeyEventArgs e)
        {
            if (pictureBox2.BorderStyle == BorderStyle.FixedSingle)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left: pictureBox2.Left -= 1; break;
                    case Keys.Right: pictureBox2.Left += 1; break;
                    case Keys.Up: pictureBox2.Top -= 1; break;
                    case Keys.Down: pictureBox2.Top += 1; break;
                }

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Up)
                {
                    resizePictureBox(1);
                }
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Down)
                {
                    resizePictureBox(-1);
                }
            }
        }

        #endregion

        #region Log image size change

        public void resizePictureBox(int size)
        {
            if (pictureBox2.Image.Width == pictureBox2.Image.Height)
            {
                pictureBox2.Height += size;
                pictureBox2.Width += size;
            }
            else if (pictureBox2.Image.Width < pictureBox2.Image.Height)
            {
                pictureBox2.Width += size;
                pictureBox2.Height += size * (pictureBox2.Image.Height / pictureBox2.Image.Width);
            }
            else
            {
                pictureBox2.Height += size;
                pictureBox2.Width += size * (pictureBox2.Image.Width / pictureBox2.Image.Height);
            }
        }

        #endregion

        public void SampleChange(object sender, EventArgs e)
        {
            var s = comboBox1.SelectedValue;
            if (s == "==请选择==")
            {
                listView1.Clear();
                SampleImageList.Images.Clear();
                return;
            }

            listView1.Clear();
            SampleImageList.Images.Clear();
            SampleImageList.ImageSize = new Size(40, 40);
            

            var pathInfo = new DirectoryInfo(Application.StartupPath + "\\Image\\Sample\\" + s + "");
            if (pathInfo == null || pathInfo.EnumerateFiles() == null) return;

            List<Image> images = new List<Image>();
            foreach (var item in pathInfo.EnumerateFiles())
            {
                var image = ImageGenerate.CreateImage(item.FullName,true);
                if (image != null) SampleImageList.Images.Add(image);
            }

            listView1.View = View.LargeIcon;
            listView1.CheckBoxes = true;
            
            listView2.MultiSelect = false;
            listView1.LargeImageList = SampleImageList;
            listView1.BeginUpdate();

            for (int i = 0; i < SampleImageList.Images.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Checked = true;
                lvi.Name = pathInfo.EnumerateFiles().ToList()[i].Name;
                lvi.ImageIndex = i;
                this.listView1.Items.Add(lvi);

                if (i == 0)
                {
                    if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                    pictureBox1.Image = ImageGenerate.CreateImage(Application.StartupPath + "\\Image\\Sample\\" + s + "\\" + lvi.Name);
                    ImageGenerate.ResetSize(pictureBox1);
                }
            }

            this.listView1.EndUpdate();  
        }

        public void LogoChange(object sender, EventArgs e)
        {
            var s = comboBox2.SelectedValue;
            if (s == "==请选择==")
            {
                listView2.Clear();
                LogoImageList.Images.Clear();
                return;
            }

            listView2.Clear();
            LogoImageList.Images.Clear();
            LogoImageList.ImageSize = new Size(60, 60);


            var pathInfo = new DirectoryInfo(Application.StartupPath + "\\Image\\Logo\\" + s + "");
            if (pathInfo == null || pathInfo.EnumerateFiles() == null) return;

            List<Image> images = new List<Image>();
            foreach (var item in pathInfo.EnumerateFiles())
            {
                var image = ImageGenerate.CreateImage(item.FullName,true);
                if (image != null) LogoImageList.Images.Add(image);
            }
                

            listView2.View = View.LargeIcon;
            listView2.MultiSelect = false;
            listView2.LargeImageList = LogoImageList;
            listView2.BeginUpdate();

            for (int i = 0; i < LogoImageList.Images.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Name = pathInfo.EnumerateFiles().ToList()[i].Name;
                lvi.ImageIndex = i;
                this.listView2.Items.Add(lvi);

                if (i == 0)
                {
                    lvi.Selected = true;
                    if (pictureBox2.Image != null) pictureBox2.Image.Dispose();
                    pictureBox2.Image = ImageGenerate.CreateImage(Application.StartupPath + "\\Image\\Logo\\" + s + "\\" + lvi.Name);
                    ImageGenerate.ResetSize(pictureBox2);
                }
            }

            this.listView2.EndUpdate();
        }

        public void ListView1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = ImageGenerate.CreateImage(Application.StartupPath + "\\Image\\Sample\\" + comboBox1.SelectedValue + "\\" + listView1.SelectedItems[0].Name);
            ImageGenerate.ResetSize(pictureBox1);
        }

        public void listView2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image.Dispose();
            pictureBox2.Name = listView2.SelectedItems[0].Name;
            pictureBox2.Image = ImageGenerate.CreateImage(Application.StartupPath + "\\Image\\Logo\\" + comboBox2.SelectedValue + "\\" + listView2.SelectedItems[0].Name);
            ImageGenerate.ResetSize(pictureBox2);
        }

        public void picture2_Click(object sender, EventArgs e)
        {
            if (pictureBox2.BorderStyle == BorderStyle.FixedSingle)
                pictureBox2.BorderStyle = BorderStyle.None;
            else
                pictureBox2.BorderStyle = BorderStyle.FixedSingle;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var k = new KeywoardManage();
            k.ShowDialog();
        }
    }
}
