using System;

namespace ChatApp.Contracts
{
    public static class ApiRoutes
    {
        const string Base = "api/v1";
        public static class Identity
        {
            const string BaseIdentity = Base+"/"+"identity";


            public static string Register = BaseIdentity + "register";
            public static string Login = BaseIdentity + "login";
            public static string Refresh = BaseIdentity + "refresh";

            
        }
    }
}
