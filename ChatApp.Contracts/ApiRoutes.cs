﻿using System;

namespace ChatApp.Contracts
{
    public static class ApiRoutes
    {
        const string Base = "api/v1";
        public static class Identity
        {
            const string BaseIdentity = Base+"/"+"identity"+"/";


            public const string Register = BaseIdentity + "register";
            public const string Login = BaseIdentity + "login";
            public const string Refresh = BaseIdentity + "refresh";

            
        }

        public static class Chat
        {
            const string BaseChat = Base + "/" + "chat" + "/";


            public const string SendMessage = BaseChat +   "sendmessage";
            public const string JoinChat = BaseChat +  "joinchat";
        }

    }
}
