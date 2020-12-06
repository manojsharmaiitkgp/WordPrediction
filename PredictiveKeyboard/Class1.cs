using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredictiveKeyboard
{
    class Class1
    {
        public int s_id = -1;

        public int GetSentID()
        {
            return s_id;
        }

        public void SetSentID(int id)
        {
            s_id = id;
        }
        public int GetWS()
        {
            return _WS;
        }

        public void SetWS(int WS)
        {
            _WS = WS;
        }

        public string Loc_ErrorAt = string.Empty;
        public double _pks = 0.0;
        public int _TE = -1;
        public int _WS = -1;
        public int _UE = -1;
        public int _TW = -1;

        public int _TC = -1;
        public double _HR = -1;
        public double _KuP = -1;
        public int _TT = -1;

        public int _sid = -1;
        public string _Corpus = string.Empty;


        public void SetCorpus(int sentenceID)
        {
            string output = "";
            if (sentenceID >= 1 && sentenceID <= 307)
            {
                output = "H1";
 
            }
            if (sentenceID >= 308 && sentenceID <= 426)
            {
                output = "H2";

            }
            if (sentenceID >= 427 && sentenceID <= 547)
            {
                output = "H3";

            }
            if (sentenceID >= 548 && sentenceID <= 680)
            {
                output = "H4";

            }
            if (sentenceID >= 681 && sentenceID <= 6864)
            {
                output = "H5";

            }
           // return output;
            _Corpus = output;
 
        }
        public string GetCorpus()
        {
            return _Corpus;
 
        }
       
        public string GetErrorAt()
        {
            return Loc_ErrorAt;
        }

        public void SetErrorAt(string ErrorAt)
        {
            Loc_ErrorAt = ErrorAt;
        }
        public double GetPKS()
        {
            return _pks;
        }

        public void SetPKS(double PKS)
        {
            _pks = PKS;
        }
        public int GetTE()
        {
            return _TE;
        }

        public void SetTE(int TE)
        {
            _TE = TE;
        }
        public int GetUE()
        {
            return _UE;
        }

        public void SetUE(int UE)
        {
            _UE = UE;
        }
        public int GetTW()
        {
            return _TW;
        }

        public void SetTW(int TW)
        {
            _TW = TW;
        }

        public int GetTC()
        {
            return _TC;
        }

        public void SetTC(int TC)
        {
            _TC = TC;
        }

        public double GetHR()
        {
            return _HR;
        }

        public void SetHR(double HR)
        {
            _HR = HR;
        }

        public double GetKuP()
        {
            return _KuP;
        }

        public void SetKuP(double KuP)
        {
            _KuP = KuP;
        }
        public int GetTT()
        {
            return _TT;
        }

        public void SetTT(int TT)
        {
            _TT = TT;
        }
        
    }
}
