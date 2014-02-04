using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace SkyDriveWeb.App
{
        public class OAuthError
        {
            public OAuthError(string code, string desc)
            {
                this.Code = code;
                this.Description = desc;
            }

            [DataMember(Name = OAuthConstants.Error)]
            public string Code { get; private set; }

            [DataMember(Name = OAuthConstants.ErrorDescription)]
            public string Description { get; private set; }
        }
    

}