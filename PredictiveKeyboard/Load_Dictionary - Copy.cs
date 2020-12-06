using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace PredictiveKeyboard
{
    class Load_Dictionary
    {
      
        public void load_dictionaries(
                                    Dictionary<string, string> Dic_Tri_new,
                                      Dictionary<string, string> Dic_Bi_new,
                                      Dictionary<string, string> Dic_Uni_new 
                                     )
        {
          
             string filePath1 = @"..\..\..\..\N-Grams\data\new_output_tri.txt";

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
                            String[] key_value = obj.Split(' ');
                            if (key_value.Length > 4)
                            {
                                string word_key = key_value[1].ToString() + " " + key_value[2].ToString() + " " + key_value[3].ToString();
                                string prob_key = key_value[0].ToString();

                                if (word_key != "")
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
            /////// bigram arpa
            filePath1 = @"..\..\..\..\N-Grams\data\new_output_bi.txt";

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
                            String[] key_value = obj.Split(' ');
                            if (key_value.Length > 3)
                            {
                                string word_key = key_value[1].ToString() + " " + key_value[2].ToString();
                                string prob_key = key_value[0].Trim().ToString() + "\t" + key_value[3].Trim().ToString();

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
            ////// uni gram arpa
               filePath1 = @"..\..\..\..\N-Grams\data\new_output_uni.txt";

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
        }


        private void load_EBX()
        {
            //if (File.Exists(@"..\..\..\..\N-Grams\Probability\EBB.txt"))
            //{
            //    string filePath = @"..\..\..\..\N-Grams\Probability\EBB.txt";
            //    string total_text = File.ReadAllText(filePath, Encoding.UTF8);
            //    string[] regencyWord = Regex.Split(total_text, "\n");

            //    foreach (string key in regencyWord)
            //    {
            //        string[] array_of_word = Regex.Split(key, "#");
            //        if (array_of_word.Length >= 2)
            //        {
            //            string data_string = array_of_word[1].ToString();
            //            if (!EBX.ContainsKey(array_of_word[0].ToString()))
            //            {
            //                EBX.Add(array_of_word[0].ToString(), array_of_word[1].ToString());
            //            }
            //        }
            //    }

            //}

        }


        //public void TRI_GRAM_EXT()
        //{

        //    string filePath = @"..\..\..\..\N-Grams\data\new_output_tri.txt";

        //    if (File.Exists(filePath))
        //    {

        //        string total_text = File.ReadAllText(filePath, Encoding.UTF8);
        //        string[] uniqWord = Regex.Split(total_text, "\n");
        //        foreach (string obj in uniqWord)
        //        {
        //            if (obj != "")
        //            {
        //                try
        //                {
        //                    String[] key_value = obj.Split(' ');
        //                    if (key_value.Length > 4)
        //                    {
        //                        string word_key = key_value[1].ToString() + " " + key_value[2].ToString() + " " + key_value[3].ToString();
        //                        string prob_key = key_value[0].ToString();

        //                        if (word_key != "")
        //                        {
        //                            if (!Dic_Tri_new.ContainsKey(word_key))
        //                                Dic_Tri_new.Add(word_key, prob_key);
        //                        }
        //                    }
                            
        //                }
        //                catch (Exception ee)
        //                { }
        //            }
        //        }

        //    }
        //}

        //public void BI_GRAM_EXT()
        //{

        //    string filePath = @"..\..\..\..\N-Grams\data\new_output_bi.txt";

        //    if (File.Exists(filePath))
        //    {

        //        string total_text = File.ReadAllText(filePath, Encoding.UTF8);
        //        string[] uniqWord = Regex.Split(total_text, "\n");
        //        foreach (string obj in uniqWord)
        //        {
        //            if (obj != "")
        //            {
        //                try
        //                {
        //                    String[] key_value = obj.Split(' ');
        //                    if (key_value.Length > 3)
        //                    {
        //                        string word_key = key_value[1].ToString() + " " + key_value[2].ToString();
        //                        string prob_key = key_value[0].Trim().ToString() + "\t" + key_value[3].Trim().ToString();

        //                        if (word_key != "")
        //                        {
        //                            if (!Dic_Bi_new.ContainsKey(word_key))
        //                                Dic_Bi_new.Add(word_key, prob_key);
        //                        }
        //                    }

        //                }
        //                catch (Exception ee)
        //                { }
        //            }
        //        }

        //    }

        //}
        //public void UNI_GRAM_EXT()
        //{

        //    string filePath = @"..\..\..\..\N-Grams\data\new_output_uni.txt";

        //    if (File.Exists(filePath))
        //    {
        //        string total_text = File.ReadAllText(filePath, Encoding.UTF8);
        //        string[] uniqWord = Regex.Split(total_text, "\n");
        //        foreach (string obj in uniqWord)
        //        {
        //            if (obj != "")
        //            {
        //                try
        //                {
        //                    string obj1 = obj.Replace("\t", " ");
        //                    String[] key_value = obj1.Split(' ');
        //                    if (key_value.Length > 2)
        //                    {
        //                        string word_key = key_value[1].ToString();
        //                        string prob_key = key_value[0].Trim().ToString() + "\t" + key_value[2].Trim().ToString();


        //                        if (word_key != "")
        //                        {
        //                            if (!Dic_Uni_new.ContainsKey(word_key))
        //                                Dic_Uni_new.Add(word_key, prob_key);
        //                        }
        //                    }

        //                }
        //                catch (Exception ee)
        //                { }
        //            }
        //        }

        //    }

        //}
    }
}
