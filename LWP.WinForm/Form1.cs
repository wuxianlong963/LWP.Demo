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

namespace LWP.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loadSimpleClass();
            loadLogoClass();
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Parent = pictureBox1;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }

        private void loadSimpleClass()
        {
            comboBox1.Items.Insert(0, new { Name = "请选择" });

            var pathInfo = new DirectoryInfo(Application.StartupPath + "\\Image\\Sample\\");
            if (pathInfo == null) return;

            var list = pathInfo.EnumerateDirectories().Select(d=>d.Name).ToList();
            list.Insert(0,"==请选择==");
            comboBox1.DataSource = list;
            
            
        }

        private void loadLogoClass()
        {
            var pathInfo = new DirectoryInfo(Application.StartupPath + "\\Image\\Logo\\");
            if (pathInfo == null) return;

            var list = pathInfo.EnumerateDirectories().Select(d => d.Name).ToList();
            list.Insert(0, "==请选择==");
            comboBox2.DataSource = list;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var img = SaveImage();
            img.Save(Application.StartupPath + "\\Product\\merge.jpg");
            MessageBox.Show("ok");
        }

        public Image SaveImage()
        {
            Image imgBack = pictureBox1.Image; 
            Image img = pictureBox2.Image;  
  
            Graphics g = Graphics.FromImage(imgBack);

            //g.DrawImage(imgBack, 0, 0, 148, 124);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高); 
            //g.FillRectangle(System.Drawing.Brushes.Black, 16, 16, (int)112 + 2, ((int)73 + 2));//相片四周刷一层黑色边框

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);
            var w = pictureBox2.Width * imgBack.Width / pictureBox1.Width;
            var h = pictureBox2.Height * imgBack.Height / pictureBox1.Height;
            int x = pictureBox2.Location.X * imgBack.Width / pictureBox1.Width;
            int y = pictureBox2.Location.Y * imgBack.Height / pictureBox1.Height;
            g.DrawImage(img, x, y, w, h);
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

        #region Logo image size change

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
                SampleImageList.Images.Add(Image.FromFile(item.FullName));

            listView1.View = View.LargeIcon;
            listView1.CheckBoxes = true;
            listView2.MultiSelect = false;
            listView1.LargeImageList = SampleImageList;
            listView1.BeginUpdate();

            for (int i = 0; i < SampleImageList.Images.Count;i++ )
            {
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = i;
                this.listView1.Items.Add(lvi);
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
                LogoImageList.Images.Add(Image.FromFile(item.FullName));

            listView2.View = View.LargeIcon;
            listView2.MultiSelect = false;
            listView2.LargeImageList = LogoImageList;
            listView2.BeginUpdate();

            for (int i = 0; i < LogoImageList.Images.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = i;
                this.listView2.Items.Add(lvi);
            }

            this.listView2.EndUpdate();
        }

        public void ListView1_Click(object sender, EventArgs e)
        {
            var i = listView1.SelectedItems[0].Index;

            pictureBox1.Image = Image.FromFile("");
        }
    }
}
