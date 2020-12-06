#define Ratio
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
using System.Diagnostics;

namespace PredictiveKeyboard
{
    public partial class Form2 : Form
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
       
        public Form2()
        {
            InitializeComponent();
        }
       // System.Drawing.Size size;
        private void button5_MouseEnter(object sender, EventArgs e)
        {
          //  Point loc = new System.Drawing.Point(142, 200);
            Point loc = this.button5.Location;
            this.button5.Location = new System.Drawing.Point(loc.X - 5, loc.Y - 5);
            System.Drawing.Size size = this.button1.Size;
            this.button5.Size = new System.Drawing.Size(size.Width + 10,size.Height+ 10 );
            this.button5.FlatAppearance.BorderSize = 5;

        
           
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            Point loc = this.button5.Location;
            this.button5.Location = new System.Drawing.Point(loc.X + 5, loc.Y + 5);
            System.Drawing.Size size = this.button1.Size;
            this.button5.Size = new System.Drawing.Size(size.Width , size.Height);
            this.button5.FlatAppearance.BorderSize = 1;

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        static string output = "";
    
        private void button7_Click(object sender, EventArgs e)
        {
            HindiToEnglish obj = new HindiToEnglish();
            string input = "भारतीय जीवन बीमा निगम लिमिटेड ";
            
            obj.Hindi_English(ref output,ref input);
            MessageBox.Show(output.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int val=LCS("quick", "quixc");
            int ed=myCostChecker("quick", "quixc"); 
            MessageBox.Show("MAx=\t"+val.ToString()+"\tED=\t"+ed.ToString()); 
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
                        Console.Write(retCostDist);
                    }
                    Console.WriteLine(" ");
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

        public void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            string pridict = @"D:\imp_research_tool\rank_hindi\predictions";

            string commd = @"D:\imp_research_tool\rank_hindi\svm_rank_classify D:\imp_research_tool\rank_hindi\test1.dat D:\imp_research_tool\rank_hindi\modelNew.dat D:\imp_research_tool\rank_hindi\predictions";
            ExecuteCommandSync(commd);
            string data = System.IO.File.ReadAllText(pridict);

            Console.WriteLine(data);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> Hindi_Dic = new Dictionary<string, string>();
            string Hindi = @"input_pTable.txt";
            string Sound = @"soundex.txt";
            string dataHindi = System.IO.File.ReadAllText(Hindi);
            string dataSound = System.IO.File.ReadAllText(Sound);
            string[] wordarrayHindi = Regex.Split(dataHindi, "\t");
            string[] wordarraySound = Regex.Split(dataSound, "\t");
            int i = 0;
            foreach (string obj in wordarraySound)
            {

                try
                {
                    string obj_key = obj.Trim();
                    string obj_value = wordarrayHindi[i].ToString().Trim();
                    obj_value += "\t";
                    if (!Hindi_Dic.ContainsKey(obj_key))
                    {
                        Hindi_Dic.Add(obj_key, obj_value);
                    }
                    else
                    {
                        Hindi_Dic[obj_key] += obj_value;
                    }
                    i++;
                }
                catch (Exception ee)
                { }
 
            }
            string p_output = @"p_output.txt";
            string output = "";
            foreach (var pair in Hindi_Dic)
            {
                //i++;
                output += pair.Key.ToString() + ":\t" + pair.Value.ToString()+"\r\n";

            }
            System.IO.File.WriteAllText(p_output, output, Encoding.UTF8);
            int j = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {
           string input_path=@"File4ACM\input";
           string output_path = @"File4ACM\out";
            List<string> file_paths = new List<string>();
            file_paths = subdirectory(input_path);
            int i = 0;

            string data1 = "";
            string[] content1;

            string format = "";
            int count = 0;
         
            foreach (string file in file_paths)
            {
                data1 = NewMethod(data1, file); 

                content1 = data1.Split('।');
                string file_name_only = "";
                file_name_only = file.Substring(file.IndexOf("input")+6);
                string output_path_toWrite = output_path + @"\" + "out_"+file_name_only;
                string output_text = "";
                int cnt = 0;

                foreach (string st in content1)
                {
                    string data = st.Trim();

                    string[] chk= data.Split(' ');
                    if (chk.Length >= 4)//minimim sentence length
                    {

                        if (data.Length > 0)
                        {
                            count++;
                            cnt++;
                            //output_text += data + "\r\n";
                            //output_text += "sID" + cnt.ToString() + "\t" + data + "\r\n"; 
                            output_text += "sID" + count.ToString() + "\t" + data + "\r\n";
                            format += "sID" + count.ToString() + "\t" + data + "\r\n";

                        }
                    }
                }
                cnt = 0;

              // format= "sID" +count.ToString()+"\t";
                File.WriteAllText(output_path_toWrite, output_text,Encoding.UTF8);

                output_text = "";



            
                File.WriteAllText(output_path + @"\" + "AllText.txt", format, Encoding.UTF8);


            }

            MessageBox.Show("done");
        }

        private static string NewMethod(string data1, string file)
        {
            data1 = File.ReadAllText(file);
            data1 = data1.Replace('-', ' ');
            data1 = data1.Replace("?", "\r\n");
            data1 = data1.Replace('‘', ' ');
            data1 = data1.Replace('’', ' ');
            data1 = data1.Replace('/', ' ');
            data1 = data1.Replace('!', ' ');
            data1 = data1.Replace(',', ' ');
            data1 = data1.Replace('—', ' ');
            data1 = data1.Replace("'", " ");
            data1 = data1.Replace(";", " ");
            data1 = data1.Replace("(", " ");
            data1 = data1.Replace(")", " ");
            data1 = data1.Replace(".", "\r\n"); 

            data1 = data1.Replace("0", "०");
            data1 = data1.Replace("1", "१");
            data1 = data1.Replace("2", "२");
            data1 = data1.Replace("3", "३");
            data1 = data1.Replace("4", "४");
            data1 = data1.Replace("5", "५");
            data1 = data1.Replace("6", "६");
            data1 = data1.Replace("7", "७");
            data1 = data1.Replace("8", "८");
            data1 = data1.Replace("9", "९");

            data1 = data1.Replace("  ", " ");

            data1 = data1.Replace("\r\n", "।");
            return data1;
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

        private void button12_Click(object sender, EventArgs e)
        {

          //  Sim();
            corpusAna();
            
            MessageBox.Show("done");
        }

        private static void Sim()
        {
            string input_path = System.Environment.CurrentDirectory.ToString() + @"\File4ACM\log_process\";
            // string input_path = System.Environment.CurrentDirectory.ToString() + @"\File4ACM\CorpusAnalysis\";
            List<string> file_paths = new List<string>();
            file_paths = subdirectory(input_path);

            //  int value = 1;

            for (int i = 0; i <= 20; i++)
            {
                string output = Query(file_paths, i);
                /*var orderCounts = from c in Container
                                 //where c._pks > 20.0
                                 group c by c._WS into g
                                 select new { Category = g.Key, orderCount = g.Average(c=>c._pks), Words = g };


               string output = "";
               foreach (var g in orderCounts)
               {                                  
                   Console.WriteLine("are '{0}':", g.Category);
                   Console.WriteLine(g.orderCount);
                                                                                               
                   string value = g.Category + "\t" + g.orderCount;
                   output += value + "\n";

               }*/

                File.AppendAllText(System.Environment.CurrentDirectory.ToString() + @"\File4ACM\mylog.txt", output);
            }
        }

        public void corpusAna()
        {
            string input_path = System.Environment.CurrentDirectory.ToString() + @"\File4ACM\CorpusAnalysis\";
            List<string> file_paths = new List<string>();
            file_paths = subdirectory(input_path);
            string output = CorpusAnaQuery(file_paths, 7);
            File.AppendAllText(System.Environment.CurrentDirectory.ToString() + @"\File4ACM\mylog_CorpusAnalysis.txt", output);
           // MessageBox.Show("done");


 
        }
        private static string CorpusAnaQuery(List<string> file_paths, int winS)
        {
            int windowSize = winS;

            int numLine = 0;
            int count = 0;
            List<Class1> Container = new List<Class1>();
            foreach (string file in file_paths)
            {
                numLine++;
                string strText = File.ReadAllText(file, Encoding.UTF8);
                string[] data = Regex.Split(strText, "\n");

                foreach (string st in data)
                {
                    count++;

                    if (st.Trim().Length != 0)
                    {
                        Class1 Line = new Class1();
                        string firtChar = st.Substring(0, 1);
                        if ((firtChar == "1") || (firtChar == "2") || (firtChar == "3") || (firtChar == "4") || (firtChar == "5") || (firtChar == "6") || (firtChar == "7") || (firtChar == "8") || (firtChar == "9"))
                        {
                            string[] chunk = Regex.Split(st.Trim(), "\t");
                            Line.SetWS(Convert.ToInt32(chunk[0]));
                            Line.SetSentID(Convert.ToInt32(chunk[1]));
                            Line.SetCorpus(Convert.ToInt32(chunk[1]));
                            Line.SetErrorAt(chunk[4]);
                            Line.SetPKS(Convert.ToDouble(chunk[8]));
                            Line.SetTE(Convert.ToInt32(chunk[2]));
                            Line.SetUE(Convert.ToInt32(chunk[3]));
                            Line.SetTW(Convert.ToInt32(chunk[7]));
                            Line.SetHR(Convert.ToDouble(chunk[11]));
                            Line.SetKuP(Convert.ToDouble(chunk[12]));


                            Container.Add(Line);
                        }
                    }
                }
            }


            string output = "";
            //for (int kk = 0; kk <= 5; kk++)
            //{
              //string variable = kk.ToString();
            string variable = "p5";
            //    if (kk == 6)
            //    {
            //        variable = "p2";
 
            //    }
            //    if (kk == 7)
            //    {
            //        variable = "p3";

            //    }
            //    if (kk == 8)
            //    {
            //        variable = "p4";

            //    }
            //    if (kk == 9)
            //    {
            //        variable = "p5";

            //    }

                var orderCounts = from c in Container
                                  where c.GetWS() == windowSize && c.GetErrorAt() == variable
                                  //where c.GetWS() == windowSize && c.GetErrorAt() == c.GetCorpus()
                                  //group c by c.Loc_ErrorAt into g
                                  group c by c._Corpus into g


                                  select new { Category = g.Key, avgKup = g.Average(c => c._KuP), totalWord = g.Sum(c => c._TW), avgHR = g.Average(c => c._HR), totalErrorAvoided = g.Sum(c => c._TE), unCorrectedError = g.Sum(c => c._UE), AvgPKS = g.Average(c => c._pks), Words = g };


                
                foreach (var g in orderCounts)
                {
                    Console.WriteLine("are '{0}':", g.Category);

                    Console.WriteLine(g.AvgPKS.ToString("00.00"));

                    string value = "\nErrorType" + variable + "\tWS:\t" + windowSize.ToString() + "\t";

                    value += g.Category + "\t" + g.AvgPKS.ToString("00.00") + "\t" + g.avgHR.ToString("00.00") + "\t" + g.avgKup.ToString("00.00");
                    output += value + "\t";

                //}
                output += "\n";
            }
            
            return output;
        }
        private static string Query(List<string> file_paths,int winS)
        {
            int windowSize = winS;

            int numLine = 0;
            int count = 0;
            List<Class1> Container = new List<Class1>();
            foreach (string file in file_paths)
            {
                numLine++;
                string strText = File.ReadAllText(file, Encoding.UTF8);
                string[] data = Regex.Split(strText, "\n");

                foreach (string st in data)
                {
                    count++;

                    if (st.Trim().Length != 0)
                    {
                        Class1 Line = new Class1();
                        string firtChar = st.Substring(0, 1);
                        if ((firtChar == "1") || (firtChar == "2") || (firtChar == "3") || (firtChar == "4") || (firtChar == "5") || (firtChar == "6") || (firtChar == "7") || (firtChar == "8") || (firtChar == "9"))
                        {
                            string[] chunk = Regex.Split(st.Trim(), "\t");
                            Line.SetWS(Convert.ToInt32(chunk[0]));
                            Line.SetSentID(Convert.ToInt32(chunk[1]));
                            Line.SetErrorAt(chunk[4]);
                            Line.SetPKS(Convert.ToDouble(chunk[8]));
                            Line.SetTE(Convert.ToInt32(chunk[2]));
                            Line.SetUE(Convert.ToInt32(chunk[3]));
                            Line.SetTW(Convert.ToInt32(chunk[7]));
                            Line.SetHR(Convert.ToDouble(chunk[11]));
                            Line.SetKuP(Convert.ToDouble(chunk[12]));


                            Container.Add(Line);
                        }
                    }
                }
            }


            var orderCounts = from c in Container
                              where c.GetWS() == windowSize
                              group c by c.Loc_ErrorAt into g

                                                                        //select new { Category = g.Key, AvgPKS = g.Average(c => c._pks), Words = g };
                              select new { Category = g.Key, avgKup=g.Average(c=> c._KuP) ,totalWord=g.Sum(c=>c._TW), avgHR=g.Average(c=>c._HR), totalErrorAvoided = g.Sum( c=>c._TE),unCorrectedError= g.Sum(c=>c._UE), AvgPKS = g.Average(c => c._pks), Words = g };
                                                                    //var orderCounts = from c in Container
                                                                    //                  select new { errorType= c.Loc_ErrorAt(), pks= c.GetPKS, winSize=c.GetWS from oo in Container group oo by oo.GetWS into  }
                                                                    //                  group c by c.Loc_ErrorAt into g
                                                                    //                  select new { Category = g.Key, orderCount = g.Average(c => c._pks), Words = g };


            string output = "";
            foreach (var g in orderCounts)
            {
                Console.WriteLine("are '{0}':", g.Category);
                Console.WriteLine(g.AvgPKS.ToString("00.00"));
                                                                //foreach (var c in g.Words)
                                                                //{
                                                                //    string value = c.GetErrorAt().ToString()+"\t"+ c.GetSentID().ToString()+"\t"+ c.GetPKS().ToString()+"\tcount"+g.orderCount.ToString() ;

                                                                //    output += value + "\n";
                                                                //}
                string value = "WS:\t" + windowSize.ToString() + "\t";
                                                                //value += g.Category + "\t" + g.AvgPKS.ToString("00.00")+"\t"+g.totalWord+"\t"+g.totalErrorAvoided+"\t"+g.unCorrectedError.ToString();
                value += g.Category + "\t" + g.AvgPKS.ToString("00.00") + "\t" + g.avgHR.ToString("00.00") + "\t" + g.avgKup.ToString("00.00");
                output += value + "\t";

            }
            output += "\n";
            return output;
        }
        Dictionary<string, string> Dic_Uni_new = new Dictionary<string, string>();
        Dictionary<string, string> Dic_RegencyOfWord = new Dictionary<string, string>();
        private void button13_Click(object sender, EventArgs e)
        {
           string filePath1 = @"File4ACM\data\new_output_uni.txt";

            if (File.Exists(filePath1))
            {
                string total_text = File.ReadAllText(filePath1, Encoding.UTF8);
                string[] uniqWord = Regex.Split(total_text, "\n");
                foreach (string obj in uniqWord)
                {
                    if (obj != "")
                    {
                        try
                        {
                            string obj1 = obj.Replace("\t", " ");
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
            Random random = new Random();
            foreach (var pair in Dic_Uni_new)
            {
                string word_key = pair.Key.ToString();
                
                int startingValue = 457194730; //624474492-457194730;
                int stopingValue = 675217432;//634474788-675217432
               

                
                int random_number  =random.Next(startingValue, stopingValue);

                string LRU_key = random_number.ToString();
                if (word_key != "")
                {

                    if (!Dic_RegencyOfWord.ContainsKey(word_key)) // add LRU
                        Dic_RegencyOfWord.Add(word_key, "634474788"+LRU_key);
                }
            }
            string outputData = "";
            string outputFilePath = "LRU_Simulated.txt";
            int counter = 0;
            foreach (var pair in Dic_RegencyOfWord)
            {
                counter++;
                outputData += pair.Key.ToString() + "\t" + pair.Value.ToString() + "\n";
                if (counter % 1000==0)
                {
                    File.AppendAllText(outputFilePath, outputData, Encoding.Unicode);
                    outputData = "";
 
                }
 
            }
            File.AppendAllText(outputFilePath, outputData, Encoding.Unicode);

            int i = 0;
            MessageBox.Show("done");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //string filePath1 = @"traning_20_test.txt";
            string filePath1 = @"training.txt";
            string rawData = File.ReadAllText(filePath1, Encoding.UTF8);
            string[] collection = Regex.Split(rawData.Trim(),"\n");
            ArrayList arr = new ArrayList();
            
            int Old_Tracker = 0;
            int New_Tracker = 20;
            int diff = 0;
            int cntQyerySet = 0;
            int counterForEachQuery = 0;
            for (int i = 0; i < collection.Length-1; i++)
            {
                string firstChar = collection[i].ToString().Substring(0, 1);
                if (firstChar == "#")
                {                   
                    Old_Tracker = i;
                }
                string firstCharNextLine = collection[i+1].ToString().Substring(0, 1);
                if (firstCharNextLine == "#")
                {                    
                    New_Tracker = i+1;
                }
               diff  =New_Tracker - Old_Tracker;

              
                if ( diff != 1)
                {
                    string st=collection[i].ToString();
                    string[] SplitedData = Regex.Split(st.Trim()," ");
                    string StrForQuerySet=SplitedData[0].ToString() + " " +SplitedData[1].ToString();
                    string newQuerySet = "";
                    if (StrForQuerySet == "# query")
                    {
                        cntQyerySet++;
                        newQuerySet = SplitedData[0].ToString() + " " + SplitedData[1].ToString() + " " + cntQyerySet.ToString() + " " + SplitedData[3].ToString();
                        arr.Add(newQuerySet);
                        counterForEachQuery = 0;

                    }
                    else
                    {
                        counterForEachQuery++;
                        //string chengeValue = SplitedData[1].Substring(0, SplitedData[1].Length - 1) + counterForEachQuery;
                        string changeValue = counterForEachQuery + " " + SplitedData[1].Substring(0, SplitedData[1].IndexOf(":")+1) + cntQyerySet;

                        //for (int jj = 2;jj<= 4; jj++)
                        //{
                        //    changeValue += " " + SplitedData[jj].ToString();
 
                        //}
                        string prb = SplitedData[5];
                        string lru = SplitedData[6];
                        prb = prb.Replace("4:", "3:");
                      
                        lru = lru.Replace("5:", "4:");
                        changeValue += " " + SplitedData[2] + " " + SplitedData[3] + " " + prb + " " + lru;
                       
                        
                        //string RmPhoneticPart= SplitedData[7]
                        string TypedWord = SplitedData[7];
                        TypedWord = TypedWord.Replace("#", "\t");
                        TypedWord = TypedWord.Trim();
                        changeValue += " "+ TypedWord;

                        arr.Add(changeValue);
                    }
                }
               
            }

            string outputData = "";
            int counter = 0;
            string outputFilePath = "FilteredText.txt";
            File.WriteAllText(outputFilePath, outputData, Encoding.UTF8);
            foreach (string obj in arr)
            {
                counter++;
                outputData += obj.ToString() + "\r\n";
                if (counter % 1000 == 0)
                {
                    File.AppendAllText(outputFilePath, outputData, Encoding.UTF8);
                    outputData = "";

                }

            }
            File.AppendAllText(outputFilePath, outputData, Encoding.UTF8);

            //File.AppendAllText(outputFilePath, outputData, Encoding.Unicode);

            int j = 0;
            MessageBox.Show("done");

        }
        public static string GetHexEncoding(string str)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedChars = utf8.GetBytes(str);
            str = "";
            for (int i = 0; i < encodedChars.Length; i++)
                str += "%" + string.Format("{0:X2}", encodedChars[i]);
            return str;
        }
      private void button15_Click(object sender, EventArgs e)
        {
            //string text = @"%E0%A4%A4%E0%A5%80%E0%A4%A8+%E0%A4%B2%E0%A4%A1%E0%A4%BC%E0%A4%95%E0%A5%87+%E0%A4%AC%E0%A4%9A%E0%A4%AA%E0%A4%A8+%E0%A4%B9%E0%A5%80+%E0%A4%AE%E0%A5%87%E0%A4%82+%E0%A4%AE%E0%A4%B0+%E0%A4%97%E0%A4%8F";
            string text = @"फिर वह भी हार न मानती थी और इस विषय पर स्त्री पुरुष में आये दिन संग्राम छिड़ा रहता था";
            string textToPlay = GetHexEncoding(text);
            string urlCreated = @"http://translate.google.com/translate_tts?ie=UTF-8&q=" + textToPlay + @"&tl=hi&prev=input";
            string path = urlCreated;
            webBrowser1.Navigate(path);

            //WebBrowser browser = new WebBrowser();

            //browser.Navigate(new Uri(urlCreated));


            //ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
            //startInfo.CreateNoWindow = false;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;                    

            //startInfo.Arguments = urlCreated;
          
            //try
            //{
               
            //    using (Process exeProcess = Process.Start(startInfo))
            //    {
            //        //exeProcess.WaitForExit();
            //        System.Threading.Thread.Sleep(50000);
            //        exeProcess.Kill();
            //    }
            //}
            //catch
            //{
            //    // Log error.
            //} 
            //string path = @"http://link1.songs.pk/song1.php?songid=8091";
           

           

           

        }

      private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
      {
         // MessageBox.Show( webBrowser1.Document.ToString());

      }

      Dictionary<string, int> Char_uni_Dic = new Dictionary<string, int>();
      Dictionary<string, int> Char_Bi_Dic = new Dictionary<string, int>();
      Dictionary<string, int> Convert_Zone_Dic_uni = new Dictionary<string, int>();
      Dictionary<string, int> Convert_Zone_Dic_bi = new Dictionary<string, int>();
      Dictionary<string, string> Zone_Dic = new Dictionary<string, string>();

      private void button16_Click(object sender, EventArgs e)
      {
          iniZoneFile();
          string input_path = @"D:\anmol_manoj\dataWiki";


          List<string> file_paths = new List<string>();
          file_paths = subdirectory(input_path);

          foreach (string file in file_paths)
          {


              string text1 = File.ReadAllText(file, Encoding.UTF8);
              string text = complexCharFilter(text1);
              ZoneCalculationMethod(text);
              Console.WriteLine(file.ToString());
          }



          //string text1 = "देवनागरी एक लिपि है जिसमें अनेक भारतीय भाषाएँ तथा कुछ विदेशी भाषाएं लिखीं जाती हैं।";

          //string text = complexCharFilter(text1); 

         // ZoneCalculationMethod(text);


          WriteZoneInformation();
          WrteUniGramInformation();
          WrteBiGramInformation();

          int kk = 0;
          MessageBox.Show("done");
          
                                   

      }

      private static string complexCharFilter(string text1)
      {
          string text = Regex.Replace(text1, "क्ष", "A");
          text = Regex.Replace(text, "त्र", "B");
          text = Regex.Replace(text, "ज्ञ", "C");
          text = Regex.Replace(text, "श्र", "D");
          text = Regex.Replace(text, "प्र", "E");
          text = Regex.Replace(text, "ज़", "F");
          text = Regex.Replace(text, "फ़", "G");
          text = Regex.Replace(text, "\r", "");
          text = Regex.Replace(text, "\n", " ");
          return text;
      }

      private void iniZoneFile()
      {

          string filePath1 = @"File4ACM\Analysis\input\InputAllChar.txt";
          string total_text = File.ReadAllText(filePath1, Encoding.UTF8);
          string[] uniqWord = Regex.Split(total_text, "\n");

          foreach (string obj in uniqWord)
          {
              string st = obj.TrimEnd();
              if (st.Length > 1)
              {
                  string key = st.Substring(0, 1);
                  string value = st.Substring(st.Length - 1, 1);
                  if (!Zone_Dic.ContainsKey(key))
                  {
                      Zone_Dic.Add(key, value);
                  }
              }
          }
      }

      private void ZoneCalculationMethod(string text)
      {
          char[] collection = text.ToCharArray();
          int length = collection.Length;

          string str = "";
          for (int i = 0; i < length; i++)
          {
              int value = 1;
              str = collection[i].ToString();
              if (Char_uni_Dic.ContainsKey(str))
              {
                  value = Char_uni_Dic[str] + 1;
                  Char_uni_Dic[str] = value;

              }
              else
              {
                  Char_uni_Dic.Add(str, value);

              }
          }
          for (int j = 0; j < length - 1; j++)
          {
              int value = 1;
              str = collection[j].ToString() + collection[j + 1].ToString();
              if (Char_Bi_Dic.ContainsKey(str))
              {
                  value = Char_Bi_Dic[str] + 1;
                  Char_Bi_Dic[str] = value;

              }
              else
              {
                  Char_Bi_Dic.Add(str, value);

              }
          }


          //string filePath1 = @"File4ACM\Analysis\InputAllChar.txt";
          //string total_text = File.ReadAllText(filePath1, Encoding.UTF8);
          //string[] uniqWord = Regex.Split(total_text, "\n");

          //foreach (string obj in uniqWord)
          //{
          //    string st = obj.TrimEnd();
          //    if (st.Length > 1)
          //    {
          //        string key = st.Substring(0, 1);
          //        string value = st.Substring(st.Length - 1, 1);
          //        if (!Zone_Dic.ContainsKey(key))
          //        {
          //            Zone_Dic.Add(key, value);
          //        }
          //    }
          //}

          zoneDistribuation(text);
      }

      private void WriteZoneInformation()
      {
          List<string> list = new List<string>();
          foreach (var pair in Convert_Zone_Dic_bi)
          {
              list.Add(pair.Key.ToString() + "\t" + pair.Value.ToString());

          }

          string outputData = "";
          string pathToWrite = @"File4ACM\Analysis\output\ZoneInformation.txt";
          File.WriteAllText(pathToWrite, outputData, Encoding.Unicode);
          foreach (var pair in list)
          {

              outputData = pair.ToString() + "\n";


              File.AppendAllText(pathToWrite, outputData, Encoding.Unicode);
          }
      }

      private void WrteUniGramInformation()
      {
          List<string> list = new List<string>();
          foreach (var pair in Char_uni_Dic)
          {
              list.Add(pair.Key.ToString() + "\t" + pair.Value.ToString());

          }

          string outputData = "";
          string pathToWrite = @"File4ACM\Analysis\output\unigramInformation.txt";
          File.WriteAllText(pathToWrite, outputData, Encoding.Unicode);
          foreach (var pair in list)
          {

              outputData = pair.ToString() + "\n";


              File.AppendAllText(pathToWrite, outputData, Encoding.Unicode);
          }
      }
      private void WrteBiGramInformation()
      {
          List<string> list = new List<string>();
          foreach (var pair in Char_Bi_Dic)
          {
              list.Add(pair.Key.ToString() + "\t" + pair.Value.ToString());

          }

          string outputData = "";
          string pathToWrite = @"File4ACM\Analysis\output\BigramInformation.txt";
          File.WriteAllText(pathToWrite, outputData, Encoding.Unicode);
          foreach (var pair in list)
          {

              outputData = pair.ToString() + "\n";


              File.AppendAllText(pathToWrite, outputData, Encoding.Unicode);
          }
      }
      public void zoneDistribuation(string text)
      {
          string str = Regex.Replace(text,"s", "*",RegexOptions.IgnoreCase);
        
          char[] collection = text.ToCharArray();
          for (int i = 0; i < text.Length; i++)
          {
              string key=collection[i].ToString();

              if (Zone_Dic.ContainsKey(key))
              {
                  string value = Zone_Dic[key].ToString();
                  str = Regex.Replace(str, key, value, RegexOptions.IgnoreCase);
              }
          }


          char[] collection_final = str.ToCharArray();

          string str1 = "";
          for (int i = 0; i < collection_final.Length; i++)
          {
              int value = 1;

              str1 = collection_final[i].ToString();
              if (Convert_Zone_Dic_uni.ContainsKey(str1))
              {
                  value = Convert_Zone_Dic_uni[str1] + 1;
                  Convert_Zone_Dic_uni[str1] = value;

              }
              else
              {
                  Convert_Zone_Dic_uni.Add(str1, value);

              }
          }

          for (int j = 0; j < collection_final.Length - 1; j++)
          {
              int value = 1;
              str1 = collection_final[j].ToString() + collection_final[j + 1].ToString();
              if (Convert_Zone_Dic_bi.ContainsKey(str1))
              {
                  value = Convert_Zone_Dic_bi[str1] + 1;
                  Convert_Zone_Dic_bi[str1] = value;

              }
              else
              {
                  Convert_Zone_Dic_bi.Add(str1, value);

              }
          }

         // int kk = 0;
 
      }

      Dictionary<string, int> LoadUniGramDicForCOM = new Dictionary<string, int>();

      private void button17_Click(object sender, EventArgs e)
      {

          string UniGramPath = @"File4ACM\Analysis\output\unigramInformation.txt";
          string text = File.ReadAllText(UniGramPath, Encoding.UTF8);
          string[] charAndCount = Regex.Split(text, "\n");

          foreach (string objText in charAndCount)
          {
              if (objText.Trim().Length > 0)
              {
                  string[] d1 = Regex.Split(objText, "\t");
                  string key = d1[0].ToString();
                  int value = Convert.ToInt32(d1[1]);

                  if (!LoadUniGramDicForCOM.ContainsKey(key))
                  {
                      LoadUniGramDicForCOM[key] = value;
                  }
              }
          }


          double centerOFMassCase8 = CoMforCase8();
          double centerOFMassOtherCase = CoMforAllOtherCase();

          int ii = 0;
      }

      private double CoMforCase8()
      {
          string filePath1 = @"File4ACM\case8.txt";
          string total_text = File.ReadAllText(filePath1, Encoding.UTF8);
          string[] uniqWord = Regex.Split(total_text, "\n");

          int numerator = 0;
          int denomenator = 0;
          double centerOFMassCase8 = 0.0f;
         // double centerOFMassOtherCase = 0.0f;
          int counter = 0;
         
          foreach (string line in uniqWord)
          {
              counter++;
              if (line.Trim().Length > 0)
              {
                  string str = Regex.Replace(line, "\r", "\t");
                  string[] collChar = Regex.Split(str, "\t");

                  for (int i = 0; i < collChar.Length; i++)
                  {
                      string key = collChar[i].ToString();
                      if (LoadUniGramDicForCOM.ContainsKey(key))
                      {
                          //if (counter == 1)
                          //{

                          //}
                          //else 
                          //{
                          //    numerator += LoadUniGramDicForCOM[key] * (i + 1);
                          //}

                         numerator += LoadUniGramDicForCOM[key] * (i + 1);
                         
                          denomenator += LoadUniGramDicForCOM[key];

                      }
                  }
              }


          }
         return centerOFMassCase8 = (double)(numerator) / denomenator;

      }
      private double CoMforAllOtherCase()
      {
          string filePath1 = @"File4ACM\otherCase.txt";
          string total_text = File.ReadAllText(filePath1, Encoding.UTF8);
          string[] uniqWord = Regex.Split(total_text, "\n");

          double numerator = 0.0f;
          int denomenator = 0;
          double centerOFMass = 0.0f;
          int counter = 0;
          foreach (string line in uniqWord)
          {
              counter++;
              if (line.Trim().Length > 0)
              {
                  string str = Regex.Replace(line, "\r", "\t");
                  string[] collChar = Regex.Split(str, "\t");

                  for (int i = 0; i < collChar.Length; i++)
                  {
                      string key = collChar[i].ToString();
                      if (LoadUniGramDicForCOM.ContainsKey(key))
                      {
                          if (counter == 1)
                          {
                              numerator += LoadUniGramDicForCOM[key] * (i + 1) * ((double)16 / 20);
                          }
                          else
                          {
                              numerator += LoadUniGramDicForCOM[key] * (i + 1);
 
                          }
                          denomenator += LoadUniGramDicForCOM[key];

                      }
                  }
              }


          }
          return centerOFMass = (double)(numerator) / denomenator;
      }

      private void button18_Click(object sender, EventArgs e)
      {
          string input_path = @"D:\anmol_manoj\dataWiki";
          string ouputFile = @"D:\anmol_manoj\CompleteFile.txt";

          List<string> file_paths = new List<string>();
          file_paths = subdirectory(input_path);

          foreach (string file in file_paths)
          {


              string text1 = File.ReadAllText(file, Encoding.UTF8);
              File.AppendAllText(ouputFile, text1, Encoding.UTF8);
            
              Console.WriteLine(file.ToString());
          }
          MessageBox.Show("done");
      }

      private void button19_Click(object sender, EventArgs e)
      {
          for (int i = 0; i < 300000; i++)
          {
              Random rnd = new Random();

              int randomNumber = rnd.Next(0, 2);
              if (randomNumber == 2)
              {
                  MessageBox.Show(randomNumber.ToString());
              }
          }
          MessageBox.Show("done");
      }



    }
}
