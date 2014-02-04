﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
namespace SkyDriveWeb.App
{

    [DataContract]
    public class OAuthToken
    {
        [DataMember(Name = OAuthConstants.AccessToken)]
        public string AccessToken { get; set; }

        [DataMember(Name = OAuthConstants.AuthenticationToken)]
        public string AuthenticationToken { get; set; }

        [DataMember(Name = OAuthConstants.RefreshToken)]
        public string RefreshToken { get; set; }

        [DataMember(Name = OAuthConstants.ExpiresIn)]
        public string ExpiresIn { get; set; }

        [DataMember(Name = OAuthConstants.Scope)]
        public string Scope { get; set; }
    }


}