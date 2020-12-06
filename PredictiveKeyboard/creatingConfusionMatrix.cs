using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace PredictiveKeyboard
{
    public partial class creatingConfusionMatrix : Form
    {
        public creatingConfusionMatrix()
        {
            InitializeComponent();
        }
        public static void Main(object sender,EventArgs e )
        {

        }

        Dictionary<string, string> conf_dic = new Dictionary<string, string>();

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

        private void button1_Click(object sender, EventArgs e)
        {
            string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();
         //   string filePath = strCurrentDirectory + @"\Data\Dic\" + firstChar + ".txt";
            string errfilePath  = strCurrentDirectory + @"\confusion\err_input.txt";


            string corrfilePath  = strCurrentDirectory + @"\confusion\cor_input.txt";

            string err_total_text = File.ReadAllText(errfilePath, Encoding.UTF8);
           // string[] err_wordDic_new = Regex.Split(err_total_text, "\n");

            string cor_total_text = File.ReadAllText(corrfilePath, Encoding.UTF8);
            string[] cor_wordDic_new = Regex.Split(cor_total_text, "\n");
            int cost = 0;
            File.WriteAllText(strCurrentDirectory + @"\confusion\conf.txt", " ");

            Dictionary<string, string> conf_dic = new Dictionary<string, string>();
            string input_path = strCurrentDirectory + @"\confusion\Dic\";
            List<string> file_paths = new List<string>();
            file_paths = subdirectory(input_path);
            string temp_dic = "";
            ArrayList arr_word = new ArrayList();

            foreach (string file in file_paths)
            {
                 temp_dic += File.ReadAllText(file, Encoding.UTF8);
                
 
            }
            string[] err_wordDic_new = Regex.Split(temp_dic, "\n");

            foreach (string cor_obj in cor_wordDic_new)
            {
                //try
                //{
                //    var earlyBirdQuery = from word in err_wordDic_new where word.Substring(0, 1) == cor_obj.Substring(0, 1) select word;
                //    foreach (var v in earlyBirdQuery)
                //    {

                //        arr_word.Add(v.ToString());

                //    }
                //}
                //catch(Exception ee)
                //{}

                //string[] selectedArr = (string[])arr_word.ToArray(typeof(string[]));

               foreach (string err_obj in err_wordDic_new)
              //  foreach (string err_obj in cor_wordDic_new)
                {

                    cost = myCostChecker (err_obj.Trim(), cor_obj.Trim());
                    if (cost ==1  && cor_obj.Trim().Length != 0 && err_obj.Trim().Length != 0)
                    {
                      //  File.AppendAllText(strCurrentDirectory + @"\confusion\conf.txt",cor_obj.Trim() + "\t" + err_obj.Trim()+"\n"); 
                        if (conf_dic.ContainsKey(cor_obj.Trim().ToString()))
                        {
                            conf_dic[cor_obj.Trim().ToString()] += ","+err_obj.Trim().ToString();
                        }
                        else
                        {
                            conf_dic.Add(cor_obj.Trim().ToString(), err_obj.Trim().ToString());
                        }
 
                    }
 
                }
            }
            foreach (KeyValuePair<string, string> pair in conf_dic)
            {
               // count++;
               // output += pair + "\n";
               // if (count % 1000 == 0)
               // {
                File.AppendAllText(strCurrentDirectory + @"\confusion\conf.txt", pair.Key + "\t(" + pair.Value + ")\n");
                   // output = "";
               // }
            }
           // File.AppendAllText(@"D:\anmol_manoj\N-Grams\data\uniGram.txt", output, Encoding.Unicode);
            MessageBox.Show("done");

        }
        private void method()
        {
            string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();
            
            string errfilePath = strCurrentDirectory + @"\confusion\err_input.txt";


            string corrfilePath = strCurrentDirectory + @"\confusion\cor_input.txt";

            string err_total_text = File.ReadAllText(errfilePath, Encoding.UTF8);
            // string[] err_wordDic_new = Regex.Split(err_total_text, "\n");

            string cor_total_text = File.ReadAllText(corrfilePath, Encoding.UTF8);
            string[] cor_wordDic_new = Regex.Split(cor_total_text, "\n");
            int cost = 0;
            File.WriteAllText(strCurrentDirectory + @"\confusion\conf.txt", " ");

          //  Dictionary<string, string> conf_dic = new Dictionary<string, string>();
            
            
            
            string input_path = strCurrentDirectory + @"\confusion\Dic\";
            List<string> file_paths = new List<string>();
            file_paths = subdirectory(input_path);
            string temp_dic = "";
            ArrayList arr_word = new ArrayList();

            foreach (string file in file_paths)
            {
                temp_dic = File.ReadAllText(file, Encoding.UTF8);


                // }
                 string[] err_wordDic_new = Regex.Split(temp_dic, "\n");

                 foreach (string cor_obj in err_wordDic_new)
             //   foreach (string cor_obj in cor_wordDic_new)
                {
                    //try
                    //{
                    //    var earlyBirdQuery = from word in err_wordDic_new where word.Substring(0, 1) == cor_obj.Substring(0, 1) select word;
                    //    foreach (var v in earlyBirdQuery)
                    //    {

                    //        arr_word.Add(v.ToString());

                    //    }
                    //}
                    //catch(Exception ee)
                    //{}

                    //string[] selectedArr = (string[])arr_word.ToArray(typeof(string[]));

                    foreach (string err_obj in err_wordDic_new)
                    //  foreach (string err_obj in cor_wordDic_new)
                    {

                        string source=err_obj.Trim();
                        string target=cor_obj.Trim();

                        if (source.Contains("\t"))
                        {
                            source = source.Substring(0, source.IndexOf("\t"));
                        }
                        if (target.Contains("\t"))
                        {
                            target = target.Substring(0, target.IndexOf("\t"));
                        }

 
                        cost = myCostChecker(source, target);
                        if (cost == 1 && target.Length != 0 && source.Length != 0)
                        {
                            //  File.AppendAllText(strCurrentDirectory + @"\confusion\conf.txt",cor_obj.Trim() + "\t" + err_obj.Trim()+"\n"); 
                            if (conf_dic.ContainsKey(target))
                            {
                                conf_dic[target] += "," + source;
                            }
                            else
                            {
                                conf_dic.Add(target, source);
                            }

                        }

                    }
                }
               
            }
            foreach (KeyValuePair<string, string> pair in conf_dic)
            {
                File.AppendAllText(strCurrentDirectory + @"\confusion\conf.txt", pair.Key + "\t(" + pair.Value + ")\n");
            }

            MessageBox.Show("done composing");

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
                        x = d[i - 1, j] + 1; // deletion

                        y = d[i, j - 1] + 1;// insertion

                        z = d[i - 1, j - 1] + cost; // substitution



                        d[i, j] = min(x, y, z);
                        // 
                        //for transpose errrr but it is not sutible for deletin error and should be checked again
                        //if (i > 1 && j > 1 && arr1[i - 1] == arr2[j - 2] && arr1[i - 2] == arr2[j - 1])
                        //{
                        //    d[i, j] = min(d[i, j], d[i - 2, j - 2] + cost, 0);

                        //}
                        // end of transpose error
                        retCostDist = d[i, j];
                        
                      //  Console.Write(retCostDist+"\t"); 
                    }
                  //  Console.WriteLine("\n");


                }
            }
            catch (Exception ee)
            {

            }

            return retCostDist;


        }
        public static int myCostChecker_new(string str1, string str2)
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
               // Console.Write("\t");
                //for (int i = 0; i < m; i++)
                //{

                //    Console.Write(arr1[i] + "\t");
                //}
                //Console.WriteLine("\n");

               // Console.Write("\t");
               


                for (int i = 1; i <= m; i++)
                {
                  //  Console.Write(arr2[i] + "\t");
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
                        x = d[i - 1, j] + 1; // deletion

                        y = d[i, j - 1] + 1;// insertion

                        z = d[i - 1, j - 1] + cost; // substitution



                        d[i, j] = min(x, y, z);
                        // 
                        //for transpose errrr but it is not sutible for deletin error and should be checked again
                        //if (i > 1 && j > 1 && arr1[i - 1] == arr2[j - 2] && arr1[i - 2] == arr2[j - 1])
                        //{
                        //    d[i, j] = min(d[i, j], d[i - 2, j - 2] + cost, 0);

                        //}
                        // end of transpose error
                        retCostDist = d[i, j];

                        Console.Write(retCostDist + "\t");
                    }
                    Console.WriteLine("\n");


                }
            }
            catch (Exception ee)
            {

            }
        
            int ii=m;
            int jj=n;
            int val = 0;
            string status = "";
            int o_val = 0;
            int n_val = 0;

            while (ii > 0)
            {
                if (arr1[ii - 1] == arr2[jj - 1])
                {
                    cost = 0;

                }
                else
                {
                    cost = 1;
                }
                x = d[ii, jj - 1];
                y = d[ii - 1, jj];
                z = d[ii - 1, jj - 1];
                val = min(x, y, z);
                n_val =val;
                o_val = d[ii, jj];

                Console.Write(d[ii,jj] + "\ti=" + ii + "\tj" + jj + "\t" + arr1[ii - 1] + "\t" + arr2[jj - 1]+"\t");
               // Console.WriteLine(val + "\ti=" + ii + "\tj" + jj + "\t" + arr1[ii - 1] + "\t" + arr2[jj - 1]);

                //if (o_val != n_val)
                //{
                //    o_val = n_val;
                //    Console.Write("\t:do");
                //}
                
                
                //if (val == x)
                //{
                //    jj = jj - 1;
                //    status = "ins";
                //}
                if (val == z)
                {
                    jj = jj - 1;
                    ii = ii - 1;
                    status = "sub";
                }
                else
                {
                    if (val == y)
                    {
                        ii = ii - 1;
                        status = "del";
                    }
                    else
                    //if (val == z)
                    //{
                    //    jj = jj - 1;
                    //    ii = ii - 1;
                    //    status = "sub";
                    //}
                    if (val == x)
                    {
                        jj = jj - 1;
                        status = "ins";
                    }
                }
                int v1 = 0;
                int v2 = 0;


             //   if (o_val != n_val)// if this condition in on then it will give only original value, but when  off it will give all value
                {
                    o_val = n_val;
                    Console.Write("\t:do");
                    if (status == "sub")
                    {
                        try
                        {

                            for (int p = 0; p < 93; p++)
                            {

                                if (uniArr[0, p] == arr1[ii].ToString())
                                {
                                    v1 = p;
                                }
                                if (uniArr[0, p] == arr2[ii].ToString())
                                {
                                    v2 = p;
                                }

                            }
                            if (v1 != 0 && v2 != 0)
                            {
                                uniArr[v1, v2] = (Convert.ToInt32(uniArr[v1, v2].ToString()) + 1).ToString();
                            }
                        }
                        catch (Exception e1)
                        { }
 
                    }
                    if (status == "ins")
                    {
                        for (int p = 0; p < 93; p++)
                        {
                            if (iniArr[0, p] == arr1[ii-1].ToString())
                            {
                                v1 = p;
                            }
                            if (iniArr[0, p] == arr2[ii].ToString())
                            {
                                v2 = p;
                            }
                        }
                        if (v1 != 0 && v2 != 0)
                        {
                            iniArr[v1, v2] = (Convert.ToInt32(iniArr[v1, v2].ToString()) + 1).ToString();
                        }

                    }
                    if (status == "del")
                    {
                        for (int p = 0; p < 93; p++)
                        {
                            if (delArr[0, p] == arr1[ii].ToString())
                            {
                                v1 = p;
                            }
                            if (delArr[0, p] == arr2[ii-1].ToString())
                            {
                                v2 = p;
                            }
                        }
                        if (v1 != 0 && v2 != 0)
                        {
                           // delArr[v1, v2] = (Convert.ToInt32(delArr[v1, v2].ToString()) + 1).ToString();
                            delArr[v1, v2] = (Convert.ToInt32(delArr[v2, v1].ToString()) + 1).ToString();
                        }

                    }
                    
                }
             //   Console.WriteLine(val + "\ti=" + ii + "\tj" + jj+"\t"+arr1[ii-1]+"\t"+arr2[jj-1]);
                Console.WriteLine(status);

            }

            return retCostDist;


        }
       static string[,] uniArr = new string[93, 93];//used for substitution matrix
       static string[,] iniArr = new string[93, 93];//used for insertion matrix
       static string[,] delArr = new string[93, 93];//used for deletion matrix

       int size = 93;//93; //27
        private void button2_Click(object sender, EventArgs e)
        {
            string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();

            string filePath = strCurrentDirectory + @"\confusion\raw_dic.txt";
            File.WriteAllText(strCurrentDirectory + @"\confusion\cor_input.txt", " ");
            string total_text = File.ReadAllText(filePath, Encoding.UTF8);
            total_text = total_text.Replace("/", "\"1."); 
            total_text = total_text.Replace('"', '#'); 
            string[] wordDic_new = Regex.Split(total_text, "#");
            ArrayList arr= new ArrayList ();
            foreach (string obj in wordDic_new)
            {
                if (obj.Contains("1."))
                {
                    string value = obj.Replace("1.","");
                    arr.Add(value);
                    File.AppendAllText(strCurrentDirectory + @"\confusion\cor_input.txt", value+"\n");
                }
            }
            int i = 0;
           // File.AppendAllText(strCurrentDirectory + @"\confusion\cor_input.txt", output, Encoding.Unicode);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            method();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string strCurrentDirectory = System.Environment.CurrentDirectory.ToString();

            string filePath = strCurrentDirectory + @"\confusion\hin\unigram.txt";
            string data = File.ReadAllText(filePath, Encoding.UTF8);
            string[] cor_wordDic_new = Regex.Split(data.Trim(), "\n");
           
            string target = "";
            foreach (string obj in cor_wordDic_new)
            {
                string value = obj;
               target += value.Substring(0, value.IndexOf("\t"))+"#";

            }
            
            string[] unigram = Regex.Split(target.Trim(), "#");
            //string[,] uniArr = new string[93, 93];


         

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    uniArr[i, j] = ("0").ToString();//substitution
                    iniArr[i, j] = ("0").ToString();//insertion
                    delArr[i, j] = ("0").ToString();//insertion
                }

            }

            for (int j = 0; j < size; j++)
            {
                uniArr[0, j] = unigram[j].ToString();
                iniArr[0, j] = unigram[j].ToString();
                delArr[0, j] = unigram[j].ToString();

            }


            for (int j = 0; j < size; j++)
            {
                uniArr[j, 0] = unigram[j].ToString();
                iniArr[j, 0] = unigram[j].ToString();
                delArr[j, 0] = unigram[j].ToString();
            }

            method();
          //  int cost = myCostChecker_new("कंगन", "कंचन");


            foreach (KeyValuePair<string, string> pair in conf_dic)
            {
              //  File.AppendAllText(strCurrentDirectory + @"\confusion\conf.txt", pair.Key + "\t(" + pair.Value + ")\n");
              //  int cost = myCostChecker_new("कंगन", "कंचन");
                string[] targ = Regex.Split(pair.Value, ",");
                foreach (string obj in targ)
                {
                    Console.WriteLine("\n---------------\n");
                    Console.WriteLine("source\t:" + pair.Key);
                    Console.WriteLine("source\t:" + obj);

                    int cost = myCostChecker_new(pair.Key, obj);
 
                }
            }


            foreach (KeyValuePair<string, string> pair in conf_dic)
            {
                File.AppendAllText(strCurrentDirectory + @"\confusion\conf.txt", pair.Key + "\t(" + pair.Value + ")\n");
            }



            File.WriteAllText(strCurrentDirectory + @"\confusion\confSubMatrix.txt", " ");
            File.WriteAllText(strCurrentDirectory + @"\confusion\confIniMatrix.txt", " ");
            File.WriteAllText(strCurrentDirectory + @"\confusion\confDelMatrix.txt", " ");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    File.AppendAllText(strCurrentDirectory + @"\confusion\confSubMatrix.txt", uniArr[i, j] + "\t");
                    File.AppendAllText(strCurrentDirectory + @"\confusion\confIniMatrix.txt", iniArr[i, j] + "\t");
                    File.AppendAllText(strCurrentDirectory + @"\confusion\confDelMatrix.txt", delArr[i, j] + "\t");
                }
                File.AppendAllText(strCurrentDirectory + @"\confusion\confSubMatrix.txt", "\n");
                File.AppendAllText(strCurrentDirectory + @"\confusion\confIniMatrix.txt", "\n");
                File.AppendAllText(strCurrentDirectory + @"\confusion\confDelMatrix.txt", "\n");

            }
           

            int k = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int  cost = myCostChecker_new("manojz", "manoj");
            //int  cost = myCostChecker_new("कंगन", "कंचन");
             //कंगन	(कंगना,कंगान,कंचन)
            int val = 0;
        }
    }
}
