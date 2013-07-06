using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace FacebookAgaveWeb
{
    [ServiceContract(Namespace = "", ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
#if DEBUG
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
#endif
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Friends
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebGet]
        public void LogAction(string Action, string UserID, string Environment, string Error)
        {
            DataAccess.Instance.LogAction(UserID, Action, Error, Environment);
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
