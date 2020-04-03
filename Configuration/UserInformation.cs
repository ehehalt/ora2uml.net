using System;
using System.Collections.Generic;

namespace Ora2Uml.Configuration
{
    public class UserInformation
    {
        public string UserId { get; set; }
        public string Password { get; set; }

        public UserInformation()
            : this(String.Empty, String.Empty)
        {
        }

        public UserInformation(String userId, String password)
        {
            this.UserId = userId;
            this.Password = password;
        }
    }
}