using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace PredictiveKeyboard
{
    public partial class logAnalysis : Form
    {
        public logAnalysis()
        {
            InitializeComponent();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            iniSentenceID();
            string SenID = "";
            string file_w = @"..\..\..\..\N-Grams\log_key_press\testdata1.txt";
            string total_text_w = File.ReadAllText(file_w, Encoding.UTF8);
            string[] wordDic_new_w = Regex.Split(total_text_w, "\n");
            string temp = "";
            ArrayList arr = new ArrayList();
            bool flagStart = false;
            bool val_start = false;
            bool val_stop = false;
            ArrayList arr1 = new ArrayList();
            foreach (string obj in wordDic_new_w)
            {
                val_start = obj.StartsWith("Test data:");
               val_stop = obj.StartsWith("----");
                if (val_start ==true)
                {
                    flagStart = true;
                }
                if (val_stop == true)
                {
                    
                    flagStart = false;
                }
                if (flagStart == true)
                {
                    arr.Add(obj);
                }
            }
               
            int startIndex=0;
            int nextIndex =0;
            string temp_var = "";
            startIndex = total_text_w.IndexOf("M");
            string value = "";
            string time1 = "";
            string time2 = "";
            string local = "";
            string timeElasp = "";
            string txtTimeDiff = "";
            int hitCount = 0;
            string text1="";
            string rowString = "";
            string final = "";
            DateTime dt = System.DateTime.Now;
            try
            {

                while ((nextIndex + 2 <= total_text_w.Length))
                {
                    startIndex = total_text_w.IndexOf("M", startIndex);
                    nextIndex = total_text_w.IndexOf("M", startIndex + 1);
                    if (nextIndex != -1)
                    {
                        temp_var = total_text_w.Substring(nextIndex, 9);
                        temp_var = "";
                        //richTextBox1.Text += total_text_w.Substring(startIndex+10, nextIndex - startIndex+9-32)+"\t"+temp_var + "\n";
                        value+= total_text_w.Substring(startIndex , nextIndex - startIndex + 9) + "\t" + temp_var + "\n";
                        local = total_text_w.Substring(startIndex, nextIndex - startIndex + 10);

                        time1=local.Substring(2, 8);
                        time2 = local.Substring(local.Trim().Length - 8, 8);
                        txtTimeDiff = (Convert.ToDateTime(time2)).Subtract(Convert.ToDateTime(time1)).ToString();
                        hitCount=CountChar(local, '(');
                        text1 =local.Substring(11, local.Length - 37 - 8);

                        bool f1 = false;
                        bool f2 = false;
                        rowString = "";
                        foreach (char ob in text1)
                        {
                            if (ob == '(')
                            {
                                f1 = true;
                               // f2 = false;
                            }
                            else
                            {
                                if (ob == ')')
                                {
                                   // f2 = true;
                                    f1 = false;
                                }
                                
                            }
                            if (f1 == false && ob != ')')
                            {
                                rowString += ob;
                            }
                        }
                        SenID = local.Substring(local.IndexOf("sID"), 5).Trim();
                        string[] NumberOfWord = Regex.Split(SenDic[SenID].ToString().Trim(), " ");
                        string NW = NumberOfWord.Length.ToString();
                        dt = Convert.ToDateTime(txtTimeDiff);

                        final = "\nSentenceID=" + SenID + "\tSentence=\t" + SenDic[SenID].ToString() + "\tUserTyped=\t" + text1.Trim() + "\tTime=\t" + txtTimeDiff + "\thitCount=\t" + hitCount + "\trawString=\t" + rowString.Trim() + "\tcharPress=\t" + (rowString.Length) + "\tKeypress=\t" + (rowString.Length + hitCount);
                        final += "\tHit Rate=\t" + Convert.ToDouble((hitCount / Convert.ToDouble(NW)) * 100);
                        final += "\tSaving=\t" + Convert.ToDouble(SenDic[SenID].ToString().Trim().Length - (rowString.Length + hitCount));
                        
                        final += "\tNC=\t" + SenDic[SenID].ToString().Trim().Length;

                      
                        final += "\tNW=\t" + NW;
                        
                        
                        final += "\tWPM=\t" + Convert.ToDouble(NW)*60 / Convert.ToDouble(dt.Minute * 60 + dt.Second);
                        final += "\tCPS=\t" + Convert.ToDouble(SenDic[SenID].ToString().Trim().Length) * 60 / Convert.ToDouble(dt.Minute * 60 + dt.Second);
                        final += "\tKSP=\t" + Convert.ToDouble(SenDic[SenID].ToString().Trim().Length - (rowString.Length + hitCount)) / Convert.ToDouble(SenDic[SenID].ToString().Trim().Length);
                        
                        
                        //MessageBox.Show(SenDic[SenID].ToString());
                       
                        arr1.Add(final);
                        richTextBox1.Text += final;
                    }
                    startIndex = nextIndex+1;
                }
            }
            catch (Exception ee)
            { }
            //richTextBox1.Text += value;
            //richTextBox1.Text += final;
            string toPrint = richTextBox1.Text;
            Log_data(toPrint);
            MessageBox.Show("done"); 
        }
        public static int CountChar(string input, char c)
        {
            int retval = 0;
            for (int i = 0; i < input.Length; i++)
                if (c == input[i])
                    retval++;
            return retval;
        }
        private static void Log_data(string toPrint)
        {
            string file_path = @"..\..\..\..\N-Grams\log_key_press\testing.txt";
             

            File.AppendAllText(file_path, toPrint, Encoding.Unicode);
        }

        Dictionary<string, string> SenDic = new Dictionary<string, string>();
        //Dictionary<string, string> Dic_Tri_new = new Dictionary<string, string>();

        public void button1_Click(object sender, EventArgs e)
        {
            //iniSentenceID();
            //processing_Darpa();

            //TRI_GRAM_EXT();
            //button1.Text = "tri";

            //BI_GRAM_EXT();
            //button1.Text = "bi";
            //UNI_GRAM_EXT();
            //button1.Text = "uni";


            //Arpa_prob();
            string st = "उत्तर";
            st = Regex.Replace(st, "्", "");
            string tex = removedupes(st);
            MessageBox.Show(tex);

            int text = 0;


        }

        private void Arpa_prob()
        {
            string text_sample = "मन्दिर का स";           //प       "यह पठार अ";
            List<string> list = new List<string>();
            string[] word_list_all = new string[65005];
            ArrayList final_prob_smooth = new ArrayList();

            int i = -1;
            ArrayList arr_word = new ArrayList();
            string str = text_sample;
            string n_1 = "";
            string n_2 = "";
            string n_3 = "";

            string[] wordarray = Regex.Split(str, " ");

            if (wordarray.Length > 0)
            {
                n_1 = wordarray[wordarray.Length - 1];
            }
            if (wordarray.Length > 1)
            {
                n_2 = wordarray[wordarray.Length - 2];
            }
            if (wordarray.Length > 2)
            {
                n_3 = wordarray[wordarray.Length - 3];
            }





            foreach (var pair in Dic_Uni_new)
            {
                i++;
                list.Add(pair.Key.ToString());

            }
            try
            {
                string strL = n_1;
                int len = strL.Length;

                var earlyBirdQuery = from word in list where word.Substring(0, len) == strL select word;
                foreach (var v in earlyBirdQuery)
                {
                    arr_word.Add(v.ToString());

                }
            }
            catch (Exception ee)
            { }


            ArrayList arr_all_unique_word = new ArrayList();
          

            foreach( KeyValuePair<string, string> data in Dic_Uni_new )  
            {
                arr_all_unique_word .Add(data.Key);  
            }

            string Tri_value = "";
            string bi_value = "";
            string uni_value = "";

            string cmp_string = "";
            string prb = "";
            //foreach (string obj in arr_word)
            foreach (string obj in arr_all_unique_word)
            {
                //string obj = Dic_Uni_new.Keys.ToString();
                cmp_string = n_3 + " " + n_2 + " " + obj;
                if (Dic_Tri_new.TryGetValue(cmp_string, out Tri_value))
                {
                    prb = Tri_value;
                    final_prob_smooth.Add(obj + " " + prb);
                }
                else
                {

                    cmp_string = n_2 + " " + obj;
                    if (Dic_Bi_new.TryGetValue(cmp_string, out bi_value))
                    {
                        string v1 = bi_value.Substring(0, bi_value.IndexOf("\t"));
                        string v2 = bi_value.Substring(bi_value.IndexOf("\t") + 1);
                        prb = (Convert.ToDouble(v1) + Convert.ToDouble(v2)).ToString();
                        final_prob_smooth.Add(obj + " " + prb);
                    }
                    else
                    {
                        cmp_string = obj;
                        if (Dic_Uni_new.TryGetValue(cmp_string, out uni_value))
                        {
                            string v1 = uni_value.Substring(0, uni_value.IndexOf("\t"));
                            string v2 = uni_value.Substring(uni_value.IndexOf("\t") + 1);
                            prb = (Convert.ToDouble(v1) + Convert.ToDouble(v2)).ToString();
                            final_prob_smooth.Add(obj + " " + prb);
                        }
                    }
                }
            }
            int chk = 0;
        }

        private static void processing_Darpa()
        {
            string filePath = @"..\..\..\..\N-Grams\data\d.arpa";

            string total_text = File.ReadAllText(filePath, Encoding.UTF8);
            // string[] uniqWord = Regex.Split(total_text, "\n");
            int index_unigram = 0;
            int index_bigram = 0;
            //int index_trigram = 0;

            index_unigram = total_text.IndexOf(@"\2-grams:");
            index_bigram = total_text.IndexOf(@"\3-grams:");


            string uni_text = total_text.Substring(0, index_unigram);
            string bi_text = total_text.Substring(index_unigram, index_bigram - index_unigram);
            string tri_text = total_text.Substring(index_bigram, total_text.Length - index_bigram);

            string output_file_uni = @"..\..\..\..\N-Grams\data\new_output_uni.txt";
            File.AppendAllText(output_file_uni, uni_text, Encoding.Unicode);

            string output_file_bigram = @"..\..\..\..\N-Grams\data\new_output_bi.txt";
            File.AppendAllText(output_file_bigram, bi_text, Encoding.Unicode);

            string output_file_trigram = @"..\..\..\..\N-Grams\data\new_output_tri.txt";
            File.AppendAllText(output_file_trigram, tri_text, Encoding.Unicode);

            MessageBox.Show("done");
        }

       Dictionary<string, string> Dic_Tri_new = new Dictionary<string, string>();
         Dictionary<string, string> Dic_Bi_new = new Dictionary<string, string>();
         Dictionary<string, string> Dic_Uni_new = new Dictionary<string, string>();

        private  void TRI_GRAM_EXT()
        {
           
            string filePath = @"..\..\..\..\N-Grams\data\new_output_tri.txt";
            
            if (File.Exists(filePath))
            {
                              
                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] uniqWord = Regex.Split(total_text, "\n");
                foreach (string obj in uniqWord)
                {
                    if (obj != "")
                    {
                        try
                        {
                            String[] key_value = obj.Split(' ');
                            if(key_value.Length >4)
                            {
                                string word_key = key_value[1].ToString() + " " + key_value[2].ToString() + " " + key_value[3].ToString();
                                string prob_key = key_value[0].ToString();
                                                             
                                    if(word_key !="")
                                    {
                                        if (!Dic_Tri_new.ContainsKey(word_key))
                                            Dic_Tri_new.Add(word_key, prob_key);
                                    }
                            }

                        }
                        catch (Exception ee)
                        { }
                    }
                }

            }               
        }

        private  void BI_GRAM_EXT()
        {

            string filePath = @"..\..\..\..\N-Grams\data\new_output_bi.txt";

            if (File.Exists(filePath))
            {
               
                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] uniqWord = Regex.Split(total_text, "\n");
                foreach (string obj in uniqWord)
                {
                    if (obj != "")
                    {
                        try
                        {
                            String[] key_value = obj.Split(' ');
                            if (key_value.Length > 3)
                            {
                                string word_key = key_value[1].ToString() +" "+ key_value[2].ToString();
                                string prob_key = key_value[0].Trim().ToString()+"\t"+ key_value[3].Trim().ToString();

                                if (word_key != "")
                                {
                                    if (!Dic_Bi_new.ContainsKey(word_key))
                                        Dic_Bi_new.Add(word_key, prob_key);
                                }
                            }

                        }
                        catch (Exception ee)
                        { }
                    }
                }

            }
            
        }
        private  void UNI_GRAM_EXT()
        {

            string filePath = @"..\..\..\..\N-Grams\data\new_output_uni.txt";

            if (File.Exists(filePath))
            {
                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] uniqWord = Regex.Split(total_text, "\n");
                foreach (string obj in uniqWord)
                {
                    if (obj != "")
                    {
                        try
                        {
                          string obj1= obj.Replace("\t", " ");
                            String[] key_value = obj1.Split(' ');
                            if (key_value.Length > 2)
                            {
                                string word_key = key_value[1].ToString();
                                string prob_key = key_value[0].Trim().ToString() + "\t" + key_value[2].Trim().ToString();

                              
                                if (word_key != "")
                                {
                                    if (!Dic_Uni_new.ContainsKey(word_key))
                                        Dic_Uni_new.Add(word_key, prob_key);
                                }
                            }

                        }
                        catch (Exception ee)
                        { }
                    }
                }

            }
            
        }
        private  void iniSentenceID()
        {
            //string file_w = @"..\..\..\..\N-Grams\log_key_press\sentenceID.txt";
            //string total_text_w = File.ReadAllText(file_w, Encoding.UTF8);
            //string[] wordDic_new_w = Regex.Split(total_text_w, "\n");



            if (File.Exists(@"..\..\..\..\N-Grams\log_key_press\sentenceID.txt"))
            {
                StreamReader str = new StreamReader(@"..\..\..\..\N-Grams\log_key_press\sentenceID.txt", Encoding.UTF8);
                while (str.Peek() != -1)
                {
                    String line = str.ReadLine();    //read each line and load it in dictionary
                    String[] key_value = line.Split('\t');
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        try
                        {
                            if (!SenDic.ContainsKey(key_value[0]))
                                SenDic.Add(key_value[0], key_value[1].Trim());
                        }
                        catch (Exception ee)
                        { }
                    }
                }
            }
        }
        string removedupes(string s)
        {
            string newString = string.Empty;
            List<char> found = new List<char>();
            foreach (char c in s)
            {
                if (found.Contains(c))
                    continue;

                newString += c.ToString();
                found.Add(c);
            }
            return newString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string input = "क्ष".Trim();//"ॻ".Trim();// "हिंदी हिन्दी";//"हिन्दी";     अा      आ    //क़    क़

            //string data2 = "क्ष".Trim();//"ग॒".Trim();

            //string input = "\u0915"+"\u094d"+"\u0937";
            string input = "\u0915" + "\u094d" + "\u200c" + "\u0937";
            //string data2 = "\u0915" + "\u094d" +"\u200c"+ "\u0937";
            string data2 = "\u0915" + "\u094d" + "\u200d" + "\u0937";
            //if (input== data2)
            if (input.Normalize() == data2.Normalize())
            {
                MessageBox.Show("yes");
            }

            richTextBox1.Text += input +"\tlen=" +input.Length+ "\t"+data2.Normalize()+"\tlen="+data2.Length+"\r\n";

            string val2 = input.Normalize();              
            Console.WriteLine(val2);
            richTextBox1.Text += "Normalize\t"+ val2 + "\tlen=" + val2.Length + "\t" + data2.Normalize() + "\tlen=" + data2.Length + "\r\n";

            string val3 = input.Normalize(NormalizationForm.FormD);
            Console.WriteLine(val3);
            richTextBox1.Text += "FormD\t" + val3 + "\tlen=" + val3.Length + "\t" + data2.Normalize() + "\tlen=" + data2.Length + "\r\n";

            string val4 = input.Normalize(NormalizationForm.FormKC);
            Console.WriteLine(val4);
            richTextBox1.Text += "FormKC\t" + val4 + "\tlen=" + val4.Length + "\t" + data2.Normalize() + "\tlen=" + data2.Length + "\r\n";

            string val5 = input.Normalize(NormalizationForm.FormKD);
            Console.WriteLine(val5);
            richTextBox1.Text += "FormKD\t" + val5 + "\tlen=" + val5.Length + "\t" + data2.Normalize() + "\tlen=" + data2.Length + "\r\n";

            string val6 = input.Normalize(NormalizationForm.FormC);
            Console.WriteLine(val6);
            richTextBox1.Text += "FormC\t" + val6 + "\tlen=" + val6.Length + "\t" + data2.Normalize() + "\tlen=" + data2.Length + "\r\n";


            msg(val2, data2);
            msg(val3, data2);
            msg(val4, data2);
            msg(val5, data2);
            msg(val6, data2);
            MessageBox.Show("over");
            
            //byte[] utf8String = Encoding.UTF8.GetBytes("\u9015");
            //MessageBox.Show(BitConverter.ToString(utf8String));


        }
        private void msg(string s1, string s2)
        {
            if (s1 == s2)
            {
                MessageBox.Show("yes");
            }
        }
    }
}
