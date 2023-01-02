namespace Ynventory.Backend.Resources
{
    public static class ErrorCodes
    {
        private static readonly Dictionary<int, int> _statusCodeMap = new()
        {
            [InvalidRequest] = StatusCodes.Status400BadRequest,
            [Authentication.InvalidPassword] = StatusCodes.Status401Unauthorized,
            [User.UserNotFound] = StatusCodes.Status404NotFound,
            [User.UserAlreadyExists] = StatusCodes.Status400BadRequest,
            [Data.EntityNotFound] = StatusCodes.Status404NotFound,
            [Data.EntityAlreadyExists] = StatusCodes.Status400BadRequest,
            [Data.EntityIllegalState] = StatusCodes.Status400BadRequest,
            [Scryfall.CardNotFound] = StatusCodes.Status400BadRequest,
            [Scryfall.ApiError] = StatusCodes.Status502BadGateway,
        };

        public const int InvalidRequest = 001;

        public static class Authentication
        {
            public const int InvalidPassword = 101;
        }

        public static class User
        {
            public const int UserNotFound = 201;
            public const int UserAlreadyExists = 202;
        }

        public static class Data
        {
            public const int EntityNotFound = 301;
            public const int EntityAlreadyExists = 302;
            public const int EntityIllegalState = 303;
        }

        public static class Scryfall
        {
            public const int CardNotFound = 401;
            public const int ApiError = 402;
        }

        public static int StatusCode(int code)
        {
            return _statusCodeMap.TryGetValue(code, out var statusCode) ? statusCode : StatusCodes.Status400BadRequest;
        }
    }
}
