using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Web;

namespace WCFSymendsTest
{
   
    public class WCFSymendsTest : IService1
    {
        public void ProcessMessage(Stream TwilioResponse)
        {
           
            StreamReader messageReader = new StreamReader(TwilioResponse);
            String rawBody = messageReader.ReadToEnd();

            NameValueCollection qs = HttpUtility.ParseQueryString(rawBody); 
            string From = qs["From"];
            string Body = qs["Body"];

            WebOperationContext.Current.OutgoingRequest.Accept = "text/xml";
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            ProcessMessage messageHandler = new ProcessMessage();
            
            SMSMessage SMSMessage = messageHandler.MessageHandler(From, Body);

            if(SMSMessage != null)
            {
                SaveToDatabase saveMessage = new SaveToDatabase();

                saveMessage.SaveMessage(SMSMessage);
               
            }
           
           
        }

        public List<SMSMessage> RetrieveSMSData()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            LoadMessages messages = new LoadMessages();
            return messages.GetMessages();

        }

        public void GetOptions()
        {

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Cache-Control", "no-cache");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");
        }
    }
}
