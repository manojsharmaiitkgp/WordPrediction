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
                                      Dictionary<string, string> Dic_Uni_new, 
                                      Dictionary<string,string> Dic_local_data,
                                      Dictionary<string,string> Dic_RegencyOfWord
                                     )
        {
          
             //string filePath1 = @"..\..\..\..\N-Grams\data\new_output_tri.txt";
            string filePath1 = @"File4ACM\data\new_output_tri.txt";
            

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
            //filePath1 = @"..\..\..\..\N-Grams\data\new_output_bi.txt";
            filePath1 = @"File4ACM\data\new_output_bi.txt";
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
               //filePath1 = @"..\..\..\..\N-Grams\data\new_output_uni.txt";
            filePath1 = @"File4ACM\data\new_output_uni.txt";

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

             //load local dictionary
               filePath1 = @"Data\Local_Data.txt";
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
                               String[] key_value = obj1.Trim().Split(' ');
                               if (key_value.Length > 1)
                               {
                                   string word_key = key_value[0].ToString();
                                   string prob_key = Dic_Uni_new["<UNK>"].ToString();
                                     


                                   if (word_key != "")
                                   {
                                       
                                       if (!Dic_Uni_new.ContainsKey(word_key)) // update unigram list for local data base
                                           Dic_Uni_new.Add(word_key, prob_key);
                                   }
                               }

                           }
                           catch (Exception ee)
                           { }
                       }
                   }
               }
            // dictionary of LRU
               filePath1 = @"Data\Regency_Data.txt";
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
                               String[] key_value = obj1.Trim().Split(' ');
                               if (key_value.Length > 1)
                               {
                                   string word_key = key_value[0].ToString();
                                   //string prob_key = Dic_Uni_new["<UNK>"].ToString();
                                   string LRU_key = key_value[1].ToString();


                                   if (word_key != "")
                                   {

                                       if (!Dic_RegencyOfWord.ContainsKey(word_key)) // add LRU
                                           Dic_RegencyOfWord.Add(word_key, LRU_key);
                                   }
                               }

                           }
                           catch (Exception ee)
                           { }
                       }
                   }
               }
               int che = 0;


        }
           
    }
}
