namespace Fletchling.Api.Constants;

public static class RouteConstants
{
    public static class AuthRoutes
    {
        public const string CREATE_JWT = "auth/createjwt";
    }

    public static class TwitterRoutes
    {
        public const string GET_USER = "twitter/user/get";
        public const string SEARCH_USER = "twitter/user/search";
    }
}