namespace ChatApp.Routes
{
    public abstract class BaseRoute
    {
        public const string Base = "api/v1";
    }

    public class AccountRoutes : BaseRoute
    {
        public const string Controller = Base + "/account";
        public const string Login =  "login";
        public const string Register = "register";  
    }

    public class ChatRoutes : BaseRoute
    {
        public const string Controller = Base + "/chat";
    }

    public class RoomRoutes : BaseRoute
    {
        public const string Controller = Base + "/room";
    }

    public class TeamRoutes : BaseRoute
    {
        public const string Controller = Base + "/team";
    }

    public class ChatHubRoutes : BaseRoute
    {
        public const string Hub = "chathub";
    }
}
