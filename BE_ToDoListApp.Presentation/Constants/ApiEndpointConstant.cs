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

        public static class ToDoTask
        {
            public const string ToDoTasksEndpoint = ApiEndpoint + "/to-do-tasks/{userId}";
        }

        public static class TaskStatistic
        {
            public const string TodayStatEndpoint = ApiEndpoint + "/today-stat/{userId}";
            public const string WeekStatEndpoint = ApiEndpoint + "/week-stat/{userId}";
        }

    }
}
