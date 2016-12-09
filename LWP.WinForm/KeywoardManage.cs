using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LWP.WinForm.Lib;

namespace LWP.WinForm
{
    public partial class KeywoardManage : Form
    {
        public KeywoardManage()
        {
            InitializeComponent();
            loadKeywords();
        }

        private void loadKeywords()
        {
            var st = new StreamReader(Application.StartupPath + "\\Keywords\\k1.txt");
            textBox1.Text = st.ReadToEnd();
            st.Close();
            st.Dispose();
            st = new StreamReader(Application.StartupPath + "\\Keywords\\k2.txt");
            textBox2.Text = st.ReadToEnd();
            st.Close();
            st.Dispose();
            st = new StreamReader(Application.StartupPath + "\\Keywords\\k3.txt");
            textBox3.Text = st.ReadToEnd();
            st.Close();
            st.Dispose();
            st = new StreamReader(Application.StartupPath + "\\Keywords\\k4.txt");
            textBox4.Text = st.ReadToEnd();
            st.Close();
            st.Dispose();
            st = new StreamReader(Application.StartupPath + "\\Keywords\\k5.txt");
            textBox5.Text = st.ReadToEnd();
            st.Close();
            st.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var st = new StreamWriter(Application.StartupPath + "\\Keywords\\k1.txt");
                st.WriteLine(textBox1.Text.Trim());
                st.Close();
                st.Dispose();
                st = new StreamWriter(Application.StartupPath + "\\Keywords\\k2.txt");
                st.WriteLine(textBox2.Text.Trim());
                st.Close();
                st.Dispose();
                st = new StreamWriter(Application.StartupPath + "\\Keywords\\k3.txt");
                st.WriteLine(textBox3.Text.Trim());
                st.Close();
                st.Dispose();
                st = new StreamWriter(Application.StartupPath + "\\Keywords\\k4.txt");
                st.WriteLine(textBox4.Text.Trim());
                st.Close();
                st.Dispose();
                st = new StreamWriter(Application.StartupPath + "\\Keywords\\k5.txt");
                st.WriteLine(textBox5.Text.Trim());
                st.Close();
                st.Dispose();

                KeywordsHandle.ReflashKeys();

                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                Log.AddLog(ex);
                throw;
            }
        }
    }
}
