using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.Exceptions;
using opennlp.tools.namefind;
using opennlp.tools.tokenize;
using opennlp.tools.sentdetect;
using opennlp.tools.postag;
using opennlp.tools.util;

namespace WCFSymendsTest
{
    public class ProcessMessage
    {
        public SMSMessage MessageHandler(string From, string Body)
        {
            SMSMessage message = new SMSMessage();
            message.message_in = Body;
            message.message_out = ReverseIt(Body).ToString();
            message.Timestamp = DateTime.Now;

            var accountSid = "tokenID";
            var authToken = "tokenAuth";

            TwilioClient.Init(accountSid, authToken);

            try
            {
                var SMS = MessageResource.Create(to: new PhoneNumber(From), from: new PhoneNumber("+15873295321"), body: message.message_out);
                return message;
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
           
        }

        private StringBuilder ReverseIt(string Message)
        {
            StringBuilder reversedString = new StringBuilder();

            //lets do some language processing tasks to identify sentence structure
            SentenceDetectorME sentenceParser = new SentenceDetectorME(LoadNLP.sentenceModel);
            TokenizerME tokenizer = new TokenizerME(LoadNLP.tokenModel);

            string[] sentences = sentenceParser.sentDetect(Message);

            //iterate through each sentence
            foreach (string sentence in sentences)
            {
                string[] tokens = tokenizer.tokenize(sentence);
              
                //reverse the tokens
                for (int i = 0; i < tokens.Length / 2; i++)
                {
                    string storage = tokens[i];
                    tokens[i] = tokens[tokens.Length - i - 1];
                    tokens[tokens.Length - i - 1] = storage;
                }

                //Now that we've organized the sentence nicely, lets detokenize it and convert back to a usable string
                reversedString.Append(DeTokenize(tokens, DetokenizationDictionary.Operation.MOVE_LEFT)); 

            }

            return reversedString;
        }

        public static String DeTokenize(String[] tokens, DetokenizationDictionary.Operation operation)
        {
            DetokenizationDictionary.Operation[] operations = new DetokenizationDictionary.Operation[tokens.Length];

            for (int i = 0; i < tokens.Length; i++)
            {
                operations[i] = operation;
            }

            DetokenizationDictionary dictionary = new DetokenizationDictionary(
              tokens, operations);
            DictionaryDetokenizer detokenizer = new DictionaryDetokenizer(
              dictionary);

            return detokenizer.detokenize(tokens, " ");
        }

    }
}