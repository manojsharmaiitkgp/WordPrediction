using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredictiveKeyboard
{
    class HindiToEnglish
    {
            char[] characterArray;
        string first_char, second_char;
        int i;

        public bool char_exceptions(string second_char)            /* In char_exceptions all the exceptional characters including space and special symbols 
                                                                    * are checked here as we generally don't need 'a' before it*/
        {
            if (second_char == "\u0020" || second_char == "\u0021" || second_char == "\u0022" || second_char == "\u0023" || second_char == "\u0024" || second_char == "\u0025" || second_char == "\u0026" || second_char == "\u0027" || second_char == "\u0028" || second_char == "\u0029" || second_char == "\u002a" || second_char == "\u002b" || second_char == "\u002c" || second_char == "\u002d" || second_char == "\u002e" || second_char == "\u002f" || second_char == "\u003a" || second_char == "\u003b" || second_char == "\u003c" || second_char == "\u003d" || second_char == "\u003e" || second_char == "\u003f" || second_char == "\u0040" || second_char == "\u0964" || second_char == "\u0965" || second_char == "\u2018" || second_char == "\u2019" || second_char == "\u8220" || second_char == "\u8221") 
                return true;
            else
                return false;
        }

        public string joint_char1(string second_char)              /* In joint_char1 checking for the occurance of joint vowels and symbols with the words */
        {
             switch (second_char)        
             {
                 case "\u0901":
                     return "\u0061" + "\u006e";

                 case "\u0902":
                     if(i+1 < characterArray.Length)
                        i++;
                     string third_char = characterArray[i].ToString();
                     if (third_char == "\u0020")
                     {
                         i--; 
                         return "\0";
                     }
                     if (char_exceptions(third_char))
                     {
                         i--;
                         return "\0";
                     }
                     else
                     {
                         i--;
                         return ("\u0061" + "\u006D");
                     }

                 case "\u0903":
                     return "\0";
                        
                 case "\u093c":
                         i++;
                         second_char = characterArray[i].ToString();
                         third_char = joint_char1(second_char);
                         return third_char;

                 case "\u093D" :
                         return "\u0061";
                     
                 case "\u093e":
                     return "\u0061";

                 case "\u093f":
                     return "\u0069";

                 case "\u0940":
                     return "\u0069";

                 case "\u0941":
                     return "\u0075";

                 case "\u0942":
                     return "\u0075";

                 case "\u0943":
                 case "\u0944":
                     return "\u0072" + "\u0069";

                 case "\u0945":
                     return "\u006f";

                 case "\u0946":
                 case "\u0947":
                     return "\u0065";

                 case "\u0948":
                     return "\u0061" + "\u0069";

                 case "\u0949":
                     return "\u006f";

                 case "\u094A":
                 case "\u094b":
                     return "\u006f";

                 case "\u094c":
                     return "\u0061" + "\u0075";

                 case "\u094d":
                     return "\0";

                 case "\u0962":
                 case "\u0963":
                     return "\u006c" + "\u0072" + "\u0069";

                 default:
                     if (char_exceptions(second_char))
                     {
                         if (i - 2 < characterArray.Length)
                         {
                             if (characterArray[i - 2].ToString() == "\u0020")
                             {
                                 i--;
                                 return "\u0061";
                             }
                             else
                             {
                                 i--;
                                 return "\0";
                             }
                         }
                         else
                         {
                             i--;
                             return "\0";
                         }
                     }
                     else
                     {
                         i--;
                         return "\u0061"; 
                     }
             }                   
        }

        public string joint_char2(string second_char)              /* In  joint_char2 the difference from joint_char1 is it 
                                                                    * doesn't gives 'a' occurance after these characters */
        {
            switch (second_char)
            {
                case "\u0901":
                    return "\u0061" + "\u006e";

                case "\u0902":
                    if (i + 1 < characterArray.Length)
                        i++;
                    string third_char = characterArray[i].ToString();
                    if (third_char == "\u0020")
                    {
                        i--;
                        return "\0";
                    }
                    if (char_exceptions(third_char))
                    {
                        i--;
                        return "\0";
                    }
                    else
                    {
                        i--;
                        return ("\u0061" + "\u006D");
                    }

                case "\u093c":
                    i++;
                    second_char = characterArray[i].ToString();
                    third_char = joint_char1(second_char);
                    return third_char;

                case "\u093D":
                    return "\u0061";

                case "\u093e":
                    return "\u0061";

                case "\u093f":
                    return "\u0069";

                case "\u0940":
                    return "\u0069";

                case "\u0941":
                    return "\u0075";

                case "\u0942":
                    return "\u0075";

                case "\u0943":
                case "\u0944":
                    return "\u0072" + "\u0069";

                case "\u0945":
                    return "\u006f";

                case "\u0946":
                case "\u0947":
                    return "\u0065";

                case "\u0948":
                    return "\u0061" + "\u0069";

                case "\u0949":
                    return "\u006f";

                case "\u094A":
                case "\u094b":
                    return "\u006f";

                case "\u094c":
                    return "\u0061" + "\u0075";

                case "\u094d":
                    return "\0";

                case "\u0962":
                case "\u0963":
                    return "\u006c" + "\u0072" + "\u0069";

                default:
                    if (char_exceptions(second_char))
                    {
                        if (i - 2 < characterArray.Length)
                        {
                            if (characterArray[i - 2].ToString() == "\u0020")
                            {
                                i--;
                                return "\u0061";
                            }
                            else
                            {
                                i--;
                                return "\0";
                            }
                        }
                        else
                        {
                            i--;
                            return "\0";
                        }
                    }
                    else
                    {
                        i--;
                        return "\0";
                    }
            }
        }

        public string joint_char3(string second_char)              /* In joint_char3 the difference from joint_char1 & joint_char2 is it 
                                                                    * gives 'aa'( "\u0061" + "\u0061" ) on occurance of '|'("\u093e")*/
        {
            switch (second_char)
            {
                case "\u0901":
                    return "\u0061" + "\u006e";

                case "\u0902":
                    if (i + 1 < characterArray.Length)
                        i++;
                    string third_char = characterArray[i].ToString();
                    if (third_char == "\u0020")
                    {
                        i--;
                        return "\0";
                    }
                    if (char_exceptions(third_char))
                    {
                        i--;
                        return "\0";
                    }
                    else
                    {
                        i--;
                        return ("\u0061" + "\u006D");
                    }

                case "\u0903":
                    return "\0";

                case "\u093c":
                    i++;
                    second_char = characterArray[i].ToString();
                    third_char = joint_char1(second_char);
                    return third_char;

                case "\u093D":
                    return "\u0061";

                case "\u093e":
                    return "\u0061" + "\u0061";

                case "\u093f":
                    return "\u0069";

                case "\u0940":
                    return "\u0069";

                case "\u0941":
                    return "\u0075";

                case "\u0942":
                    return "\u0075";

                case "\u0943":
                case "\u0944":
                    return "\u0072" + "\u0069";

                case "\u0945":
                    return "\u006f";

                case "\u0946":
                case "\u0947":
                    return "\u0065";

                case "\u0948":
                    return "\u0061" + "\u0069";

                case "\u0949":
                    return "\u006f";

                case "\u094A":
                case "\u094b":
                    return "\u006f";

                case "\u094c":
                    return "\u0061" + "\u0075";

                case "\u094d":
                    return "\0";

                case "\u0962":
                case "\u0963":
                    return "\u006c" + "\u0072" + "\u0069";

                default:
                    if (char_exceptions(second_char))
                    {
                        if (i - 2 < characterArray.Length)
                        {
                            if (characterArray[i - 2].ToString() == "\u0020")
                            {
                                i--;
                                return "\u0061";
                            }
                            else
                            {
                                i--;
                                return "\0";
                            }
                        }
                        else
                        {
                            i--;
                            return "\0";
                        }
                    }
                    else
                    {

                        i--;
                        return "\u0061"; 
                    }
            }
        }



        public void Hindi_English(ref string text_to_convert,ref string input_text)
        {
            text_to_convert = null;

            characterArray = input_text.ToCharArray();
            for (i = 0; i < characterArray.Length; i++)                /*checking each of the characters in the text one by one */
            {
                string s = characterArray[i].ToString();
                switch (s)
                {
                    case "\u0901":
                        if ((i + 1) < characterArray.Length)
                            if (char_exceptions(characterArray[i + 1].ToString()))
                            {
                                text_to_convert += "\0";                     /* returns null for "\u0901" if spaces or symbols or char_exceptions are occuring after "\u0901"  */
                                break;
                            }
                            else
                            {
                                text_to_convert += "\u0061" + "\u006e";      /* returning 'an'("\u0061" + "\u006e") for this symbol "\u0901" if it occurs in between */
                                break;
                            }
                        break;

                    case "\u0902":                                                /* behaviour similar to "\u0901" */
                        if ((i + 1) < characterArray.Length)
                            if (char_exceptions(characterArray[i + 1].ToString()))
                            {
                                text_to_convert += "\0";
                                break;
                            }
                            else
                            {
                                text_to_convert += "\u006e";
                                break;
                            }
                        break;

                    case "\u0905":
                        if ((i + 1) < characterArray.Length)
                            if (characterArray[++i].ToString() == "\u0902")
                            {
                                text_to_convert += "\u0061" + "\u006e";              /* showing special behaviour with occurance of \u0902 with \u0905
                                                                                        * otherwise normal */
                                break;
                            }
                            else
                                i--;
                        text_to_convert += "\u0061";
                        break;

                    case "\u0906":
                        if ((i + 1) < characterArray.Length)
                            if (characterArray[++i].ToString() == "\u0901")
                            {
                                text_to_convert += "\u0061" + "\u0061" + "\u006e";        /* showing special behaviour with occurance of \u0901 with \u0906
                                                                                             * otherwise normal */
                                break;
                            }
                            else
                                i--;
                        text_to_convert += "\u0061" + "\u0061";
                        break;

                    case "\u0907":
                        text_to_convert += "\u0069";
                        break;

                    case "\u0908":
                        text_to_convert += "\u0069";
                        break;

                    case "\u0909":
                        text_to_convert += "\u0075";
                        break;

                    case "\u090a":
                        text_to_convert += "\u0075";
                        break;

                    case "\u090b":
                    case "\u0960":
                        text_to_convert += "\u0072" + "\u0069";
                        break;

                    case "\u090c":
                    case "\u0961":
                        text_to_convert += "\u006c" + "\u0072" + "\u0079";
                        break;

                    case "\u090e":
                        text_to_convert += "\u0061" + "\u0065";
                        break;

                    case "\u090f":
                        text_to_convert += "\u0065";
                        break;

                    case "\u0910":
                        text_to_convert += "\u0061" + "\u0065";
                        break;

                    case "\u0911":
                        text_to_convert += "\u0061" + "\u0061";
                        break;

                    case "\u0912":
                        text_to_convert += "\u0061" + "\u0075";
                        break;

                    case "\u0913":
                        text_to_convert += "\u0061" + "\u0075";
                        break;

                    case "\u0914":
                        text_to_convert += "\u0061" + "\u0075";
                        break;

                    case "\u0915":                                           /* start checking the occurances of consonants in hindi  */
                        first_char = "\u006b";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char3(second_char);        /* depending on the behaviour joint_char3 is called */
                        }
                        break;


                    case "\u0916":
                        first_char = "\u006b" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0917":
                        first_char = "\u0067";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0918":
                        first_char = "\u0067" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char3(second_char);
                        }
                        break;

                    case "\u0919":
                        first_char = "\u0072";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char2(second_char);                /* depending on the behaviour joint_char2 is called */
                        }
                        break;

                    case "\u091A":
                        first_char = "\u0063" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u091b":
                        first_char = "\u0063" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u091c":
                        first_char = "\u006a";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char3(second_char);
                        }
                        break;

                    case "\u091d":
                        first_char = "\u006a" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u091e":
                        first_char = "\u0079";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char2(second_char);
                        }
                        break;

                    case "\u091f":
                        first_char = "\u0074";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0920":
                        first_char = "\u0074" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0921":
                        first_char = "\u0064";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0922":
                        first_char = "\u0064" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0923":
                        first_char = "\u006e";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char2(second_char);
                        }
                        break;

                    case "\u0924":
                        first_char = "\u0074";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0925":
                        first_char = "\u0074" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0926":
                        first_char = "\u0064";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0927":
                        first_char = "\u0064" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0928":
                        first_char = "\u006e";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0929":
                        first_char = "\u006e";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u092a":
                        first_char = "\u0070";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u092b":
                        first_char = "\u0066";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u092c":
                        first_char = "\u0062";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char3(second_char);
                        }
                        break;

                    case "\u092d":
                        first_char = "\u0062" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u092e":
                        first_char = "\u006d";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)                              /* occurance of "\u0902" is considered as a exception with "\u092e"
                                                                                       * and corresponding value is returned */
                        {
                            second_char = characterArray[i].ToString();
                            if (second_char == "\u0902")
                                text_to_convert += "\u0061" + "\u006e";
                            else
                                text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u092f":
                        first_char = "\u0079";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0930":
                        first_char = "\u0072";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0931":
                        first_char = "\u0072";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0932":
                        first_char = "\u006c";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0933":
                        first_char = "\u006c";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0934":
                        first_char = "\u006c";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0935":
                        first_char = "\u0076";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0936":
                        first_char = "\u0073" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char2(second_char);
                        }
                        break;

                    case "\u0937":
                        first_char = "\u0073" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char2(second_char);
                        }
                        break;

                    case "\u0938":
                        first_char = "\u0073";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0939":
                        first_char = "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u093c":                                /* occurance of \u093c and \u093d and \u094d are ignored  */
                        break;

                    case "\u093d":
                        text_to_convert += "\u0061";
                        break;

                    case "\u094d":
                        break;

                    case "\u0950":
                        first_char = "\u006F";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char2(second_char);
                        }
                        break;

                    case "\u0951":
                    case "\u0952":                              /* cases showing similar behaviour but not of much use ignored */
                    case "\u0953":
                    case "\u0954":
                        break;

                    case "\u0958":
                        first_char = "\u006b";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0959":
                        first_char = "\u006B" + "\u0061";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u095A":
                        first_char = "\u0067";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u095B":
                        first_char = "\u006A";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u095C":
                        first_char = "\u0064";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u095d":
                        first_char = "\u0064" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u095E":
                        first_char = "\u0070" + "\u0068";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u095F":
                        first_char = "\u0079";
                        text_to_convert += first_char;
                        if (++i < characterArray.Length)
                        {
                            second_char = characterArray[i].ToString();
                            text_to_convert += joint_char1(second_char);
                        }
                        break;

                    case "\u0964":
                    case "\u0965":
                        text_to_convert += "\u0020" + "\u002e";
                        break;

                    case "\u0966":
                        text_to_convert += "\u0030";                                       /* checking the occurances of counts like 1,2,.. */
                        break;

                    case "\u0967":
                        text_to_convert += "\u0031";
                        break;

                    case "\u0968":
                        text_to_convert += "\u0032";
                        break;

                    case "\u0969":
                        text_to_convert += "\u0033";
                        break;

                    case "\u096a":
                        text_to_convert += "\u0034";
                        break;

                    case "\u096b":
                        text_to_convert += "\u0035";
                        break;

                    case "\u096c":
                        text_to_convert += "\u0036";
                        break;

                    case "\u096d":
                        text_to_convert += "\u0037";
                        break;

                    case "\u096e":
                        text_to_convert += "\u0038";
                        break;

                    case "\u096f":
                        text_to_convert += "\u0039";
                        break;

                    case "\u0970":
                    case "\u0971":
                        break;

                    default:
                        text_to_convert += s;                          /* all unrecognized words in the above code , put them directly */
                        break;
                }
            }
        }
       
    }
}
