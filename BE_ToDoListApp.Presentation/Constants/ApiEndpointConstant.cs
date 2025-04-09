namespace BE_ToDoListApp.Presentation.Constants
{
    public static class ApiEndpointConstant
    {
        static ApiEndpointConstant()
        {
        }

        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class Authentication
        {
            public const string AuthEndpoint = ApiEndpoint + "/auth";
            public const string SignInEndpoint = AuthEndpoint + "/sign-in";
            public const string SignUpEndpoint = AuthEndpoint + "/sign-up";
        }

    }
}
