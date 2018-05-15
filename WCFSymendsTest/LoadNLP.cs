using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using opennlp.tools.namefind;
using opennlp.tools.tokenize;
using opennlp.tools.sentdetect;
using opennlp.tools.postag;
using java.io;

namespace WCFSymendsTest
{
    public static class LoadNLP
    {
        static string sentenceModelPath = @"J:\Models\en-sent.bin";
        static string tokenModelPath = @"J:\Models\en-token.bin";
       
        public static FileInputStream tokenModeInputStream = new FileInputStream(tokenModelPath);
        public static TokenizerModel tokenModel = new TokenizerModel(tokenModeInputStream);

        public static FileInputStream sentenceModeInputStream = new FileInputStream(sentenceModelPath);
        public static SentenceModel sentenceModel = new SentenceModel(sentenceModeInputStream);

     

    }
}
