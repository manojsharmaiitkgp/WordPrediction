using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Transliteration
/// </summary>
public class Transliteration
{
    string hin_char, s, output = null, input;
    int flag = 0;
    string[] retext;
    List<string> newretext;
    Regex reg = new Regex("([^a-z])|([bcdfghjklmnpqrstvwxyz]?[aeiou]{0,2})");    //split the string in the strings with the symbol or a consonants followed by any no. of vowels 
    Dictionary<string, string> d = new Dictionary<string,string>();           
    public Transliteration()
	{
		        //Adding keys to Dictionary
                /* Consonant Block */                            /* Vowels occuring alone */                            /*vowel pairs of cons + 'i' */                         /* Block of repeated consonant*/
                d.Add("\u0062", "\u092c");                       d.Add("V" + "\u0061", "\u0905");                     d.Add("\u0069" + "\u0061","\u0940"+"\u092F"+"\u093E");   d.Add("\u0062" + "\u0062", "\u092C" + "\u094D" + "\u092C");          
		        d.Add("\u0063","\u0915");                        d.Add("V"+"\u0065","\u0907");                        d.Add("\u0069"+"\u0065","\u0940");                       d.Add("\u0063"+"\u0063","\u0915"+"\u094D"+"\u0915");
		        d.Add("\u0064","\u0926");                        d.Add("V"+"\u0069","\u0908");                        d.Add("\u0069"+"\u0069","\u0940");                       d.Add("\u0064"+"\u0064","\u0926"+"\u094D"+"\u0926");
		        d.Add("\u0066","\u092B");                        d.Add("V"+"\u006F","\u0913");                        d.Add("\u0069"+"\u006F","\u093F"+"\u0913");              d.Add("\u0066"+"\u0066","\u092B"+"\u094D"+"\u092B");
		        d.Add("\u0067","\u0917");                        d.Add("V"+"\u0075","\u092F"+"\u0942");               d.Add("\u0069"+"\u0075","\u0940"+"\u0905");              d.Add("\u0067"+"\u0067","\u0917"+"\u094D"+"\u0917");
		        d.Add("\u0068","\u0939");                        /*vowel occuring alone end*/                           /*vowel pair of cons+ 'i' end*/    
                d.Add("\u006A","\u091C");                                                                                                                                      d.Add("\u006A"+"\u006A","\u091C"+"\u094D"+"\u091C");
		        d.Add("\u006B","\u0915");                        /*vowel pairs of cons+ 'a' */                          /*vowel pair of cons+ 'o'*/                            d.Add("\u006B"+"\u006B","\u0915"+"\u094D"+"\u0915");
		        d.Add("\u006c","\u0932");                        d.Add("\u0061"+"\u0061","\u093E");                   d.Add("\u006F"+"\u0061","\u094B");                       d.Add("\u006C"+"\u006C","\u0932"+"\u094D"+"\u0932");            
		        d.Add("\u006D","\u092E");                        d.Add("\u0061"+"\u0065","\u0947");                   d.Add("\u006F"+"\u0065","\u0940");                       d.Add("\u006D"+"\u006D","\u092E"+"\u094D"+"\u092E");
		        d.Add("\u006E","\u0928");                        d.Add("\u0061"+"\u0069","\u0948");                   d.Add("\u006F"+"\u0069","\u094B"+"\u0911");              d.Add("\u006E"+"\u006E","\u0928"+"\u094D"+"\u0928");
		        d.Add("\u0070","\u092A");                        d.Add("\u0061"+"\u006F","\u094C");                   d.Add("\u006F"+"\u006F","\u0942");                       d.Add("\u0070"+"\u0070","\u092A"+"\u094D"+"\u092A");
		        d.Add("\u0071","\u0915");                        d.Add("\u0061"+"\u0075","\u094B");                   d.Add("\u006F"+"\u0075","\u093E"+"\u0909");              d.Add("\u0071"+"\u0071","\u0915"+"\u094D"+"\u0915");
		        d.Add("\u0072","\u0930");                        /*vowel pair of cons+ 'a' end*/                        /*vowel pair of cons+ 'o' end*/                     
		        d.Add("\u0073","\u0938");                                                                                                                                      d.Add("\u0073"+"\u0073","\u0938"+"\u094D"+"\u0938");                                
		        d.Add("\u0074","\u0924");                        /*vowel pair of cons+ 'e' */                           /*vowel pair of cons+ 'u' */                           d.Add("\u0074"+"\u0074","\u0924"+"\u094D"+"\u0924");
		        d.Add("\u0076","\u0935");                        d.Add("\u0065"+"\u0061","\u0947");                   d.Add("\u0075"+"\u0061","\u0949");                       d.Add("\u0076"+"\u0076","\u0935"+"\u094D"+"\u0935");
		        d.Add("\u0077","\u0935");                        d.Add("\u0065"+"\u0065","\u093F");                   d.Add("\u0075"+"\u0065","\u0941"+"\u090F");              d.Add("\u0077"+"\u0077","\u0935"+"\u094D"+"\u0935");
		        d.Add("\u0078","\u091C");                        d.Add("\u0065"+"\u0069","\u0948");                   d.Add("\u0075"+"\u0069","\u0941"+"\u0907");              d.Add("\u0078"+"\u0078","\u091C"+"\u094D"+"\u091C");
		        d.Add("\u0079","\u092F");                        d.Add("\u0065"+"\u006F","\u0940"+"\u0913");          d.Add("\u0075"+"\u006F","\u0941"+"\u0913");              d.Add("\u0079"+"\u0079","\u092F"+"\u094D"+"\u092F");
		        d.Add("\u007A","\u091C");                        d.Add("\u0065"+"\u0075","\u092F"+"\u0942");          d.Add("\u0075"+"\u0075","\u0942");                       d.Add("\u007A"+"\u007A","\u091C"+"\u094D"+"\u091C");             
                /*Consonant Block end*/                             /*vowel pair of cons+ 'e' end*/                   /*vowel pair of cons+ 'u' end*/                          /* Block of repeated consonant end*/         

                /*vowel pair of 'a' alone*/                         /*const+'h' pair block*/                           /*const+ 'r' pair block*/            
                d.Add("V"+"\u0061"+"\u0061","\u0906");           d.Add("\u0062"+"\u0068","\u092D");                   d.Add("\u0062"+"\u0072","\u092C"+"\u094D"+"\u0930");            
                d.Add("V"+"\u0061"+"\u0065","\u090F");           d.Add("\u0063"+"\u0068","\u091A");                   d.Add("\u0063"+"\u0072","\u0915"+"\u094D"+"\u0930");
                d.Add("V"+"\u0061"+"\u0069","\u0910");           d.Add("\u0064"+"\u0068","\u0927");                   d.Add("\u0064"+"\u0072","\u0926"+"\u094D"+"\u0930");
                d.Add("V"+"\u0061"+"\u006F","\u0914");           d.Add("\u0066"+"\u0068","\u092B");                   d.Add("\u0066"+"\u0072","\u092B"+"\u094D"+"\u0930");
                d.Add("V"+"\u0061"+"\u0075","\u0913");           d.Add("\u0067"+"\u0068","\u0918");                   d.Add("\u0067"+"\u0072","\u0917"+"\u094D"+"\u0930");
                /*vowel pair of 'a' alone end*/                  d.Add("\u0068"+"\u0068","\u0939"+"\u094D"+"\u0939"); d.Add("\u0068"+"\u0072","\u0939"+"\u094D"+"\u0930");
                                                                 d.Add("\u006A"+"\u0068","\u091D");                   d.Add("\u006A"+"\u0072","\u091C"+"\u094D"+"\u0930");
                /*vowel pair of 'e' alone*/                      d.Add("\u006B"+"\u0068","\u0916");                   d.Add("\u006B"+"\u0072","\u0915"+"\u094D"+"\u0930");
                d.Add("V"+"\u0065"+"\u0061","\u0907");           d.Add("\u006C"+"\u0068","\u0932"+"\u0939");          d.Add("\u006C"+"\u0072","\u0932"+"\u094D"+"\u0930");
                d.Add("V"+"\u0065"+"\u0065","\u0908");           d.Add("\u006D"+"\u0068","\u092E"+"\u0939");          d.Add("\u006D"+"\u0072","\u092E"+"\u094D"+"\u0930");
                d.Add("V"+"\u0065"+"\u0069","\u090F");           d.Add("\u006E"+"\u0068","\u0928"+"\u0939");          d.Add("\u006E"+"\u0072","\u0928"+"\u0930");
                d.Add("V"+"\u0065"+"\u006F","\u0913");           d.Add("\u0070"+"\u0068","\u092B");                   d.Add("\u0070"+"\u0072","\u092A"+"\u094D"+"\u0930");
                d.Add("V"+"\u0065"+"\u0075","\u092F"+"\u0942");  d.Add("\u0071"+"\u0068","\u0916");                   d.Add("\u0071"+"\u0072","\u0915"+"\u094D"+"\u0930");
                /*vowel pair of 'e' alone end*/                  d.Add("\u0072"+"\u0068","\u0930"+"\u0939");          d.Add("\u0072"+"\u0072","\u0930"+"\u094D"+"\u0930");
                                                                 d.Add("\u0073"+"\u0068","\u0936");                   d.Add("\u0073"+"\u0072","\u0936"+"\u0930");
                /*vowel pair of 'i' alone*/                      d.Add("\u0074"+"\u0068","\u0925");                   d.Add("\u0074"+"\u0072","\u0924"+"\u094D"+"\u0930");
                d.Add("V"+"\u0069"+"\u0061","\u090F");           d.Add("\u0076"+"\u0068","\u092D");                   d.Add("\u0076"+"\u0072","\u0935"+"\u094D"+"\u0930");
                d.Add("V"+"\u0069"+"\u0065","\u0910");           d.Add("\u0077"+"\u0068","\u092D");                   d.Add("\u0077"+"\u0072","\u0935"+"\u0930");
                d.Add("V"+"\u0069"+"\u0069","\u0908");           d.Add("\u0078"+"\u0068","\u091D");                   d.Add("\u0078"+"\u0072","\u0936"+"\u094D"+"\u0930");
                d.Add("V"+"\u0069"+"\u006F","\u0911");           d.Add("\u0079"+"\u0068","\u092F"+"\u0939");          d.Add("\u0079"+"\u0072","\u092F"+"\u0930");
                d.Add("V"+"\u0069"+"\u0075","\u092F"+"\u0941");  d.Add("\u007A"+"\u0068","\u091C"+"\u0939");          d.Add("\u007A"+"\u0072","\u091C"+"\u094D"+"\u0930");
                /*vowel pair of 'i' alone end*/                  /*const+'h' pair block end*/                         /*const+ 'r' pair block end*/

                /*vowel pair of 'o' alone*/                      /*single vowel after consonant*/         
                d.Add("V"+"\u006F"+"\u0061","\u0911");           d.Add("\u0061","\u093E");
                d.Add("V"+"\u006F"+"\u0065","\u0913");           d.Add("\u0065","\u0947");
                d.Add("V"+"\u006F"+"\u0069","\u0913"+"\u0908");  d.Add("\u0069","\u0940");
                d.Add("V"+"\u006F"+"\u006F","\u090A");           d.Add("\u006F","\u094B");
                d.Add("V"+"\u006F"+"\u0075","\u0906"+"\u0909");  d.Add("\u0075","\u0941");
                /*vowel pair of 'o' alone end*/                  /*single vowel after consonant end*/ 
 
                /*vowel pair of 'u' alone*/
                d.Add("V"+"\u0075"+"\u0061","\u092F"+"\u0942");
                d.Add("V"+"\u0075"+"\u0065","\u092F"+"\u0942");
                d.Add("V"+"\u0075"+"\u0069","\u0909"+"\u0907");
                d.Add("V"+"\u0075"+"\u006F","\u092F"+"\u094B");
                d.Add("V"+"\u0075"+"\u0075","\u090A");
                /*vowel pair of 'u' alone end*/ 
            //Dictionary Add Over
	}
    public static bool char_vowel(string char_str) /* In char_vowel all occurances of consonants are checked */
    {
        if (char_str == "\u0061" || char_str == "\u0065" || char_str == "\u0069" || char_str == "\u006F" || char_str == "\u0075")
            return true;
        else
            return false;
    }

    public static bool char_consonants(string char_str) /* In char_consonants all occurances of consonants are checked */
    {
        if (char_str == "\u0062" || char_str == "\u0063" || char_str == "\u0064" || char_str == "\u0066" || char_str == "\u0067" || char_str == "\u0068" || char_str == "\u006A" || char_str == "\u006B" || char_str == "\u006C" || char_str == "\u006D" || char_str == "\u006E" || char_str == "\u0070" || char_str == "\u0071" || char_str == "\u0072" || char_str == "\u0073" || char_str == "\u0074" || char_str == "\u0076" || char_str == "\u0077" || char_str == "\u0078" || char_str == "\u0079" || char_str == "\u007A")
            return true;
        else
            return false;
    }

    public static List<string> remove_null(string[] splitArray)
    {
        List<string> newsplitArray = new List<string>();
        foreach (string str_char in splitArray)
        {
            if (str_char != "")
                newsplitArray.Add(str_char);
        }
        return newsplitArray;
    }
    public string  transliterate(string eng_string)
    {
        output = null;
        input = eng_string.ToLower();   //convert entire string to lower case 
        retext = reg.Split(input);
        newretext = remove_null(retext); 
                for(int k=0; k < newretext.Count ; k++)  // check each of string[] from newretext
                {       
                    char[] charArray = newretext[k].ToCharArray();   /* Each of string sequences for consonants followed by vowels converted to char[] */
                    for (int i = 0; i < charArray.Length; i++)
                    {
                        if (char_vowel(charArray[i].ToString()))
                        {
                            s = null;
                            if (i == 0)   //if vowel is single vowel in string[] from newretext put "v" before it
                                s = "V";
                            for (int j = i; j < charArray.Length; j++)
                            s += charArray[j].ToString();    // take all vowels in string[] as one string 
                            if(d.TryGetValue(s, out hin_char))
                                output += hin_char;
                            break;
                        }
                        else if (char_consonants(charArray[i].ToString()))   // if character is consonant
                        {
                            if (flag == 1)   // if flag = 1, proceed to next char in char[]
                            {               // consonant has been considered previously
                                flag = 0;
                                continue;
                            }
                            if (charArray.Length == 1 && k + 1 < newretext.Count)  //if consonant is not followed by vowels
                            {
                                string next_charstr = ((newretext[k + 1].ToCharArray())[0]).ToString();
                                if (next_charstr == "h" || next_charstr == "r" || charArray[i].ToString() == next_charstr) // check whether next consonant is 'h' or 'r' or a repeated consonant
                                {
                                    s = charArray[i].ToString() + next_charstr;   // if 'h' or 'r' or similar consonant, put it along with current consonant , ie make consonant pair
                                    flag = 1;     // falg =1 , don't consider 'h' or 'r' or consonant during next loop
                                }
                                else
                                    s = newretext[k];   // if next consonant not 'h' , 'r' or similar consonant
                            }
                            else
                                s = charArray[i].ToString();  
                            if(d.TryGetValue(s,out hin_char))    // search hindi_char corresponding to consonant pair or single consonant provided
                                output += hin_char;              
                        }
                        else
                        {
                        output += charArray[i].ToString();  // if not vowel or consonant , put it directly
                        }
                    }
                }
            return output;
        }
}
