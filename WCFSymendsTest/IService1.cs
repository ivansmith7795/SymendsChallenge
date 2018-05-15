using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace WCFSymendsTest
{
    // WCF REST service for processing SMS text messages and reversing text order
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]

        [WebInvoke(Method = "GET",

                   BodyStyle = WebMessageBodyStyle.Wrapped,
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "GetMessages")]
        List<SMSMessage> RetrieveSMSData();

        [WebInvoke(Method = "POST",

                  BodyStyle = WebMessageBodyStyle.Wrapped,
                  ResponseFormat = WebMessageFormat.Xml,
                  UriTemplate = "ProcessMessage")]
        void ProcessMessage(Stream TwilioResponse);

        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();
    }


   
}
