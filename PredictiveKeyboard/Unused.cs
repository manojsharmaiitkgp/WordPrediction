using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredictiveKeyboard
{
    class Unused
    { 
/*
        if (File.Exists(@"D:\Hindi_tag\unigram_word.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\unigram_word.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t");
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (unigram_word.ContainsKey(key_value[0]))
                            unigram_word[key_value[0]] += 1;
                        else
                            unigram_word.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }

            if (File.Exists(@"D:\Hindi_tag\bigram_word.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\bigram_word.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t");
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (bigram_word.ContainsKey(key_value[0]))
                            bigram_word[key_value[0]] += 1;
                        else
                            bigram_word.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }

            if (File.Exists(@"D:\Hindi_tag\trigram_word.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\trigram_word.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t");
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (trigram_word.ContainsKey(key_value[0]))
                            trigram_word[key_value[0]] += 1;
                        else
                            trigram_word.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }

            if (File.Exists(@"D:\Hindi_tag\unigram_tagonly.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\unigram_tagonly.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t", RegexOptions.Singleline);
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (unigram_tagonly.ContainsKey(key_value[0]))
                            unigram_tagonly[key_value[0]] += 1;
                        else
                            unigram_tagonly.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }

            if (File.Exists(@"D:\Hindi_tag\bigram_tagonly.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\bigram_tagonly.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t", RegexOptions.Singleline);
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (bigram_tagonly.ContainsKey(key_value[0]))
                            bigram_tagonly[key_value[0]] += 1;
                        else
                            bigram_tagonly.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }

            if (File.Exists(@"D:\Hindi_tag\trigram_tagonly.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\trigram_tagonly.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t", RegexOptions.Singleline);
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (trigram_tagonly.ContainsKey(key_value[0]))
                            trigram_tagonly[key_value[0]] += 1;
                        else
                            trigram_tagonly.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }

            if (File.Exists(@"D:\Hindi_tag\unigram_word_tag.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\unigram_word_tag.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t", RegexOptions.Singleline);
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (unigram_word_tag.ContainsKey(key_value[0]))
                            unigram_word_tag[key_value[0]] += 1;
                        else
                            unigram_word_tag.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }

            if (File.Exists(@"D:\Hindi_tag\bigram_word_tag.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\bigram_word_tag.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t", RegexOptions.Singleline);
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (bigram_word_tag.ContainsKey(key_value[0]))
                            bigram_word_tag[key_value[0]] += 1;
                        else
                            bigram_word_tag.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }

            if (File.Exists(@"D:\Hindi_tag\trigram_word_tag.txt"))
            {
                String[] lines = File.ReadAllLines(@"D:\Hindi_tag\trigram_word_tag.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] key_value = Regex.Split(lines[i], "\t", RegexOptions.Singleline);
                    if (key_value[0] != null && key_value[0] != "" && key_value[1] != null && key_value[1] != "")
                    {
                        if (trigram_word_tag.ContainsKey(key_value[0]))
                            trigram_word_tag[key_value[0]] += 1;
                        else
                            trigram_word_tag.Add(key_value[0], int.Parse(key_value[1]));
                    }
                }
            }
*/
    }
}
