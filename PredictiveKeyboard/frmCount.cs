using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PredictiveKeyboard
{
    public partial class frmCount : Form
    {
        public frmCount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = richTextBox1.Text;
            string[] raw_values = Regex.Split(str, " ");
            int cnt = 0;
            foreach (string obj in raw_values)
            {
                foreach (char ch in obj)
                {
                    cnt++;
                }
            }
            cnt = cnt + raw_values.Length;
            label1.Text = "char\t" + cnt.ToString();
            label2.Text = "word\t" + raw_values.Length.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = richTextBox2.Text;
            string[] raw_values = Regex.Split(str, " ");
            int cnt = 0;
            char start = '(';
            char stop = ')';
            bool flag = false;
            int Hit = 0;

            foreach (string obj in raw_values)
            {
                foreach (char ch in obj)
                {
                    if (ch == start)
                    {
                        flag = true;
                    }
                    if (flag == true && ch == stop)
                    {
                        Hit++;
                       // cnt++;
                        flag = false;
                    }
                    else
                    {
                        if (flag == false)
                        {
                            cnt++;
                        }
                    }

                       // cnt++;
                    
                }
               // inside = 0;
                flag = false;
              

            }
           // str = str.Replace("(", " ");
           // string[] value = Regex.Split(str, " ");
            cnt = (cnt); //+ raw_values.Length;
            label1.Text = "char\t" + cnt.ToString();
            label2.Text = "Hit\t" + Hit.ToString();
        }
    }
}
