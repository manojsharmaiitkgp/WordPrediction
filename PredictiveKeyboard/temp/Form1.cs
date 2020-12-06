#define Regency_Of_word
#define Make_Corr_Bigram
#define Typing1
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Net.Sockets;
using System.IO;


namespace PredictiveKeyboard
{

    public partial class Form1 : Form
    {
    #region old_dic
        Dictionary<string, int> unigram_word = new Dictionary<string, int>();
        Dictionary<string, int> bigram_word = new Dictionary<string, int>();
        Dictionary<string, int> bigram_word_tag = new Dictionary<string, int>();


        Dictionary<string, decimal> Prob_a = new Dictionary<string, decimal>();
        Dictionary<string, string> Prob_a_b = new Dictionary<string, string>();
        Dictionary<string, string> Prob_a_bc = new Dictionary<string, string>();

        Dictionary<string, string> Prob_Wi_ti1_ti2 = new Dictionary<string, string>();
        Dictionary<string, string> Prob_Wi_Wi1_ti1_ti2 = new Dictionary<string, string>();
        Dictionary<string, string> Prob_alpha_Wi_Wi1_ti1_ti2 = new Dictionary<string, string>();
        Dictionary<string, string> Prob_a_b_new = new Dictionary<string, string>();
        Dictionary<string, int> list_of_word = new Dictionary<string, int>();
        Dictionary<string, string> EBX = new Dictionary<string, string>();
        Dictionary<string, string> Loc_dic = new Dictionary<string, string>();
        Dictionary<string, string> prob_list = new Dictionary<string, string>();
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        
  #endregion

        Dictionary<string, string> Dic_Tri_new = new Dictionary<string, string>();
        Dictionary<string, string> Dic_Bi_new = new Dictionary<string, string>();
        Dictionary<string, string> Dic_Uni_new = new Dictionary<string, string>();

              

        public Form1()
        {
            InitializeComponent();            
        }
        int windowSize = 7;
      
        private void Form1_Load(object sender, EventArgs e)
        {
            this.btnSp014.Click += new System.EventHandler(this.int_independent_vowel);
            Load_Dictionary dictionaries = new Load_Dictionary();
            dictionaries.load_dictionaries(
                                           //unigram_word,
                                           //bigram_word,
                                           //bigram_word_tag,
                                           //Prob_a, Prob_a_b, Prob_a_bc,
                                           //Prob_Wi_ti1_ti2,
                                           //Prob_alpha_Wi_Wi1_ti1_ti2 ,
                                           //EBX,
                                           Dic_Tri_new,
                                           Dic_Bi_new,
                                           Dic_Uni_new 
                                          );
                                           //Prob_Wi_ti1_ti2, Prob_Wi_Wi1_ti1_ti2, Prob_alpha_Wi_Wi1_ti1_ti2); 
            Load_Sound_path();
            button1.Enabled = false;
            button55.Enabled = false;
            button56.Enabled = false;
            FolderPathName = System.Environment.CurrentDirectory.ToString ();
            load_regency();
            string toPrint = "\r\n" + System.DateTime.Now.ToString() + "\r\n";
            Log_data(toPrint);
            final_prob_smooth = new ArrayList();
        }
        public static List<string> subdirectory(string input_path)    /* Get list of all files in directories & subdirectories */
        {
            List<string> result = new List<string>();    /* store lists of files in all directories */
            Queue<string> queue = new Queue<string>();   /* Store a stack of subdirectories */
            queue.Enqueue(input_path);
            while (queue.Count > 0)       /* continue while there are directories to process */
            {
                string dir = queue.Dequeue();  /* Get topmost directory on stack */
                try
                {
                    result.AddRange(Directory.GetFiles(dir, "*.*"));     // Add all files at this directory to the result List
                    foreach (string subdir in Directory.GetDirectories(dir))
                    {
                        queue.Enqueue(subdir);    /* push sub directories at this directory */
                    }
                }
                catch
                {
                }
            }
            return result;
        }
        
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (flagClear == false)
            {
                  
                //check_for_disable_button();
                //Re_set_back_color();
                //Next_char_Highlighter();//enable char level prediction      
                String text = richTextBox1.Text.ToString();    
                if (text.Trim().Length == 0)
                {
#if Typing
                    string toPrint = "\r\nStart Time:\t" + System.DateTime.Now.ToString() + "\r\n";
                    Log_data(toPrint);
#endif
                }
                if (text.IndexOf("\u0964") != -1)
                    text = text.Substring(text.LastIndexOf("\u0964") + 1);
                //text = "$" + text.TrimStart();    
                string status = toolStripStatusLabel3.Text.Trim() + "_" + toolStripStatusLabel2.Text.ToString();
                //status = "Word Level_Unigram";//remove this to allow context based application
                //status = "Word Level_Tri-gram";
                //status = "Word Level_Bi-gram";
                status = "new Arpa Tri-gram";
                switch (status)
                {
                    
                    case "Word Level_Unigram":
                        //Word_Level_Unigram(text);
                        try
                        {
                            isolated_word(text);

                        }
                        catch (Exception ee)
                        { }
                        break;

                    case "Word Level_Bi-gram":
                        Word_Level_Bi_gram(text);
                        break;
                
                    case "new Arpa Tri-gram":
                        arpa_ngram(text);
                        break;

                    default:
                        Word_Level_Tri_gram(text);
                        break;
                }

                //underline();

                //Calling_EBX(text);
            }
            else
            {
                //listBox1.Items.Clear();
                //word_coll.Clear();
 
            }
          
        }
       
        ArrayList final_prob_smooth;
       
        private void arpa_ngram(string text)
        {
            final_prob_smooth = new ArrayList();
            Arpa_prob(text);
            //listBox1.Items.Clear();
            word_coll.Clear();
            string data_only = "";

            int count = 0;
            listBox1.Items.Clear();
            
            foreach (string ob in final_prob_smooth)
            {
                count++;
                if (count <= windowSize)
                {
                    string dummy = ob.Replace(" ", "[");
                    data_only = dummy.Substring(dummy.IndexOf("[") + 1);
                    word_coll.Add(ob);

                    listBox1.Items.Add(data_only);
                }
                else
                {
                   
                    break;
                }
            }
          
        }
        private void Arpa_prob(string text_sample)
        {
            //string text_sample = "<UNK> <UNK> अ";           //प       "यह पठार अ";
            List<string> list = new List<string>();
            string[] word_list_all = new string[65005];
             //final_prob_smooth = new ArrayList();

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
                //i++;
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
          
            Decider_my_Filter(n_1, arr_all_unique_word);

            string Tri_value = "";
            string bi_value = "";
            string uni_value = "";

            string cmp_string = "";
            string prb = "";
           
            if (n_2 == "")
            {
                n_2 = "<UNK>";
                n_3 = "<UNK>";
            }
            foreach (string obj in arr_all_unique_word)
            {
                             
                cmp_string = n_3 + " " + n_2 + " " + obj;
                if (Dic_Tri_new.TryGetValue(cmp_string, out Tri_value))
                {
                    prb = Tri_value;
                    Decider_my_method(n_1, prb, obj);

                }
                else
                {    

                    cmp_string = n_2 + " " + obj;
                    if (Dic_Bi_new.TryGetValue(cmp_string, out bi_value))
                    {
                        string v1 = bi_value.Substring(0, bi_value.IndexOf("\t"));
                        string v2 = bi_value.Substring(bi_value.IndexOf("\t") + 1);
                        prb = (Convert.ToDouble(v1) + Convert.ToDouble(v2)).ToString();
                        Decider_my_method(n_1, prb, obj);
                    }
                    else
                    {
                        cmp_string = obj;
                        if (Dic_Uni_new.TryGetValue(cmp_string, out uni_value))
                        {
                            string v1 = uni_value.Substring(0, uni_value.IndexOf("\t"));
                            string v2 = uni_value.Substring(uni_value.IndexOf("\t") + 1);
                            prb = (Convert.ToDouble(v1) + Convert.ToDouble(v2)).ToString();
                            Decider_my_method(n_1, prb, obj);
                        }
                    }
                }
            }          



            final_prob_smooth.Sort();
           
        }

        private void For_Correct(string n_1, ArrayList arr_all_unique_word)
        {
            string chk = "";
            foreach (KeyValuePair<string, string> data in Dic_Uni_new)
            {
                if (data.Key.ToString() != "" && data.Key.Length >= n_1.Length && data.Key.ToString().Length > 1 && data.Key.ToString() != "<EOF>" && data.Key.ToString() != "<UNK>" && data.Key.ToString() != "<START>" && "॰" != data.Key.Substring(0, 1) && "ं" != data.Key.Substring(0, 1) && "ः" != data.Key.Substring(0, 1))
                {
                    chk = data.Key.Substring(0, n_1.Length);
                                        
                        if (chk == n_1)                       
                        {
                            arr_all_unique_word.Add(data.Key);
                        }
                   
                }
            }
        }
 

        private void For_Error(string n_1, ArrayList arr_all_unique_word)
        {
            string chk = "";
            foreach (KeyValuePair<string, string> data in Dic_Uni_new)
            {
                if (n_1.Length > 0 || n_1 == " ")
                {
                    if (data.Key.ToString() != "" && data.Key.Length >= n_1.Length && data.Key.ToString().Length > 1 && data.Key.ToString() != "<EOF>" && data.Key.ToString() != "<UNK>" && data.Key.ToString() != "<START>" && "॰" != data.Key.Substring(0, 1) && "ं" != data.Key.Substring(0, 1) && "ः" != data.Key.Substring(0, 1))
                    {
                        chk = data.Key.Substring(0, 1);
                        
                        string source = n_1.Substring(0, 1);
                        string seq_source1 = Cal_phonatic_Seq(chk);
                        string seq_source2 = Cal_phonatic_Seq(source);
                        if (seq_source1 == seq_source2)//if (chk == n_1.Substring(0, 1))       //if some error occure comment previous 4 statement and uncomment this 
                        {
                            arr_all_unique_word.Add(data.Key);
                        }

                    }
                }
                else
                {
                    if (arr_all_unique_word.Count == 0)
                    {
                        foreach (KeyValuePair<string, string> data1 in Dic_Uni_new)
                        {
                            if (data1.Key.ToString() != "" && data1.Key.Length >= n_1.Length && data1.Key.ToString().Length > 1 && data1.Key.ToString() != "<EOF>" && data1.Key.ToString() != "<UNK>" && data1.Key.ToString() != "<START>" && "॰" != data1.Key.Substring(0, 1) && "ं" != data1.Key.Substring(0, 1) && "ः" != data1.Key.Substring(0, 1))
                            {
                                chk = data1.Key.Substring(0, n_1.Length);

                                string seq_source1 = Cal_phonatic_Seq(chk);
                                string seq_source2 = Cal_phonatic_Seq(n_1);
                                if (seq_source2 == seq_source1)
                                //if (chk == n_1)        //if some error occure comment previous 4 statement and uncomment this 
                                {
                                    arr_all_unique_word.Add(data1.Key);
                                }

                            }
                        }
                    }
                }
            }
            int ch = 0;
        }

        private void Decider_my_Filter(string n_1, ArrayList arr_all_unique_word)
        {
            For_Correct(n_1, arr_all_unique_word);
            //For_Error(n_1, arr_all_unique_word);
        }
        private void Decider_my_method(string n_1, string prb, string obj)
        {
            final_prob_smooth.Add(prb + "[" + obj);
            //Error_Handling(ref final_prob_smooth, n_1, obj, prb);
        }

        private void Error_Handling(ref ArrayList final_prob_smooth, string typedString, string targetString, string frq)
        {
            string StrWordFrequency = frq;
            double EDscr = 0.0f;

            double PScore = 0.0f;

            string strEDscr = "";


            string str_obj = targetString;


            PScore = Math.Round(Cal_phonatic_method(typedString, targetString), 3);
            strEDscr = EDscr.ToString();
            if (strEDscr == "0")
            {
                strEDscr = "0.0";
            }

            
            double lamda1 = 0.0f;
            double lamda2 = 0.0f;
            double lamda3 = 0.0f;
            string Score = "";
            //double Score = 0.0f;

            lamda1 = 2.504;
            lamda2 = 2.959;
            lamda3 = 3.0435;

            //Score = System.Convert.ToDouble(PScore) * lamda1 + System.Convert.ToDouble(strEDscr) * lamda2 + Math.Abs(System.Convert.ToDouble(StrWordFrequency) * lamda3);

            Score = PScore.ToString() + "\t" + strEDscr.ToString() + "\t" + (Math.Abs(Convert.ToDouble(StrWordFrequency))).ToString() + "\t";


            //string output_text = "P=" + Score + "\t[" + str_obj + "]";
            string output_text = "P=" + Score + "\t[" + str_obj;

            final_prob_smooth.Add(output_text);
        }
      
        
   #region Required proposed error tech

        private string Cal_phonatic_Seq(string source)
        {
            string str = "";
            foreach (char obj in source)
            {
                switch (obj)
                {
                    case 'क':
                    case 'ख':
                        str += 'क'.ToString();
                        break;

                    case 'ग':
                    case 'घ':
                        str += 'ग'.ToString();
                        break;

                    case 'च':
                    case 'छ':
                        str += 'च'.ToString();
                        break;

                    case 'ज':
                    case 'झ':
                        str += 'ज'.ToString();
                        break;


                    case 'ट':
                    case 'ठ':
                        str += 'ट'.ToString();
                        break;

                    case 'ड':
                    case 'ढ':
                        str += 'ड'.ToString();
                        break;

                    case 'त':
                    case 'थ':
                        str += 'त'.ToString();
                        break;

                    case 'द':
                    case 'ध':
                        str += 'द'.ToString();
                        break;


                    case 'प':
                    case 'फ':
                        str += 'प'.ToString();
                        break;



                    case 'ब':
                    case 'भ':
                        str += 'ब'.ToString();
                        break;

                    case 'स':
                    case 'श':
                    case 'ष':
                        str += 'स'.ToString();
                        break;

                    case 'न':
                    case 'ण':
                    case 'ञ':
                    case 'ङ':
                        str += 'न'.ToString();
                        break;



                    case 'अ':
                    case 'आ':
                        str += 'अ'.ToString();
                        break;


                    case 'इ':
                    case 'ई':
                        str += 'इ'.ToString();
                        break;


                    case 'उ':
                    case 'ऊ':
                        str += 'उ'.ToString();
                        break;



                    case 'ए':
                    case 'ऐ':
                        str += 'ए'.ToString();
                        break;


                    case 'ओ':
                    case 'औ':
                        str += 'ओ'.ToString();
                        break;


                    case 'ी':
                    case 'ि':
                        str += 'ी'.ToString();
                        break;


                    case 'ु':
                    case 'ू':
                        str += 'ु'.ToString();
                        break;


                    case 'े':
                    case 'ै':
                        str += 'े'.ToString();
                        break;


                    case 'ो':
                    case 'ौ':
                        str += 'ो'.ToString();
                        break;

                    /*
                case 'ं':
                case 'ः':
                    str += 'ं'.ToString();
                    break;*/

                    case 'ं':
                        // case '्':
                        str += 'ं'.ToString();
                        break;

                    case 'ॅ':
                    case 'ँ':
                        str += 'ॅ'.ToString();
                        break;
                    /*
                        
                case '्':
                   
                    str += "";
                    break;*/

                    default:
                        str += obj;
                        break;

                }

            }
            return str;
        }


        private float Cal_phonatic_method(string source, string target)
        {
            string seq_source = "";
            string seq_target = "";
            float PEDscr = 0.0f;

            seq_source = Cal_phonatic_Seq(source);
            seq_target = Cal_phonatic_Seq(target);

            PEDscr = Cal_EDScore_Method(seq_source, seq_target);
            //PEDscr = seq_source.Length - LCS(seq_source, seq_target);




            //  PEDscr = LCS(seq_source, seq_target);

            // phonatic_value = Cal_EDScore_Method(seq_source, seq_target);
            return PEDscr;

        }

        public static int min(int a, int b, int c)
        {
            int val = 0;
            if (a < b)
            {
                if (a < c)
                {
                    val = a;
                }
                else
                {
                    val = c;
                }
            }
            else
            {
                //b<a
                if (b < c)
                {
                    val = b;
                }
                else
                {
                    val = c;
                }
            }
            return val;
        }

        public static int myCostChecker(string str1, string str2)
        {
            char[] arr1 = str1.ToCharArray();
            char[] arr2 = str2.ToCharArray();

            int m = arr1.Length;
            int n = arr2.Length;
            int[,] d = new int[m + 1, n + 1];
            int cost;
            int x;
            int y;
            int z;
            int retCostDist = 0;

            try
            {
                for (int i = 0; i <= m; i++)
                {
                    d[i, 0] = i;
                }
                for (int j = 0; j <= n; j++)
                {
                    d[0, j] = j;
                }
                for (int i = 1; i <= m; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (arr1[i - 1] == arr2[j - 1])
                        {
                            cost = 0;

                        }
                        else
                        {
                            cost = 1;
                        }
                        x = d[i - 1, j] + 1;
                        y = d[i, j - 1] + 1;
                        z = d[i - 1, j - 1] + cost;


                        d[i, j] = min(x, y, z);
                        // 
                        //for transpose errrr but it is not sutible for deletin error and should be checked again
                        if (i > 1 && j > 1 && arr1[i - 1] == arr2[j - 2] && arr1[i - 2] == arr2[j - 1])
                        {
                            d[i, j] = min(d[i, j], d[i - 2, j - 2] + cost, 0);

                        }
                        // end of transpose error
                        retCostDist = d[i, j];
                    }



                }
            }
            catch (Exception ee)
            {

            }

            return retCostDist;


        }

        public static int LCS(string str1, string str2)
        {
            char[] arr1 = str1.ToCharArray();
            char[] arr2 = str2.ToCharArray();
            int m = arr1.Length;
            int n = arr2.Length;
            int retCostDist = 0;
            int[,] d = new int[m + 1, n + 1];
            try
            {
                for (int i = 0; i <= m; i++)
                {
                    d[i, 0] = 0;
                }

                for (int j = 0; j <= n; j++)
                {
                    d[0, j] = 0;
                }
                for (int i = 1; i <= m; i++)
                {
                    for (int j = 1; j <= n; j++)
                    {
                        if (arr1[i - 1] == arr2[j - 1])
                        {
                            d[i, j] = d[i - 1, j - 1] + 1;
                        }
                        else
                        {
                            d[i, j] = max(d[i, j - 1], d[i - 1, j]);
                        }
                        retCostDist = d[m, n];
                    }
                }
            }
            catch (Exception eee)
            {

            }
            return retCostDist;
        }
        public static int max(int val1, int val2)
        {
            if (val1 > val2)
            {
                return val1;
            }
            else
            {
                return val2;
            }
        }
        
    #endregion
       
        
        Dictionary<string, string> SenDic = new Dictionary<string, string>();
        private void iniSentenceID()
        {
           
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

        private void SimulateForPKS()
        {
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            string data = richTextBox3.Text;
            int Total_num_char = richTextBox3.Text.Length;
            string[] different_word = data.Split(' ');
          
            int count = 0;
            int keypress = 0;
            bool hit = false;
            int val1 = -1;
            string charRecord = "";
            //string toPrint = "\n-------------------------------\nsimulated result\nOriginal text:\t";
            string toPrint = "Original text:\t";
            toPrint += richTextBox3.Text + "\tTyped Text:\t";
            Log_data(toPrint);

            foreach (string word in different_word)
            {
                val1 = listBox1.Items.IndexOf(word);
                ArrayList arr1 = new ArrayList();
                foreach (string ob in listBox1.Items)
                {
                    arr1.Add(ob);
                }

                charRecord = "";

                if (val1 <= windowSize && word.Trim() != "" && val1 >= 0)
                {
                    // MessageBox.Show("hit");
                    hit = true;
                    count++;
                    keypress++;
                    // break;
                }
                else
                {

                    foreach (char ch in word)
                    {
                        hit = false;
                        richTextBox1.Text += ch.ToString();
                        toPrint = ch.ToString();
                        Log_data(toPrint);
                        charRecord += ch.ToString();
                        keypress++;

                        ArrayList arr = new ArrayList();
                        foreach (string ob in listBox1.Items)
                        {
                            arr.Add(ob);
                        }
                        val1 = listBox1.Items.IndexOf(word);
                        if (val1 <= windowSize && word.Trim() != "" && val1 >= 0)
                        {
                            // MessageBox.Show("hit");
                            hit = true;
                            count++;
                            keypress++;
                            break;
                        }

                    }
                }
                string calData = word.Substring(charRecord.Length);
                if (hit == true)
                {
                    //richTextBox1.Text += "("+word.ToString()+") ";
                    //richTextBox1.Text += word.ToString()+" ";
                    richTextBox1.Text += calData + " ";
                    toPrint = "(" + word.ToString() + ")";
                    Log_data(toPrint);
                }
                else
                {
                    richTextBox1.Text += " ";
                }
                toolStripStatusLabel2.Text = "Total hit: \t" + count.ToString() + "\tTotal keypress:\t" + keypress.ToString();
            }
            //MessageBox.Show("Total hit: \t"+count.ToString()+"\r\nTotal keypress:\t"+keypress .ToString());
            toolStripStatusLabel2.Text = "Total hit: \t" + count.ToString() + "\tTotal keypress:\t" + keypress.ToString();
            double PKS = (Total_num_char - keypress) / Convert.ToDouble(Total_num_char);
            toPrint = "\tTotal hit: \t" + count.ToString() + "\tTotal keypress:\t" + keypress.ToString() + "\tTotal Word=" + different_word.Length.ToString() + "\tPKS=" + PKS.ToString();
            toPrint += "\tTotal_num_char\t:" + Total_num_char.ToString() + "\tSavings\t" + (Total_num_char - keypress).ToString();
            toPrint += "\tHit Rate:\t" + Convert.ToDouble((count / Convert.ToDouble(different_word.Length)) * 100);
            Log_data(toPrint);
        }
        private void button61_Click(object sender, EventArgs e)
        {
            this.Text = "simulation started";
            iniSentenceID();
            int Size = 64;
            string time1 = "";
            string time2 = "";
            string txtTimeDiff = "";


            for (int j = 1; j <= 20; j++)
            {
                for (int i = 1; i <= Size; i++)
                {
                    string value = "";
                    if (SenDic.TryGetValue("sID" + i.ToString(), out value))
                    {
                        windowSize = i;
                        button61.Text = "S="+j.ToString()+"sID" + i.ToString();
                        value = SenDic["sID" + i.ToString()].ToString();
                        richTextBox3.Text = value;
                        richTextBox1.Text = "";
                        time1 = System.DateTime.Now.ToString();
                        //Log_data("\nWinSize:" + windowSize + "\t" + button61.Text + "\t");
                        Log_data("\nWinSize:" + j.ToString() + "\t" + "sID" + i.ToString() + "\t");
                        this.Refresh();
                        SimulateForPKS();

                    }
                    time2 = System.DateTime.Now.ToString();
                    txtTimeDiff = (Convert.ToDateTime(time2)).Subtract(Convert.ToDateTime(time1)).ToString();
                    Log_data("\tTime Taken:\t" + txtTimeDiff);

                }
            }
            
            MessageBox.Show("done");
        }

        ArrayList word_coll = new ArrayList();
        ArrayList color_play_btn = new ArrayList();

        private void Next_char_Highlighter()
        {
            string text = richTextBox1.Text;
            string[] words = text.Split(' ');
            ArrayList arr = new ArrayList();
            ArrayList arr_char = new ArrayList();
            string st_arr = "";
            color_play_btn.Clear();
            //foreach (string wc in word_coll)
            //{
            //    st_arr += wc+"\n";
            //}
            //string[] col_word_list = Regex.Split(st_arr,"\n");

            if (text.Trim().Length != 0)
            {

                string last_word = words[words.Length - 1];
                int len = last_word.Length;

                //var word_list = from word in col_word_list where (word.Length >= len && word.Substring(0, len) == last_word) select word;
                //var word_list = from word in unigram_word where (word.Key.Length >= len && word.Key.Substring(0, len) == last_word) select word.Key;
                //foreach (string wc in listBox1.Items)
                //{
                //    word_coll.Add(wc);
                //}

                //foreach (string word in word_list)
                foreach (string word in word_coll)
                {
                    if (word.StartsWith("$"))
                    {
                        arr.Add(word.Remove(0, 1));
                    }
                    else
                    {
                        arr.Add(word);
                        if ((word.Length >= len + 1) && (arr_char.Count < 7))
                        {
                            if (!arr_char.Contains(word.Substring(len, 1)))
                            {
                                arr_char.Add(word.Substring(len, 1));
                                //color_play_btn.Add(last_word + word.Substring(len, 1));
                            }
                        }
                    }
                }

                Setting_back_color(arr_char);
                //foreach (object play_btn in panel4.Controls)
                //{
                //    if (play_btn is Button)
                //    {
                //        try
                //        {
                //         //   function_individual_button_play_button(arr, color_play_btn, Color.White, play_btn, last_word);
                //        }
                //        catch (Exception ee)
                //        { }
                //    }

                //}
                int a = 0;

            }
        }

        private void Setting_back_color(ArrayList arr_char)
        {
            Color n_color = Color.Red;
            ArrayList arr_matra = new ArrayList();
            foreach (object pnl in panel13.Controls)
            {
                if (pnl is Panel)
                {

                }
                if (pnl is Button)
                {
                    n_color = function_individual_button(arr_char, n_color, pnl);

                }
            }
            foreach (object pnl in panel12.Controls)
            {
                if (pnl is Panel)
                {

                }
                if (pnl is Button)
                {
                    if (old_char_selected == "")
                    {
                        // old_char_selected = "a";
                        n_color = function_individual_button(arr_char, n_color, pnl);
                    }
                    else
                    {
                        foreach (string st in arr_char)
                        {
                            arr_matra.Add(old_char_selected + st);

                        }
                        //foreach (string st in arr_char)
                        {
                            //if (((Button)pnl).Text == st)
                            {
                                n_color = function_individual_button(arr_matra, n_color, pnl);
                            }
                        }
                    }



                }
            }

        }

        ArrayList color_array_button = new ArrayList();

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                int ind_value = richTextBox1.Text.LastIndexOf(' ');
                string val = listBox1.SelectedItem.ToString();
                if (ind_value != -1)
                {
                    string new_text = richTextBox1.Text.Substring(0, ind_value + 1);
                    richTextBox1.Text = new_text + val + " ";
                    string toPrint = "(" + val + ")";
                    Log_data(toPrint);
                }
                else
                {
                    richTextBox1.Text = val + " ";
                    string toPrint = "(" + val + ")";
                    Log_data(toPrint);
                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  Load_Sound_path();
            //int chk = 0;

            //string selected_text = listBox1.SelectedItem.ToString().Trim();
            //if (list_of_file_name.ContainsKey(selected_text))
            //{
            //    MessageBox .Show ("file");
            //}
            //int chk = 0;
            //Re_set_back_color();

            //Next_char_Highlighter();

            //remote_connection();

        }


        static string old_char_selected = "";
        bool flag_runTime_button_check = false;

        private void addToTextBox(object sender, EventArgs e)
        {
            panel12.Enabled = true;
            // Re_set_back_color();
            Spacebar_flag = false;

            if (flag_runTime_button_check == false)
            {
                foreach (object obj in panel12.Controls)
                {
                    ((Button)obj).Enabled = true;
                }
            }

            richTextBox1.Text += ((Button)sender).Text;

            string baseChar = ((Button)sender).Text;
            RunTimeGeneration(baseChar);

            old_char_selected = ((Button)sender).Text;

            Re_set_back_color();
            Next_char_Highlighter();

            string toPrint = ((Button)sender).Text;
            Log_data(toPrint);
        }

        private void RunTimeGeneration(string baseChar)
        {
            Initilisation_Dependent_Vovel_Button.Text = baseChar;
            btnIV01.Text = baseChar + "ा";
            btnIV02.Text = baseChar + "ि";
            btnIV03.Text = baseChar + "ी";
            btnIV04.Text = baseChar + "ु";
            btnIV05.Text = baseChar + "ू";
            btnIV10.Text = baseChar + "ृ";
            button54.Text = baseChar + "ॢ";
            btnIV06.Text = baseChar + "े";
            btnIV07.Text = baseChar + "ै";
            btnIV08.Text = baseChar + "ो";
            btnIV09.Text = baseChar + "ौ";

            btnIV15.Text = baseChar + "ँ";
            btnIV16.Text = baseChar + "्";
            btnIV12.Text = baseChar + "ं";
            btnIV13.Text = baseChar + "ः";
            btnIV15.Text = baseChar + "ँ";
            btnIV14.Text = baseChar + "़";


            button58.Text = baseChar + "ॅ";
            button59.Text = baseChar + "ॆ";


            button60.Text = baseChar + "ॉ";
            button17.Text = baseChar + "ॊ";

            btnIV11.Text = baseChar + "्र";
        }

        private static void Log_data(string toPrint)
        {
            string file_path = @"..\..\..\..\N-Grams\log_key_press\log_file.txt";

            File.AppendAllText(file_path, toPrint, Encoding.Unicode);
        }
       
        private void run_time_text(object sender, EventArgs e)
        {

            string name = ((Button)sender).Name.ToString();
            string value = "";
            switch (name)
            {
                case "btnIV01":
                    value = "ा";
                    break;
                case "btnIV02":
                    value = "ि";
                    break;
                case "btnIV03":
                    value = "ी";
                    break;
                case "btnIV04":
                    value = "ु";
                    break;
                case "btnIV05":
                    value = "ू";
                    break;
                case "btnIV06":
                    value = "े";
                    break;
                case "btnIV07":
                    value = "ै";
                    break;
                case "btnIV08":
                    value = "ो";
                    break;
                case "btnIV09":
                    value = "ौ";
                    break;
                case "btnIV10":
                    value = "ृ";
                    break;
                case "btnIV11":
                    value = "्र";
                    break;

                case "btnIV12":
                    value = "ं";
                    break;
                case "btnIV13":
                    value = "ः";
                    break;
                case "btnIV14":
                    value = "़";
                    break;
                case "btnIV15":
                    value = "ँ";
                    break;
                case "btnIV16":
                    value = "्";
                    break;
                case "button58":
                    value = "ॅ";
                    break;
                case "button59":
                    value = "ॆ";
                    break;
                case "button60":
                    value = "ॉ";
                    break;
                case "button54":
                    value = "ॢ";
                    break;
                default:
                    break;
            }
            richTextBox1.Text += value;

            string toPrint = value;
            Log_data(toPrint);
            //richTextBox1.Text += ((Button)sender).Text;

        }
       
        private void int_independent_vowel(object sender, EventArgs e)
        {
            richTextBox1.Text += ((Button)sender).Text;

            string toPrint = ((Button)sender).Text;
            Log_data(toPrint);

            //foreach (object obj in panel12.Controls)
            //{
            //    ((Button)obj).Enabled = false;
            //}
            //flag_runTime_button_check = false;
            Re_set_back_color();
            Next_char_Highlighter();

        }

        bool flagClear = false;
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            //flagClear = true;
            //panel12.Enabled = false;

            richTextBox1.Text = "";
            string toPrint = "\r\nStop Time:\t" + System.DateTime.Now.ToString() + "\r\n";
            Log_data(toPrint);
            listBox1.Items.Clear();

        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            string str = richTextBox1.Text;
            if (str.Length > 0)
            {
                richTextBox1.Text = str.Substring(0, str.Length - 1);
            }

            string file_path = @"..\..\..\..\N-Grams\log_key_press\log_file.txt";
            string toPrint = "B";

            File.AppendAllText(file_path, toPrint, Encoding.Unicode);
            try
            {

                string value1 = richTextBox1.Text.Substring(richTextBox1.Text.Length - 1);

                int index1 = value1.IndexOfAny(new char[] { 'ा', 'ि', 'ी', 'ु', 'ू', 'ृ', 'ॢ', 'े', 'ै', 'ो', 'ौ', 'ँ', '्', 'ं', 'ः', 'ँ', '़', 'ॅ', 'ॆ', 'ॉ', 'ॊ' });
                if (index1 == 0)
                {
                    string baseChar = richTextBox1.Text.Substring(richTextBox1.Text.Length - 2, 1);
                    RunTimeGeneration(baseChar);
                }
                else
                {
                    string baseChar = richTextBox1.Text.Substring(richTextBox1.Text.Length - 1, 1);
                    RunTimeGeneration(baseChar);
                }
            }
            catch (Exception ee)
            { }
        }

        bool Spacebar_flag = false;

        private void SpaceBar_Click(object sender, EventArgs e)
        {
            Spacebar_flag = true;
            toolStripStatusLabel2.Text = "Adding Space";
            adding_local_dic();
            toolStripStatusLabel2.Text = "Adding Space .........";
            this.Refresh();
            string file_path = @"..\..\..\..\N-Grams\log_key_press\log_file.txt";
            string toPrint = " ";

            File.AppendAllText(file_path, toPrint, Encoding.Unicode);
            //adding_local_dic_for_bigram();
            richTextBox1.Text += " ";
            try
            {
                //remote_connection();
            }
            catch (Exception ee)
            {
                MessageBox.Show("no remote connection");
            }
            toolStripStatusLabel2.Text = "Data Added";

        }
  
    #region check before delete
        private void remote_connection()
        {
            try
            {


                // TcpClient tc = new TcpClient("10.14.44.43", 50000);
                TcpClient tc = new TcpClient("10.14.44.49", 50000);

                NetworkStream ns = tc.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                string input_string = richTextBox1.Text;
                sw.WriteLine(input_string);

                sw.Flush();
                StreamReader sr = new StreamReader(ns);
                string value = sr.ReadLine();



                string[] wordArr = value.Split('#');
                //
                string output_tag = "";
                foreach (string ob in wordArr)
                {
                    if (ob.Trim().Length != 0)
                    {
                        string word1 = ob.Substring(0, ob.IndexOf("\t"));
                        string tag = ob.Substring(ob.LastIndexOf("\t"));
                        output_tag += word1.Trim() + "<" + tag.Trim() + ">\t";
                    }

                }

                toolStripStatusLabel2.Text = output_tag;
                tc.Close();

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MessageBox.Show(ee.Message);
            }
        }

        #region OLD Function not checked// may be latter on deleted
        private void Calling_EBX(String text)
        {
            try
            {
                if (groupBox8.Visible == true)
                {
                    ArrayList temp_ebx = new ArrayList();
                    foreach (string obj in listBox2.Items)
                    {
                        string orig_typed = text.Substring(text.LastIndexOf(" ")).Trim();
                        string last_typed = orig_typed.Substring(2);
                        //  string check = obj.Substring(obj.LastIndexOf("\t")).Trim();
                        string check = obj.Substring(obj.LastIndexOf(" ")).Trim();
                        if (check.IndexOf(last_typed) >= 0)
                        {
                            //int start_ind=obj.IndexOf("(");
                            //int len=  obj.IndexOf(")")-start_ind;
                            //temp_ebx.Add(obj.Substring(start_ind+1,len-1));
                            temp_ebx.Add(obj);
                        }
                    }
                    listBox2.Items.Clear();
                    foreach (string ob in temp_ebx)
                    {
                        listBox2.Items.Add(ob);
                    }

                }
            }
            catch (Exception eee)
            { }
        }

        private void check_for_disable_button()
        {
            if (richTextBox1.Text.Trim().Length == 0)
            {
                button1.Enabled = false;
                button55.Enabled = false;
                button56.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button55.Enabled = true;
                button56.Enabled = true;

            }
        }
        public void Word_Level_Lexicographic(string text)
        {
            this.Text = "lexicogrphic";
            string[] words = text.Split(' ');
            if (text.Trim().Length != 0)
            {
                listBox1.Items.Clear();
                word_coll.Clear();
                string last_word = words[words.Length - 1];
                int len = last_word.Length;
                var word_list = from word in unigram_word where (word.Key.Length >= len && word.Key.Substring(0, len) == last_word) select word.Key;
                foreach (string word in word_list)
                {
                    if (word.StartsWith("$"))
                    {
                        listBox1.Items.Add(word.Remove(0, 1));
                        word_coll.Add(word.Remove(0, 1));
                    }
                    else
                    {
                        listBox1.Items.Add(word);
                        word_coll.Add(word);
                    }
                }
            }
            else
            {
                listBox1.Items.Clear();
                word_coll.Clear();
            }
            if (words.Length > 1 && words[words.Length - 1] == "")
            {
                if (!File.Exists(@"..\..\..\..\N-Grams\temp\temp_unigram.txt"))
                {
                    FileStream tempFile = new FileStream(@"..\..\..\..\N-Grams\temp\temp_unigram.txt", FileMode.Create, FileAccess.Write);
                    tempFile.Close();
                }
                if (!unigram_word.ContainsKey(words[words.Length - 2]))
                {
                    FileStream tempFile = new FileStream(@"..\..\..\..\N-Grams\temp\temp_unigram.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(tempFile);
                    sw.WriteLine(words[words.Length - 2] + "\t" + "50");
                    unigram_word.Add(words[words.Length - 2], 50);
                    sw.Close();
                    tempFile.Close();
                }
            }
        }

        public void Word_Level_Unigram_1(string text)
        {
            string[] words = text.Split(' ');
            if (text.Trim().Length != 0)
            {
                string last_word = words[words.Length - 1];
                listBox1.Items.Clear();
                word_coll.Clear();
                int len = last_word.Length;
                var word_list = from entry in Prob_a where (entry.Key.Length >= len && entry.Key.Substring(0, len) == last_word) select entry.Key;
                foreach (string word in word_list)
                {
                    if (word.StartsWith("$"))
                    {
                        listBox1.Items.Add(word.Remove(0, 1));
                        word_coll.Add(word.Remove(0, 1));
                    }
                    else
                    {
                        listBox1.Items.Add(word);
                        word_coll.Add(word);

                    }
                }
                Unigram_temp(words);   //call the temporary storage dictionary
            }
            else
                Word_Level_Lexicographic(text);
        }

        public void Word_Level_Bi_gram_1(string text)
        {
            string[] words = text.Split(' ');
            if (words.Length > 1)
            {
                string second_last_word = words[words.Length - 2];
                string last_word = words[words.Length - 1];
                int len = last_word.Length;
                string value = null;
                if (Prob_a_b.TryGetValue(second_last_word, out value)) // searching and retreiveing in one step
                {
                    string[] raw_values = value.Split('#');
                    List<string> values = new List<string>();
                    foreach (string raw_value in raw_values)
                    {
                        if (raw_value != null && raw_value != "")
                            values.Add(raw_value.Substring(raw_value.IndexOf(' ') + 1));
                    }
                    var item_values = from item in values where (item.Length >= len && item.Substring(0, len) == last_word && item != null && item != "") select item.Trim();
                    listBox1.Items.Clear();
                    word_coll.Clear();
                    foreach (string item in item_values)
                    {
                        if (item.StartsWith("$"))
                        {
                            listBox1.Items.Add(item.Remove(0, 1));
                            word_coll.Add(item.Remove(0, 1));
                        }
                        else
                        {
                            listBox1.Items.Add(item);
                            word_coll.Add(item);
                        }
                    }
                    //Bigram_temp(words);   // Here the temp adding can be done
                    //Unigram_temp(words);
                    if (listBox1.Items.Count == 0)
                        Word_Level_Unigram(text);
                }
                else
                    Word_Level_Unigram(text);
            }
            else
                Word_Level_Unigram(text);
        }

        public void Word_Level_Tri_gram(string text)
        {
            string[] words = text.Split(' ');
            if (words.Length > 2)
            {
                string last_two_words = words[words.Length - 3] + " " + words[words.Length - 2];
                string last_word = words[words.Length - 1];
                int len = last_word.Length;
                string value = null;
                if (Prob_a_bc.TryGetValue(last_two_words, out value)) // searching and retreiveing in one step
                {
                    string[] raw_values = value.Split('#');
                    List<string> values = new List<string>();
                    foreach (string raw_value in raw_values)
                    {
                        if (raw_value != null && raw_value != "")
                            values.Add(raw_value.Substring(raw_value.IndexOf(' ') + 1));
                    }
                    var item_values = from item in values where (item.Length >= len && item.Substring(0, len) == last_word && item != null && item != "") select item.Trim();
                    listBox1.Items.Clear();
                    word_coll.Clear();//check this also
                    foreach (string item in item_values)
                    {

                        string[] str1 = Regex.Split(item, "\t");
                        string val_to_add = str1[1].ToString();
                        if (item.StartsWith("$"))
                        {
                            //listBox1.Items.Add(item.Remove(0, 1));
                            //word_coll.Add(item.Remove(0, 1));
                            listBox1.Items.Add(val_to_add.Remove(0, 1));
                            word_coll.Add(val_to_add.Remove(0, 1));
                        }
                        else
                        {
                            //listBox1.Items.Add(item);
                            //word_coll.Add(item);//may be remove if not req used for checking highlighter
                            //this.Text = "tri-gram";
                            listBox1.Items.Add(val_to_add);
                            word_coll.Add(val_to_add);//may be remove if not req used for checking highlighter
                            this.Text = "tri-gram";
                        }
                    }
                    if (listBox1.Items.Count == 0)
                        Word_Level_Bi_gram(text);
                }
                else
                    Word_Level_Bi_gram(text);
            }
            else if (words.Length > 1)
            {
                Word_Level_Bi_gram(text);
            }
            else
            {
                Word_Level_Unigram(text);
            }
        }

        public void Tag_Level_Two_Tag(string text)
        {
            string[] words = text.Split(' ');
            if (words.Length > 2)
            {
                string last_two_word = words[words.Length - 3] + " " + words[words.Length - 2];
                string last_word = words[words.Length - 1];
                string last_two_tags = "";
                int len = last_word.Length;
                foreach (KeyValuePair<string, int> pair in bigram_word_tag)
                    if (pair.Key.StartsWith(last_two_word))
                        last_two_tags = pair.Key.Substring(pair.Key.IndexOf("<"));
                string value = null;
                if (Prob_Wi_ti1_ti2.TryGetValue(last_two_tags, out value))
                {
                    string[] raw_values = value.Split('#');
                    List<string> values = new List<string>();
                    foreach (string raw_value in raw_values)
                    {
                        if (raw_value != null && raw_value != "")
                            values.Add(raw_value.Substring(raw_value.IndexOf(' ') + 1));
                    }
                    var item_values = from item in values where (item.Length >= len && item.Substring(0, len) == last_word && item != null && item != "") select item.Trim();
                    listBox1.Items.Clear();
                    foreach (string item in item_values)
                    {
                        if (item.StartsWith("$"))
                            listBox1.Items.Add(item.Remove(0, 1));
                        else
                            listBox1.Items.Add(item);
                    }
                    if (listBox1.Items.Count == 0)
                        Word_Level_Bi_gram(text);
                }
                else
                    Word_Level_Bi_gram(text);
            }
            else if (words.Length > 1)
                Word_Level_Bi_gram(text);
            else
                Word_Level_Unigram(text);
        }

        /// <summary>
        /// <remarks>Not Implemented</remarks>
        /// </summary>
        /// <param name="text"></param>
        public void Tag_Level_Two_Tag_Word(string text)
        {
            MessageBox.Show("Error");
        }

        /// <summary>
        /// <param1>text</param1>
        /// <remarks>Implemented but ERROR </remarks>
        /// </summary>
        /// <param name="text"></param>
        public void Tag_Level_Alpha(string text)
        {
            string[] words = text.Split(' ');
            if (words.Length > 2)
            {
                string last_two_word = words[words.Length - 3] + " " + words[words.Length - 2];
                string second_last_word = words[words.Length - 2];
                string last_word = words[words.Length - 1];
                int len = last_word.Length;
                var last_two_tags_values = from entry in bigram_word_tag where entry.Key.StartsWith(last_two_word) select entry.Key.Substring(entry.Key.IndexOf("<"));
                //foreach (KeyValuePair<string, int> pair in bigram_word_tag)
                //if (pair.)
                //    last_two_tags = pair.Key.Substring(pair.Key.IndexOf("<"));
                listBox1.Items.Clear();
                foreach (string last_two_tags in last_two_tags_values)
                {
                    string value = null;
                    if (Prob_alpha_Wi_Wi1_ti1_ti2.TryGetValue(last_two_tags + " " + second_last_word, out value))
                    {
                        string[] raw_values = value.Split('#');
                        List<string> values = new List<string>();
                        foreach (string raw_value in raw_values)
                        {
                            if (raw_value != null && raw_value != "")
                                values.Add(raw_value.Substring(raw_value.IndexOf(' ') + 1));
                        }
                        var item_values = from item in values where (item.Length >= len && item.Substring(0, len) == last_word && item != null && item != "") select item.Trim();
                        foreach (string item in item_values)
                        {
                            if (item.StartsWith("$"))
                                listBox1.Items.Add(item.Remove(0, 1));
                            else
                                listBox1.Items.Add(item);
                        }
                    }
                    else
                    {
                        Tag_Level_Two_Tag(text);
                        break;    // needed to stop the recursive loop
                    }
                }
                if (listBox1.Items.Count == 0)
                    Tag_Level_Two_Tag(text);
            }
            else if (words.Length > 1)
                Word_Level_Bi_gram(text);
            else
                Word_Level_Unigram(text);
        }

        public void Bigram_temp(string[] words)
        {
            //saving unknown words to temp file
            if (words.Length > 2 && words[words.Length - 1] == "")
            {
                if (!File.Exists(@"..\..\..\..\N-Grams\temp\temp_bigram.txt"))
                {
                    FileStream tempFile = new FileStream(@"..\..\..\..\N-Grams\temp\temp_bigram.txt", FileMode.Create, FileAccess.Write);
                    tempFile.Close();
                }
                if (!bigram_word.ContainsKey(words[words.Length - 3] + " " + words[words.Length - 2]))
                {
                    FileStream tempFile = new FileStream(@"..\..\..\..\N-Grams\temp\temp_bigram.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(tempFile);
                    sw.WriteLine(words[words.Length - 3] + " " + words[words.Length - 2] + "\t" + "50");
                    bigram_word.Add(words[words.Length - 3] + " " + words[words.Length - 2], 50);
                    sw.Close();
                    tempFile.Close();
                }
            }
        }

        public void Unigram_temp(string[] words)
        {
            if (words.Length > 1 && words[words.Length - 1] == "")
            {
                if (!File.Exists(@"..\..\..\..\N-Grams\temp\temp_unigram.txt"))
                {
                    FileStream tempFile = new FileStream(@"..\..\..\..\N-Grams\temp\temp_unigram.txt", FileMode.Create, FileAccess.Write);
                    tempFile.Close();
                }
                if (!unigram_word.ContainsKey(words[words.Length - 2]))
                {
                    FileStream tempFile = new FileStream(@"..\..\..\..\N-Grams\temp\temp_unigram.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(tempFile);
                    sw.WriteLine(words[words.Length - 2] + "\t" + "50");
                    unigram_word.Add(words[words.Length - 2], 50);
                    sw.Close();
                    tempFile.Close();
                }
            }
        }


        #endregion


        #region old4
        private void Display_Status(string msg2, string msg3)
        {

            toolStripStatusLabel2.Text = msg3;
            toolStripStatusLabel3.Text = msg2;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string str2 = "Char Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string str2 = "Char Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void bigramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str2 = "Char Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void triGramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str2 = "Char Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void lexicographicToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string str2 = "Word Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void unigramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str2 = "Word Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void bigramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string str2 = "Word Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void trigramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string str2 = "Word Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void pWiTi1Ti2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string str2 = "Tag Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void pWiWi1Ti1Ti2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str2 = "Tag Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }

        private void alphaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str2 = "Tag Level ";
            string str3 = ((ToolStripMenuItem)sender).Text;
            Display_Status(str2, str3);
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        static SortedDictionary<string, string> list_of_file_name = new SortedDictionary<string, string>();
        static SortedDictionary<string, string> Maping_table = new SortedDictionary<string, string>();
        //static SortedDictionary<string, string> EBX = new SortedDictionary<string, string>();
        private void Cal_Regency_method(string data)
        {
            /* Regency = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
             */
            string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();
            string filePath = strCurrentDirectory + @"\Data\Regency_Data.txt";

            //string data=listBox1.SelectedItem.ToString();  
            string key = System.DateTime.Now.Ticks.ToString();
            if (Regency.ContainsKey(data))
            {
                Regency.Remove(data);
                Regency.Add(data, key);


            }
            else
            {

                Regency.Add(data, key);
            }

            string output_text = null;
            List<string> output = new List<string>();

            foreach (KeyValuePair<string, string> pair in Regency)
                output.Add(pair.Key + "\t" + pair.Value.Trim() + "\n");
            //output.Sort();
            foreach (string item in output)
            {
                if (item != "")
                {
                    output_text += item;
                }

            }

            // File.AppendAllText(filePath, data+"\t"+key+"\n", Encoding.UTF8);
            File.WriteAllText(filePath, output_text, Encoding.UTF8);

        }
        private void Core_method_Correct_word(string str, string typedString, ref string targetString, ref float ngramScore, int phonatic_value, ArrayList arr, string firstChar, string obj)
        {

            if (obj != "")
            {
                // obj = obj.Substring(0, obj.IndexOf(" ")); // using original single dictionary
                if (obj.IndexOf("\t") == -1)
                {
                }
                else
                {
                    obj = obj.Substring(0, obj.IndexOf("\t")); // using old multi dictionary
                }

                double iFreq = 0.0F;
                string StrWordFrequency = "";
                double EDscr = 0.0f;
                double lenScore = 0.0F;
                double PScore = 0.0f;
                double LCScore = 0.0F;
                string strEDscr = "";
                string strLenScore = "";
                string strRegencyScore = "";

                this.Text = "शब्द भविष्यवाणी+phonatic calculation";

                string str_obj = "";
                if (obj.IndexOf("\t") < 0)
                {
                    str_obj = obj;
                }
                else
                {
                    str_obj = obj.Substring(0, obj.IndexOf("\t"));
                }

                if (typedString.Length <= str_obj.Length)
                {

#if BannedWord


                    bool banFlag = false;
                    if (FlagUsedBannedWord == true)
                    {
                        banFlag = Dic_BannedWord.ContainsKey(str_obj);
                        FlagUsedlocalDic = true;
                    }
                    if ((firstChar.Trim() == str_obj.Substring(0, 1)) && banFlag == false)
#else
                    if (firstChar.Trim() == str_obj.Substring(0, 1))
#endif
                    {
                        if (FlagUsediFreq == true)
                        {
                            string freq = "";
                            targetString = str_obj.Substring(0, typedString.Length);

                            if (obj.IndexOf("\t") < 0)
                            {
                                freq = "999";
                            }
                            else
                            {
                                freq = obj.Substring(obj.IndexOf("\t"));
                            }
                            iFreq = 1 / (double)(Convert.ToInt32(freq));
                            iFreq = Math.Round(iFreq, 5);
                            StrWordFrequency = (iFreq.ToString().PadRight(8, '0'));
                        }
                        if (FlagUsedPscore == true)
                        {
                            PScore = Math.Round(Cal_phonatic_method(typedString, targetString), 3);
                        }
                        if (FlagUsedngramScore == true)
                        {
                            // ngramScore = Cal_ngram_method(str, typedString, ngramScore, str_obj);

                        }
                        if (FlagUsedBiGram == true)
                        {
                            // ngramScore = Cal_Bigram_method(str, typedString, ngramScore, str_obj);

                        }


#if Regency_Of_word
                        if (FlagUsedRegency == true)
                        {
                            if (Regency.ContainsKey(str_obj))
                            {
                                strRegencyScore = Regency[str_obj].Trim();
                                double RS = 999999999999999999 - (double)Convert.ToInt64(strRegencyScore);
                                strRegencyScore = RS.ToString();

                            }
                        }
#endif
                        if (FlagUsedLCScore == true)
                        {
                            LCScore = targetString.Length - LCS(typedString, targetString);
                            LCScore = targetString.Length - LCS(typedString, targetString) + 1;
                            LCScore = typedString.Length - LCS(typedString, str_obj);
                        }
                        if (FlagUsedEDscr == true)
                        {
                            EDscr = Math.Round(Cal_EDScore_Method(typedString, targetString), 3);

                            if (EDscr == 0.0)
                            {
                                strEDscr = "0.0";
                            }
                            else
                            {
                                strEDscr = EDscr.ToString();
                            }
                        }
                        if (FlagUsedlenScore == true)
                        {
                            lenScore = Cal_LScore_method(typedString, str_obj) + 1;

                            if (lenScore == 0.0)
                            {
                                strLenScore = "0.0";
                            }
                            else
                            {
                                strLenScore = lenScore.ToString();
                            }
                        }


                        //testing purpose for adding probablity for frq


                        try
                        {


                            if (Prob_a.ContainsKey(str_obj))
                            {
                                string freq = Prob_a[str_obj].ToString();

                                // iFreq =  (double)(Convert.ToInt32(freq));
                                //iFreq = Math.Round(iFreq, 5);
                                StrWordFrequency = (freq.ToString().PadRight(8, '0'));
                            }



                        }
                        catch (Exception ee)
                        { }

                        ////end



                        //arr.Add("P="+PScore.ToString() + "\tL=" + LCScore.ToString() + "\tE=" + strEDscr.ToString() + "\tN=" + ngramScore.ToString() + "\tL=" + lenScore.ToString() + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");
                        //   if (obj.Length > 3)
                        {
                            //old working
                            //arr.Add("P=" + PScore.ToString() + "\tLCS=" + LCScore.ToString() + "\tE=" + strEDscr.ToString() + "\tR=" + strRegencyScore.PadLeft(18, '9') + "\tN=" + ngramScore.ToString() + "\tL=" + lenScore.ToString().PadRight(8, '0').Substring(2, 6) + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");
                            //new i m trying
                            // arr.Add("P=" + PScore.ToString() + "\tE=" + strEDscr.ToString() + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");


                            string output_text = "P=" + PScore.ToString() + "\tE=" + strEDscr.ToString() + "\tR=" + strRegencyScore.PadLeft(18, '9') + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]";
                            arr.Add(output_text);

                            //arr.Add("P=" + PScore.ToString() + "\tE=" + strEDscr.ToString() +"\tR=" + strRegencyScore.PadLeft(18, '9')+ "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");


                            //  arr.Add("E=" + strEDscr.ToString() + "\tP=" + PScore.ToString() + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");

                            //if (File.Exists(@"D:\test.txt"))
                            //{
                            //    File.AppendAllText(@"D:\test.txt", output_text+"\r\n", Encoding.UTF8);
                            //}

                        }

                    }

                }
            }
        }

        private void Core_method_Correct_word(string str, string typedString, ref string targetString, ref float ngramScore, int phonatic_value, ArrayList arr, string firstChar, string obj, string wordFrq)
        {

            if (obj != "")
            {
                // obj = obj.Substring(0, obj.IndexOf(" ")); // using original single dictionary
                if (obj.IndexOf("\t") == -1)
                {
                }
                else
                {
                    obj = obj.Substring(0, obj.IndexOf("\t")); // using old multi dictionary
                }

                double iFreq = 0.0F;
                string StrWordFrequency = "";
                double EDscr = 0.0f;
                double lenScore = 0.0F;
                double PScore = 0.0f;
                double LCScore = 0.0F;
                string strEDscr = "";
                string strLenScore = "";
                string strRegencyScore = "";

                this.Text = "शब्द भविष्यवाणी+phonatic calculation";

                string str_obj = "";
                if (obj.IndexOf("\t") < 0)
                {
                    str_obj = obj;
                }
                else
                {
                    str_obj = obj.Substring(0, obj.IndexOf("\t"));
                }

                if (typedString.Length <= str_obj.Length)
                {

#if BannedWord


                    bool banFlag = false;
                    if (FlagUsedBannedWord == true)
                    {
                        banFlag = Dic_BannedWord.ContainsKey(str_obj);
                        FlagUsedlocalDic = true;
                    }
                    if ((firstChar.Trim() == str_obj.Substring(0, 1)) && banFlag == false)
#else
                    if (firstChar.Trim() == str_obj.Substring(0, 1))
#endif
                    {
                        if (FlagUsediFreq == true)
                        {
                            string freq = "";
                            targetString = str_obj.Substring(0, typedString.Length);

                            if (obj.IndexOf("\t") < 0)
                            {
                                freq = "999";
                            }
                            else
                            {
                                freq = obj.Substring(obj.IndexOf("\t"));
                            }
                            iFreq = 1 / (double)(Convert.ToInt32(freq));
                            iFreq = Math.Round(iFreq, 5);
                            StrWordFrequency = (iFreq.ToString().PadRight(8, '0'));
                        }
                        if (FlagUsedPscore == true)
                        {
                            PScore = Math.Round(Cal_phonatic_method(typedString, targetString), 3);
                        }
                        if (FlagUsedngramScore == true)
                        {
                            // ngramScore = Cal_ngram_method(str, typedString, ngramScore, str_obj);

                        }
                        if (FlagUsedBiGram == true)
                        {
                            // ngramScore = Cal_Bigram_method(str, typedString, ngramScore, str_obj);

                        }


#if Regency_Of_word
                        if (FlagUsedRegency == true)
                        {
                            if (Regency.ContainsKey(str_obj))
                            {
                                strRegencyScore = Regency[str_obj].Trim();
                                double RS = 999999999999999999 - (double)Convert.ToInt64(strRegencyScore);
                                strRegencyScore = RS.ToString();

                            }
                        }
#endif
                        if (FlagUsedLCScore == true)
                        {
                            LCScore = targetString.Length - LCS(typedString, targetString);
                            LCScore = targetString.Length - LCS(typedString, targetString) + 1;
                            LCScore = typedString.Length - LCS(typedString, str_obj);
                        }
                        if (FlagUsedEDscr == true)
                        {
                            EDscr = Math.Round(Cal_EDScore_Method(typedString, targetString), 3);

                            if (EDscr == 0.0)
                            {
                                strEDscr = "0.0";
                            }
                            else
                            {
                                strEDscr = EDscr.ToString();
                            }
                        }
                        if (FlagUsedlenScore == true)
                        {
                            lenScore = Cal_LScore_method(typedString, str_obj) + 1;

                            if (lenScore == 0.0)
                            {
                                strLenScore = "0.0";
                            }
                            else
                            {
                                strLenScore = lenScore.ToString();
                            }
                        }


                        //testing purpose for adding probablity for frq


                        try
                        {

                            StrWordFrequency = wordFrq.PadRight(8, '0');


                        }
                        catch (Exception ee)
                        { }

                        ////end



                        //arr.Add("P="+PScore.ToString() + "\tL=" + LCScore.ToString() + "\tE=" + strEDscr.ToString() + "\tN=" + ngramScore.ToString() + "\tL=" + lenScore.ToString() + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");
                        //   if (obj.Length > 3)
                        {
                            //old working
                            //arr.Add("P=" + PScore.ToString() + "\tLCS=" + LCScore.ToString() + "\tE=" + strEDscr.ToString() + "\tR=" + strRegencyScore.PadLeft(18, '9') + "\tN=" + ngramScore.ToString() + "\tL=" + lenScore.ToString().PadRight(8, '0').Substring(2, 6) + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");
                            //new i m trying
                            // arr.Add("P=" + PScore.ToString() + "\tE=" + strEDscr.ToString() + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");


                            //string output_text = "P=" + PScore.ToString() + "\tE=" + strEDscr.ToString() + "\tR=" + strRegencyScore.PadLeft(18, '9') + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]";
                            double Score = 0.0f;
                            double lamda1 = 0.0f;
                            double lamda2 = 0.0f;
                            double lamda3 = 0.0f;

                            lamda1 = 2.504;
                            lamda2 = 2.959;
                            lamda3 = 3.0435;

                            Score = System.Convert.ToDouble(PScore) * lamda1 + System.Convert.ToDouble(strEDscr) * lamda2 + Math.Abs(System.Convert.ToDouble(StrWordFrequency) * lamda3);



                            string output_text = "P=" + Score + "\t[" + str_obj + "]";

                            arr.Add(output_text);

                            //arr.Add("P=" + PScore.ToString() + "\tE=" + strEDscr.ToString() +"\tR=" + strRegencyScore.PadLeft(18, '9')+ "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");


                            //  arr.Add("E=" + strEDscr.ToString() + "\tP=" + PScore.ToString() + "\tF=" + StrWordFrequency.ToString() + "\t[" + str_obj + "]");

                            //if (File.Exists(@"D:\test.txt"))
                            //{
                            //    File.AppendAllText(@"D:\test.txt", output_text+"\r\n", Encoding.UTF8);
                            //}

                        }

                    }

                }
            }
        }
        private static float Cal_EDScore_Method(string typedString, string targetString)
        {
            int cost = myCostChecker(targetString, typedString);
            float EDscr = 0.0F;
            EDscr = 1 - (targetString.Length - cost) / (float)(typedString.Length);
            // EDscr = (float)cost;
            return EDscr;
        }

        private static double Cal_LScore_method(string typedString, string obj)
        {
            float secSort = 0.0F;
            secSort = ((float)(obj.Length - typedString.Length) / obj.Length);
            double lenScore = Math.Round(secSort, 5);
            return lenScore;
        }


        #endregion


        #region old3

        private static void Load_Sound_path()
        {
            string input_path = System.Environment.CurrentDirectory.ToString() + @"\merge";
            string input_map = System.Environment.CurrentDirectory.ToString() + @"\maping.txt";
            string Data_map = File.ReadAllText(input_map, Encoding.UTF8);
            string[] wordDic_map = Regex.Split(Data_map, "\n");

            List<string> file_paths = new List<string>();
            file_paths = subdirectory(input_path);
            int i = 0;

            foreach (string file in file_paths)
            {
                string file_name_only = "";
                file_name_only = file.Substring(file.IndexOf("merge"));
                file_name_only = file_name_only.Substring(8, file_name_only.Length - 12);


                if (list_of_file_name.ContainsKey(file_name_only))
                {
                    //
                }
                else
                {
                    list_of_file_name.Add(file_name_only, file);
                }

            }
            foreach (string obj in wordDic_map)
            {
                if (obj.Trim().Length != 0)
                {
                    string line = obj.Substring(0, obj.IndexOf("\t"));
                    string new_name = obj.Substring(obj.IndexOf("\t")).Trim();

                    if (Maping_table.ContainsKey(line))
                    {
                        //  bigram[key] += 1;
                    }
                    else
                    {
                        Maping_table.Add(line, list_of_file_name[new_name]);
                    }
                }

            }

        }

        #region SpeacnRunTime
        private void button56_Click(object sender, EventArgs e)
        {
            string temp_folder = "sound_research";
            string input_path = FolderPathName + @"\temp\" + temp_folder;

            List<string> file_paths = new List<string>();
            file_paths = subdirectory(input_path);
            bool flag = false;
            string temp = "";
            string old = "";
            foreach (string file in file_paths)
            {
                string file_name_only = "";
                file_name_only = file.Substring(file.IndexOf(temp_folder));
                file_name_only = file_name_only.Substring(temp_folder.Length + 1);


                string newName = "tempFile";
                temp = file.Replace(file_name_only, newName);

                if ((richTextBox1.Text.Trim() + ".mp3").ToString() == file_name_only)
                {
                    flag = true;
                    old = file;

                }

            }
            if (flag == true)
            {
                mciSendString("close MediaFile", null, 0, IntPtr.Zero);
                System.IO.File.Copy(old, temp + ".mp3", true);

                mciSendString("open \"" + temp + ".mp3" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                mciSendString("play MediaFile", null, 0, IntPtr.Zero);
            }
            else
            {
                button56.Enabled = false;
                string inputFile = richTextBox1.Text;
                merge(inputFile);
                flag = false;

            }

        }
        private void merge(string obj)
        {
            string input = obj.Trim();
            flag = false;
            strUrl = "";
            doc_str = "";
            fileName = "";
            flagTerminate = false;
            strText = obj.Trim();

            string part1 = File.ReadAllText(FolderPathName + @"/temp/part1.txt", Encoding.UTF8);
            string part2 = File.ReadAllText(FolderPathName + @"/temp/part2.txt", Encoding.UTF8);


            string final_str = "";
            final_str = part1 + input + part2;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentText = final_str;
            strUrl = @"http://vozme.com/text2voice.php?lang=hi";
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            flag = true;
            e.Cancel = true;
            webBrowser1.Navigate(strUrl);
        }
        private void download(string url)
        {
            MyWebClient client = new MyWebClient();
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync(new Uri(url), FolderPathName + @"\temp\sound_research\" + strText + ".mp3");
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //timer1.Enabled = true;

            //button3.Text = i.ToString();
            //if (i >= listBox1.Items.Count)
            //{
            //    this.Close();
            //}
            button56.Enabled = true;
        }
        static int i = 0;
        string doc_str = "";
        string fileName = "";
        bool flagTerminate = false;
        string strUrl = "";
        bool flag = false;
        string strText = "";
        string FolderPathName = "";
        string data = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    i++;
            //    timer1.Enabled = false;

            //    //if (listBox1.Items.Count - i >= 0)
            //    //{
            //    //    System.Threading.Thread.Sleep(7000);                  
            //    //    merge(listBox1.Items[listBox1.Items.Count - i].ToString());
            //    //}
            //    timer1.Enabled = false;
            //}
            //catch (Exception ee)
            //{ }
        }
        private void wb_newWindow(object sender, CancelEventArgs e)
        {
            flag = true;
            e.Cancel = true;
            ((WebBrowser)(sender)).Navigate(strUrl);

        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            doc_str = ((WebBrowser)(sender)).DocumentText;
            System.Threading.Thread.Sleep(50);

            if (flag == true && flagTerminate == false)
            {
                try
                {
                    string st = @"http://vozme.com/speech/hi-ml/";
                    int index1 = doc_str.IndexOf(st);
                    int index2 = doc_str.IndexOf(@".mp3", index1);
                    string output = doc_str.Substring(index1, index2 - index1) + @".mp3";
                    if (output != "")
                    {
                        fileName = output;
                        download(fileName);
                        flagTerminate = true;
                    }
                }
                catch (Exception ee)
                { }
            }
        }
        #endregion

        private void bpl1_Click(object sender, EventArgs e)
        {
            int index = 0;

            PlaySelectedText_on_demand(index);
        }

        private void PlaySelectedText_on_demand(int index)
        {
            ArrayList arr = new ArrayList();

            foreach (string obj in listBox1.Items)
            {
                arr.Add(obj);

            }

            string selected_text = arr[index].ToString();
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
            if (Maping_table.ContainsKey(selected_text))
            {
                mciSendString("open \"" + Maping_table[selected_text].ToString() + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                mciSendString("play MediaFile", null, 0, IntPtr.Zero);

            }
        }

        private void bpl2_Click(object sender, EventArgs e)
        {
            int index = 1;

            PlaySelectedText_on_demand(index);
        }

        private void bpl3_Click(object sender, EventArgs e)
        {
            int index = 2;

            PlaySelectedText_on_demand(index);
        }

        private void bpl4_Click(object sender, EventArgs e)
        {
            int index = 3;

            PlaySelectedText_on_demand(index);
        }

        private void bpl5_Click(object sender, EventArgs e)
        {
            int index = 4;

            PlaySelectedText_on_demand(index);
        }

        private void bpl6_Click(object sender, EventArgs e)
        {
            int index = 5;

            PlaySelectedText_on_demand(index);
        }

        private void bpl7_Click(object sender, EventArgs e)
        {
            int index = 6;

            PlaySelectedText_on_demand(index);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button57_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files .txt|*.txt";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.UTF8);

            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            // Create new SaveFileDialog object
            SaveFileDialog DialogSave = new SaveFileDialog();

            // Default file extension
            DialogSave.DefaultExt = "txt";

            // Available file extensions
            DialogSave.Filter = "Text file (*.txt)|*.txt|XML file (*.xml)|*.xml|All files (*.*)|*.*";

            // Adds a extension if the user does not
            DialogSave.AddExtension = true;

            // Restores the selected directory, next time
            DialogSave.RestoreDirectory = true;

            // Dialog title
            DialogSave.Title = "Where do you want to save the file?";

            // Startup directory
            DialogSave.InitialDirectory = @"C:/";

            // Show the dialog and process the result
            if (DialogSave.ShowDialog() == DialogResult.OK)
            {
                //  MessageBox.Show("You selected the file: " + DialogSave.FileName);
                File.WriteAllText(DialogSave.FileName.ToString(), richTextBox1.Text, Encoding.Unicode);
            }
            else
            {
                // MessageBox.Show("You hit cancel or closed the dialog.");
            }

            DialogSave.Dispose();
            DialogSave = null;
        }
        public static string CoreTranslation(string str)
        {
            Encoding targetEncoding;                                //defininig the targetEncoding
            byte[] encodedChars;                                    //byde array
            targetEncoding = Encoding.UTF8;                         //target utf8 encoding
            encodedChars = targetEncoding.GetBytes(str);            //byte representation of string in target encoding

            string newString = "";                                  //defining new place holder for encoded string
            for (int i = 0; i < encodedChars.Length; i++)
            {

                //string created depending on encoding and decoding formate used by google
                newString += "%" + string.Format("{0:X2}", encodedChars[i]); // original value of encoded char ready for use


            }
            return newString;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.google.co.in/search?q=" + CoreTranslation(richTextBox1.Text.ToString()));
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.Cut();
            }

        }

        private void cutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        Color old_color;
        private void not_needed()
        {
            ////   bi_gram_saperator();
            //// //  tri_gram_saperator();
            //////   uni_gram_saperator();
            ////   //old test data
            ////   //load_new_bigram();
            ////   //load_new_unigram();
            ////  //end old test

            ////   Next_char_Highlighter();

            //   underline();

            //  MessageBox.Show(richTextBox1.SelectedText.ToString());
            //  Formatting_data_t_a_bc();
            // Formatting_data_Word_tag();
            //  load_EBX();

            //formating_for_ebb();

        }
        private void button14_Click(object sender, EventArgs e)
        {
            // Formatting_data_tag_word();
            // Context_based_abbr();
            //  P_Tag_Word();
            // Storing_trigram();
            groupBox9.Enabled = true;
            groupBox9.Visible = true;

            button62.Visible = true;
        }

        private void Context_based_abbr()
        {
            groupBox8.Visible = true;
            groupBox8.Location = new System.Drawing.Point(583, 10);




            string str = richTextBox1.Text;
            string[] wordarray = Regex.Split(str, " ");
            int LocOfLastWord = 0;
            LocOfLastWord = wordarray.Length - 1;
            string lastWord = wordarray[LocOfLastWord].ToString().Trim();
            ArrayList arr_data = new ArrayList();

            string search_key = "";

            if (lastWord.Length == 3)
            {
                search_key = lastWord;
            }
            else
            {
                search_key = lastWord.Substring(0, 3);

            }

            if (EBX.ContainsKey(search_key))
            {
                string coll = EBX[search_key];
                string[] str_arr = Regex.Split(coll, "-");
                listBox2.Items.Clear();
                foreach (string obj in str_arr)
                {
                    if (obj.Trim().Length > 0)
                    {

                        int start_ind = obj.IndexOf("(");
                        int len = obj.IndexOf(")") - start_ind;
                        string first_word = obj.Substring(start_ind + 1, obj.IndexOf("\t") - start_ind - 1);


                        double prob_ini_word = Convert.ToDouble(Prob_a[first_word]);
                        prob_ini_word = Math.Pow(10, prob_ini_word);
                        string word_to_check_for_bigram1 = wordarray[wordarray.Length - 2].Trim().ToString();
                        string word_to_check_for_bigram2 = Prob_a_b[first_word];
                        string bi_word = Prob_a_b[word_to_check_for_bigram1];
                        string[] bi_word_regex = Regex.Split(bi_word, "#");


                        double prob_bigram_orig = 0;
                        foreach (string bi_chk in bi_word_regex)
                        {
                            if (bi_chk.Trim().Length > 0)
                            {
                                string val1 = bi_chk.Substring(0, bi_chk.IndexOf("\t"));
                                string data = bi_chk.Substring(bi_chk.IndexOf("\t") + 1);
                                if (data == word_to_check_for_bigram1)
                                {
                                    prob_bigram_orig = Math.Pow(10, Convert.ToDouble(val1));
                                    break;
                                }
                            }

                        }

                        if (word_coll.Contains(first_word))//context sensentive abbrivation expansion
                        {
                            string word_to_add = obj.Substring(start_ind + 1, len - 1);

                            string change = word_to_add.Replace("\t", " ");

                            string orig_typed = lastWord;//.Substring(lastWord.LastIndexOf(" ")).Trim();
                            string last_typed = orig_typed.Substring(2);
                            string check = obj.Substring(obj.LastIndexOf("\t"), obj.IndexOf(")") - obj.LastIndexOf("\t")).Trim();
                            //
                            double frq = Convert.ToDouble("-" + obj.Substring(0, obj.IndexOf("(")));
                            //double prob =1- Math.Pow(10, frq);
                            double prob = Math.Pow(10, frq);
                            string merge_change = prob.ToString() + "\t" + change;

                            string second_word = change.Substring(change.IndexOf(" ")).Trim(); //"लिखी जाती";



                            if (check.IndexOf(last_typed) >= 0)
                            {
                                if (Prob_a_bc.ContainsKey(second_word))
                                {
                                    //  MessageBox.Show(second_word+"\t"+Prob_a_bc[second_word].ToString());
                                }
                                else
                                {
                                    // MessageBox.Show(second_word);
                                }
                                // arr_data.Add(merge_change);
                                arr_data.Add((1 - prob).ToString() + "\t" + change);


                                string key_w = second_word;
                                string key_v = Prob_a_bc[second_word].ToString();
                                string[] key_value = Regex.Split(key_v, "#");

                                ArrayList temp_arr = new ArrayList();


                                foreach (string st in key_value)
                                {
                                    if (st.Trim().Length > 0)
                                    {
                                        double frq1 = Convert.ToDouble(st.Substring(0, st.IndexOf("\t")));

                                        double prob1 = Math.Pow(10, frq1);
                                        string merge_change1 = prob1.ToString() + "\t" + st.Substring(st.IndexOf("\t") + 1);

                                        // string prob_final = (1 - prob_ini_word*prob * prob1).ToString();
                                        string prob_final = (1 - prob_bigram_orig * prob * prob1).ToString();

                                        string Data_to_add = prob_final.ToString() + "\t" + change + " " + st.Substring(st.IndexOf("\t") + 1);
                                        arr_data.Add(Data_to_add);

                                    }

                                }

                                // arr_data.Sort();                                                          


                            }
                        }

                    }
                }
                arr_data.Sort();
                foreach (string st in arr_data)
                {
                    string only_data = st.Substring(st.IndexOf("\t") + 1);
                    listBox2.Items.Add(only_data);

                }


            }
            else
            {
                MessageBox.Show(search_key + "\tnot found");
            }
        }

        private void Isolated_abbr()
        {
            groupBox8.Visible = true;
            groupBox8.Location = new System.Drawing.Point(583, 10);




            string str = richTextBox1.Text;
            string[] wordarray = Regex.Split(str, " ");
            int LocOfLastWord = 0;
            LocOfLastWord = wordarray.Length - 1;
            string lastWord = wordarray[LocOfLastWord].ToString().Trim();
            ArrayList arr_data = new ArrayList();

            string search_key = "";

            if (lastWord.Length == 3)
            {
                search_key = lastWord;
            }
            else
            {
                search_key = lastWord.Substring(0, 3);

            }

            if (EBX.ContainsKey(search_key))
            {
                string coll = EBX[search_key];
                string[] str_arr = Regex.Split(coll, "-");
                listBox2.Items.Clear();
                foreach (string obj in str_arr)
                {
                    if (obj.Trim().Length > 0)
                    {

                        int start_ind = obj.IndexOf("(");
                        int len = obj.IndexOf(")") - start_ind;
                        string first_word = obj.Substring(start_ind + 1, obj.IndexOf("\t") - start_ind - 1);

                        //  if (word_coll.Contains(first_word))//context sensentive abbrivation expansion
                        {
                            string word_to_add = obj.Substring(start_ind + 1, len - 1);
                            string change = word_to_add.Replace("\t", " ");

                            string orig_typed = lastWord;
                            string last_typed = orig_typed.Substring(2);
                            string check = obj.Substring(obj.LastIndexOf("\t"), obj.IndexOf(")") - obj.LastIndexOf("\t")).Trim();

                            double frq = Convert.ToDouble("-" + obj.Substring(0, obj.IndexOf("(")));
                            double prob = 1 - Math.Pow(10, frq);
                            string merge_change = prob.ToString() + "\t" + change;

                            if (check.IndexOf(last_typed) >= 0)
                            {
                                arr_data.Add(merge_change);
                                //  listBox2.Items.Add(change);
                            }
                        }

                    }
                }
                arr_data.Sort();
                foreach (string st in arr_data)
                {
                    string only_data = st.Substring(st.IndexOf("\t") + 1);
                    listBox2.Items.Add(only_data);

                }
            }
            else
            {
                MessageBox.Show(search_key + "\tnot found");
            }
        }

        private void load_EBX()
        {
            if (File.Exists(@"..\..\..\..\N-Grams\Probability\EBB.txt"))
            {
                string filePath = @"..\..\..\..\N-Grams\Probability\EBB.txt";
                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] regencyWord = Regex.Split(total_text, "\n");

                foreach (string key in regencyWord)
                {
                    string[] array_of_word = Regex.Split(key, "#");
                    if (array_of_word.Length >= 2)
                    {
                        string data_string = array_of_word[1].ToString();
                        if (!EBX.ContainsKey(array_of_word[0].ToString()))
                        {
                            EBX.Add(array_of_word[0].ToString(), array_of_word[1].ToString());
                        }
                    }
                }

            }
        }

        private void formating_for_ebb()
        {

            SortedDictionary<string, string> Prob_t_a_bc = new SortedDictionary<string, string>();


            string filePath = @"..\..\..\..\N-Grams\Probability\triGram.txt";

            string total_text = File.ReadAllText(filePath, Encoding.UTF8);
            string[] regencyWord = Regex.Split(total_text, "\n");
            //  char[] list_of_char = { '०', '१', '२', '३', '४', '५', '६', '७', '८', '९', '<' };
            char[] list_of_char = { '०', '१', '२', '३', '४', '५', '६', '७', '८', '९', '<', 'ा', 'ी', 'ि', 'ु', 'ू', 'ृ', 'े', 'ै', 'ो', 'ौ', '्', 'ऽ', '॥', '़', 'ं', 'ः' };
            bool flag_repeat = false;

            foreach (string obj in regencyWord)
            {
                if (obj != "")
                {
                    try
                    {
                        String[] key_value = obj.Trim().Split(' ');

                        string first = key_value[1].ToString().Trim();
                        string second = key_value[2].ToString().Trim();
                        string third = key_value[3].ToString().Trim();

                        string key = key_value[1].ToString().Substring(0, 1).Trim() + key_value[2].ToString().Substring(0, 1).Trim() + key_value[3].ToString().Substring(0, 1).Trim() + "#";
                        string frq = key_value[0].ToString().Substring(1) + "(" + key_value[1].ToString().Trim() + "\t" + key_value[2].ToString().Trim() + "\t" + key_value[3].ToString().Trim() + ")";

                        if ((first == second) || (first == third) || (second == third))
                        {
                            flag_repeat = true;
                        }
                        else
                        {
                            flag_repeat = false;

                        }


                        int flg = key.IndexOfAny(list_of_char);
                        //if(!key.Contains("<"))
                        if (flg == -1 && (flag_repeat == false))
                        {
                            if (Prob_t_a_bc.ContainsKey(key))
                            {
                                Prob_t_a_bc[key] += frq;
                            }
                            else
                            {
                                Prob_t_a_bc.Add(key, frq);
                            }
                        }
                    }
                    catch (Exception ee)
                    { }
                }

            }

            File.WriteAllText(@"..\..\..\..\N-Grams\Probability\EBB.txt", "", Encoding.Unicode);
            foreach (KeyValuePair<string, string> pair in Prob_t_a_bc)
            {
                File.AppendAllText(@"..\..\..\..\N-Grams\Probability\EBB.txt", pair.Key + "\t" + pair.Value + "\n", Encoding.Unicode);
            }
            int ii = 0;
        }

        private void Formatting_data_t_a_bc()
        {
            SortedDictionary<string, string> Prob_t_a_bc = new SortedDictionary<string, string>();

            if (File.Exists(@"..\..\..\..\N-Grams\tagging\Tag_3gram.txt"))
            {
                //  StreamReader str = new StreamReader(@"..\..\..\..\N-Grams\tagging\Tag_3gram.txt", Encoding.UTF8);
                File.WriteAllText(@"..\..\..\..\N-Grams\tagging\n_Tag_3gram.txt", "", Encoding.Unicode);

                string filePath = @"..\..\..\..\N-Grams\tagging\Tag_3gram.txt";

                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] regencyWord = Regex.Split(total_text, "\r\n");
                foreach (string obj in regencyWord)
                {
                    if (obj != "")
                    {
                        try
                        {
                            String[] key_value = obj.Trim().Split(' ');


                            string data = key_value[0].ToString() + key_value[3].ToString() + "#";
                            string key = key_value[1].ToString() + key_value[2].ToString();
                            if (Prob_t_a_bc.ContainsKey(key))
                            {
                                Prob_t_a_bc[key] += data;
                            }
                            else
                            {
                                Prob_t_a_bc.Add(key, data);
                            }
                        }
                        catch (Exception ee)
                        { }
                    }

                }
            }
            foreach (KeyValuePair<string, string> pair in Prob_t_a_bc)
            {
                File.AppendAllText(@"..\..\..\..\N-Grams\tagging\n_Tag_3gram.txt", pair.Key + "\t" + pair.Value + "\n", Encoding.Unicode);
            }
            MessageBox.Show("done");
        }
        private void Formatting_data_Word_tag()
        {
            Dictionary<string, string> Prob_word_t = new Dictionary<string, string>();

            if (File.Exists(@"..\..\..\..\N-Grams\tagging\P_w_t.txt"))
            {
                //  StreamReader str = new StreamReader(@"..\..\..\..\N-Grams\tagging\Tag_3gram.txt", Encoding.UTF8);
                File.WriteAllText(@"..\..\..\..\N-Grams\tagging\n_P_w_t.txt", "", Encoding.Unicode);

                string filePath = @"..\..\..\..\N-Grams\tagging\P_w_t.txt";

                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] regencyWord = Regex.Split(total_text, "\r\n");
                foreach (string obj in regencyWord)
                {
                    if (obj != "")
                    {
                        try
                        {
                            String[] key_value = obj.Trim().Split(' ', '\t');


                            string data = key_value[0].ToString() + key_value[2].ToString() + "#";
                            string key = key_value[1].ToString();
                            if (Prob_word_t.ContainsKey(key))
                            {
                                Prob_word_t[key] += data;
                            }
                            else
                            {
                                Prob_word_t.Add(key, data);
                            }
                        }
                        catch (Exception ee)
                        { }
                    }

                }
            }
            foreach (KeyValuePair<string, string> pair in Prob_word_t)
            {
                File.AppendAllText(@"..\..\..\..\N-Grams\tagging\n_P_w_t.txt", pair.Key + "\t" + pair.Value + "\n", Encoding.Unicode);
            }
            MessageBox.Show("done");
        }

        private void Formatting_data_tag_word()
        {
            Dictionary<string, string> Prob_t_word = new Dictionary<string, string>();

            if (File.Exists(@"..\..\..\..\N-Grams\tagging\C_w_t.txt"))
            {
                //  StreamReader str = new StreamReader(@"..\..\..\..\N-Grams\tagging\Tag_3gram.txt", Encoding.UTF8);
                File.WriteAllText(@"..\..\..\..\N-Grams\tagging\n_P_t_w.txt", "", Encoding.Unicode);

                string filePath = @"..\..\..\..\N-Grams\tagging\C_w_t.txt";

                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] regencyWord = Regex.Split(total_text, "\r\n");
                foreach (string obj in regencyWord)
                {
                    if (obj != "")
                    {
                        try
                        {
                            String[] key_value = obj.Trim().Split(' ', '\t');


                            string data = key_value[1].ToString() + key_value[2].ToString() + "#";
                            string key = key_value[0].ToString();
                            if (Prob_t_word.ContainsKey(key))
                            {
                                Prob_t_word[key] += data;
                            }
                            else
                            {
                                Prob_t_word.Add(key, data);
                            }
                        }
                        catch (Exception ee)
                        { }
                    }

                }
            }
            foreach (KeyValuePair<string, string> pair in Prob_t_word)
            {
                File.AppendAllText(@"..\..\..\..\N-Grams\tagging\n_P_t_w.txt", pair.Key + "\t" + pair.Value + "\n", Encoding.Unicode);
            }
            MessageBox.Show("done");

            //  P_Tag_Word();
        }

        private static void P_Tag_Word()
        {
            SortedDictionary<string, int> unigram_t_w = new SortedDictionary<string, int>();
            SortedDictionary<string, int> unigram_w = new SortedDictionary<string, int>();
            SortedDictionary<string, string> prob = new SortedDictionary<string, string>();
            string file_w = @"D:\anmol_manoj\N-Grams\Probability\p_a.txt";//prob of word
            string total_text_w = File.ReadAllText(file_w, Encoding.UTF8);
            string[] wordDic_new_w = Regex.Split(total_text_w, "\n");
            string temp = "";
            foreach (string obj in wordDic_new_w)
            {
                temp = obj;
                if (temp.Trim().Length != 0)
                {
                    String[] array = Regex.Split(temp, " ", RegexOptions.Singleline);

                    if (unigram_w.ContainsKey(array[0].ToString()))
                    {
                        // unigram_t[temp] += 1;
                    }
                    else
                    {
                        unigram_w.Add(array[0].ToString(), Convert.ToInt32(array[1].ToString()));
                    }
                }

            }

            string file_t_w = @"D:\anmol_manoj\N-Grams\tagging\C_w_t.txt";
            string total_text_t_w = File.ReadAllText(file_t_w, Encoding.UTF8);
            string[] wordDic_new_t_w = Regex.Split(total_text_t_w, "\n");
            foreach (string obj in wordDic_new_t_w)
            {
                temp = obj;
                if (temp.Trim().Length != 0)
                {
                    String[] array = Regex.Split(temp, "\t", RegexOptions.Singleline);

                    if (unigram_t_w.ContainsKey(array[0].ToString()))
                    {
                        // unigram_t[temp] += 1;
                    }
                    else
                    {
                        unigram_t_w.Add(array[0].ToString(), Convert.ToInt32(array[1].ToString()));
                    }
                }

            }
            foreach (KeyValuePair<string, int> pair in unigram_t_w)
            {
                //File.AppendAllText(@"D:\anmol_manoj\N-Grams\tagging\P_w_t.txt", pair.Key + "\t" + pair.Value + "\r\n"); //File.AppendAllText(@"D:\merge\Final_vocab.txt", pair.Key + "\r\n", Encoding.Unicode);
                temp = pair.Key.ToString();
                String[] array = Regex.Split(temp, " ", RegexOptions.Singleline);
                string chk = array[1].ToString();
                double val = 0;
                int count_w;
                if (unigram_w.ContainsKey(chk))
                {
                    count_w = unigram_w[chk];
                    val = pair.Value / (double)count_w;
                    // unigram_w_t[pair.Key] += val;
                    if (prob.ContainsKey(pair.Key))
                    {

                    }
                    else
                    {
                        prob.Add(pair.Key, (Math.Log10(val)).ToString());
                    }

                }

            }
            foreach (KeyValuePair<string, string> pair in prob)
            {
                File.AppendAllText(@"D:\anmol_manoj\N-Grams\tagging\Final_P_w_t.txt", pair.Key + "\t" + pair.Value + "\r\n");
            }

            MessageBox.Show("done");

        }

        static bool flag_for_highlighter = false;
        private void underline()
        {
            string str = richTextBox1.Text;
            string[] wordarray = Regex.Split(str, " ");
            int LocOfLastWord = 0;
            LocOfLastWord = wordarray.Length - 1;
            string lastWord = wordarray[LocOfLastWord].ToString().Trim();




            ArrayList arr = new ArrayList();
            foreach (string st in listBox1.Items)
            {
                arr.Add(st);
            }

            try
            {
                //string secondLastWord = wordarray[LocOfLastWord - 1].ToString().Trim();
                //richTextBox1.SelectionStart = richTextBox1.Text.Length - secondLastWord.Length;
                //richTextBox1.SelectionLength = secondLastWord.Length;
                //richTextBox1.SelectionFont = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //richTextBox1.SelectionColor = Color.Black;
            }
            catch (Exception ee)
            { }



            //bool flag_for_highlighter = false;
            flag_for_highlighter = false;

            foreach (string st in arr)
            {
                if (lastWord == st.Substring(0, lastWord.Length))
                {
                    flag_for_highlighter = true;
                }
                else
                {

                    // flag = false;
                }
            }
            if (flag_for_highlighter == false)
            {
                richTextBox1.SelectionStart = richTextBox1.Text.Length - lastWord.Length;
                richTextBox1.SelectionLength = lastWord.Length;

                richTextBox1.SelectionFont = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                richTextBox1.SelectionColor = Color.Red;
            }

        }

        private void Storing_trigram()
        {
            if (File.Exists(@"..\..\..\..\N-Grams\Probability\triGram.txt"))
            {
                //StreamReader str = new StreamReader(@"..\..\..\..\N-Grams\Probability\p_a.txt", Encoding.UTF8);
                string filePath = @"..\..\..\..\N-Grams\Probability\triGram.txt";

                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] uniqWord = Regex.Split(total_text, "\n");
                Prob_a_bc.Clear();
                //char[] list_of_char = { '०', '१', '२', '३', '४', '५', '६', '७', '८', '९', '<', 'ा', 'ी', 'ि', 'ु', 'ू', 'ृ', 'े', 'ै', 'ो', 'ौ', '्', 'ऽ', '॥', '़', 'ं', 'ः' };
                char[] list_of_char = { '०', '१', '२', '३', '४', '५', '६', '७', '८', '९', '<', 'ऽ', '॥' };

                foreach (string obj in uniqWord)
                {
                    if (obj != "")
                    {
                        try
                        {
                            String[] key_value = obj.Split(' ');

                            string one = key_value[1].Trim();
                            string two = key_value[2].Trim();
                            string three = key_value[3].Trim();
                            string prob = key_value[0].Substring(1);
                            //string key = one + " " + two + " " + three;
                            string key = one + " " + two;

                            if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                            {
                                //if (!Prob_a_bc.ContainsKey(key))
                                //{
                                //    Prob_a_bc.Add(key, prob);
                                //}

                                if (!Prob_a_bc.ContainsKey(key))
                                {
                                    Prob_a_bc.Add(key, prob + "\t" + three + "#");
                                }
                                else
                                {

                                    Prob_a_bc[key] += prob + "\t" + three + "#";

                                }
                            }


                        }
                        catch (Exception ee)
                        { }
                    }
                }
                File.WriteAllText(@"..\..\..\..\N-Grams\Probability\triGram_new.txt", " ", Encoding.Unicode);
                foreach (KeyValuePair<string, string> pair in Prob_a_bc)
                {
                    File.AppendAllText(@"..\..\..\..\N-Grams\Probability\triGram_new.txt", pair.Key + "\t" + pair.Value + "\n", Encoding.Unicode);
                }

            }
        }

        #endregion


        #region old2

        private Color function_individual_button(ArrayList arr_char, Color n_color, object ob)
        {
            int cnt = 0;
            foreach (string st in arr_char)
            {
                cnt++;
                if (((Button)ob).Text == st)//error in this loop
                {


                    string i = cnt.ToString();
                    switch (i)
                    {
                        case "1":
                            n_color = Color.Tomato;
                            // n_color = Color.HotPink;

                            break;
                        case "2":
                            n_color = Color.Tomato;

                            break;
                        case "3":
                            n_color = Color.Tomato;

                            break;
                        case "4":
                            n_color = Color.Yellow;
                            // n_color = Color.DarkTurquoise;

                            break;
                        case "5":
                            n_color = Color.Yellow;

                            break;
                        case "6":
                            // n_color = Color.SpringGreen;
                            n_color = Color.SpringGreen;

                            break;
                        case "7":
                            n_color = Color.SpringGreen;

                            break;
                        default:
                            break;

                    }

                    old_color = ((Button)ob).BackColor;
                    ((Button)ob).BackColor = n_color;

                    ((Button)ob).FlatAppearance.BorderSize = 4;
                    ((Button)ob).FlatAppearance.BorderColor = Color.LightSeaGreen;

                    if (!color_array_button.Contains(n_color))
                    {
                        color_array_button.Add(n_color);
                    }


                }
            }
            return n_color;
        }

        private Color function_individual_button_play_button(ArrayList all_list, ArrayList arr_char, Color n_color, object ob, string lastword)
        {

            Color[] list_of_color = new Color[7];

            color_array_button.CopyTo(list_of_color);
            int cnt = 0;
            //foreach (string st in all_list)
            // foreach (string st in listBox1.Items)
            for (int i = 0; i < listBox1.Items.Count; i++)
            {

                //if (((Button)ob).Text == st)
                string chk = (listBox1.Items[i].ToString().Substring(0, lastword.Length + 1));
                foreach (string ob1 in arr_char)
                {
                    if (ob1 == chk)
                    {

                        if (((Button)ob).Name == "bpl" + i.ToString())
                        {
                            string val = chk.Substring(chk.Length - 1, 1);

                            n_color = list_of_color[i];

                            old_color = ((Button)ob).BackColor;
                            ((Button)ob).BackColor = n_color;
                        }


                    }
                }
            }
            return n_color;
        }
        private void reset_size(Button ob)
        {
            if (((Button)ob).FlatAppearance.BorderSize == 4)
            {
                ((Button)ob).FlatAppearance.BorderSize = 2;
                ((Button)ob).Size = new System.Drawing.Size(36, 35);
                ((Button)ob).FlatAppearance.BorderColor = Color.LightSteelBlue;
            }
        }
        private void Re_set_back_color()
        {
            old_color = Color.White;
            foreach (object pnl in panel13.Controls)
            {
                if (pnl is Panel)
                {

                }
                if (pnl is Button)
                {
                    ((Button)pnl).BackColor = old_color;
                    reset_size((Button)pnl);

                }
            }
            foreach (object pnl in panel12.Controls)
            {
                if (pnl is Panel)
                {

                }
                if (pnl is Button)
                {
                    if (old_char_selected == "")
                    {
                        ((Button)pnl).BackColor = old_color;
                    }
                    else
                    {
                        ((Button)pnl).BackColor = old_color;
                    }
                    reset_size((Button)pnl);


                }
            }
            //foreach (object pln in panel11.Controls)
            //{
            //    if (pln is Panel)
            //        foreach (object ob in ((Panel)pln).Controls)
            //        {
            //            if (ob is Button)
            //            {

            //                ((Button)ob).BackColor = old_color;

            //            }
            //            if (ob is Panel)
            //            {
            //                foreach (object ob_new in ((Panel)ob).Controls)
            //                {
            //                    if (ob_new is Button)
            //                    {
            //                        ((Button)ob_new).BackColor = old_color;
            //                    }
            //                    if (ob_new is Panel)
            //                    {
            //                        foreach (object innerPanel in ((Panel)ob_new).Controls)
            //                        {
            //                            if (innerPanel is Button)
            //                            {
            //                                ((Button)innerPanel).BackColor = old_color;

            //                            }
            //                        }

            //                    }

            //                }

            //            }
            //        }
            //}
            int chk = 0;
        }

        private void load_new_bigram()
        {
            if (File.Exists(@"..\..\..\..\N-Grams\Probability\p_a_b.txt")) //load
            {
                Prob_a_b = new Dictionary<string, string>();
                StreamReader str = new StreamReader(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\biGram_new.txt", Encoding.UTF8);
                //  StreamReader str = new StreamReader(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\tt.txt", Encoding.UTF8);
                while (str.Peek() != -1)
                {
                    String line = str.ReadLine();    //read each line and load it in dictionary
                    String[] key_value = line.Split(' ');

                    //if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    //{
                    //    if (!Prob_a_b.ContainsKey(key_value[0]))
                    //        Prob_a_b.Add(key_value[0], key_value[1]);
                    //}
                    string st1 = line.Substring(0, line.IndexOf(' '));
                    string st2 = line.Substring(st1.Length);
                    if (!Prob_a_b.ContainsKey(st1))
                        Prob_a_b.Add(st1, st2);

                }
            }
        }
        private void load_new_unigram()
        {
            if (File.Exists(@"..\..\..\..\N-Grams\Probability\p_a_b.txt")) //load
            {
                Prob_a = new Dictionary<string, decimal>();
                //   StreamReader str = new StreamReader(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\biGram_new.txt", Encoding.UTF8);
                StreamReader str = new StreamReader(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\uniGram_new.txt", Encoding.UTF8);
                while (str.Peek() != -1)
                {
                    String line = str.ReadLine();    //read each line and load it in dictionary
                    String[] key_value = line.Split(' ');

                    //if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    //{
                    //    if (!Prob_a_b.ContainsKey(key_value[0]))
                    //        Prob_a_b.Add(key_value[0], key_value[1]);
                    //}
                    string st1 = line.Substring(0, line.IndexOf(' '));
                    string st2 = line.Substring(st1.Length);
                    if (!Prob_a.ContainsKey(st1))
                        Prob_a.Add(st1, Convert.ToDecimal(st2));

                }
            }
        }

        private void bi_gram_saperator()
        {
            //bigram saperator
            if (File.Exists(@"..\..\..\..\N-Grams\Probability\p_a_b.txt"))
            {
                StreamReader str = new StreamReader(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\biGram.txt", Encoding.UTF8);
                File.WriteAllText(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\biGram_new.txt", " ", Encoding.Unicode);
                //while (str.Peek() != -1)
                //{
                //    String line = str.ReadLine();    //read each line and load it in dictionary
                //    //String[] key_value = line.Split('\t');

                //    line = line.Replace("[", "");
                //    line = line.Replace("]", "");
                string file = @"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\biGram.txt";
                string total_text = File.ReadAllText(file, Encoding.UTF8);
                string[] wordDic_new = Regex.Split(total_text, "\n");
                string line = "";
                foreach (string obj in wordDic_new)
                {
                    line = obj;
                    line = line.Replace("[", "");
                    line = line.Replace("]", "");
                    String[] key_value = line.Split(' ');

                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        try
                        {
                            if (!Prob_a_b_new.ContainsKey(key_value[1]))
                            {
                                Prob_a_b_new.Add(key_value[1], key_value[0] + "\t" + key_value[2] + "#");
                            }
                            else
                            {
                                //  Prob_a_b_new[key_value[1]] += "#" + key_value[0] + "\t" + key_value[2];

                                Prob_a_b_new[key_value[1]] += key_value[0] + "\t" + key_value[2] + "#";

                            }

                        }
                        catch (Exception ee)
                        { }
                    }
                }
            }
            foreach (KeyValuePair<string, string> ob in Prob_a_b_new)
            {
                File.AppendAllText(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\biGram_new.txt", ob.Key + " " + ob.Value + "\n", Encoding.Unicode);

            }
        }
        private void tri_gram_saperator()
        {
            //bigram saperator
            if (File.Exists(@"..\..\..\..\N-Grams\Probability\p_a_b.txt"))
            {
                StreamReader str = new StreamReader(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\tt1.txt", Encoding.UTF8);
                while (str.Peek() != -1)
                {
                    String line = str.ReadLine();    //read each line and load it in dictionary
                    //String[] key_value = line.Split('\t');

                    line = line.Replace("[", "");
                    line = line.Replace("]", "");

                    String[] key_value = line.Split(' ');

                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        try
                        {
                            if (!Prob_a_b_new.ContainsKey(key_value[1]))
                            {
                                Prob_a_b_new.Add(key_value[1] + " " + key_value[2], key_value[0] + "\t" + key_value[3] + "#");
                            }
                            else
                            {
                                //  Prob_a_b_new[key_value[1]] += "#" + key_value[0] + "\t" + key_value[2];

                                Prob_a_b_new[key_value[1]] += key_value[0] + " " + key_value[2] + "#";

                            }

                        }
                        catch (Exception ee)
                        { }
                    }
                }
            }
            foreach (KeyValuePair<string, string> ob in Prob_a_b_new)
            {
                File.AppendAllText(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\biGram_new.txt", ob.Key + " " + ob.Value + "\n", Encoding.Unicode);

            }
        }
        private void uni_gram_saperator()
        {
            //bigram saperator
            if (File.Exists(@"..\..\..\..\N-Grams\Probability\p_a_b.txt"))
            {
                StreamReader str = new StreamReader(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\uniGram.txt", Encoding.UTF8);
                while (str.Peek() != -1)
                {
                    String line = str.ReadLine();    //read each line and load it in dictionary
                    //String[] key_value = line.Split('\t');

                    line = line.Replace("[", "");
                    line = line.Replace("]", "");
                    line = line.Replace("\t", " ");
                    line = line.Replace(",", "");

                    String[] key_value = line.Split(' ');

                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        try
                        {
                            if (!Prob_a_b_new.ContainsKey(key_value[1]))
                            {
                                Prob_a_b_new.Add(key_value[1], key_value[2]);
                            }
                            else
                            {
                                //  Prob_a_b_new[key_value[1]] += "#" + key_value[0] + "\t" + key_value[2];

                                //  Prob_a_b_new[key_value[1]] += key_value[0] + " " + key_value[2] + "#";

                            }

                        }
                        catch (Exception ee)
                        { }
                    }
                }
            }
            foreach (KeyValuePair<string, string> ob in Prob_a_b_new)
            {
                File.AppendAllText(@"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\uniGram_new.txt", ob.Key + " " + ob.Value + "\n", Encoding.Unicode);

            }
        }
        public void Word_Level_Unigram(string text)
        {
            text = text.Replace("$", "<START> ");
            string[] words = text.Split(' ');
            toolStripStatusLabel1.Text = "Bigram";
            this.Refresh();
            //if (text.Trim().Length != 0)
            {
                string last_word = words[words.Length - 1];
                listBox1.Items.Clear();
                word_coll.Clear();
                int len = last_word.Length;
                var word_list = from entry in Prob_a where (entry.Key.Length >= len && entry.Key.Substring(0, len) == last_word) select entry.Key;
                foreach (string word in word_list)
                {
                    // if (word.StartsWith("$"))
                    if (word.StartsWith("<START>"))
                    {
                        listBox1.Items.Add(word.Remove(0, 1));
                        this.Text = "uni-gram";
                    }
                    else
                    {
                        listBox1.Items.Add(word);
                        word_coll.Add(word);//may be remove if not req used for checking highlighter
                    }
                }
                //  Unigram_temp(words);
                this.Text = "Word unigram";   //call the temporary storage dictionary


            }

        }

        public void Word_Level_Bi_gram(string text)
        {
            this.Text = "bigram";

            //text = text.Replace("$", "<START> ");
            text = text.Replace("$", " ");

            string[] words = text.Split(' ');

            if (words[words.Length - 1].ToString() == "")
            {
                string second_last_word = words[words.Length - 2];
                string last_word = words[words.Length - 1];
                int len = last_word.Length;
                string value = null;

                if (Prob_a_b.TryGetValue(second_last_word, out value))
                {

                    value = value.Replace(" ", "\t");
                    string[] raw_values = value.Split('#');
                    List<string> values = new List<string>();
                    toolStripStatusLabel1.Text = "Bigram";
                    this.Refresh();
                    foreach (string raw_value in raw_values)
                    {
                        if (raw_value != null && raw_value != "")
                            values.Add(raw_value.Substring(raw_value.Trim().IndexOf('\t') + 1).Trim());
                    }
                    var item_values = from item in values where (item.Length >= len && item.Substring(0, len) == last_word && item != null && item != "") select item.Trim();

                    listBox1.Items.Clear();
                    word_coll.Clear();
                    foreach (string ob in values)
                    {
                        if ((ob.ToString() == "<UNK>") || ((ob.ToString() == "<EOF>")))
                        {
                            // MessageBox.Show(ob.ToString());
                        }
                        else
                        {
                            listBox1.Items.Add(ob);
                            word_coll.Add(ob);//may be remove if not req used for checking highlighter
                        }

                    }

                    if (listBox1.Items.Count == 0)
                        Word_Level_Unigram(text);
                }
                else
                {
                    // string value = "";
                    // if (Prob_a.TryGetValue(text, out value))
                    if (Spacebar_flag == true)
                    {
                        //Word_Level_Unigram(text);
                        Spacebar_flag = false;
                    }
                }
            }
            else
            {
                if (word_coll.Count == 0)
                {
                    Word_Level_Unigram(text);
                }
                else
                {


                    string last_word = words[words.Length - 1];
                    int len = last_word.Length;

                    //listBox1.Items.Clear();

                    string str1 = richTextBox1.Text;
                    string[] wordarray = Regex.Split(str1, " ");
                    int LocOfLastWord = 0;
                    LocOfLastWord = wordarray.Length - 1;
                    string lastWord = wordarray[LocOfLastWord].ToString().Trim();
                    string typedString = lastWord;
                    string targetString = "";
                    float ngramScore = 0.0F;
                    int phonatic_value = 0;

                    string firstChar = lastWord.Substring(0, 1);
                    ArrayList arr = new ArrayList();
                    string second_last_word = words[words.Length - 2];
                    string output = "";

                    string value = null;

                    if (Prob_a_b.TryGetValue(second_last_word, out value))
                    {
                        output = Prob_a_b[second_last_word].ToString();
                        string[] wordarrayProb = Regex.Split(output, "#");
                        prob_list.Clear();
                        foreach (string str_val in wordarrayProb)
                        {
                            try
                            {
                                string obj = str_val;
                                String[] key_value = obj.Split('\t');

                                if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                                {
                                    if (!prob_list.ContainsKey(key_value[1]))
                                        prob_list.Add(key_value[1], key_value[0].ToString());
                                }

                            }
                            catch (Exception ee)
                            { }
                        }

                    }




                    foreach (string obj in word_coll)
                    {
                        // Core_method(str, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj);

                        string wordFrq = "";

                        //if (wordFrq.Trim().Length == 0)
                        if (prob_list.TryGetValue(obj, out value))
                        {
                            wordFrq = prob_list[obj].ToString();
                        }
                        else
                        {
                            wordFrq = "0";
                        }
                        if (obj.Length > 1)
                        {
                            Core_method_Correct_word(str1, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj, wordFrq);
                        }
                    }

                    if (arr.Count < 7)
                    {
                        //Word_Level_Unigram(text);
                        toolStripStatusLabel1.Text = "Uni-gram";
                        this.Refresh();

                        toolStripStatusLabel3.Text = "Roll Back";
                        int len1 = last_word.Length;
                        var word_list = from entry in Prob_a where (entry.Key.Length >= len1 && entry.Key.Substring(0, len1) == last_word) select entry.Key;
                        foreach (string word in word_list)
                        {
                            // if (word.StartsWith("$"))
                            if (word.StartsWith("<START>"))
                            {
                                listBox1.Items.Add(word.Remove(0, 1));
                                this.Text = "uni-gram";
                            }
                            else
                            {
                                listBox1.Items.Add(word);
                                word_coll.Add(word);//may be remove if not req used for checking highlighter
                            }
                        }
                        foreach (string obj in word_coll)
                        {
                            //string wordFrq = "0";
                            try
                            {
                                string wordFrq = Prob_a[obj].ToString();
                                if (obj.Length > 2)
                                {
                                    Core_method_Correct_word(str1, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj, wordFrq);
                                }
                            }
                            catch (Exception e3)
                            { }

                        }
                        //MessageBox.Show(arr.Count.ToString()); 
                        toolStripStatusLabel3.Text = "Low....";
                    }
                    else
                    {
                        toolStripStatusLabel3.Text = "Normal Progress";
                    }

                    toolStripStatusLabel2.Text = "Ranking Stated";

                    Sort_error_handler(arr);
                    toolStripStatusLabel2.Text = "Ranking Done";


                }
            }
        }

        public void Word_Level_Bi_gram_old(string text)
        {
            this.Text = "bigram";
            //text = text.Replace("$", "<START> ");
            text = text.Replace("$", " ");

            string[] words = text.Split(' ');

            if (words[words.Length - 1].ToString() == "")
            {
                string second_last_word = words[words.Length - 2];
                string last_word = words[words.Length - 1];
                int len = last_word.Length;
                string value = null;

                if (Prob_a_b.TryGetValue(second_last_word, out value))
                {

                    value = value.Replace(" ", "\t");
                    string[] raw_values = value.Split('#');
                    List<string> values = new List<string>();
                    foreach (string raw_value in raw_values)
                    {
                        if (raw_value != null && raw_value != "")
                            values.Add(raw_value.Substring(raw_value.Trim().IndexOf('\t') + 1).Trim());
                    }
                    var item_values = from item in values where (item.Length >= len && item.Substring(0, len) == last_word && item != null && item != "") select item.Trim();

                    listBox1.Items.Clear();
                    word_coll.Clear();
                    foreach (string ob in values)
                    {
                        if ((ob.ToString() == "<UNK>") || ((ob.ToString() == "<EOF>")))
                        {
                            // MessageBox.Show(ob.ToString());
                        }
                        else
                        {
                            listBox1.Items.Add(ob);
                            word_coll.Add(ob);//may be remove if not req used for checking highlighter
                        }

                    }

                    if (listBox1.Items.Count == 0)
                        Word_Level_Unigram(text);
                }
                else
                    Word_Level_Unigram(text);
            }
            else
            {
                if (word_coll.Count == 0)
                {
                    Word_Level_Unigram(text);
                }
                else
                {

                    //local data base addition
                    string total_text = "";
                    foreach (string ob in word_coll)
                    {
                        total_text += ob + "\n";

                    }



                    string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();

                    string fileLoc_word = strCurrentDirectory + @"\Data\Local_Data.txt";
                    string total_local_text = File.ReadAllText(fileLoc_word, Encoding.UTF8);

                    if (total_local_text.Trim().Length != 0)
                    {
                        string only_text = total_local_text.Substring(0, total_local_text.IndexOf("\t"));
                        int val_chk = total_text.IndexOf(only_text);

                        if (val_chk > 0)
                        {
                            total_text += total_local_text;

                        }
                    }





                    string[] old_dic = Regex.Split(total_text, "\n");

                    word_coll.Clear();
                    foreach (string ob1 in old_dic)
                    {
                        word_coll.Add(ob1);
                    }



                    string last_word = words[words.Length - 1];
                    int len = last_word.Length;

                    listBox1.Items.Clear();

                    string str = richTextBox1.Text;
                    string[] wordarray = Regex.Split(str, " ");
                    int LocOfLastWord = 0;
                    LocOfLastWord = wordarray.Length - 1;
                    string lastWord = wordarray[LocOfLastWord].ToString().Trim();
                    string typedString = lastWord;
                    string targetString = "";
                    float ngramScore = 0.0F;
                    int phonatic_value = 0;
                    string firstChar = lastWord.Substring(0, 1);
                    ArrayList arr = new ArrayList();
                    foreach (string obj in word_coll)
                    {
                        // Core_method(str, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj);
                        Core_method_Correct_word(str, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj);
                    }
                    Sort_error_handler(arr);


                }
            }
        }  //which was working with new word learning      and unigram lexico also

        public void Word_Level_Bi_gram_imergency(string text)
        {
            //text = text.Replace("$", "<START> ");
            text = text.Replace("$", " ").Trim();
            // string first_char = "$";

            //if (first_char == text.Substring(0, 1))
            //{
            //    text = text.Substring(1);
            //}
            text = text.Trim();
            string[] words = text.Split(' ');
            if (words.Length >= 1)
            {
                // string second_last_word = words[words.Length - 2];
                string last_word = words[words.Length - 1];
                int len = last_word.Length;
                string value = null;
                if (Prob_a_b.ContainsKey(last_word))
                {
                    MessageBox.Show("y");
                }
                else
                {
                    //   MessageBox.Show("n");

                }
                string filePath = @"D:\anmol_manoj\PredictiveKeyboard\PredictiveKeyboard\bin\Debug\inp\biGram_new.txt";

                string total_text = File.ReadAllText(filePath, Encoding.UTF8);
                string[] Prob = Regex.Split(total_text, "\n");



                var earlyBirdQuery = from word in Prob where (word.Length >= len && word.Substring(0, len) == last_word) select word;
                foreach (var v in earlyBirdQuery)
                {
                    word_coll.Add(v);
                }


                //foreach (string obj in Prob)
                //{

                //}



                if (Prob_a_b.TryGetValue(last_word, out value)) // searching and retreiveing in one step
                {
                    // Prob_a_b.cont
                    //value = value.Replace("\t", " ");
                    value = value.Replace(" ", "\t");
                    string[] raw_values = value.Split('#');
                    List<string> values = new List<string>();
                    foreach (string raw_value in raw_values)
                    {
                        if (raw_value != null && raw_value != "")
                            values.Add(raw_value.Substring(raw_value.Trim().IndexOf('\t') + 1).Trim());
                    }
                    var item_values = from item in values where (item.Length >= len && item.Substring(0, len) == last_word && item != null && item != "") select item.Trim();
                    listBox1.Items.Clear();
                    word_coll.Clear();
                    foreach (string item in item_values)
                    {
                        if (item.StartsWith("$"))
                        //if (item.StartsWith("<START>"))
                        {
                            listBox1.Items.Add(item.Remove(0, 1));
                            word_coll.Add(item.Remove(0, 1));
                            this.Text = "Bi-gram";
                        }
                        else
                        {
                            listBox1.Items.Add(item);
                            word_coll.Add(item);

                        }
                    }
                    listBox1.Items.Clear();
                    word_coll.Clear();
                    foreach (string ob in values)
                    {
                        listBox1.Items.Add(ob);
                        word_coll.Add(ob);//may be remove if not req used for checking highlighter

                    }
                    //Bigram_temp(words);   // Here the temp adding can be done
                    //Unigram_temp(words);
                    if (listBox1.Items.Count == 0)
                        Word_Level_Unigram(text);
                }
                else
                    Word_Level_Unigram(text);
            }
            else
                Word_Level_Unigram(text);
        }



        Dictionary<string, string> DataBase;

        public void adding_local_dic()
        {

            string str2 = richTextBox1.Text;
            string[] wordarray = Regex.Split(str2, " ");
            int LocOfLastWord = wordarray.Length - 1;
            string lastWord = wordarray[LocOfLastWord].ToString().Trim();
            string typedString = lastWord;
            //if (Prob_a_b.ContainsKey(typedString))        //used for bigram list check
            if (Prob_a.ContainsKey(typedString))            //used for unigram list check
            {

            }
            else
            {

                try
                {
                    string str1 = richTextBox1.Text;

                    string firstChar = lastWord.Substring(0, 1);
                    string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();
                    string path = strCurrentDirectory + @"\Data\Local_Data.txt";
                    if (File.Exists(path))
                    {
                        StreamReader str = new StreamReader(path, Encoding.UTF8);
                        while (str.Peek() != -1)
                        {
                            String line = str.ReadLine();    //read each line and load it in dictionary
                            String[] key_value = line.Split('\t');
                            if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                            {
                                if (!Loc_dic.ContainsKey(key_value[0]))
                                {
                                    Loc_dic.Add(key_value[0], key_value[1]);

                                }
                            }

                        }
                        str.Close();
                        data = typedString;  //used for unigram list check
                        if (!Loc_dic.ContainsKey(data))
                        {
                            File.AppendAllText(path, lastWord + "\t" + "1".ToString() + "\n", Encoding.UTF8);
                            toolStripStatusLabel2.Text = "File added to local database";
                        }
                        // MessageBox.Show("done");
                    }
                }
                catch (Exception ee)
                { }




            }





            ///local end
        }




        public void adding_local_dic_for_bigram()
        {

            try
            {
                string str = richTextBox1.Text;
                string[] wordarray = Regex.Split(str, " ");
                int LocOfLastWord = wordarray.Length - 1;
                string lastWord = wordarray[LocOfLastWord].ToString().Trim();
                string firstChar = lastWord.Substring(0, 1);
                string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();


                string fileLoc_word = strCurrentDirectory + @"\Data\Local_Data.txt";
                string total_local_text = File.ReadAllText(fileLoc_word, Encoding.UTF8);



                string[] wordDic_new = Regex.Split(total_local_text, "\n");

                if (!word_coll.Contains(lastWord))
                {
                    if (!wordDic_new.Contains(lastWord + "\t1"))
                    {
                        File.AppendAllText(fileLoc_word, lastWord + "\t" + "1".ToString() + "\n", Encoding.UTF8);
                    }
                }
            }
            catch (Exception ee)
            { }

            ///local end
        }


        public void New_word_adding()
        {

            try
            {
                string str = richTextBox1.Text;
                string[] wordarray = Regex.Split(str, " ");
                int LocOfLastWord = wordarray.Length - 1;
                string lastWord = wordarray[LocOfLastWord].ToString().Trim();
                string firstChar = lastWord.Substring(0, 1);
                string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();


                string fileLoc_word = strCurrentDirectory + @"\Data\Local_Data.txt";
                string total_local_text = File.ReadAllText(fileLoc_word, Encoding.UTF8);



                string[] wordDic_new = Regex.Split(total_local_text, "\n");



                if (!word_coll.Contains(lastWord))
                {
                    if (!wordDic_new.Contains(lastWord + "\t1"))
                    {
                        File.AppendAllText(fileLoc_word, lastWord + "\t" + "1".ToString() + "\n", Encoding.UTF8);
                    }
                }
            }
            catch (Exception ee)
            { }

            ///local end
        }

        public void isolated_word(string text)
        {
            string str = richTextBox1.Text;
            string[] wordarray = Regex.Split(str, " ");
            int LocOfLastWord = 0;
            LocOfLastWord = wordarray.Length - 1;
            string lastWord = wordarray[LocOfLastWord].ToString().Trim();
            string typedString = lastWord;
            string targetString = "";
            float ngramScore = 0.0F;
            int phonatic_value = 0;
            ArrayList arr = new ArrayList();
            if (str != "")
            {
                string lastChar = str.Substring(str.Length - 1, 1);
                if (lastChar != " ")
                {
                    string firstChar = lastWord.Substring(0, 1);
                    string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();

                    //     string filePath = strCurrentDirectory = strCurrentDirectory + @"\inp\uniGram_new.txt";
                    string filePath = strCurrentDirectory + @"\Data\Dic\" + firstChar + ".txt";
                    string total_text = File.ReadAllText(filePath, Encoding.UTF8);


                    //local data base addition


                    string fileLoc_word = strCurrentDirectory + @"\Data\Local_Data.txt";
                    string total_local_text = File.ReadAllText(fileLoc_word, Encoding.UTF8);

                    total_text += total_local_text;

                    string[] wordDic_new = Regex.Split(total_text, "\n");
                    //if (!wordDic_new.Contains(lastWord))
                    //{
                    //    File.AppendAllText(fileLoc_word, lastWord + "\t" + "1".ToString() + "\n", Encoding.UTF8);
                    //}

                    ///local end


                    ArrayList arr_word = new ArrayList();


                    foreach (string temp in wordDic_new)// adding in dictionary for temp chek
                    {
                        if (temp.Contains("\t"))
                        {
                            string v1 = temp.Substring(0, temp.IndexOf("\t"));
                            if (!list_of_word.ContainsKey(v1))
                            {
                                list_of_word.Add(v1, 1);
                                //unigram_word.Add(key_value[0], Convert.ToInt32(key_value[1]));
                            }
                        }

                    }


                    try
                    {
                        string strL = lastWord;
                        int len = strL.Length;
                        var earlyBirdQuery = from word in wordDic_new where word.Substring(0, len) == str select word;
                        foreach (var v in earlyBirdQuery)
                        {
                            // MessageBox.Show(v.Substring(0, v.IndexOf(" "))); 
                            arr_word.Add(v.ToString());
                            //  arr_word.Add(v.Substring(0, v.IndexOf(" ")));
                        }
                    }
                    catch (Exception ee)
                    { }
                    if (arr_word.Count >= windowSize)
                    {
                        foreach (string obj in arr_word)
                        {
                            // Core_method(str, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj);
                            Core_method_Correct_word(str, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj);
                        }
                    }
                    else
                    {
                        foreach (string obj in wordDic_new)
                        {
                            // Core_method(str, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj);
                            Core_method_Correct_word(str, typedString, ref targetString, ref ngramScore, phonatic_value, arr, firstChar, obj);
                        }

                    }


                }


            }


            Sort_error_handler(arr);
        }

        private void Sort_error_handler(ArrayList arr)
        {
            arr.Sort();
            int cout_for_list1 = 0;
            string old_str = "";
            listBox1.Items.Clear();
            word_coll.Clear();
            foreach (string st in arr)
            {
                string val = st.Substring(st.IndexOf('[') + 1, st.IndexOf(']') - st.IndexOf('[') - 1);
                cout_for_list1++;
                if (st.Trim() != old_str.Trim())
                {
                    // if (cout_for_list1 <= 7) 
                    word_coll.Add(val);
                    if (cout_for_list1 <= windowSize)
                    {
                        listBox1.Items.Add(val);//check here

                    }
                    //  listBox2.Items.Add(st);
                }
                else
                {
                    MessageBox.Show(st);
                }

                // this.Text = "isolated";
                // WordLevel_bigram();


            }
        }
        bool FlagUsedRegency = true;
        bool FlagUsedlocalDic = false;
        bool FlagUsedEDscr = true;
        bool FlagUsedPscore = true;
        bool FlagUsedLCScore = false;
        bool FlagUsedngramScore = false;
        bool FlagUsedlenScore = true;
        bool FlagUsediFreq = true;
        bool FlagUsedBannedWord = false;
        bool FlagUsedminLength = false;
        bool FlagUsedBiGram = false;
        Dictionary<string, string> Regency;
        private void load_regency()
        {

            //local  start
            DataBase = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            //if (DataBase.ContainsKey(character))
            //{
            //    // Dic_TriGram[character] = Dic_TriGram[character] ;

            //}
            //else
            //{

            //    DataBase.Add(character, wordDic[i].Substring(len + 1));
            //}

            ///loacl end
            Regency = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();
            string filePath = strCurrentDirectory + @"\Data\Regency_Data.txt";
            string total_text = File.ReadAllText(filePath, Encoding.UTF8);
            string[] regencyWord = Regex.Split(total_text, "\n");
            foreach (string obj in regencyWord)
            {
                if (obj != "")
                {
                    string data = obj.Substring(0, obj.IndexOf("\t"));
                    string key = obj.Substring(obj.IndexOf("\t"));
                    if (Regency.ContainsKey(data))
                    {

                    }
                    else
                    {
                        Regency.Add(data, key);
                    }
                }
            }
        }

        #endregion


        #region old1


        private void button15_Click(object sender, EventArgs e)
        {
            groupBox8.Visible = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            groupBox8.Visible = false;
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            // groupBox8.Visible = true;
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            groupBox8.Visible = false;
            if (richTextBox1.Text.LastIndexOf(' ') != -1)
            {
                string new_text = richTextBox1.Text.Substring(0, richTextBox1.Text.LastIndexOf(' ') + 1);
                string val = listBox2.SelectedItem.ToString();
                richTextBox1.Text = new_text + val + " ";
                string toPrint = "(" + val + ")";
                Log_data(toPrint);
            }
            else
            {
                string val = listBox2.SelectedItem.ToString();
                richTextBox1.Text = val + " ";
                string toPrint = "(" + val + ")";
                Log_data(toPrint);


            }
        }

        private void button15_Click_2(object sender, EventArgs e)
        {
            groupBox8.Visible = false;
            if (richTextBox1.Text.LastIndexOf(' ') != -1)
            {
                string new_text = richTextBox1.Text.Substring(0, richTextBox1.Text.LastIndexOf(' ') + 1);

                string str = richTextBox1.Text;
                string[] wordarray = Regex.Split(str, " ");
                int LocOfLastWord = wordarray.Length - 1;
                string lastWord = wordarray[LocOfLastWord].ToString().Trim();

                string filanl_data = new_text + lastWord.Substring(0, 1);

                richTextBox1.Text = filanl_data;

            }
            else
            {
                //string val = listBox2.SelectedItem.ToString();
                //richTextBox1.Text = val + " ";
                //Cal_Regency_method(val);//chek


            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextBasedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Context_based_abbr();
            if (listBox2.Items.Count < 1)
            {
                Isolated_abbr();
            }
        }

        private void isolatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Isolated_abbr();
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        string output = "";
        private void richTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string str = richTextBox2.Text;



            Transliteration obj = new Transliteration();
            output = obj.transliterate(str);



            richTextBox1.Text = output;
            //  richTextBox2.Text = richTextBox1.Text;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

        }

        private void englishToHindiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox9.Visible = true;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox9.Visible = false;
        }


        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button62_Click(object sender, EventArgs e)
        {

        }

        #endregion

        
        #endregion
      

       
    }
}
