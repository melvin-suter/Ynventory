namespace Ynventory.Backend.Resources
{
    public static class ErrorCodes
    {
        public static class Authentication
        {
            public const int InvalidPassword = 101;
        }

        public static class User
        {
            public const int UserNotFound = 201;
            public const int UserAlreadyExists = 202;
        }
    }
}
